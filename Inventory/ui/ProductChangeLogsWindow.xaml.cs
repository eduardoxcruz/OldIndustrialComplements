using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Inventory.data;
using Inventory.model;
using Microsoft.EntityFrameworkCore;

namespace Inventory.ui
{
	public partial class ProductChangeLogsWindow
	{
		public static readonly ProductChangeLogsWindow Instance = new();
		private InventoryDbContext InventoryDb { get; set; }
		private ObservableCollection<ProductChangeLog> ProductChangeLogCollection { get; set; }
		private CollectionViewSource ProductChangeLogView { get; set; }
		private ObservableCollection<Product> Products { get; set; }
		private CollectionViewSource ProductsView { get; set; }

		private ProductChangeLogsWindow()
		{
			InitializeComponent();
			LoadProductsAndLogsFromDb(null, null);
		}

		public new void BringWindowToFront()
		{
			if (this.Visibility == Visibility.Collapsed)
			{
				this.Show();
			}

			if (this.WindowState == WindowState.Minimized || this.Visibility == Visibility.Hidden)
			{
				this.Visibility = Visibility.Visible;
				this.WindowState = WindowState.Normal;
			}

			this.Activate();
			LoadProductsAndLogsFromDb(null, null);
		}
		
		private void LoadProductsAndLogsFromDb(object sender, RoutedEventArgs e)
		{
			GetAllProducts();
			GetAllProductChangeLogs();
		}

		private void GetAllProducts()
		{
			InventoryDb = new InventoryDbContext();

			InventoryDb.Products.Load();
			Products = InventoryDb.Products.Local.ToObservableCollection();
			ProductsView = new CollectionViewSource() {Source = Products};
			ProductsView.SortDescriptions.Clear();
			ProductsView.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));
			DataGridProducts.ItemsSource = ProductsView.View;
		}
		
		private void GetAllProductChangeLogs()
		{
			InventoryDb = new InventoryDbContext();

			InventoryDb.ProductChangeLogs
				.Include(productRequest => productRequest.Employee)
				.Include(productRequest => productRequest.Product)
				.Load();
			ProductChangeLogCollection = InventoryDb.ProductChangeLogs.Local.ToObservableCollection();
			ProductChangeLogView = new CollectionViewSource() {Source = ProductChangeLogCollection};
			ProductChangeLogView.SortDescriptions.Clear();
			ProductChangeLogView.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Descending));
			ProductChangeLogView.Filter += FiltersForProductChangeLogView;
			DataGridProductChangeLogs.ItemsSource = ProductChangeLogView.View;
		}

		private void FiltersForProductChangeLogView(object sender, FilterEventArgs filterEventArgs)
		{
			if (filterEventArgs.Item is not ProductChangeLog productChangeLog) return;
			
			if (AllCheckBoxesAreNotChecked())
			{
				filterEventArgs.Accepted = true;
				return;
			}
			
			switch (productChangeLog.Type)
			{
				case "ENTRADA" when ChkBoxFilterByProductEntry.IsChecked == true:
				case "DEVOLUCION" when ChkBoxFilterByProductDevolution.IsChecked == true:
				case "AJUSTE" when ChkBoxFilterByProductAmountAdjustment.IsChecked == true:
				case "PRECIO" when ChkBoxFilterByProductPriceAdjustment.IsChecked == true:
				case "SALIDA" when ChkBoxFilterByProductEgress.IsChecked == true:
					filterEventArgs.Accepted = true;
					break;
				default:
					filterEventArgs.Accepted = false;
					break;
			}
		}
		
		private bool AllCheckBoxesAreNotChecked()
		{
			return ChkBoxFilterByProductEntry.IsChecked == false &&
			       ChkBoxFilterByProductAmountAdjustment.IsChecked == false &&
			       ChkBoxFilterByProductEgress.IsChecked == false &&
			       ChkBoxFilterByProductPriceAdjustment.IsChecked == false &&
			       ChkBoxFilterByProductDevolution.IsChecked == false;
		}

		private void SelectProductFromDatagrid(object sender, MouseButtonEventArgs e)
		{
			if (DataGridProductChangeLogs.ItemsSource == null || DataGridProductChangeLogs.SelectedItems.Count <= 0)
			{
				return;
			}

			ProductToBuy selectedProduct = (ProductToBuy)DataGridProductChangeLogs.SelectedItems[0];
			ProductWindow.ShowProductDetailsInstance.BringWindowToFront(selectedProduct.Product);
		}

		private void RemoveElements(object sender, RoutedEventArgs e)
		{
			if (DataGridProductChangeLogs.ItemsSource == null || DataGridProductChangeLogs.SelectedItems.Count <= 0) return;
			
			InventoryDbContext.ExecuteDatabaseRequest(() =>
			{
				InventoryDb.ProductChangeLogs.RemoveRange(DataGridProductChangeLogs.SelectedItems.Cast<ProductChangeLog>().ToList());
				InventoryDb.SaveChanges();
				RefreshProductChangeLogView(null, null);
			});
		}
		
		private void RefreshProductChangeLogView(object sender, RoutedEventArgs e)
		{
			ProductChangeLogView.View.Refresh();
		}
	}
}
