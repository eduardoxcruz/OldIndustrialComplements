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
			Properties.Settings.Default.DatabaseIp = TxtBoxIpAdress.Text;
			Properties.Settings.Default.PhotosPath = TxtxBoxSystemAdress.Text;
			Properties.Settings.Default.Save();
			MessageBox.Show("Datos guardados correctamente.", "Exito", MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			TxtBoxIpAdress.Text = Properties.Settings.Default.DatabaseIp;
			TxtxBoxSystemAdress.Text = Properties.Settings.Default.PhotosPath;
		}
	}
}
