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
			
			this.Title = TxtBlockProductTask.Text;
			SetComboBoxesItemSource();
			ConfigureInventoryAndProfitControls();
		}
		private void ConfigureWindowForAddProduct()
		{
			UnlockAllControls();
			TxtBoxIdCode.IsReadOnly = true;
			TxtBoxDebugCode.IsReadOnly = true;
			BtnRefreshProduct.IsEnabled = false;
			BtnVerifySearch.IsEnabled = false;
			BtnLoadFirstProduct.IsEnabled = false;
			TxtBlockProductTask.Text = "Nuevo Producto";
			BtnAddModifyAndSave.Content = "Agregar";
			using InventoryDbContext inventoryDb = new();
			Product.Id = inventoryDb.Products.OrderByDescending(product => product.Id).FirstOrDefault().Id + 1;
		}
		private void SetComboBoxesItemSource()
		{
			Statuses = new List<string>() {"ACTIVO", "OBSOLETO"};
			MountingTechnologies = new List<String>() {"N/A", "THT", "SMD", "LIBRE SUSPENSION", "PANEL", "RIEL"};
			
			CmbBoxStatus.ItemsSource = Statuses;
			CmbBoxMountingTechnology.ItemsSource = MountingTechnologies;
		}
		private void TxtBoxIdCodeEnterKeyPressed(object sender, KeyEventArgs e)
		{
			if (e.Key==Key.Enter) VerifySearch(sender, e);
		}
		private void VerifySearch(object sender, RoutedEventArgs e)
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
		private void SearchProductById(int id)
		{
			this.DataContext = Product = Product.GetDataFromSqlDatabase(id);
			ConfigureInventoryAndProfitControls();
		}
		private void LockAllControls()
		{
			ForegroundColor = new SolidColorBrush(Colors.Crimson);
			
			SetControlsInPanelAsReadonly(GridProductDetails, true);
			SetControlsInPanelAsReadonly(GridInventory, true);
			SetControlsInPanelAsReadonly(GridLocation, true);
			SetControlsInPanelAsReadonly(GridPriceDetails, true);
		}
		private void UnlockAllControls()
		{
			ForegroundColor = new SolidColorBrush(Colors.Black);
			
			SetControlsInPanelAsReadonly(GridProductDetails, false);
			SetControlsInPanelAsReadonly(GridInventory, false);
			SetControlsInPanelAsReadonly(GridLocation, false);
			SetControlsInPanelAsReadonly(GridPriceDetails, false);
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
					case RadioButton:
						panel.Children[index].IsEnabled = !readOnly;
						break;
				}
			}
		}
		private void ConfigureInventoryAndProfitControls()
		{
			if (Product.IsUsingInventory ?? false)
			{
				ChkBoxUseInventory.IsChecked = true;
				EnableInventory(null, null);
			}
			else
			{
				ChkBoxUseInventory.IsChecked = false;
				DisableInventory(null, null);
			}

			if (Product.IsManualProfit ?? false)
			{
				RadioBtnAutomaticProfit.IsChecked = false;
				EnableManualProfit(null, null);
			}
			else 
			{
				RadioBtnAutomaticProfit.IsChecked = true;
				EnableAutomaticProfit(null, null);
			}
		}
		private void EnableManualProfit(object sender, RoutedEventArgs e)
		{
			RadioBtnAutomaticProfit.IsChecked = false;
			
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
			
			TxtBoxPercentageOfProfit.IsReadOnly = true;
			TxtBoxPercentageOfDiscount.IsReadOnly = true;
			BtnAddProfit.IsEnabled = false;
			BtnRemoveProfit.IsEnabled = false;
			BtnAddDiscount.IsEnabled = false;
			BtnRemoveDiscount.IsEnabled = false;
		}
		private void EnableInventory(object sender, RoutedEventArgs e)
		{
			TxtBoxCurrentAmount.IsReadOnly = false;
			TxtBoxMinAmount.IsReadOnly = false;
			TxtBoxMaxProductStock.IsReadOnly = false;
		}
		private void DisableInventory(object sender, RoutedEventArgs e)
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
						ConfigureWindowForTask();
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
			TasksWindow.Instance.BringWindowToFront();
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
