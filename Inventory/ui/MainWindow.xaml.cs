using System.Windows;
using Inventory.enums;

namespace Inventory.ui
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void BtnOpenTasksWindow_Click(object sender, RoutedEventArgs e)
		{
			new TasksWindow((int) TasksWindowTasks.Entrance).Show();
		}

		private void BtnOpenSearchProductWindow_Click(object sender, RoutedEventArgs e)
		{
			new SearchProductWindow().Show();
		}

		private void BtnOpenConecctionsWindow_Click(object sender, RoutedEventArgs e)
		{
			new ConnectionsWindow().Show();
		}

		private void BtnOpenSettingWindow_Click(object sender, RoutedEventArgs e)
		{
			new SettingsWindow().Show();
		}

		private void BtnOpenModifyWindow_Click(object sender, RoutedEventArgs e)
		{
			new ProductWindow((int)ProductWindowTasks.Modify).Show();
		}

		private void BtnOpenAddProductWindow_Click(object sender, RoutedEventArgs e)
		{
			new ProductWindow((int)ProductWindowTasks.AddNewProduct).Show();
		}
	}
}
