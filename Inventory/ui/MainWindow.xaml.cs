using System;
using System.ComponentModel;
using System.Windows;

namespace Inventory.ui
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		private Version? AppVersion { get; set; }
		public MainWindow()
		{
			InitializeComponent();
			LoadAppVersion();
		}

		private void LoadAppVersion()
		{
			AppVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
			LblAppVersion.Content = $"Version {AppVersion}";
		}

		private void OpenTasksWindow(object sender, RoutedEventArgs e)
		{
			TasksWindow.Instance.BringWindowToFront(null);
		}

		private void OpenSearchProductWindow(object sender, RoutedEventArgs e)
		{
			SearchProductWindow.Instance.BringWindowToFront();
		}

		private void OpenConnectionsWindow(object sender, RoutedEventArgs e)
		{
			ConnectionsWindow.Instance.BringWindowToFront();
		}

		private void OpenSettingsWindow(object sender, RoutedEventArgs e)
		{
			SettingsWindow.Instance.BringWindowToFront();
		}

		private void OpenModifyProductWindow(object sender, RoutedEventArgs e)
		{
			ProductWindow.ModifyProductInstance.BringWindowToFront();
		}

		private void OpenAddProductWindow(object sender, RoutedEventArgs e)
		{
			ProductWindow.AddNewProductInstance.BringWindowToFront();
		}

		protected override void OnClosing(CancelEventArgs cancelEventArgs)
		{
			Application.Current.Shutdown();
		}

		private void OpenProductDetailsWindow(object sender, RoutedEventArgs e)
		{
			ProductWindow.ShowProductDetailsInstance.BringWindowToFront();
		}

		private void OpenRequestsWindow(object sender, RoutedEventArgs e)
		{
			RequestsWindow.Instance.BringWindowToFront();
		}

		private void OpenChangelog(object sender, RoutedEventArgs e)
		{
			string message = $"Cambios en la version: {AppVersion}\n\n" +
			                 "General:\n" +
			                 "*Arreglado bug en donde los threads secundarios quedan corriendo aun despues de cerrar la app.\n\n" +
			                 "Ventana de Solicitudes de Producto: \n" +
			                 "* Se agrego el filtro para ocultar las solicitudes de tipo 'No Surtir'\n\n" +
			                 "Ventana de Tareas: \n" +
			                 "* Se agrego el bloqueo de ejecucion de tareas para productos que no usan inventario.\n\n" +
			                 "Ventana de Buscar Producto: \n" +
			                 "* Se agrego la columna 'Usa Inventario' para ambas tablas de productos.\n" +
			                 "* Se agrego un texto en la parte inferior que mostrara el costo total por todos los " +
			                 "productos agregados a la tabla inferior.\n" +
			                 "* Agregado boton 'Actualizar' para la tabla de todos los productos.";
			MessageBox.Show(message, "Registro de cambios.");
		}
	}
}
