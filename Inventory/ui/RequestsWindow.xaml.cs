﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using Inventory.data;
using Inventory.model;
using Inventory.Properties;
using Microsoft.EntityFrameworkCore;
using Microsoft.Toolkit.Uwp.Notifications;

namespace Inventory.ui
{
	public partial class RequestsWindow
	{
		public static readonly RequestsWindow Instance = new();
		private DispatcherTimer NewProductRequestLookupTimer { get; set; }
		private int LastRequestsCount { get; set; }
		private InventoryDbContext InventoryDb { get; set; }
		private ObservableCollection<ProductRequest> ProductRequestsCollection { get; set; }
		private CollectionViewSource ProductRequestsView { get; set; }

		private RequestsWindow()
		{
			InitializeComponent();
			GetAllProductRequests(null, null);
			if (Settings.Default.User.Type.Equals("ALMACEN")) StartNewProductRequestNotificatorTimer();
			StartNewProductRequestLookupTimer();
		}

		private void GetAllProductRequests(object sender, RoutedEventArgs e)
		{
			InventoryDb = new InventoryDbContext();

			InventoryDb.ProductRequests
				.Include(productRequest => productRequest.Employee)
				.Include(productRequest => productRequest.Product)
				.Load();
			ProductRequestsCollection = InventoryDb.ProductRequests.Local.ToObservableCollection();
			ProductRequestsView = new CollectionViewSource() {Source = ProductRequestsCollection};
			ProductRequestsView.SortDescriptions.Clear();
			ProductRequestsView.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Descending));
			ProductRequestsView.Filter += FiltersToProductRequestsView;
			DataGridRequests.ItemsSource = ProductRequestsView.View;
			LastRequestsCount = ProductRequestsCollection.Count;
		}

		private void StartNewProductRequestLookupTimer()
		{
			NewProductRequestLookupTimer = new DispatcherTimer {Interval = new TimeSpan(0, 0, 3)};
			NewProductRequestLookupTimer.Tick += AddNewProductRequestToCollection;
			NewProductRequestLookupTimer.Dispatcher.Thread.IsBackground = true;
			NewProductRequestLookupTimer.Start();
		}

		private void AddNewProductRequestToCollection(object sender, EventArgs e)
		{
			InventoryDb.ProductRequests
				.Include(productRequest => productRequest.Employee)
				.Include(productRequest => productRequest.Product)
				.Load();
		}

		private void StartNewProductRequestNotificatorTimer()
		{
			Thread notificatorTimer = new Thread(() =>
			{
				while (true)
				{
					NotifyForNewProductRequest();
					Thread.Sleep(TimeSpan.FromSeconds(2));
				}
			}) {IsBackground = true};
			notificatorTimer.Start();
		}

		private void NotifyForNewProductRequest()
		{
			if (LastRequestsCount >= ProductRequestsCollection.Count) return;

			new ToastContentBuilder()
				.AddText("Solicitud")
				.AddText("Hay una nueva solicitud de producto.")
				.Show();

			LastRequestsCount = ProductRequestsCollection.Count;
		}

		private void SelectProductFromDatagrid(object sender, MouseButtonEventArgs e)
		{
			if (DataGridRequests.ItemsSource == null || DataGridRequests.SelectedItems.Count <= 0)
			{
				return;
			}

			ProductRequest selectedProduct = (ProductRequest)DataGridRequests.SelectedItems[0];
			ProductWindow.ShowProductDetailsInstance.BringWindowToFront(selectedProduct.Product);
		}

		private void ChangeSelectedRowTypeAsForSale(object sender, RoutedEventArgs e)
		{
			ChangeTypeForAllSelectedRows("PARA VENTA");
		}

		private void ChangeSelectedRowTypeAsForStore(object sender, RoutedEventArgs e)
		{
			ChangeTypeForAllSelectedRows("PARA TIENDA");
		}

		private void ChangeSelectedRowTypeAsNoSupply(object sender, RoutedEventArgs e)
		{
			ChangeTypeForAllSelectedRows("NO SURTIR");
		}

		private void ChangeSelectedRowTypeAsForVerify(object sender, RoutedEventArgs e)
		{
			ChangeTypeForAllSelectedRows("PARA VERIFICAR");
		}

		private void ChangeTypeForAllSelectedRows(string message)
		{
			InventoryDbContext.ExecuteDatabaseRequest(() =>
			{
				foreach (ProductRequest selectedItem in DataGridRequests.SelectedItems)
				{
					selectedItem.Type = message;
					InventoryDb.Entry(selectedItem).State = EntityState.Modified;
				}

				InventoryDb.SaveChanges();
			});
			RefreshProductRequestsView(null, null);
		}

		private void ChangeSelectedRowStatusAsDelivered(object sender, RoutedEventArgs e)
		{
			ChangeStatusForAllSelectedRows("SURTIDO");
		}

		private void ChangeSelectedRowStatusAsNotDelivered(object sender, RoutedEventArgs e)
		{
			ChangeStatusForAllSelectedRows("NO SURTIDO");
		}

		private void ChangeSelectedRowStatusAsReturned(object sender, RoutedEventArgs e)
		{
			ChangeStatusForAllSelectedRows("DEVUELTO");
		}

		private void ChangeStatusForAllSelectedRows(string message)
		{
			InventoryDbContext.ExecuteDatabaseRequest(() =>
			{
				foreach (ProductRequest selectedItem in DataGridRequests.SelectedItems)
				{
					selectedItem.Status = message;
					InventoryDb.Entry(selectedItem).State = EntityState.Modified;
				}

				InventoryDb.SaveChanges();
			});
			RefreshProductRequestsView(null, null);
		}

		private void FiltersToProductRequestsView(object sender, FilterEventArgs filterEventArgs)
		{
			if (filterEventArgs.Item is not ProductRequest productRequest) return;

			if (ChkBoxHideNoSupply.IsChecked == true)
			{
				if (productRequest.Type is "NO SURTIR")
				{
					filterEventArgs.Accepted = false;
					return;
				}

				filterEventArgs.Accepted = true;
			}

			if (AllCheckBoxesAreNotChecked())
			{
				filterEventArgs.Accepted = true;
				return;
			}

			switch (productRequest.Status)
			{
				case "SURTIDO" when ChkBoxFilterByDelivered.IsChecked == true:
				case "NO SURTIDO" when ChkBoxFilterByNotDelivered.IsChecked == true:
				case "DEVUELTO" when ChkBoxFilterByReturned.IsChecked == true:
					filterEventArgs.Accepted = true;
					break;
				default:
					filterEventArgs.Accepted = false;
					break;
			}
		}

		private bool AllCheckBoxesAreNotChecked()
		{
			return ChkBoxFilterByDelivered.IsChecked == false &&
			       ChkBoxFilterByNotDelivered.IsChecked == false &&
			       ChkBoxFilterByReturned.IsChecked == false;
		}

		private void RefreshProductRequestsView(object sender, RoutedEventArgs e)
		{
			ProductRequestsView.View.Refresh();
		}

		private void RemoveElements(object sender, RoutedEventArgs e)
		{
			if (DataGridRequests.ItemsSource == null || DataGridRequests.SelectedItems.Count <= 0) return;

			InventoryDbContext.ExecuteDatabaseRequest(() =>
			{
				InventoryDb.ProductRequests.RemoveRange(DataGridRequests.SelectedItems.Cast<ProductRequest>().ToList());
				InventoryDb.SaveChanges();
			});
		}
	}
}
