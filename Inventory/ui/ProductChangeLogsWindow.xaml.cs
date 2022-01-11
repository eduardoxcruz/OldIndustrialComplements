using System;
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
	public partial class ProductChangeLogsWindow
	{
		public static readonly ProductChangeLogsWindow Instance = new();
		private DispatcherTimer NewProductChangeLogLookupTimer { get; set; }
		private InventoryDbContext InventoryDb { get; set; }
		private ObservableCollection<ProductChangeLog> ProductChangeLogCollection { get; set; }
		private CollectionViewSource ProductChangeLogView { get; set; }

		private ProductChangeLogsWindow()
		{
			InitializeComponent();
			GetAllProductsChangeLogs(null, null);
			StartNewProductToBuyLookupTimer();
		}

		private void GetAllProductsChangeLogs(object sender, RoutedEventArgs e)
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
				case "AJUSTE" when ChkBoxFilterByProductAmountFit.IsChecked == true:
				case "PRECIO" when ChkBoxFilterByProductPriceFit.IsChecked == true:
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
			       ChkBoxFilterByProductAmountFit.IsChecked == false &&
			       ChkBoxFilterByProductEgress.IsChecked == false &&
			       ChkBoxFilterByProductPriceFit.IsChecked == false &&
			       ChkBoxFilterByProductDevolution.IsChecked == false;
		}
		
		private void StartNewProductToBuyLookupTimer()
		{
			NewProductChangeLogLookupTimer = new DispatcherTimer {Interval = new TimeSpan(0, 0, 3)};
			NewProductChangeLogLookupTimer.Tick += AddNewProductChangeLogToCollection;
			NewProductChangeLogLookupTimer.Dispatcher.Thread.IsBackground = true;
			NewProductChangeLogLookupTimer.Start();
		}

		private void AddNewProductChangeLogToCollection(object sender, EventArgs e)
		{
			ProductChangeLog nextLog = InventoryDb
				.ProductChangeLogs
				.Include(productRequest => productRequest.Employee)
				.Include(productRequest => productRequest.Product)
				.SingleOrDefault(request => request.Id == ProductChangeLogCollection.Last().Id + 1);

			if (nextLog == null)
			{
				return;
			}

			if (ProductChangeLogCollection.Last().Id < nextLog.Id)
			{
				ProductChangeLogCollection.Add(nextLog);
				//DataGridProductChangeLogs.Items.Refresh();
				RefreshProductChangeLogView(null, null);
			}
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
