using System;
using System.Data.SqlClient;
using System.Windows;
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
		private int _productId = 1;
		private int _currentTask;

		public ProductWindow(int task)
		{
			InitializeComponent();
			CurrentTask = task;
			ConfigureControlsForTask();
		}
		public ProductWindow(int task, int productId)
		{
			InitializeComponent();
			CurrentTask = task;
			ProductId = productId;
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
		private int ProductId
		{
			get => _productId;
			set
			{
				if (value < 1)
				{
					_productId = 1;
					return;
				}

				_productId = value;
			}
		}
		private void AssignProductDataToControls()
		{
			if (string.IsNullOrEmpty(TxtBoxIdCode.Text))
			{
				return;
			}
			
			string queryDataFromProductId = "SELECT * FROM dbo.productos2 WHERE id=@id";
			
			using SqlDatabase sqlDatabase = new SqlDatabase();
			using SqlCommand sqlCommand = new SqlCommand(queryDataFromProductId, sqlDatabase.DatabaseConnection);
			sqlCommand.Parameters.AddWithValue("@id", TxtBoxIdCode.Text);
			using SqlDataReader registro = sqlDatabase.Read(sqlCommand);

			if (registro.Read())
			{
				var habilitarInventario = registro["inventario"].ToString();
				var habilitarAjusteManual = registro["ajuste_manual"].ToString();
				CmbBoxStatus.Items.Add(registro["estado"].ToString());
				TxtBoxEnrollment.Text = registro["matricula"].ToString();
				CmbBoxMountingTechnology.Items.Add(registro["tecmon"].ToString());
				CmbBoxEncapsulationType.Items.Add(registro["encapsulado"].ToString());
				TxtBoxShortDescription.Text = registro["descripcion"].ToString();
				CmbBoxCategories.Items.Add(registro["categoria"].ToString());
				TxtBoxCurrentProductStock.Text = registro["existencia"].ToString();
				TxtBoxMinProductStock.Text = registro["minimo"].ToString();
				TxtBoxMaxProductStock.Text = registro["maximo"].ToString();
				TxtBoxContainer.Text = registro["contenedor"].ToString();
				TxtBoxLocation.Text = registro["ubicacion"].ToString();
				TxtBoxBranchOffice.Text = registro["s"].ToString();
				TxtBoxShelf.Text = registro["e"].ToString();
				TxtBoxLedge.Text = registro["r"].ToString();
				TxtBoxPurchasePrice.Text = registro["preciocomp"].ToString();
				CmbBoxUnit.Items.Add(registro["unidad"].ToString());
				CmbBoxManufacturer.Items.Add(registro["proveedor"].ToString());
				TxtBoxManufacturerPartNumber.Text = registro["parte"].ToString();
				CmbBoxProductType.Items.Add(registro["tipo"].ToString());
				TxtBoxPercentageOfProfit.Text = registro["ganancia"].ToString();
				TxtBoxDiscountRate.Text = registro["descuento"].ToString();
				TxtBoxSalePrice.Text = registro["preciovent"].ToString();
				TxtBoxPriceWithDiscount.Text = registro["preciodesc"].ToString();
				TxtBoxUtility.Text = registro["utilidad"].ToString();
				TxtBoxProfitWithDiscount.Text = registro["utilidaddesc"].ToString();
				TxtBoxFullDescription.Text = registro["descfull"].ToString();
				TxtBoxMemo.Text = registro["memo"].ToString();

				if (habilitarInventario.Equals("True"))
				{
					ChkBoxTheProductUsesInventory.IsChecked = true;
					EnableControlsForInventory();
				}
				if(habilitarInventario.Equals("False"))
				{
					ChkBoxTheProductUsesInventory.IsChecked = false;
					DisableControlsForInventory();
				}
				if (habilitarAjusteManual.Equals("True"))
				{
					RadioBntAutomaticProfit.IsChecked = false;
					EnableControlsForManualProfit();
				}
				if (habilitarAjusteManual.Equals("False"))
				{
					RadioBntAutomaticProfit.IsChecked = true;
					DisableControlsForAutomaticProfit();
				}
			}
		}
		private void ConfigureControlsForTask()
		{
			if (CurrentTask == (int)ProductWindowTasks.AddNewProduct)
			{
				EnableAllGroupBoxes();
				TxtBlockProductTask.Text = "Nuevo Producto";
				BtnAddModifyAndSave.Content = "Agregar";
				return;
			}
			if (CurrentTask == (int)ProductWindowTasks.Modify)
			{
				EnableAllGroupBoxes();
				TxtBlockProductTask.Text = "Modificar Producto";
				BtnAddModifyAndSave.Content = "Guardar";
				return;
			}
			
			DisableAllGroupBoxes();
			TxtBlockProductTask.Text = "Detalles del Producto";
			BtnAddModifyAndSave.Content = "Modificar";
		}
		private void DisableAllGroupBoxes()
		{
			GrpBoxInventory.IsEnabled = false;
			GrpBoxLocation.IsEnabled = false;
			GrpBoxPriceDetails.IsEnabled = false;
			GrpBoxProductDetails.IsEnabled = false;
		}
		private void EnableAllGroupBoxes()
		{
			GrpBoxInventory.IsEnabled = true;
			GrpBoxLocation.IsEnabled = true;
			GrpBoxPriceDetails.IsEnabled = true;
			GrpBoxProductDetails.IsEnabled = true;
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
			AssignProductDataToControls();
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
		private void RadioBntAutomaticProfit_OnChecked(object sender, RoutedEventArgs e)
		{
			RadioBtnManualProfit.IsChecked = false;
			DisableControlsForAutomaticProfit();
		}
		private void RadioBntAutomaticProfit_OnUnchecked(object sender, RoutedEventArgs e)
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
	}
}
