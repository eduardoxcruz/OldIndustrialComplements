using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Inventory.database;

namespace Inventory.ui
{
	public enum ProductWindowTasks
	{
		ShowDetails = 0,
		Modify = 1,
		AddNewProduct = 2
	}

	public partial class ProductWindow : Window
	{
		private int _currentTask;

		public ProductWindow(int task)
		{
			InitializeComponent();
			CurrentTask = task;
			ConfigureControlsForTask();
		}

		public ProductWindow(int task, string productId)
		{
			InitializeComponent();
			CurrentTask = task;
			AssignProductDataToControls(productId);
			ConfigureControlsForTask();
		}

		private int CurrentTask
		{
			get => _currentTask;
			set
			{
				if (value < 3)
				{
					_currentTask = value;
				}
			}
		}

		private void AssignProductDataToControls(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return;
			}
			
			string queryDataFromProductId = "SELECT * FROM dbo.productos2 WHERE id LIKE ('" + id + "')";
			
			using SqlDatabase sqlDatabase = new SqlDatabase();
			using SqlDataReader registro = sqlDatabase.Read(queryDataFromProductId);

			if (registro.Read())
			{
				var habilitarInventario = registro["inventario"].ToString();
				var habilitarAjusteManual = registro["ajuste_manual"].ToString();
				
				TxtBoxId.Text = registro["id"].ToString();
				TxtBoxIdCode.Text = registro["id"].ToString();
				TxtBoxEnrollment.Text = registro["matricula"].ToString();
				TxtBoxShortDescription.Text = registro["descripcion"].ToString();
				TxtBoxCurrentProductStock.Text = registro["existencia"].ToString();
				TxtBoxMinProductStock.Text = registro["minimo"].ToString();
				TxtBoxMaxProductStock.Text = registro["maximo"].ToString();
				TxtBoxContainer.Text = registro["contenedor"].ToString();
				TxtBoxLocation.Text = registro["ubicacion"].ToString();
				TxtBoxBranchOffice.Text = registro["s"].ToString();
				TxtBoxShelf.Text = registro["e"].ToString();
				TxtBoxLedge.Text = registro["r"].ToString();
				TxtBoxPurchasePrice.Text = registro["preciocomp"].ToString();
				TxtBoxManufacturerPartNumber.Text = registro["parte"].ToString();
				TxtBoxPercentageOfProfit.Text = registro["ganancia"].ToString();
				TxtBoxDiscountRate.Text = registro["descuento"].ToString();
				TxtBoxSalePrice.Text = registro["preciovent"].ToString();
				TxtBoxPriceWithDiscount.Text = registro["preciodesc"].ToString();
				TxtBoxUtility.Text = registro["utilidad"].ToString();
				TxtBoxProfitWithDiscount.Text = registro["utilidaddesc"].ToString();
				TxtBoxFullDescription.Text = registro["descfull"].ToString();
				TxtBoxMemo.Text = registro["memo"].ToString();
				
				CmbBoxStatus.Items.Clear();
				CmbBoxProductType.Items.Clear();
				CmbBoxCategories.Items.Clear();
				CmbBoxManufacturer.Items.Clear();
				CmbBoxUnit.Items.Clear();
				CmbBoxEncapsulationType.Items.Clear();
				CmbBoxMountingTechnology.Items.Clear();
				
				CmbBoxStatus.Items.Add(registro["estado"].ToString());
				CmbBoxProductType.Items.Add(registro["tipo"].ToString());
				CmbBoxUnit.Items.Add(registro["unidad"].ToString());
				CmbBoxManufacturer.Items.Add(registro["proveedor"].ToString());
				CmbBoxCategories.Items.Add(registro["categoria"].ToString());
				CmbBoxMountingTechnology.Items.Add(registro["tecmon"].ToString());
				CmbBoxEncapsulationType.Items.Add(registro["encapsulado"].ToString());
				
				CmbBoxStatus.SelectedIndex = 0;
				CmbBoxProductType.SelectedIndex = 0;
				CmbBoxUnit.SelectedIndex = 0;
				CmbBoxManufacturer.SelectedIndex = 0;
				CmbBoxCategories.SelectedIndex = 0;
				CmbBoxMountingTechnology.SelectedIndex = 0;
				CmbBoxEncapsulationType.SelectedIndex = 0;
				
				if (habilitarInventario.Equals("True"))
				{
					ChkBoxTheProductUsesInventory.IsChecked = true;
					EnableControlsForInventory();
				}

				if (habilitarInventario.Equals("False"))
				{
					ChkBoxTheProductUsesInventory.IsChecked = false;
					DisableControlsForInventory();
				}

				if (habilitarAjusteManual.Equals("True"))
				{
					RadioBtnAutomaticProfit.IsChecked = false;
					EnableControlsForManualProfit();
				}

				if (habilitarAjusteManual.Equals("False"))
				{
					RadioBtnAutomaticProfit.IsChecked = true;
					DisableControlsForAutomaticProfit();
				}
			}
		}

		private void ConfigureControlsForTask()
		{
			if (CurrentTask == (int)ProductWindowTasks.AddNewProduct)
			{
				UnlockEditableControls();
				TxtBlockProductTask.Text = "Nuevo Producto";
				BtnAddModifyAndSave.Content = "Agregar";
				return;
			}

			if (CurrentTask == (int)ProductWindowTasks.Modify)
			{
				UnlockEditableControls();
				TxtBlockProductTask.Text = "Modificar Producto";
				BtnAddModifyAndSave.Content = "Guardar";
				return;
			}

			LockEditableControls();
			TxtBlockProductTask.Text = "Detalles del Producto";
			BtnAddModifyAndSave.Content = "Modificar";
		}

		private void LockEditableControls()
		{
			TxtBlockProductTask.Foreground = new SolidColorBrush(Colors.Crimson);
			
			CmbBoxStatus.IsReadOnly = true;
			CmbBoxStatus.Foreground = new SolidColorBrush(Colors.Crimson);
			TxtBoxEnrollment.IsReadOnly = true;
			TxtBoxEnrollment.Foreground = new SolidColorBrush(Colors.Crimson);
			CmbBoxMountingTechnology.IsReadOnly = true;
			CmbBoxMountingTechnology.Foreground = new SolidColorBrush(Colors.Crimson);
			CmbBoxEncapsulationType.IsReadOnly = true;
			CmbBoxEncapsulationType.Foreground = new SolidColorBrush(Colors.Crimson);
			TxtBoxShortDescription.IsReadOnly = true;
			TxtBoxShortDescription.Foreground = new SolidColorBrush(Colors.Crimson);
			CmbBoxCategories.IsReadOnly = true;
			CmbBoxCategories.Foreground = new SolidColorBrush(Colors.Crimson);
			TxtBoxFullDescription.IsReadOnly = true;
			TxtBoxFullDescription.Foreground = new SolidColorBrush(Colors.Crimson);

			ChkBoxTheProductUsesInventory.IsEnabled = false;
			TxtBoxCurrentProductStock.Foreground = new SolidColorBrush(Colors.Crimson);
			TxtBoxMinProductStock.Foreground = new SolidColorBrush(Colors.Crimson);
			TxtBoxMaxProductStock.Foreground = new SolidColorBrush(Colors.Crimson);
			DisableControlsForInventory();

			TxtBoxContainer.IsReadOnly = true;
			TxtBoxContainer.Foreground = new SolidColorBrush(Colors.Crimson);
			TxtBoxLocation.IsReadOnly = true;
			TxtBoxLocation.Foreground = new SolidColorBrush(Colors.Crimson);
			TxtBoxBranchOffice.IsReadOnly = true;
			TxtBoxBranchOffice.Foreground = new SolidColorBrush(Colors.Crimson);
			TxtBoxShelf.IsReadOnly = true;
			TxtBoxShelf.Foreground = new SolidColorBrush(Colors.Crimson);
			TxtBoxLedge.IsReadOnly = true;
			TxtBoxLedge.Foreground = new SolidColorBrush(Colors.Crimson);

			TxtBoxPurchasePrice.IsReadOnly = true;
			TxtBoxPurchasePrice.Foreground = new SolidColorBrush(Colors.Crimson);
			CmbBoxUnit.IsReadOnly = true;
			CmbBoxUnit.Foreground = new SolidColorBrush(Colors.Crimson);
			CmbBoxProductType.IsReadOnly = true;
			CmbBoxProductType.Foreground = new SolidColorBrush(Colors.Crimson);
			CmbBoxManufacturer.IsReadOnly = true;
			CmbBoxManufacturer.Foreground = new SolidColorBrush(Colors.Crimson);
			TxtBoxManufacturerPartNumber.IsReadOnly = true;
			TxtBoxManufacturerPartNumber.Foreground = new SolidColorBrush(Colors.Crimson);
			
			RadioBtnAutomaticProfit.IsEnabled = false;
			RadioBtnManualProfit.IsEnabled = false;
			TxtBoxPercentageOfProfit.Foreground = new SolidColorBrush(Colors.Crimson);
			TxtBoxSalePrice.Foreground = new SolidColorBrush(Colors.Crimson);
			TxtBoxUtility.Foreground = new SolidColorBrush(Colors.Crimson);
			TxtBoxDiscountRate.Foreground = new SolidColorBrush(Colors.Crimson);
			TxtBoxPriceWithDiscount.Foreground = new SolidColorBrush(Colors.Crimson);
			TxtBoxProfitWithDiscount.Foreground = new SolidColorBrush(Colors.Crimson);
			LockControlsForAutomaticProfit();

			TxtBoxMemo.IsReadOnly = true;
			TxtBoxMemo.Foreground = new SolidColorBrush(Colors.Crimson);
		}
		private void UnlockEditableControls()
		{
			TxtBlockProductTask.Foreground = new SolidColorBrush(Colors.Black);
			
			CmbBoxStatus.IsReadOnly = false;
			CmbBoxStatus.Foreground = new SolidColorBrush(Colors.Black);
			TxtBoxEnrollment.IsReadOnly = false;
			TxtBoxEnrollment.Foreground = new SolidColorBrush(Colors.Black);
			CmbBoxMountingTechnology.IsReadOnly = false;
			CmbBoxMountingTechnology.Foreground = new SolidColorBrush(Colors.Black);
			CmbBoxEncapsulationType.IsReadOnly = false;
			CmbBoxEncapsulationType.Foreground = new SolidColorBrush(Colors.Black);
			TxtBoxShortDescription.IsReadOnly = false;
			TxtBoxShortDescription.Foreground = new SolidColorBrush(Colors.Black);
			CmbBoxCategories.IsReadOnly = false;
			CmbBoxCategories.Foreground = new SolidColorBrush(Colors.Black);
			TxtBoxFullDescription.IsReadOnly = false;
			TxtBoxFullDescription.Foreground = new SolidColorBrush(Colors.Black);

			ChkBoxTheProductUsesInventory.IsEnabled = true;
			TxtBoxCurrentProductStock.Foreground = new SolidColorBrush(Colors.Black);
			TxtBoxMinProductStock.Foreground = new SolidColorBrush(Colors.Black);
			TxtBoxMaxProductStock.Foreground = new SolidColorBrush(Colors.Black);
			EnableControlsForInventory();

			TxtBoxContainer.IsReadOnly = false;
			TxtBoxContainer.Foreground = new SolidColorBrush(Colors.Black);
			TxtBoxLocation.IsReadOnly = false;
			TxtBoxLocation.Foreground = new SolidColorBrush(Colors.Black);
			TxtBoxBranchOffice.IsReadOnly = false;
			TxtBoxBranchOffice.Foreground = new SolidColorBrush(Colors.Black);
			TxtBoxShelf.IsReadOnly = false;
			TxtBoxShelf.Foreground = new SolidColorBrush(Colors.Black);
			TxtBoxLedge.IsReadOnly = false;
			TxtBoxLedge.Foreground = new SolidColorBrush(Colors.Black);
			CmbBoxUnit.IsReadOnly = false;
			CmbBoxUnit.Foreground = new SolidColorBrush(Colors.Black);
			TxtBoxPurchasePrice.IsReadOnly = false;
			TxtBoxPurchasePrice.Foreground = new SolidColorBrush(Colors.Black);
			CmbBoxProductType.IsReadOnly = false;
			CmbBoxProductType.Foreground = new SolidColorBrush(Colors.Black);
			CmbBoxManufacturer.IsReadOnly = false;
			CmbBoxManufacturer.Foreground = new SolidColorBrush(Colors.Black);
			TxtBoxManufacturerPartNumber.IsReadOnly = false;
			TxtBoxManufacturerPartNumber.Foreground = new SolidColorBrush(Colors.Black);
			
			RadioBtnAutomaticProfit.IsEnabled = true;
			RadioBtnManualProfit.IsEnabled = true;
			TxtBoxPercentageOfProfit.Foreground = new SolidColorBrush(Colors.Black);
			TxtBoxSalePrice.Foreground = new SolidColorBrush(Colors.Black);
			TxtBoxUtility.Foreground = new SolidColorBrush(Colors.Black);
			TxtBoxDiscountRate.Foreground = new SolidColorBrush(Colors.Black);
			TxtBoxPriceWithDiscount.Foreground = new SolidColorBrush(Colors.Black);
			TxtBoxProfitWithDiscount.Foreground = new SolidColorBrush(Colors.Black);
			UnlockControlsForManualProfit();

			TxtBoxMemo.IsReadOnly = false;
			TxtBoxMemo.Foreground = new SolidColorBrush(Colors.Black);
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
			if (CurrentTask == (int)ProductWindowTasks.Modify)
			{
				CurrentTask = (int)ProductWindowTasks.ShowDetails;
			}
			else if (CurrentTask == (int)ProductWindowTasks.ShowDetails)
			{
				CurrentTask = (int)ProductWindowTasks.Modify;
			}
			else if (CurrentTask == (int)ProductWindowTasks.AddNewProduct)
			{
				CurrentTask = (int)ProductWindowTasks.ShowDetails;
			}
			
			ConfigureControlsForTask();
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
			
		}
	}
}
