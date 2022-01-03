using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Inventory.data;
using Inventory.model;
using Microsoft.EntityFrameworkCore;

namespace Inventory.ui
{
	public partial class SearchProductWindow
	{
		public static readonly SearchProductWindow Instance = new();
		private DispatcherTimer DispatcherTimer { get; set; }
		private Product LastProduct { get; set; }
		private InventoryDbContext InventoryDb { get; set; }
		private ObservableCollection<Product> Products { get; set; }
		private CollectionViewSource ProductsView { get; set; }

		private SearchProductWindow()
		{
			InitializeComponent();
			GetAllProducts();
			StartDispatcherTimer();
			ProductsView.Filter += MyProductsViewFilters;
		}

		private void GetAllProducts()
		{
			InventoryDb = new InventoryDbContext();
			
			LastProduct = InventoryDb.Products.OrderByDescending(product => product.Id).FirstOrDefault() ??
			              new Product();
			InventoryDb.Products.Load();
			Products = InventoryDb.Products.Local.ToObservableCollection();
			ProductsView = new CollectionViewSource() {Source = Products};
			ProductsView.SortDescriptions.Clear();
			ProductsView.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));
			DataGridProducts.ItemsSource = ProductsView.View;
			RefreshLblItemCount();
		}

		private void StartDispatcherTimer()
		{
			DispatcherTimer = new DispatcherTimer {Interval = new TimeSpan(0, 0, 30)};
			DispatcherTimer.Tick += AddNewProductToCollection;
			DispatcherTimer.Start();
		}
		
		private void AddNewProductToCollection(object sender, EventArgs e)
		{
			Product nextProduct = InventoryDb.Products.SingleOrDefault(product => product.Id == LastProduct.Id + 1);

			if (nextProduct == null)
			{
				return;
			}

			if (Products.Count < nextProduct.Id)
			{
				LastProduct = nextProduct;
				Products.Add(LastProduct);
			}
			
			DataGridProducts.Items.Refresh();
		}
		
		private void MyProductsViewFilters(object sender, FilterEventArgs filterEventArgs)
		{
			if (filterEventArgs.Item is not Product) return;

			if (AllFiltersAreEmpty())
			{
				filterEventArgs.Accepted = true;
				return;
			}
			
			if (!string.IsNullOrEmpty(TxtBoxQuickSearch.Text))
			{
				QuickFilter(sender, filterEventArgs);
				return;
			}
			
			AdvancedFilter(sender, filterEventArgs);
		}

		private bool AllFiltersAreEmpty()
		{
			return string.IsNullOrEmpty(TxtBoxId.Text) &&
			       string.IsNullOrEmpty(TxtBoxEnrollment.Text) &&
			       string.IsNullOrEmpty(TxtBoxDescription.Text) &&
			       string.IsNullOrEmpty(TxtBoxContainer.Text) &&
			       string.IsNullOrEmpty(TxtBoxLocation.Text) &&
			       string.IsNullOrEmpty(TxtBoxStatus.Text) &&
			       string.IsNullOrEmpty(TxtBoxMountingTechnology.Text) &&
			       string.IsNullOrEmpty(TxtBoxEncapsulation.Text) &&
			       string.IsNullOrEmpty(TxtBoxDebugCode.Text) &&
			       string.IsNullOrEmpty(TxtBoxQuickSearch.Text) &&
			       string.IsNullOrEmpty(TxtBoxMinAmount.Text) &&
			       string.IsNullOrEmpty(TxtBoxMaxAmount.Text);
		}
		
		private void SearchAfterThreeCharacteres(object sender, TextChangedEventArgs e)
		{
			if (TxtBoxQuickSearch.Text.Length <= 3)
			{
				return;
			}

			ProductsView.View.Refresh();
			RefreshLblItemCount();
		}
		
		private void QuickFilter(object sender, FilterEventArgs filterEventArgs)
		{
			Product product = (Product)filterEventArgs.Item;
			string text = TxtBoxQuickSearch.Text.ToLower();

			if (product.Id.ToString().Contains(text))
			{
				filterEventArgs.Accepted = true;
				return;
			}

			if (product.DebugCode != null && product.DebugCode.ToLower().Contains(text))
			{
				filterEventArgs.Accepted = true;
				return;
			}

			if (product.Status != null && product.Status.ToLower().Contains(text))
			{
				filterEventArgs.Accepted = true;
				return;
			}

			if (product.Enrollment != null && product.Enrollment.ToLower().Contains(text))
			{
				filterEventArgs.Accepted = true;
				return;
			}

			if (product.MountingTechnology != null && product.MountingTechnology.ToLower().Contains(text))
			{
				filterEventArgs.Accepted = true;
				return;
			}

			if (product.EncapsulationType != null && product.EncapsulationType.ToLower().Contains(text))
			{
				filterEventArgs.Accepted = true;
				return;
			}

			if (product.ShortDescription != null && product.ShortDescription.ToLower().Contains(text))
			{
				filterEventArgs.Accepted = true;
				return;
			}

			if (product.Category != null && product.Category.ToLower().Contains(text))
			{
				filterEventArgs.Accepted = true;
				return;
			}

			if (product.Container != null && product.Container.ToLower().Contains(text))
			{
				filterEventArgs.Accepted = true;
				return;
			}

			if (product.Location != null && product.Location.ToLower().Contains(text))
			{
				filterEventArgs.Accepted = true;
				return;
			}

			if (product.Manufacturer != null && product.Manufacturer.ToLower().Contains(text))
			{
				filterEventArgs.Accepted = true;
				return;
			}

			if (product.PartNumber != null && product.PartNumber.ToLower().Contains(text))
			{
				filterEventArgs.Accepted = true;
				return;
			}

			if (product.TypeOfStock != null && product.TypeOfStock.ToLower().Contains(text))
			{
				filterEventArgs.Accepted = true;
				return;
			}

			if (product.Location != null && product.Location.ToLower().Contains(text))
			{
				filterEventArgs.Accepted = true;
				return;
			}
			
			filterEventArgs.Accepted = false;
		}

		private void EnterKeyPressed(object sender, KeyEventArgs e)
		{
			if (e.Key != Key.Enter)
			{
				return;
			}
			
			SearchWithAdvancedFilters(null, null);
		}
		
		private void SearchWithAdvancedFilters(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(TxtBoxId.Text) &&
			    string.IsNullOrEmpty(TxtBoxStatus.Text) &&
			    string.IsNullOrEmpty(TxtBoxEnrollment.Text) &&
			    string.IsNullOrEmpty(TxtBoxDescription.Text) &&
			    string.IsNullOrEmpty(TxtBoxMountingTechnology.Text) &&
			    string.IsNullOrEmpty(TxtBoxEncapsulation.Text) &&
			    string.IsNullOrEmpty(TxtBoxContainer.Text) &&
			    string.IsNullOrEmpty(TxtBoxLocation.Text) &&
			    string.IsNullOrEmpty(TxtBoxDebugCode.Text) &&
			    string.IsNullOrEmpty(TxtBoxMinAmount.Text) &&
			    string.IsNullOrEmpty(TxtBoxMaxAmount.Text))
			{
				MessageBox.Show("Llene al menos un campo para buscar.", "Filtro Invalido", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				return;
			}

			TxtBoxQuickSearch.Text = "";
			ProductsView.View.Refresh();
			RefreshLblItemCount();
		}
		
		private void AdvancedFilter(object sender, FilterEventArgs filterEventArgs)
		{
			Product product = (Product)filterEventArgs.Item;

			if (product.Id.ToString().Contains(TxtBoxId.Text) &&
				product.Enrollment.ToLower().Contains(TxtBoxEnrollment.Text.ToLower()) &&
				product.ShortDescription.ToLower().Contains(TxtBoxDescription.Text.ToLower()) &&
				product.Container.ToLower().Contains(TxtBoxContainer.Text.ToLower()) &&
				product.Location.ToLower().Contains(TxtBoxLocation.Text.ToLower()) &&
				product.Status.ToLower().Contains(TxtBoxStatus.Text.ToLower()) &&
				product.MountingTechnology.ToLower().Contains(TxtBoxMountingTechnology.Text.ToLower()) &&
				product.EncapsulationType.ToLower().Contains(TxtBoxEncapsulation.Text.ToLower()) &&
				product.DebugCode.ToLower().Contains(TxtBoxDebugCode.Text.ToLower()) &&
				IsValidAmount(filterEventArgs)
				)
			{
				filterEventArgs.Accepted = true; 
			}

			else filterEventArgs.Accepted = false;
		}

		private bool IsValidAmount(FilterEventArgs filterEventArgs)
		{
			if (string.IsNullOrEmpty(TxtBoxMinAmount.Text) && string.IsNullOrEmpty(TxtBoxMaxAmount.Text)) return true;
			
			Product product = (Product)filterEventArgs.Item;
			int? minAmount = string.IsNullOrEmpty(TxtBoxMinAmount.Text) ? null : int.Parse(TxtBoxMinAmount.Text);
			int? maxAmount = string.IsNullOrEmpty(TxtBoxMaxAmount.Text) ? null : int.Parse(TxtBoxMaxAmount.Text);

			if (minAmount != null && maxAmount != null)
			{

				if (minAmount > maxAmount) return false;
				
				return product.CurrentAmount >= minAmount && product.CurrentAmount <= maxAmount;
			}

			if (minAmount != null)
			{
				return product.CurrentAmount >= minAmount;
			}

			if (maxAmount != null)
			{
				return product.CurrentAmount <= maxAmount;
			}

			return false;
		}
		
		private void ClearFilters(object sender, RoutedEventArgs e)
		{
			TxtBoxId.Text = "";
			TxtBoxStatus.Text = "";
			TxtBoxEnrollment.Text = "";
			TxtBoxDescription.Text = "";
			TxtBoxMountingTechnology.Text = "";
			TxtBoxEncapsulation.Text = "";
			TxtBoxContainer.Text = "";
			TxtBoxLocation.Text = "";
			TxtBoxQuickSearch.Text = "";
			TxtBoxDebugCode.Text = "";
			TxtBoxMinAmount.Text = "";
			TxtBoxMaxAmount.Text = "";
			ProductsView.View.Refresh();
			RefreshLblItemCount();
		}

		private void RefreshLblItemCount()
		{
			LblResultCount.Content = "Recuento: " + DataGridProducts.Items.Count;
		}

		private void SelectProductFromDataGrid(object sender, MouseButtonEventArgs e)
		{
			if (DataGridProducts.ItemsSource == null || DataGridProducts.SelectedItems.Count <= 0)
			{
				return;
			}

			Product selectedProduct = ((sender as DataGridCell).DataContext as Product);
			ProductWindow.ShowProductDetailsInstance.BringWindowToFront(selectedProduct);
		}

		private void AddProductToFilteredProducts(object sender, RoutedEventArgs e)
		{
			if (DataGridProducts.ItemsSource == null || DataGridProducts.SelectedItems.Count <= 0)
			{
				return;
			}

			foreach (Product selectedProduct in DataGridProducts.SelectedItems)
				DataGridFilteredProducts.Items.Add(selectedProduct);
		}

		private void ClearDataGridFilteredProducts(object sender, RoutedEventArgs e)
		{
			DataGridFilteredProducts.Items.Clear();
		}

		private void DataGridKeyPressed(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Down || e.Key == Key.Up)
				LoadProductImage(sender);
		}
		
		private void MouseLeftButtonClick(object sender, MouseButtonEventArgs e)
		{
			LoadProductImage(sender);
		}
		
		private void LoadProductImage(object sender)
		{
			if (DataGridProducts.ItemsSource == null || DataGridProducts.SelectedItems.Count <= 0)
			{
				return;
			}
			
			Uri imageUri = null;
			Product product = ((sender as DataGridCell).DataContext as Product);

			try
			{
				imageUri = new Uri(Properties.Settings.Default.PhotosPath + "\\" + product.Id + ".jpg");
				ProductImage.Source = new BitmapImage(imageUri);
			}
			catch (Exception exception)
			{
				imageUri = new Uri("/resources/images/defaultProductImage.jpg", UriKind.Relative);
				ProductImage.Source = new BitmapImage(imageUri);
			}
		}

		private void RequestForSell(object sender, RoutedEventArgs e)
		{
			if (DataGridProducts.ItemsSource == null || DataGridProducts.SelectedItems.Count <= 0)
			{
				return;
			}

			Product selectedProduct = (Product)DataGridProducts.SelectedItems[0];
			TasksWindow.Instance.BringWindowToFront(selectedProduct, "SOLICITAR PARA VENTA");
		}

		private void RequestForStore(object sender, RoutedEventArgs e)
		{
			if (DataGridProducts.ItemsSource == null || DataGridProducts.SelectedItems.Count <= 0)
			{
				return;
			}

			Product selectedProduct = (Product)DataGridProducts.SelectedItems[0];
			TasksWindow.Instance.BringWindowToFront(selectedProduct, "SOLICITAR PARA TIENDA");
		}

		private void RequestWithoutSupply(object sender, RoutedEventArgs e)
		{
			if (DataGridProducts.ItemsSource == null || DataGridProducts.SelectedItems.Count <= 0)
			{
				return;
			}

			Product selectedProduct = (Product)DataGridProducts.SelectedItems[0];
			TasksWindow.Instance.BringWindowToFront(selectedProduct, "SOLICITAR SIN SURTIR");
		}

		private void RequestForVerify(object sender, RoutedEventArgs e)
		{
			if (DataGridProducts.ItemsSource == null || DataGridProducts.SelectedItems.Count <= 0)
			{
				return;
			}

			Product selectedProduct = (Product)DataGridProducts.SelectedItems[0];
			TasksWindow.Instance.BringWindowToFront(selectedProduct, "SOLICITAR PARA VERIFICAR");
		}
	}
}
