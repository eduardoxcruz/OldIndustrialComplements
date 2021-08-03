using System.Windows;

namespace Inventory.ui
{ 
	public partial class SettingsWindow : Window
	{
		public SettingsWindow()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(TxtBoxIpAdress.Text) || string.IsNullOrEmpty(TxtxBoxSystemAdress.Text))
			{
				MessageBox.Show("Ningun campo puede quedar vacio");
				return;
			}

			Properties.Settings.Default.DatabaseIp = TxtBoxIpAdress.Text;
			Properties.Settings.Default.PhotosPath = TxtxBoxSystemAdress.Text;
			Properties.Settings.Default.Save();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			TxtBoxIpAdress.Text = Properties.Settings.Default.DatabaseIp;
			TxtxBoxSystemAdress.Text = Properties.Settings.Default.PhotosPath;
		}
	}
}
