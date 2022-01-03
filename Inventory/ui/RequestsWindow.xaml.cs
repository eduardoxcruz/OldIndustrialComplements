using System;
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
			using InventoryDbContext inventoryDb = new();
			
			inventoryDb.ProductRequests
				.Include(productRequest => productRequest.Employee)
				.Include(productRequest => productRequest.Product)
				.Load();
			ProductRequestsCollection = inventoryDb.ProductRequests.Local.ToObservableCollection();
			ProductRequestsView = new CollectionViewSource() { Source = ProductRequestsCollection };
			ProductRequestsView.SortDescriptions.Clear();
			ProductRequestsView.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Descending));
			ProductRequestsView.Filter += FiltersToProductRequestsView;
			DataGridRequests.ItemsSource = ProductRequestsView.View;
			LastRequestsCount = ProductRequestsCollection.Count;
		}

		private void StartNewProductRequestLookupTimer()
		{
			NewProductRequestLookupTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 3) };
			NewProductRequestLookupTimer.Tick += AddNewProductRequestToCollection;
			NewProductRequestLookupTimer.Start();
		}

		private void AddNewProductRequestToCollection(object sender, EventArgs e)
		{
			using InventoryDbContext inventoryDb = new();
			
			ProductRequest nextRequest = inventoryDb
				.ProductRequests
				.Include(productRequest => productRequest.Employee)
				.Include(productRequest => productRequest.Product)
				.SingleOrDefault(request => request.Id == ProductRequestsCollection.Last().Id + 1);

			if (nextRequest == null)
			{
				return;
			}

			if (ProductRequestsCollection.Last().Id < nextRequest.Id)
			{
				ProductRequestsCollection.Add(nextRequest);
				DataGridRequests.Items.Refresh();
			}
		}

		private void StartNewProductRequestNotificatorTimer()
		{
			new Thread(() =>
			{
				while (true)
				{
					NotifyForNewProductRequest();
					Thread.Sleep(TimeSpan.FromSeconds(2));
				}
			}).Start();
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
			InventoryDbContext.ExecuteDatabaseRequest(() => {
				using InventoryDbContext inventoryDb = new();
				
				foreach (ProductRequest selectedItem in DataGridRequests.SelectedItems)
				{
					selectedItem.Type = message;
					inventoryDb.Entry(selectedItem).State = EntityState.Modified;
				}
				inventoryDb.SaveChanges();
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
				using InventoryDbContext inventoryDb = new();
				
				foreach (ProductRequest selectedItem in DataGridRequests.SelectedItems)
				{
					selectedItem.Status = message;
					inventoryDb.Entry(selectedItem).State = EntityState.Modified;
				}
				inventoryDb.SaveChanges();
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
