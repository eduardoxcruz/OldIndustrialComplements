using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Inventory.data;
using Inventory.model;
using Microsoft.EntityFrameworkCore;

namespace Inventory.ui
{
	public partial class SearchProductWindow
	{
		public static readonly SearchProductWindow Instance = new();

		private SearchProductWindow()
		{
			InitializeComponent();
		}

		private void SearchAfterThreeCharacteres(object sender, TextChangedEventArgs e)
		{
			if (TxtBoxQuickSearch.Text.Length > 2)
			{
				QuickSearch(TxtBoxQuickSearch.Text);
			}
		}

		private void QuickSearch(string text)
		{
			using InventoryDbContext inventoryDb = new InventoryDbContext();
			DataGridProducts.ItemsSource =
				(from product in inventoryDb.Products
					where
						EF.Functions.Like(product.Id.ToString(), text) ||
						EF.Functions.Like(product.DebugCode, "%" + text + "%") ||
						EF.Functions.Like(product.Status, "%" + text + "%") ||
						EF.Functions.Like(product.Enrollment, "%" + text + "%") ||
						EF.Functions.Like(product.MountingTechnology, "%" + text + "%") ||
						EF.Functions.Like(product.EncapsulationType, "%" + text + "%") ||
						EF.Functions.Like(product.ShortDescription, "%" + text + "%") ||
						EF.Functions.Like(product.Category, "%" + text + "%") ||
						EF.Functions.Like(product.Container, "%" + text + "%") ||
						EF.Functions.Like(product.Location, "%" + text + "%") ||
						EF.Functions.Like(product.Manufacturer, "%" + text + "%") ||
						EF.Functions.Like(product.PartNumber, "%" + text + "%") ||
						EF.Functions.Like(product.TypeOfStock, "%" + text + "%") ||
						EF.Functions.Like(product.Location, "%" + text + "%")
					select product)
				.ToList();
			TxtBoxCount.Text = DataGridProducts.Items.Count.ToString();
		}

		private void EnterKeyPressed(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				SearchWithFilters(sender, e);
			}
		}

		private void SearchWithFilters(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(TxtBoxId.Text) &
			    string.IsNullOrEmpty(TxtBoxStatus.Text) &
			    string.IsNullOrEmpty(TxtBoxEnrollment.Text) &
			    string.IsNullOrEmpty(TxtBoxDescription.Text) &
			    string.IsNullOrEmpty(TxtBoxMountingTechnology.Text) &
			    string.IsNullOrEmpty(TxtBoxEncapsulation.Text) &
			    string.IsNullOrEmpty(TxtBoxContainer.Text) &
			    string.IsNullOrEmpty(TxtBoxLocation.Text) &
			    string.IsNullOrEmpty(TxtBoxDebugCode.Text))
			{
				MessageBox.Show("Llene al menos un campo para buscar.");
				return;
			}

			using InventoryDbContext inventoryDb = new InventoryDbContext();

			DataGridProducts.ItemsSource =
				(from product in inventoryDb.Products
					where
						EF.Functions.Like(product.Id.ToString(), "%" + TxtBoxId.Text + "%") &&
						EF.Functions.Like(product.Enrollment, "%" + TxtBoxEnrollment.Text + "%") &&
						EF.Functions.Like(product.ShortDescription, "%" + TxtBoxDescription.Text + "%") &&
						EF.Functions.Like(product.Container, "%" + TxtBoxContainer.Text + "%") &&
						EF.Functions.Like(product.Location, "%" + TxtBoxLocation.Text + "%") &&
						EF.Functions.Like(product.Status, "%" + TxtBoxStatus.Text + "%") &&
						EF.Functions.Like(product.MountingTechnology, "%" + TxtBoxMountingTechnology.Text + "%") &&
						EF.Functions.Like(product.EncapsulationType, "%" + TxtBoxEncapsulation.Text + "%") &&
						EF.Functions.Like(product.DebugCode, "%" + TxtBoxDebugCode.Text + "%")
					select product).ToList();

			TxtBoxCount.Text = DataGridProducts.Items.Count.ToString();
		}

		private void SelectProductFromDataGrid(object sender, MouseButtonEventArgs e)
		{
			if (DataGridProducts.ItemsSource == null || DataGridProducts.SelectedItems.Count <= 0)
			{
				return;
			}

			Product selectedProduct = (Product)DataGridProducts.SelectedItems[0];
			ProductWindow.ShowProductDetailsInstance.BringWindowToFront(selectedProduct);
		}

		private void CleanFilters(object sender, RoutedEventArgs e)
		{
			DataGridProducts.ItemsSource = null;
			TxtBoxId.Text = null;
			TxtBoxStatus.Text = null;
			TxtBoxEnrollment.Text = null;
			TxtBoxDescription.Text = null;
			TxtBoxMountingTechnology.Text = null;
			TxtBoxEncapsulation.Text = null;
			TxtBoxContainer.Text = null;
			TxtBoxLocation.Text = null;
			TxtBoxQuickSearch.Text = null;
			TxtBoxDebugCode.Text = null;
			TxtBoxCount.Text = "00";
		}

		private void OpenSettings(object sender, RoutedEventArgs e)
		{
			SettingsWindow.Instance.BringWindowToFront();
		}

		private void OpenProductRequests(object sender, RoutedEventArgs e)
		{
			RequestsWindow.Instance.BringWindowToFront();
		}

		private void AddNewProduct(object sender, RoutedEventArgs e)
		{
			ProductWindow.AddNewProductInstance.BringWindowToFront();
		}

		private void AddProductToSeparateDataGrid(object sender, RoutedEventArgs e)
		{
			if (DataGridProducts.ItemsSource == null || DataGridProducts.SelectedItems.Count <= 0)
			{
				return;
			}

			Product selectedProduct = (Product)DataGridProducts.SelectedItems[0];
			DataGridAddedProducts.Items.Add(selectedProduct);
		}

		private void IngressProduct(object sender, RoutedEventArgs e)
		{
			if (DataGridProducts.ItemsSource == null || DataGridProducts.SelectedItems.Count <= 0)
			{
				return;
			}

			Product selectedProduct = (Product)DataGridProducts.SelectedItems[0];
			TasksWindow.Instance.BringWindowToFront(selectedProduct, "ENTRADA DE PRODUCTO");
		}

		private void EgressProduct(object sender, RoutedEventArgs e)
		{
			if (DataGridProducts.ItemsSource == null || DataGridProducts.SelectedItems.Count <= 0)
			{
				return;
			}

			Product selectedProduct = (Product)DataGridProducts.SelectedItems[0];
			TasksWindow.Instance.BringWindowToFront(selectedProduct, "SALIDA DE PRODUCTO");
		}

		private void ReturnProduct(object sender, RoutedEventArgs e)
		{
			if (DataGridProducts.ItemsSource == null || DataGridProducts.SelectedItems.Count <= 0)
			{
				return;
			}

			Product selectedProduct = (Product)DataGridProducts.SelectedItems[0];
			TasksWindow.Instance.BringWindowToFront(selectedProduct, "DEVOLUCION DE PRODUCTO");
		}

		private void AdjustStock(object sender, RoutedEventArgs e)
		{
			if (DataGridProducts.ItemsSource == null || DataGridProducts.SelectedItems.Count <= 0)
			{
				return;
			}

			Product selectedProduct = (Product)DataGridProducts.SelectedItems[0];
			TasksWindow.Instance.BringWindowToFront(selectedProduct, "AJUSTE DE CANTIDAD");
		}

		private void BuyMoreProduct(object sender, RoutedEventArgs e)
		{
			if (DataGridProducts.ItemsSource == null || DataGridProducts.SelectedItems.Count <= 0)
			{
				return;
			}

			Product selectedProduct = (Product)DataGridProducts.SelectedItems[0];
			TasksWindow.Instance.BringWindowToFront(selectedProduct, "COMPRAR MAS PRODUCTO");
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

		private void CleanOtherDataGrid(object sender, RoutedEventArgs e)
		{
			DataGridAddedProducts.Items.Clear();
		}

		private void OpenTasksWindow(object sender, RoutedEventArgs e)
		{
			TasksWindow.Instance.BringWindowToFront(null);
		}
	}
}
