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
		private int _task;

		public ProductWindow(int task)
		{
			InitializeComponent();
			Task = task;
			AutoAssignTextToTxtBlockProductTask();
		}

		public ProductWindow(int task, int productId)
		{
			InitializeComponent();
			Task = task;
			ProductId = productId;
			AutoAssignTextToTxtBlockProductTask();
		}

		private int Task
		{
			get => _task;
			set
			{
				if (value < 3)
				{
					_task = value;
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

		private void AutoAssignTextToTxtBlockProductTask()
		{
			switch (Task)
			{
				case (int)ProductWindowTasks.Modify:
					TxtBlockProductTask.Text = "Modificar Producto";
					return;
				case (int)ProductWindowTasks.AddNewProduct:
					TxtBlockProductTask.Text = "Nuevo Producto";
					return;
				default:
					TxtBlockProductTask.Text = "Detalles del Producto";
					break;
			}
		}

		private void BtnSearch_Click(object sender, RoutedEventArgs e)
		{
			var query = "SELECT * FROM dbo.productos2 WHERE id=@id";
			var conexion = new SqlDatabase().StartSqlClient();
			var comando = new SqlCommand(query, conexion);
			comando.Parameters.AddWithValue("@id", TxtBoxId.Text);
			conexion.Open();
			var registro = comando.ExecuteReader();
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

				if (habilitarInventario.Equals("True"))
				{
					ChkBoxTheProductUsesInventory.IsChecked = true;
					TxtBoxCurrentProductStock.IsEnabled = true;
					TxtBoxMinProductStock.IsEnabled = true;
					TxtBoxMaxProductStock.IsEnabled = true;
				}
				else
				{
					ChkBoxTheProductUsesInventory.IsChecked = false;
					TxtBoxCurrentProductStock.IsEnabled = false;
					TxtBoxMinProductStock.IsEnabled = false;
					TxtBoxMaxProductStock.IsEnabled = false;
				}

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

				switch (habilitarAjusteManual)
				{
					case "True":
						RadioBntAutomaticProfit.IsChecked = false;
						RadioBtnManualProfit.IsChecked = true;
						TxtBoxPercentageOfProfit.IsEnabled = true;
						TxtBoxSalePrice.IsEnabled = true;
						TxtBoxUtility.IsEnabled = true;
						TxtBoxDiscountRate.IsEnabled = true;
						TxtBoxPriceWithDiscount.IsEnabled = true;
						TxtBoxProfitWithDiscount.IsEnabled = true;
						break;
					case "False":
						RadioBntAutomaticProfit.IsChecked = true;
						RadioBtnManualProfit.IsChecked = false;
						TxtBoxPercentageOfProfit.IsEnabled = false;
						TxtBoxSalePrice.IsEnabled = false;
						TxtBoxUtility.IsEnabled = false;
						TxtBoxDiscountRate.IsEnabled = false;
						TxtBoxPriceWithDiscount.IsEnabled = false;
						TxtBoxProfitWithDiscount.IsEnabled = false;
						break;
				}

				TxtBoxPercentageOfProfit.Text = registro["ganancia"].ToString();
				TxtBoxDiscountRate.Text = registro["descuento"].ToString();
				TxtBoxSalePrice.Text = registro["preciovent"].ToString();
				TxtBoxPriceWithDiscount.Text = registro["preciodesc"].ToString();
				TxtBoxUtility.Text = registro["utilidad"].ToString();
				TxtBoxProfitWithDiscount.Text = registro["utilidaddesc"].ToString();
				TxtBoxFullDescription.Text = registro["descfull"].ToString();
				TxtBoxMemo.Text = registro["memo"].ToString();
			}
		}

		private void RadioButtonAutomatica_Checked(object sender, RoutedEventArgs e)
		{
			RadioBtnManualProfit.IsChecked = false;
			TxtBoxPercentageOfProfit.IsEnabled = false;
			TxtBoxSalePrice.IsEnabled = false;
			TxtBoxUtility.IsEnabled = false;
			TxtBoxDiscountRate.IsEnabled = false;
			TxtBoxPriceWithDiscount.IsEnabled = false;
			TxtBoxProfitWithDiscount.IsEnabled = false;
		}

		private void RadioButtonAutomatica_Unchecked(object sender, RoutedEventArgs e)
		{
			RadioBtnManualProfit.IsChecked = true;
			TxtBoxPercentageOfProfit.IsEnabled = true;
			TxtBoxSalePrice.IsEnabled = true;
			TxtBoxUtility.IsEnabled = true;
			TxtBoxDiscountRate.IsEnabled = true;
			TxtBoxPriceWithDiscount.IsEnabled = true;
			TxtBoxProfitWithDiscount.IsEnabled = true;
		}

		private void CheckBoxProductoUsaInventario_Checked(object sender, RoutedEventArgs e)
		{
			TxtBoxCurrentProductStock.IsEnabled = true;
			TxtBoxMinProductStock.IsEnabled = true;
			TxtBoxMaxProductStock.IsEnabled = true;
		}

		private void CheckBoxProductoUsaInventario_Unchecked(object sender, RoutedEventArgs e)
		{
			TxtBoxCurrentProductStock.IsEnabled = false;
			TxtBoxMinProductStock.IsEnabled = false;
			TxtBoxMaxProductStock.IsEnabled = false;
		}
	}
}
