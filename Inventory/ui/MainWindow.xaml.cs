using System.Windows;

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
			TasksWindow tasksWindow = new TasksWindow();
			tasksWindow.Show();
		}

		private void BtnOpenSearchProductWindow_Click(object sender, RoutedEventArgs e)
		{
			SearchProductWindow searchProductWindow = new SearchProductWindow();
			searchProductWindow.Show();
		}

		private void BtnOpenConecctionsWindow_Click(object sender, RoutedEventArgs e)
		{
			ConnectionsWindow connectionsWindow = new ConnectionsWindow();
			connectionsWindow.Show();
		}

		private void BtnOpenSettingWindow_Click(object sender, RoutedEventArgs e)
		{
			SettingsWindow settingsWindow = new SettingsWindow();
			settingsWindow.Show();
		}

		private void BtnOpenLoginWindow_Click(object sender, RoutedEventArgs e)
		{
			LoginWindow loginWindow = new LoginWindow();
			loginWindow.Show();
		}
	}
}
