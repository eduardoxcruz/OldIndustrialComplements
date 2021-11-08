using System.Windows;

namespace Inventory.ui
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void BtnOpenTasksWindow_Click(object sender, RoutedEventArgs e)
		{
			TasksWindow.Instance.BringWindowToFront();
		}

		private void BtnOpenSearchProductWindow_Click(object sender, RoutedEventArgs e)
		{
			SearchProductWindow.Instance.BringWindowToFront();
		}

		private void BtnOpenConecctionsWindow_Click(object sender, RoutedEventArgs e)
		{
			ConnectionsWindow.Instance.BringWindowToFront();
		}

		private void BtnOpenSettingWindow_Click(object sender, RoutedEventArgs e)
		{
			SettingsWindow.Instance.BringWindowToFront();
		}

		private void BtnOpenModifyWindow_Click(object sender, RoutedEventArgs e)
		{
			ProductWindow.ModifyProductInstance.BringWindowToFront();
		}

		private void BtnOpenAddProductWindow_Click(object sender, RoutedEventArgs e)
		{
			ProductWindow.AddNewProductInstance.BringWindowToFront();
		}
	}
}
