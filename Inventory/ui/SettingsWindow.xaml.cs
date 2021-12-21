using System.Windows;

namespace Inventory.ui
{
	public partial class SettingsWindow
	{
		public static readonly SettingsWindow Instance = new();

		private SettingsWindow()
		{
			InitializeComponent();
		}

		private void SaveDatabaseIpAndPhotosPath(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(TxtBoxIpAdress.Text) || string.IsNullOrEmpty(TxtxBoxSystemAdress.Text))
			{
				MessageBox.Show("Ningun campo puede quedar vacio");
				return;
			}

			Properties.Settings.Default.DatabaseIp = TxtBoxIpAdress.Text;
			Properties.Settings.Default.PhotosPath = TxtxBoxSystemAdress.Text;
			Properties.Settings.Default.Save();
			MessageBox.Show("Campos guardados exitosamente");
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			TxtBoxIpAdress.Text = Properties.Settings.Default.DatabaseIp;
			TxtxBoxSystemAdress.Text = Properties.Settings.Default.PhotosPath;
		}
	}
}
