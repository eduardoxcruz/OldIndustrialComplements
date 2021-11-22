using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Inventory.enums;
using Inventory.model;

namespace Inventory.ui
{
	public partial class ProductWindow
	{
		public static readonly ProductWindow ShowProductDetailsInstance = new(ProductWindowTasks.ShowDetails);
		public static readonly ProductWindow ModifyProductInstance = new(ProductWindowTasks.Modify);
		public static readonly ProductWindow AddNewProductInstance = new(ProductWindowTasks.AddNewProduct);
		private static readonly SolidColorBrush BlackColorBrush = new(Colors.Black);
		private static readonly SolidColorBrush CrimsonColorBrush = new(Colors.Crimson);
		private Product Product { get; } = new();
		private ProductWindowTasks CurrentTask { get; }

		private ProductWindow(){}
		private ProductWindow(ProductWindowTasks task)
		{
			InitializeComponent();
			CurrentTask = task;
			ConfigureControlsForTask();
		}
		private void AssignProductDataToControls(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				MessageBox.Show("El campo ID no puede estar vacio", "Error");
				return;
			}

			if (Product.Id == 0 || !id.Equals(Product.Id.ToString()))
			{
				Product.GetDataFromSqlDatabase(id);
			}

			TxtBoxId.Text = Product.Id.ToString();
			TxtBoxIdCode.Text = Product.Id.ToString();
			TxtBoxEnrollment.Text = Product.Enrollment;
			TxtBoxShortDescription.Text = Product.ShortDescription;
			TxtBoxCurrentProductStock.Text = Product.ProductAmount.ToString();
			TxtBoxMinProductStock.Text = Product.MinAmount.ToString();
			TxtBoxMaxProductStock.Text = Product.MaxAmount.ToString();
			TxtBoxContainer.Text = Product.Container;
			TxtBoxLocation.Text = Product.Location;
			TxtBoxBranchOffice.Text = Product.BranchOffice;
			TxtBoxShelf.Text = Product.Shelf;
			TxtBoxLedge.Text = Product.Rack;
			TxtBoxPurchasePrice.Text = Product.BuyPrice.ToString();
			TxtBoxManufacturerPartNumber.Text = Product.PartNumber;
			TxtBoxPercentageOfProfit.Text = Product.PercentageOfProfit.ToString();
			TxtBoxDiscountRate.Text = Product.PercentageOfDiscount.ToString();
			TxtBoxSalePrice.Text = Product.SalePriceWithoutDiscount.ToString();
			TxtBoxPriceWithDiscount.Text = Product.PriceWithDiscount.ToString();
			TxtBoxUtility.Text = Product.ProfitWithoutDiscount.ToString();
			TxtBoxProfitWithDiscount.Text = Product.ProfitWithDiscount.ToString();
			TxtBoxFullDescription.Text = Product.FullDescription;
			TxtBoxMemo.Text = Product.Memo;
			
			CmbBoxStatus.Items.Clear();
			CmbBoxProductType.Items.Clear();
			CmbBoxCategories.Items.Clear();
			CmbBoxManufacturer.Items.Clear();
			CmbBoxUnit.Items.Clear();
			CmbBoxEncapsulationType.Items.Clear();
			CmbBoxMountingTechnology.Items.Clear();
			
			CmbBoxStatus.Items.Add(Product.Status);
			CmbBoxProductType.Items.Add(Product.TypeOfStock);
			CmbBoxUnit.Items.Add(Product.UnitType);
			CmbBoxManufacturer.Items.Add(Product.Manufacturer);
			CmbBoxCategories.Items.Add(Product.Category);
			CmbBoxMountingTechnology.Items.Add(Product.MountingTechnology);
			CmbBoxEncapsulationType.Items.Add(Product.EncapsulationType);
			
			CmbBoxStatus.SelectedIndex = 0;
			CmbBoxProductType.SelectedIndex = 0;
			CmbBoxUnit.SelectedIndex = 0;
			CmbBoxManufacturer.SelectedIndex = 0;
			CmbBoxCategories.SelectedIndex = 0;
			CmbBoxMountingTechnology.SelectedIndex = 0;
			CmbBoxEncapsulationType.SelectedIndex = 0;
			
			if (Product.ProductUseInventory.Equals(true))
			{
				ChkBoxTheProductUsesInventory.IsChecked = true;
				EnableControlsForInventory();
			}

			if (Product.ProductUseInventory.Equals(false))
			{
				ChkBoxTheProductUsesInventory.IsChecked = false;
				DisableControlsForInventory();
			}

			if (Product.ManualProfit.Equals(true))
			{
				RadioBtnAutomaticProfit.IsChecked = false;
				EnableControlsForManualProfit();
			}

			if (Product.ManualProfit.Equals(false))
			{
				RadioBtnAutomaticProfit.IsChecked = true;
				DisableControlsForAutomaticProfit();
			}
		}
		private void ConfigureControlsForTask()
		{
			switch (CurrentTask)
			{
				case ProductWindowTasks.AddNewProduct:
					UnlockEditableControls();
					TxtBlockProductTask.Text = "Nuevo Producto";
					BtnAddModifyAndSave.Content = "Agregar";
					break;
				case ProductWindowTasks.Modify:
					UnlockEditableControls();
					TxtBlockProductTask.Text = "Modificar Producto";
					BtnAddModifyAndSave.Content = "Guardar";
					break;
				case ProductWindowTasks.ShowDetails:
					LockEditableControls();
					TxtBlockProductTask.Text = "Detalles del Producto";
					BtnAddModifyAndSave.Content = "Modificar";
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			
			this.Title = TxtBlockProductTask.Text;
		}
		private void LockEditableControls()
		{
			TxtBlockProductTask.Foreground = CrimsonColorBrush;
			
			CmbBoxStatus.IsReadOnly = true;
			CmbBoxStatus.Foreground = CrimsonColorBrush;
			TxtBoxEnrollment.IsReadOnly = true;
			TxtBoxEnrollment.Foreground = CrimsonColorBrush;
			CmbBoxMountingTechnology.IsReadOnly = true;
			CmbBoxMountingTechnology.Foreground = CrimsonColorBrush;
			CmbBoxEncapsulationType.IsReadOnly = true;
			CmbBoxEncapsulationType.Foreground = CrimsonColorBrush;
			TxtBoxShortDescription.IsReadOnly = true;
			TxtBoxShortDescription.Foreground = CrimsonColorBrush;
			CmbBoxCategories.IsReadOnly = true;
			CmbBoxCategories.Foreground = CrimsonColorBrush;
			TxtBoxFullDescription.IsReadOnly = true;
			TxtBoxFullDescription.Foreground = CrimsonColorBrush;

			ChkBoxTheProductUsesInventory.IsEnabled = false;
			TxtBoxCurrentProductStock.Foreground = CrimsonColorBrush;
			TxtBoxMinProductStock.Foreground = CrimsonColorBrush;
			TxtBoxMaxProductStock.Foreground = CrimsonColorBrush;
			DisableControlsForInventory();

			TxtBoxContainer.IsReadOnly = true;
			TxtBoxContainer.Foreground = CrimsonColorBrush;
			TxtBoxLocation.IsReadOnly = true;
			TxtBoxLocation.Foreground = CrimsonColorBrush;
			TxtBoxBranchOffice.IsReadOnly = true;
			TxtBoxBranchOffice.Foreground = CrimsonColorBrush;
			TxtBoxShelf.IsReadOnly = true;
			TxtBoxShelf.Foreground = CrimsonColorBrush;
			TxtBoxLedge.IsReadOnly = true;
			TxtBoxLedge.Foreground = CrimsonColorBrush;

			TxtBoxPurchasePrice.IsReadOnly = true;
			TxtBoxPurchasePrice.Foreground = CrimsonColorBrush;
			CmbBoxUnit.IsReadOnly = true;
			CmbBoxUnit.Foreground = CrimsonColorBrush;
			CmbBoxProductType.IsReadOnly = true;
			CmbBoxProductType.Foreground = CrimsonColorBrush;
			CmbBoxManufacturer.IsReadOnly = true;
			CmbBoxManufacturer.Foreground = CrimsonColorBrush;
			TxtBoxManufacturerPartNumber.IsReadOnly = true;
			TxtBoxManufacturerPartNumber.Foreground = CrimsonColorBrush;
			
			RadioBtnAutomaticProfit.IsEnabled = false;
			RadioBtnManualProfit.IsEnabled = false;
			TxtBoxPercentageOfProfit.Foreground = CrimsonColorBrush;
			TxtBoxSalePrice.Foreground = CrimsonColorBrush;
			TxtBoxUtility.Foreground = CrimsonColorBrush;
			TxtBoxDiscountRate.Foreground = CrimsonColorBrush;
			TxtBoxPriceWithDiscount.Foreground = CrimsonColorBrush;
			TxtBoxProfitWithDiscount.Foreground = CrimsonColorBrush;
			LockControlsForAutomaticProfit();

			TxtBoxMemo.IsReadOnly = true;
			TxtBoxMemo.Foreground = CrimsonColorBrush;
		}
		private void UnlockEditableControls()
		{
			TxtBlockProductTask.Foreground = BlackColorBrush;
			
			CmbBoxStatus.IsReadOnly = false;
			CmbBoxStatus.Foreground = BlackColorBrush;
			TxtBoxEnrollment.IsReadOnly = false;
			TxtBoxEnrollment.Foreground = BlackColorBrush;
			CmbBoxMountingTechnology.IsReadOnly = false;
			CmbBoxMountingTechnology.Foreground = BlackColorBrush;
			CmbBoxEncapsulationType.IsReadOnly = false;
			CmbBoxEncapsulationType.Foreground = BlackColorBrush;
			TxtBoxShortDescription.IsReadOnly = false;
			TxtBoxShortDescription.Foreground = BlackColorBrush;
			CmbBoxCategories.IsReadOnly = false;
			CmbBoxCategories.Foreground = BlackColorBrush;
			TxtBoxFullDescription.IsReadOnly = false;
			TxtBoxFullDescription.Foreground = BlackColorBrush;

			ChkBoxTheProductUsesInventory.IsEnabled = true;
			TxtBoxCurrentProductStock.Foreground = BlackColorBrush;
			TxtBoxMinProductStock.Foreground = BlackColorBrush;
			TxtBoxMaxProductStock.Foreground = BlackColorBrush;
			EnableControlsForInventory();

			TxtBoxContainer.IsReadOnly = false;
			TxtBoxContainer.Foreground = BlackColorBrush;
			TxtBoxLocation.IsReadOnly = false;
			TxtBoxLocation.Foreground = BlackColorBrush;
			TxtBoxBranchOffice.IsReadOnly = false;
			TxtBoxBranchOffice.Foreground = BlackColorBrush;
			TxtBoxShelf.IsReadOnly = false;
			TxtBoxShelf.Foreground = BlackColorBrush;
			TxtBoxLedge.IsReadOnly = false;
			TxtBoxLedge.Foreground = BlackColorBrush;
			CmbBoxUnit.IsReadOnly = false;
			CmbBoxUnit.Foreground = BlackColorBrush;
			TxtBoxPurchasePrice.IsReadOnly = false;
			TxtBoxPurchasePrice.Foreground = BlackColorBrush;
			CmbBoxProductType.IsReadOnly = false;
			CmbBoxProductType.Foreground = BlackColorBrush;
			CmbBoxManufacturer.IsReadOnly = false;
			CmbBoxManufacturer.Foreground = BlackColorBrush;
			TxtBoxManufacturerPartNumber.IsReadOnly = false;
			TxtBoxManufacturerPartNumber.Foreground = BlackColorBrush;
			
			RadioBtnAutomaticProfit.IsEnabled = true;
			RadioBtnManualProfit.IsEnabled = true;
			TxtBoxPercentageOfProfit.Foreground = BlackColorBrush;
			TxtBoxSalePrice.Foreground = BlackColorBrush;
			TxtBoxUtility.Foreground = BlackColorBrush;
			TxtBoxDiscountRate.Foreground = BlackColorBrush;
			TxtBoxPriceWithDiscount.Foreground = BlackColorBrush;
			TxtBoxProfitWithDiscount.Foreground = BlackColorBrush;
			UnlockControlsForManualProfit();

			TxtBoxMemo.IsReadOnly = false;
			TxtBoxMemo.Foreground = BlackColorBrush;
		}
		private void UnlockControlsForManualProfit()
		{
			TxtBoxPercentageOfProfit.IsReadOnly = false;
			TxtBoxSalePrice.IsReadOnly = false;
			TxtBoxUtility.IsReadOnly = false;
			TxtBoxDiscountRate.IsReadOnly = false;
			TxtBoxPriceWithDiscount.IsReadOnly = false;
			TxtBoxProfitWithDiscount.IsReadOnly = false;
		}
		private void EnableControlsForManualProfit()
		{
			RadioBtnManualProfit.IsChecked = true;
			UnlockControlsForManualProfit();
		}
		private void LockControlsForAutomaticProfit()
		{
			TxtBoxPercentageOfProfit.IsReadOnly = true;
			TxtBoxSalePrice.IsReadOnly = true;
			TxtBoxUtility.IsReadOnly = true;
			TxtBoxDiscountRate.IsReadOnly = true;
			TxtBoxPriceWithDiscount.IsReadOnly = true;
			TxtBoxProfitWithDiscount.IsReadOnly = true;
		}
		private void DisableControlsForAutomaticProfit()
		{
			RadioBtnManualProfit.IsChecked = false;
			LockControlsForAutomaticProfit();
		}
		private void EnableControlsForInventory()
		{
			TxtBoxCurrentProductStock.IsReadOnly = false;
			TxtBoxMinProductStock.IsReadOnly = false;
			TxtBoxMaxProductStock.IsReadOnly = false;
		}
		private void DisableControlsForInventory()
		{
			TxtBoxCurrentProductStock.IsReadOnly = true;
			TxtBoxMinProductStock.IsReadOnly = true;
			TxtBoxMaxProductStock.IsReadOnly = true;
		}
		private void BtnSearch_Click(object sender, RoutedEventArgs e)
		{
			AssignProductDataToControls(TxtBoxIdCode.Text);
		}
		private void BtnAddModifyAndSave_OnClick(object sender, RoutedEventArgs e)
		{
			switch (CurrentTask)
			{
				case ProductWindowTasks.Modify:
					ShowProductDetailsInstance.BringWindowToFront(TxtBoxId.Text);
					break;
				case ProductWindowTasks.ShowDetails:
					ModifyProductInstance.BringWindowToFront(TxtBoxId.Text);
					break;
				case ProductWindowTasks.AddNewProduct:
					ShowProductDetailsInstance.BringWindowToFront(TxtBoxId.Text);
					break;
			}
		}
		private void RadioBtnAutomaticProfit_OnChecked(object sender, RoutedEventArgs e)
		{
			RadioBtnManualProfit.IsChecked = false;
			DisableControlsForAutomaticProfit();
		}
		private void RadioBtnAutomaticProfit_OnUnchecked(object sender, RoutedEventArgs e)
		{
			RadioBtnManualProfit.IsChecked = true;
			EnableControlsForManualProfit();
		}
		private void ChkBoxTheProductUsesInventory_OnChecked(object sender, RoutedEventArgs e)
		{
			EnableControlsForInventory();
		}
		private void ChkBoxTheProductUsesInventory_OnUnchecked(object sender, RoutedEventArgs e)
		{
			DisableControlsForInventory();
		}
		private void TxtBoxIdCode_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key==Key.Enter)
			{
				AssignProductDataToControls(TxtBoxIdCode.Text);
			}
		}
		private void BtnQuickLoad_OnClick(object sender, RoutedEventArgs e)
		{
			AssignProductDataToControls("1");
		}
		public void BringWindowToFront(string productId = "")
		{
			ConfigureControlsForTask();

			if (!string.IsNullOrEmpty(productId))
			{
				AssignProductDataToControls(productId);
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
	}
}
