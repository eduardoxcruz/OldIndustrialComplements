﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using Inventory.data;
using Inventory.model;
using Microsoft.EntityFrameworkCore;

namespace Inventory.ui
{
	public partial class RequestsWindow
	{
		public static readonly RequestsWindow Instance = new();
		private DispatcherTimer DispatcherTimer { get; set; }
		private ProductRequest LastProductRequest { get; set; }
		private InventoryDbContext InventoryDb { get; set; }
		private ObservableCollection<ProductRequest> ProductRequestsCollection { get; set; }
		private CollectionViewSource ProductRequestsView { get; set; }

		private RequestsWindow()
		{
			InitializeComponent();
			InventoryDb = new InventoryDbContext();
			GetAllProductRequests(null, null);
			StartDispatcherTimer();
		}

		private void GetAllProductRequests(object sender, RoutedEventArgs e)
		{
			LastProductRequest =
				InventoryDb.ProductRequests.OrderByDescending(request => request.Id).FirstOrDefault() ??
				new ProductRequest();
			InventoryDb.ProductRequests
				.Include(productRequest => productRequest.Employee)
				.Include(productRequest => productRequest.Product)
				.Load();
			ProductRequestsCollection = InventoryDb.ProductRequests.Local.ToObservableCollection();
			ProductRequestsView = new CollectionViewSource() { Source = ProductRequestsCollection };
			ProductRequestsView.SortDescriptions.Clear();
			ProductRequestsView.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Descending));
			ProductRequestsView.Filter += FiltersToProductRequestsView;
			DataGridRequests.ItemsSource = ProductRequestsView.View;
		}

		private void StartDispatcherTimer()
		{
			DispatcherTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 3) };
			DispatcherTimer.Tick += AddNewProductRequestToCollection;
			DispatcherTimer.Start();
		}

		private void AddNewProductRequestToCollection(object sender, EventArgs e)
		{
			ProductRequest nextRequest = InventoryDb
				.ProductRequests
				.Include(productRequest => productRequest.Employee)
				.Include(productRequest => productRequest.Product)
				.SingleOrDefault(request => request.Id == LastProductRequest.Id + 1);

			if (nextRequest == null)
			{
				return;
			}

			if (ProductRequestsCollection.Count < nextRequest.Id)
			{
				LastProductRequest = nextRequest;
				ProductRequestsCollection.Add(LastProductRequest);
			}

			DataGridRequests.Items.Refresh();
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
			InventoryDbContext.ExecuteDatabaseRequest(() => {
				foreach (ProductRequest selectedItem in DataGridRequests.SelectedItems)
				{
					selectedItem.Type = message;
					InventoryDb.Entry(selectedItem).State = EntityState.Modified;
				}
				InventoryDb.SaveChanges();
			});
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
			InventoryDbContext.ExecuteDatabaseRequest(() => {
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
			
			if (ChkBoxFilterByDelivered.IsChecked == false &&
			    ChkBoxFilterByNotDelivered.IsChecked == false &&
			    ChkBoxFilterByReturned.IsChecked == false)
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

		private void RefreshProductRequestsView(object sender, RoutedEventArgs e)
		{
			ProductRequestsView.View.Refresh();
		}
		
		private void RemoveElement(object sender, RoutedEventArgs e)
		{
			using InventoryDbContext inventoryDb = new InventoryDbContext();
			ProductRequest productToDelete = (ProductRequest)DataGridRequests.SelectedItems[0];
			inventoryDb.Remove(productToDelete);
			inventoryDb.SaveChanges();
		}
	}
}
