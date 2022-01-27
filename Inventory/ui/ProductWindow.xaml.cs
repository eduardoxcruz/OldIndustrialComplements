using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Inventory.data;
using Inventory.enums;
using Inventory.model;
using Microsoft.EntityFrameworkCore;

namespace Inventory.ui
{
	public partial class ProductWindow
	{
		public static readonly ProductWindow ShowProductDetailsInstance = new(ProductWindowTasks.ShowDetails);
		public static readonly ProductWindow ModifyProductInstance = new(ProductWindowTasks.Modify);
		public static readonly ProductWindow AddNewProductInstance = new(ProductWindowTasks.AddNewProduct);
		private static SolidColorBrush ForegroundColor { get; set; }
		private static List<String> Statuses { get; set; }
		private static List<String> MountingTechnologies { get; set; }
		private Product Product { get; set; }
		private ProductWindowTasks CurrentTask { get; }

		private ProductWindow()
		{
			InitializeComponent();
		}

		private ProductWindow(ProductWindowTasks task)
		{
			InitializeComponent();
			Product = new Product();
			this.DataContext = Product;
			CurrentTask = task;
			ConfigureWindowForTask();
		}

		public void BringWindowToFront(Product product = null)
		{
			if (product != null)
			{
				SearchProductById(product.Id);
			}

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
		}

		private void ConfigureWindowForTask()
		{
			switch (CurrentTask)
			{
				case ProductWindowTasks.AddNewProduct:
					ConfigureWindowForAddProduct();
					break;
				case ProductWindowTasks.Modify:
					TxtBlockProductTask.Content = "Modificar Producto";
					BtnAddModifyAndSave.Content = "Guardar";
					break;
				case ProductWindowTasks.ShowDetails:
					ConfigureWindowForShowDetails();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			this.Title = TxtBlockProductTask.Content.ToString();
			SetComboBoxesItemSource();
		}

		private void ConfigureWindowForAddProduct()
		{
			TxtBoxIdCode.IsReadOnly = true;
			TxtBoxDebugCode.IsReadOnly = true;
			BtnRefreshProduct.IsEnabled = false;
			BtnVerifySearch.IsEnabled = false;
			BtnLoadFirstProduct.IsEnabled = false;
			BtnLoadLastProduct.IsEnabled = false;
			TxtBlockProductTask.Content = "Nuevo Producto";
			BtnAddModifyAndSave.Content = "Agregar";
			Product lastProduct = null;
			InventoryDbContext.ExecuteDatabaseRequest(() =>
			{
				using InventoryDbContext inventoryDb = new();
				lastProduct = inventoryDb.Products.OrderByDescending(product => product.Id).FirstOrDefault();
			});
			Product.Id = lastProduct == null ? 0 : lastProduct.Id + 1;
		}

		private void ConfigureWindowForShowDetails()
		{
			ForegroundColor = new SolidColorBrush(Colors.Crimson);

			TxtBlockProductTask.Content = "Detalles del Producto";
			TxtBlockProductTask.Foreground = ForegroundColor;
			BtnAddModifyAndSave.Content = "Modificar";
			LblSalePriceWithoutDiscount.Foreground = ForegroundColor;
			LblSalePriceWithDiscount.Foreground = ForegroundColor;
			LblProfitWithoutDiscount.Foreground = Brushes.GreenYellow;
			LblProfitWithDiscount.Foreground = Brushes.GreenYellow;
			FlowDocumentScrollViewer.Foreground = ForegroundColor;
			TxtBoxShortDescription.Foreground = ForegroundColor;

			RadioBtnIsAutomaticProfit.IsEnabled = false;
			RadioBtnIsManualProfit.IsEnabled = false;
			TxtBoxShortDescription.IsReadOnly = true;
			TxtBoxMemo.IsReadOnly = true;
			TxtBoxMemo.Foreground = ForegroundColor;
			DisableProfitAndDiscountButtons();

			SetControlsInPanelAsReadonly(GridProductDetails, true);
			SetControlsInPanelAsReadonly(GridInventory, true);
			SetControlsInPanelAsReadonly(GridLocation, true);
			SetControlsInPanelAsReadonly(GridPriceDetails, true);
		}

		private static void SetControlsInPanelAsReadonly(Panel panel, bool readOnly)
		{
			for (int index = 0; index < panel.Children.Count; index++)
			{
				switch (panel.Children[index])
				{
					case TextBox textBox:
						textBox = (TextBox)panel.Children[index];
						textBox.Foreground = ForegroundColor;
						textBox.IsReadOnly = readOnly;
						panel.Children[index] = textBox;
						break;
					case ComboBox comboBox:
						comboBox = (ComboBox)panel.Children[index];
						comboBox.Foreground = ForegroundColor;
						comboBox.IsReadOnly = readOnly;
						panel.Children[index] = comboBox;
						break;
					case CheckBox:
						panel.Children[index].IsEnabled = !readOnly;
						break;
				}
			}
		}

		private void SetComboBoxesItemSource()
		{
			Statuses = new List<string>() {"ACTIVO", "OBSOLETO"};
			MountingTechnologies = new List<String>()
			{
				"N/A",
				"THT",
				"SMD",
				"LIBRE SUSPENSION",
				"PANEL",
				"RIEL"
			};

			CmbBoxStatus.ItemsSource = Statuses;
			CmbBoxMountingTechnology.ItemsSource = MountingTechnologies;
		}

		private void TxtBoxIdCodeEnterKeyPressed(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter) VerifySearch(sender, e);
		}

		private void VerifySearch(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(TxtBoxIdCode.Text))
			{
				MessageBox.Show("Ingresa un Id o Codigo Debug de un producto.", "Id o Codigo Debug Invalido",
					MessageBoxButton.OK, MessageBoxImage.Exclamation);
				return;
			}

			SearchProductById(int.Parse(TxtBoxIdCode.Text));
		}

		private void RefreshProduct(object sender, RoutedEventArgs e)
		{
			if (Product.Id > 0)
				SearchProductById(Product.Id);
		}

		private void LoadFirstProduct(object sender, RoutedEventArgs e)
		{
			SearchProductById(1);
		}

		private void LoadLastProduct(object sender, RoutedEventArgs e)
		{
			Product lastProduct = null;
			InventoryDbContext.ExecuteDatabaseRequest(() =>
			{
				using InventoryDbContext inventoryDb = new();
				lastProduct = inventoryDb.Products.OrderByDescending(product => product.Id).FirstOrDefault();
			});

			SearchProductById(lastProduct?.Id ?? 1);
		}

		private void SearchProductById(int id)
		{
			this.DataContext = Product = Product.GetDataFromSqlDatabase(id);
			if (Product != null && Product.Id != 0) LoadProductImage();
		}

		private void LoadProductImage()
		{
			Uri imageUri = null;

			try
			{
				imageUri = new Uri(Properties.Settings.Default.PhotosPath + "\\" + Product.Id + ".jpg");
				ProductImage.Source = new BitmapImage(imageUri);
			}
			catch (Exception exception)
			{
				imageUri = new Uri("/resources/images/defaultProductImage.jpg", UriKind.Relative);
				ProductImage.Source = new BitmapImage(imageUri);
			}
		}

		private void ChangeTask(object sender, RoutedEventArgs e)
		{
			if (Product.Id < 1)
				return;

			switch (CurrentTask)
			{
				case ProductWindowTasks.Modify:
					if (MessageBox.Show(
						    "¿Esta seguro de guardar los cambios?",
						    "Confirmacion",
						    MessageBoxButton.OKCancel,
						    MessageBoxImage.Question) == MessageBoxResult.OK)
						SaveProduct();
					break;
				case ProductWindowTasks.ShowDetails:
					ModifyProductInstance.BringWindowToFront(Product);
					break;
				case ProductWindowTasks.AddNewProduct:
					if (MessageBox.Show(
						    "¿Esta seguro de guardar el nuevo producto?",
						    "Confirmacion",
						    MessageBoxButton.OKCancel,
						    MessageBoxImage.Question) == MessageBoxResult.OK)
					{
						SaveProduct();
						ShowProductDetailsInstance.BringWindowToFront(Product);
						ConfigureWindowForTask();
					}

					break;
			}
		}

		private void SaveProduct()
		{
			InventoryDbContext.ExecuteDatabaseRequest(() =>
			{
				if (Product.Id == 0) return;

				using InventoryDbContext inventoryDb = new();

				switch (CurrentTask)
				{
					case ProductWindowTasks.Modify:
						if (ProfitWithoutDiscountIsZero())
						{
							MessageBox.Show("La utilidad debe ser mayor a 0.",
								"Error",
								MessageBoxButton.OK,
								MessageBoxImage.Error);
							return;
						}

						inventoryDb.Entry(Product).State = EntityState.Modified;
						break;
					case ProductWindowTasks.AddNewProduct:
						if (!IsNewProductValid()) return;
						inventoryDb.Products.Add(Product);
						break;
				}

				inventoryDb.SaveChanges();
				MessageBox.Show("Hecho", "Exito", MessageBoxButton.OK, MessageBoxImage.Asterisk);
			});
		}

		private bool IsNewProductValid()
		{
			bool isValid = true;

			if (string.IsNullOrEmpty(Product.Enrollment) ||
			    string.IsNullOrEmpty(Product.Status) ||
			    string.IsNullOrEmpty(Product.MountingTechnology) ||
			    string.IsNullOrEmpty(Product.EncapsulationType) ||
			    string.IsNullOrEmpty(Product.ShortDescription) ||
			    string.IsNullOrEmpty(Product.TypeOfStock) ||
			    string.IsNullOrEmpty(Product.UnitType) ||
			    ProfitWithoutDiscountIsZero())
			{
				isValid = false;
				MessageBox.Show("Los campos con * son obligatorios y la utilidad debe ser mayor a 0.",
					"Error",
					MessageBoxButton.OK,
					MessageBoxImage.Error);
			}

			return isValid;
		}

		private bool ProfitWithoutDiscountIsZero()
		{
			return Product.ProfitWithoutDiscount <= 0;
		}

		private void EnableInventory(object sender, RoutedEventArgs e)
		{
			if (CurrentTask == ProductWindowTasks.ShowDetails) return;

			TxtBoxCurrentAmount.IsReadOnly = false;
			TxtBoxMaxAmount.IsReadOnly = false;
			TxtBoxMinAmount.IsReadOnly = false;
		}

		private void DisableInventory(object sender, RoutedEventArgs e)
		{
			if (CurrentTask == ProductWindowTasks.ShowDetails) return;

			TxtBoxCurrentAmount.IsReadOnly = true;
			TxtBoxMaxAmount.IsReadOnly = true;
			TxtBoxMinAmount.IsReadOnly = true;
		}

		private void EnableManualProfit(object sender, RoutedEventArgs e)
		{
			if (CurrentTask == ProductWindowTasks.ShowDetails) return;

			EnableProfitAndDiscountButtons();

			TxtBoxPercentageOfProfit.IsReadOnly = false;
			TxtBoxPercentageOfDiscount.IsReadOnly = false;
		}

		private void EnableAutomaticProfit(object sender, RoutedEventArgs e)
		{
			if (CurrentTask == ProductWindowTasks.ShowDetails) return;

			RadioBtnIsAutomaticProfit.IsChecked = true;

			DisableProfitAndDiscountButtons();

			TxtBoxPercentageOfProfit.IsReadOnly = true;
			TxtBoxPercentageOfDiscount.IsReadOnly = true;
		}

		private void EnableProfitAndDiscountButtons()
		{
			BtnAddProfit.IsEnabled = true;
			BtnAddDiscount.IsEnabled = true;
			BtnRemoveProfit.IsEnabled = true;
			BtnRemoveDiscount.IsEnabled = true;
		}

		private void DisableProfitAndDiscountButtons()
		{
			BtnAddProfit.IsEnabled = false;
			BtnAddDiscount.IsEnabled = false;
			BtnRemoveProfit.IsEnabled = false;
			BtnRemoveDiscount.IsEnabled = false;
		}

		private void AddProfit(object sender, RoutedEventArgs e)
		{
			Product.PercentageOfProfit += 1;
		}

		private void RemoveProfit(object sender, RoutedEventArgs e)
		{
			Product.PercentageOfProfit -= 1;
		}

		private void AddDiscount(object sender, RoutedEventArgs e)
		{
			Product.PercentageOfDiscount += 1;
		}

		private void RemoveDiscount(object sender, RoutedEventArgs e)
		{
			Product.PercentageOfDiscount -= 1;
		}

		private void OpenTasksWindow(object sender, RoutedEventArgs e)
		{
			TasksWindow.Instance.BringWindowToFront(Product);
		}
	}
}
