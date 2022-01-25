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
			LblAppVersion.Content = $"Versión {AppVersion}";
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

		private void OpenShoppingCartWindow(object sender, RoutedEventArgs e)
		{
			ShoppingCartWindow.Instance.BringWindowToFront();
		}

		private void OpenChangelog(object sender, RoutedEventArgs e)
		{
			string message = $"Cambios en la versión: {AppVersion}\n\n" +
			                 "General:\n" +
			                 "* Agregado Material Design a toda la aplicación.";
			MessageBox.Show(message, "Registro de cambios.");
		}

		private void OpenProductChangeLogsWindow(object sender, RoutedEventArgs e)
		{
			ProductChangeLogsWindow.Instance.BringWindowToFront();
		}
	}
}
