using System.Data.SqlClient;
using System.Windows;
using Inventory.database;

namespace Inventory.ui
{
	public enum ProductWindowTasks : int
	{
		ShowDetails = 0,
		Modify = 1,
		AddNewProduct = 2
	}

	public partial class ProductWindow : Window
	{
		private int _task = 0;

		private int Task
		{
			get
			{
				return _task;
			}
			set
			{
				if (value < 3)
				{
					_task = value;
				}
			}
		}

		public ProductWindow(int task)
		{
			InitializeComponent();
			Task = task;
			AutoAssignTextToTxtBlockProductTask();
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
			string query = "SELECT * FROM dbo.productos2 WHERE id=@id";
			SqlConnection conexion = new SqlDatabase().StartSqlClient();
			SqlCommand comando = new SqlCommand(query, conexion);
			comando.Parameters.AddWithValue("@id", TxtBoxId.Text);
			conexion.Open();
			SqlDataReader registro = comando.ExecuteReader();
			if (registro.Read())
			{
				string habilitarInventario = registro["inventario"].ToString();
				string habilitarAjusteManual = registro["ajuste_manual"].ToString();
				ComboBoxEstado.Items.Add(registro["estado"].ToString());
				TxtBoxMatricula.Text = registro["matricula"].ToString();
				ComboBoxTecMontaje.Items.Add(registro["tecmon"].ToString());
				ComboBoxEncapsulado.Items.Add(registro["encapsulado"].ToString());
				TxtBoxDescripcionCorta.Text = registro["descripcion"].ToString();
				ComboBoxCategoria.Items.Add(registro["categoria"].ToString());

				if (habilitarInventario.Equals("True"))
				{
					CheckBoxProductoUsaInventario.IsChecked = true;
					TxtBoxExistencia.IsEnabled = true;
					TxtBoxMinimo.IsEnabled = true;
					TxtBoxMaximo.IsEnabled = true;
				}
				else
				{
					CheckBoxProductoUsaInventario.IsChecked = false;
					TxtBoxExistencia.IsEnabled = false;
					TxtBoxMinimo.IsEnabled = false;
					TxtBoxMaximo.IsEnabled = false;
				}

				TxtBoxExistencia.Text = registro["existencia"].ToString();
				TxtBoxMinimo.Text = registro["minimo"].ToString();
				TxtBoxMaximo.Text = registro["maximo"].ToString();
				TxtBoxContenedor.Text = registro["contenedor"].ToString();
				TxtBoxUbicacion.Text = registro["ubicacion"].ToString();
				TxtBoxS.Text = registro["s"].ToString();
				TxtBoxE.Text = registro["e"].ToString();
				TxtBoxR.Text = registro["r"].ToString();
				TxtBoxPrecioCompra.Text = registro["preciocomp"].ToString();
				ComboBoxUnidad.Items.Add(registro["unidad"].ToString());
				ComboBoxFabricante.Items.Add(registro["proveedor"].ToString());
				TxtBoxNumParteDelFabricante.Text = registro["parte"].ToString();
				ComboBoxTipoDeProducto.Items.Add(registro["tipo"].ToString());

				switch (habilitarAjusteManual)
				{
					case "True":
						RadioButtonAutomatica.IsChecked = false;
						RadioButtonEntradaManual.IsChecked = true;
						TxtBoxGanancia.IsEnabled = true;
						TxtBoxPrecioDeVenta.IsEnabled = true;
						TxtBoxUtilidad.IsEnabled = true;
						TxtBoxDescuento.IsEnabled = true;
						TxtBoxPrecioDescuento.IsEnabled = true;
						TxtBoxUtilidadConDesc.IsEnabled = true;
						break;
					case "False":
						RadioButtonAutomatica.IsChecked = true;
						RadioButtonEntradaManual.IsChecked = false;
						TxtBoxGanancia.IsEnabled = false;
						TxtBoxPrecioDeVenta.IsEnabled = false;
						TxtBoxUtilidad.IsEnabled = false;
						TxtBoxDescuento.IsEnabled = false;
						TxtBoxPrecioDescuento.IsEnabled = false;
						TxtBoxUtilidadConDesc.IsEnabled = false;
						break;
				}

				TxtBoxGanancia.Text = registro["ganancia"].ToString();
				TxtBoxDescuento.Text = registro["descuento"].ToString();
				TxtBoxPrecioDeVenta.Text = registro["preciovent"].ToString();
				TxtBoxPrecioDescuento.Text = registro["preciodesc"].ToString();
				TxtBoxUtilidad.Text = registro["utilidad"].ToString();
				TxtBoxUtilidadConDesc.Text = registro["utilidaddesc"].ToString();
				TxtBoxDescripcionCompleta.Text = registro["descfull"].ToString();
				TxtBoxMemo.Text = registro["memo"].ToString();
			}
		}

		private void RadioButtonAutomatica_Checked(object sender, RoutedEventArgs e)
		{
			RadioButtonEntradaManual.IsChecked = false;
			TxtBoxGanancia.IsEnabled = false;
			TxtBoxPrecioDeVenta.IsEnabled = false;
			TxtBoxUtilidad.IsEnabled = false;
			TxtBoxDescuento.IsEnabled = false;
			TxtBoxPrecioDescuento.IsEnabled = false;
			TxtBoxUtilidadConDesc.IsEnabled = false;
		}

		private void RadioButtonAutomatica_Unchecked(object sender, RoutedEventArgs e)
		{
			RadioButtonEntradaManual.IsChecked = true;
			TxtBoxGanancia.IsEnabled = true;
			TxtBoxPrecioDeVenta.IsEnabled = true;
			TxtBoxUtilidad.IsEnabled = true;
			TxtBoxDescuento.IsEnabled = true;
			TxtBoxPrecioDescuento.IsEnabled = true;
			TxtBoxUtilidadConDesc.IsEnabled = true;
		}

		private void CheckBoxProductoUsaInventario_Checked(object sender, RoutedEventArgs e)
		{
			TxtBoxExistencia.IsEnabled = true;
			TxtBoxMinimo.IsEnabled = true;
			TxtBoxMaximo.IsEnabled = true;
		}

		private void CheckBoxProductoUsaInventario_Unchecked(object sender, RoutedEventArgs e)
		{
			TxtBoxExistencia.IsEnabled = false;
			TxtBoxMinimo.IsEnabled = false;
			TxtBoxMaximo.IsEnabled = false;
		}
	}
}
