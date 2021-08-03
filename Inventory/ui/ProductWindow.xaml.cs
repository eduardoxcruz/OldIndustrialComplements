using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
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
				EnableEditableControls();
				TxtBlockProductTask.Text = "Nuevo Producto";
				BtnAddModifyAndSave.Content = "Agregar";
				return;
			}

			if (CurrentTask == (int)ProductWindowTasks.Modify)
			{
				EnableEditableControls();
				TxtBlockProductTask.Text = "Modificar Producto";
				BtnAddModifyAndSave.Content = "Guardar";
				return;
			}

			DisableEditableControls();
			TxtBlockProductTask.Text = "Detalles del Producto";
			BtnAddModifyAndSave.Content = "Modificar";
		}

		private void DisableEditableControls()
		{
			
		}
		private void EnableEditableControls()
		{
			
		}
		private void EnableControlsForManualProfit()
		{
			RadioBtnManualProfit.IsChecked = true;
			TxtBoxPercentageOfProfit.IsEnabled = true;
			TxtBoxSalePrice.IsEnabled = true;
			TxtBoxUtility.IsEnabled = true;
			TxtBoxDiscountRate.IsEnabled = true;
			TxtBoxPriceWithDiscount.IsEnabled = true;
			TxtBoxProfitWithDiscount.IsEnabled = true;
		}
		private void DisableControlsForAutomaticProfit()
		{
			RadioBtnManualProfit.IsChecked = false;
			TxtBoxPercentageOfProfit.IsEnabled = false;
			TxtBoxSalePrice.IsEnabled = false;
			TxtBoxUtility.IsEnabled = false;
			TxtBoxDiscountRate.IsEnabled = false;
			TxtBoxPriceWithDiscount.IsEnabled = false;
			TxtBoxProfitWithDiscount.IsEnabled = false;
		}
		private void EnableControlsForInventory()
		{
			TxtBoxCurrentProductStock.IsEnabled = true;
			TxtBoxMinProductStock.IsEnabled = true;
			TxtBoxMaxProductStock.IsEnabled = true;
		}
		private void DisableControlsForInventory()
		{
			TxtBoxCurrentProductStock.IsEnabled = false;
			TxtBoxMinProductStock.IsEnabled = false;
			TxtBoxMaxProductStock.IsEnabled = false;
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
	}
}
