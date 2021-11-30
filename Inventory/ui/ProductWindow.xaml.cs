using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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
		private static readonly SolidColorBrush BlackColorBrush = new(Colors.Black);
		private static readonly SolidColorBrush CrimsonColorBrush = new(Colors.Crimson);
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
			Statuses = new List<string>() {"ACTIVO", "OBSOLETO"};
			MountingTechnologies = new List<String>() {"N/A", "THT", "SMD", "LIBRE SUSPENSION", "PANEL", "RIEL"};
			CurrentTask = task;
			ConfigureControlsForTask();
		}
		public void BringWindowToFront(Product product = null)
		{
			ConfigureControlsForTask();

			if (product != null)
			{
				UpdateProduct(product.Id);
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
		private void ConfigureControlsForTask()
		{
			using InventoryDbContext inventoryDb = new();
			
			switch (CurrentTask)
			{
				case ProductWindowTasks.AddNewProduct:
					UnlockAllControls();
					TxtBoxIdCode.IsReadOnly = true;
					TxtBoxDebugCode.IsReadOnly = true;
					TxtBlockProductTask.Text = "Nuevo Producto";
					BtnAddModifyAndSave.Content = "Agregar";
					Product.Id = inventoryDb
						.Products
						.OrderByDescending(product => product.Id)
						.FirstOrDefault()
						.Id + 1;
					break;
				case ProductWindowTasks.Modify:
					UnlockAllControls();
					TxtBlockProductTask.Text = "Modificar Producto";
					BtnAddModifyAndSave.Content = "Guardar";
					break;
				case ProductWindowTasks.ShowDetails:
					LockAllControls();
					TxtBlockProductTask.Text = "Detalles del Producto";
					BtnAddModifyAndSave.Content = "Modificar";
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			CmbBoxStatus.ItemsSource = Statuses;
			CmbBoxMountingTechnology.ItemsSource = MountingTechnologies;
			this.Title = TxtBlockProductTask.Text;
		}
		private void TxtBoxIdCodeEnterPressed(object sender, KeyEventArgs e)
		{
			if (e.Key==Key.Enter) SearchProduct(null, null);
		}
		private void SearchProduct(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(TxtBoxIdCode.Text))
			{
				MessageBox.Show("Ingresa un Id o Codigo Debug de un producto.", "Error");
				return;
			}

			if (int.Parse(TxtBoxIdCode.Text) <= 0)
			{
				MessageBox.Show("Ingresa un Id o Codigo Debug valido.", "Error");
				return;
			}

			UpdateProduct(int.Parse(TxtBoxIdCode.Text));
		}
		private void UpdateOnClick(object sender, RoutedEventArgs e)
		{
			UpdateProduct(Product.Id);
		}
		private void LoadFirstProduct(object sender, RoutedEventArgs e)
		{
			UpdateProduct(1);
		}
		private void UpdateProduct(int id)
		{
			this.DataContext = Product = Product.GetDataFromSqlDatabase(id);

			if (Product.IsUsingInventory.Equals(true))
			{
				ChkBoxUseInventory.IsChecked = true;
				UseInventoryEnabled(null, null);
			}

			if (Product.IsUsingInventory.Equals(false))
			{
				ChkBoxUseInventory.IsChecked = false;
				UseInventoryDisabled(null, null);
			}

			if (Product.IsManualProfit.Equals(true))
			{
				RadioBtnAutomaticProfit.IsChecked = false;
				EnableManualProfit(null, null);
			}

			if (Product.IsManualProfit.Equals(false))
			{
				RadioBtnAutomaticProfit.IsChecked = true;
				EnableAutomaticProfit(null, null);
			}
		}
		private void LockAllControls()
		{
			ForegroundColor = CrimsonColorBrush;
			
			ReadonlyControlsInGrid(GridProductDetails, true);
			ReadonlyControlsInGrid(GridInventory, true);
			ReadonlyControlsInGrid(GridLocation, true);
			ReadonlyControlsInGrid(GridPriceDetails, true);
		}
		private void UnlockAllControls()
		{
			ForegroundColor = BlackColorBrush;
			
			ReadonlyControlsInGrid(GridProductDetails, false);
			ReadonlyControlsInGrid(GridInventory, false);
			ReadonlyControlsInGrid(GridLocation, false);
			ReadonlyControlsInGrid(GridPriceDetails, false);
		}
		private static void ReadonlyControlsInGrid(Panel panel, bool readOnly)
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
					case RadioButton:
						panel.Children[index].IsEnabled = !readOnly;
						break;
				}
			}
		}
		private void EnableManualProfit(object sender, RoutedEventArgs e)
		{
			RadioBtnAutomaticProfit.IsChecked = false;
			ConfigureForManualProfit();
		}
		private void ConfigureForManualProfit()
		{
			TxtBoxPercentageOfProfit.IsReadOnly = false;
			TxtBoxPercentageOfDiscount.IsReadOnly = false;
			BtnAddProfit.IsEnabled = true;
			BtnRemoveProfit.IsEnabled = true;
			BtnAddDiscount.IsEnabled = true;
			BtnRemoveDiscount.IsEnabled = true;
		}
		private void EnableAutomaticProfit(object sender, RoutedEventArgs e)
		{
			RadioBtnIsManualProfit.IsChecked = false;
			ConfigureForAutomaticProfit();
		}
		private void ConfigureForAutomaticProfit()
		{
			TxtBoxPercentageOfProfit.IsReadOnly = true;
			TxtBoxPercentageOfDiscount.IsReadOnly = true;
			BtnAddProfit.IsEnabled = false;
			BtnRemoveProfit.IsEnabled = false;
			BtnAddDiscount.IsEnabled = false;
			BtnRemoveDiscount.IsEnabled = false;
		}
		private void UseInventoryEnabled(object sender, RoutedEventArgs e)
		{
			TxtBoxCurrentAmount.IsReadOnly = false;
			TxtBoxMinAmount.IsReadOnly = false;
			TxtBoxMaxProductStock.IsReadOnly = false;
		}
		private void UseInventoryDisabled(object sender, RoutedEventArgs e)
		{
			TxtBoxCurrentAmount.IsReadOnly = true;
			TxtBoxMinAmount.IsReadOnly = true;
			TxtBoxMaxProductStock.IsReadOnly = true;
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
						MessageBoxButton.OKCancel) == MessageBoxResult.OK)
						SaveProduct();
					break;
				case ProductWindowTasks.ShowDetails:
					ModifyProductInstance.BringWindowToFront(Product);
					break;
				case ProductWindowTasks.AddNewProduct:
					if (MessageBox.Show(
						"¿Esta seguro de guardar el nuevo producto?",
						"Confirmacion",
						MessageBoxButton.OKCancel) == MessageBoxResult.OK)
					{
						SaveProduct();
						ShowProductDetailsInstance.BringWindowToFront(Product);
						ConfigureControlsForTask();
					}
					break;
			}
		}
		private void SaveProduct()
		{
			using InventoryDbContext inventoryDb = new();
			
			if (CurrentTask == ProductWindowTasks.Modify)
				inventoryDb.Entry(Product).State = EntityState.Modified;
			
			if (CurrentTask == ProductWindowTasks.AddNewProduct)
				inventoryDb.Products.Add(Product);
			
			inventoryDb.SaveChanges();
		}
		private void OpenTasksWindow(object sender, RoutedEventArgs e)
		{
			
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
	}
}
