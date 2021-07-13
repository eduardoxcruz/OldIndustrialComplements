using System.Data.SqlClient;
using System.Windows;
using Inventory.database;

namespace Inventory.ui
{
	/// <summary>
	/// Lógica de interacción para DetailsWindow.xaml
	/// </summary>
	public partial class DetailsWindow : Window
	{
		public DetailsWindow()
		{
			InitializeComponent();
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



		private void BtnSearch_Click(object sender, RoutedEventArgs routedEventArgs)
		{
			string query="SELECT * FROM dbo.productos WHERE id=@id";
			SqlConnection conexion = new SqlDatabase().StartSqlClient();
			SqlCommand comando = new SqlCommand(query,conexion);
			comando.Parameters.AddWithValue("@id", TxtBoxId.Text);
			conexion.Open();
			SqlDataReader registro = comando.ExecuteReader();
			if (registro.Read())
			{
				string habilitarInventario = registro["inventario"].ToString();
				string habilitarAjusteManual = registro["ajuste_manual"].ToString();
				TxtBoxEstado.Text = registro["estado"].ToString();
				TxtBoxMatricula.Text = registro["matricula"].ToString();
				TxtBoxMontaje.Text = registro["tecmon"].ToString();
				TxtBoxEncapsulado.Text = registro["encapsulado"].ToString();
				TxtBoxDescripcionCorta.Text = registro["descripcion"].ToString();
				TxtBoxCategoria.Text = registro["categoria"].ToString();

				if (habilitarInventario.Equals("True")){
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
				TxtBoxUnidad.Text = registro["unidad"].ToString();
				TxtBoxFabricante.Text = registro["proveedor"].ToString();
				TxtBoxNumParteDelFabricante.Text = registro["parte"].ToString();
				TxtBoxTipoDeProducto.Text = registro["tipo"].ToString();

				switch (habilitarAjusteManual)
				{
					case "True":
						RbtnAutomatica.IsChecked = false;
						RbtnEntradaManual.IsChecked = true;
						TxtBoxGanancia.IsEnabled = true;
						TxtBoxPrecioVenta.IsEnabled = true;
						TxtBoxUtilidad.IsEnabled = true;
						TxtBoxDescuento.IsEnabled = true;
						TxtBoxPrecioDescuento.IsEnabled = true;
						TxtBoxUtilidadConDesc.IsEnabled = true;
						break;
					case "False":
						RbtnAutomatica.IsChecked = true;
						RbtnEntradaManual.IsChecked = false;
						TxtBoxGanancia.IsEnabled = false;
						TxtBoxPrecioVenta.IsEnabled = false;
						TxtBoxUtilidad.IsEnabled = false;
						TxtBoxDescuento.IsEnabled = false;
						TxtBoxPrecioDescuento.IsEnabled = false;
						TxtBoxUtilidadConDesc.IsEnabled = false;
						break;
				}

				TxtBoxGanancia.Text = registro["ganancia"].ToString();
				TxtBoxDescuento.Text = registro["descuento"].ToString();
				TxtBoxPrecioVenta.Text = registro["preciovent"].ToString();
				TxtBoxPrecioDescuento.Text = registro["preciodesc"].ToString();
				TxtBoxUtilidad.Text = registro["utilidad"].ToString();
				TxtBoxUtilidadConDesc.Text = registro["utilidaddesc"].ToString();
				TxtBoxDescripcionCompleta.Text = registro["descfull"].ToString();
				TxtBoxMemo.Text = registro["memo"].ToString();
			}
		}

		private void RbtnAutomatica_Checked(object sender, RoutedEventArgs e)
		{
			RbtnEntradaManual.IsChecked = false;
			TxtBoxGanancia.IsEnabled = false;
			TxtBoxPrecioVenta.IsEnabled = false;
			TxtBoxUtilidad.IsEnabled = false;
			TxtBoxDescuento.IsEnabled = false;
			TxtBoxPrecioDescuento.IsEnabled = false;
			TxtBoxUtilidadConDesc.IsEnabled = false;
		}

		private void RbtnAutomatica_Unchecked(object sender, RoutedEventArgs e)
		{
			RbtnEntradaManual.IsChecked = true;
			TxtBoxGanancia.IsEnabled = true;
			TxtBoxPrecioVenta.IsEnabled = true;
			TxtBoxUtilidad.IsEnabled = true;
			TxtBoxDescuento.IsEnabled = true;
			TxtBoxPrecioDescuento.IsEnabled = true;
			TxtBoxUtilidadConDesc.IsEnabled = true;
		}
	}
}
