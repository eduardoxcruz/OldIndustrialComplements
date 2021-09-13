using System.Data.SqlClient;
using System.Windows;
using Inventory.data;

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
