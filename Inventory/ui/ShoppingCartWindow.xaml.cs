using System;
using System.Collections.Generic;
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
	public partial class ShoppingCartWindow
	{
		public static readonly ShoppingCartWindow Instance = new();
		private DispatcherTimer NewProductToBuyLookupTimer { get; set; }
		private InventoryDbContext InventoryDb { get; set; }
		private ObservableCollection<ProductForBuy> ShoppingCartCollection { get; set; }
		private CollectionViewSource ShoppingCartView { get; set; }

		public ShoppingCartWindow()
		{
			InitializeComponent();
			GetAllProductsToBuy(null, null);
			StartNewProductToBuyLookupTimer();
			SetComboBoxItemSource();
		}

		private void SetComboBoxItemSource()
		{
			List<string> providers = new List<string>()
			{
				"Utsource",
				"AG Electronica",
				"Mercado Libre",
				"Digi-Key",
				"Electronica 2000",
				"Electronica Felcy",
				"JAMS Electronica",
				"Mouser",
				"Ferelect",
				"Steren",
				"Dominion",
				"ITC",
				"Lumicolor",
				"Coel"
			};

			CmbBoxProviders.ItemsSource = providers;
		}

		private void GetAllProductsToBuy(object sender, RoutedEventArgs e)
		{
			InventoryDb = new InventoryDbContext();

			InventoryDb.ProductsForBuy
				.Include(productRequest => productRequest.Employee)
				.Include(productRequest => productRequest.Product)
				.Load();
			ShoppingCartCollection = InventoryDb.ProductsForBuy.Local.ToObservableCollection();
			ShoppingCartView = new CollectionViewSource() {Source = ShoppingCartCollection};
			ShoppingCartView.SortDescriptions.Clear();
			ShoppingCartView.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Descending));
			ShoppingCartView.Filter += FiltersForShoppingCartView;
			DataGridShoppingCart.ItemsSource = ShoppingCartView.View;
		}

		private void FiltersForShoppingCartView(object sender, FilterEventArgs filterEventArgs)
		{
			if (filterEventArgs.Item is not ProductForBuy productForBuy) return;
			
			if (AllCheckBoxesAreNotChecked())
			{
				filterEventArgs.Accepted = true;
				return;
			}
			
			switch (productForBuy.Status)
			{
				case "PENDIENTE" when ChkBoxFilterByPending.IsChecked == true:
				case "COMPRADO" when ChkBoxFilterByPurchased.IsChecked == true:
				case "SOLICITADO" when ChkBoxFilterByRequested.IsChecked == true:
					filterEventArgs.Accepted = true;
					break;
				default:
					filterEventArgs.Accepted = false;
					break;
			}
		}
		
		private bool AllCheckBoxesAreNotChecked()
		{
			return ChkBoxFilterByPending.IsChecked == false &&
			       ChkBoxFilterByPurchased.IsChecked == false &&
			       ChkBoxFilterByRequested.IsChecked == false;
		}
		
		private void StartNewProductToBuyLookupTimer()
		{
			NewProductToBuyLookupTimer = new DispatcherTimer {Interval = new TimeSpan(0, 0, 3)};
			NewProductToBuyLookupTimer.Tick += AddNewProductToBuyToShoppingCartCollection;
			NewProductToBuyLookupTimer.Dispatcher.Thread.IsBackground = true;
			NewProductToBuyLookupTimer.Start();
		}

		private void AddNewProductToBuyToShoppingCartCollection(object sender, EventArgs e)
		{
			ProductForBuy nextRequest = InventoryDb
				.ProductsForBuy
				.Include(productRequest => productRequest.Employee)
				.Include(productRequest => productRequest.Product)
				.SingleOrDefault(request => request.Id == ShoppingCartCollection.Last().Id + 1);

			if (nextRequest == null)
			{
				return;
			}

			if (ShoppingCartCollection.Last().Id < nextRequest.Id)
			{
				ShoppingCartCollection.Add(nextRequest);
				DataGridShoppingCart.Items.Refresh();
			}
		}

		private void SelectProductFromDatagrid(object sender, MouseButtonEventArgs e)
		{
			if (DataGridShoppingCart.ItemsSource == null || DataGridShoppingCart.SelectedItems.Count <= 0)
			{
				return;
			}

			ProductForBuy selectedProduct = (ProductForBuy)DataGridShoppingCart.SelectedItems[0];
			ProductWindow.ShowProductDetailsInstance.BringWindowToFront(selectedProduct.Product);
		}

		private void ChangeSelectedRowStatusAsPending(object sender, RoutedEventArgs e)
		{
			InventoryDbContext.ExecuteDatabaseRequest(() =>
			{
				foreach (ProductForBuy selectedItem in DataGridShoppingCart.SelectedItems)
				{
					selectedItem.Provider = null;
					InventoryDb.Entry(selectedItem).State = EntityState.Modified;
				}

				InventoryDb.SaveChanges();
				ChangeStatusForAllSelectedRows("PENDIENTE");
			});
		}

		private void ChangeSelectedRowStatusAsRequested(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(CmbBoxProviders.Text))
			{
				MessageBox.Show("Seleccione el proveedor al que le solicito el producto.", "Error", MessageBoxButton.OK,
					MessageBoxImage.Error);
				return;
			}

			InventoryDbContext.ExecuteDatabaseRequest(() =>
			{
				foreach (ProductForBuy selectedItem in DataGridShoppingCart.SelectedItems)
				{
					selectedItem.Provider = CmbBoxProviders.Text;
					InventoryDb.Entry(selectedItem).State = EntityState.Modified;
				}

				InventoryDb.SaveChanges();
				ChangeStatusForAllSelectedRows("SOLICITADO");
			});
		}

		private void ChangeSelectedRowStatusAsPurchased(object sender, RoutedEventArgs e)
		{
			ChangeStatusForAllSelectedRows("COMPRADO");
		}

		private void ChangeStatusForAllSelectedRows(string status)
		{
			InventoryDbContext.ExecuteDatabaseRequest(() =>
			{
				foreach (ProductForBuy selectedItem in DataGridShoppingCart.SelectedItems)
				{
					selectedItem.Status = status;
					InventoryDb.Entry(selectedItem).State = EntityState.Modified;
				}

				InventoryDb.SaveChanges();
			});
			RefreshShoppingCartView(null, null);
		}

		private void RemoveElements(object sender, RoutedEventArgs e)
		{
			if (DataGridShoppingCart.ItemsSource == null || DataGridShoppingCart.SelectedItems.Count <= 0) return;
			
			InventoryDbContext.ExecuteDatabaseRequest(() =>
			{
				InventoryDb.ProductsForBuy.RemoveRange(DataGridShoppingCart.SelectedItems.Cast<ProductForBuy>().ToList());
				InventoryDb.SaveChanges();
				RefreshShoppingCartView(null, null);
			});
		}
		
		private void RefreshShoppingCartView(object sender, RoutedEventArgs e)
		{
			ShoppingCartView.View.Refresh();
		}
	}
}
