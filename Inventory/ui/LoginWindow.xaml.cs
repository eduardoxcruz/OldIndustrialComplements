using System.Windows;

namespace Inventory.ui
{
	public partial class LoginWindow : Window
	{
		public LoginWindow()
		{
			InitializeComponent();
		}

		private void ChkBoxRememberData_Checked(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(TxtBoxPassword.Password) && string.IsNullOrEmpty(TxtxBoxUser.Text))
			{
				ChkBoxRememberData.IsChecked = false;
				MessageBox.Show("Introduce datos para guardar la información");
			}
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			TxtxBoxUser.Text = Properties.Settings.Default.User;
			TxtBoxPassword.Password = Properties.Settings.Default.Pass;
			ChkBoxRememberData.IsChecked = Properties.Settings.Default.SaveSession;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(TxtBoxPassword.Password) && string.IsNullOrEmpty(TxtxBoxUser.Text))
			{
				ChkBoxRememberData.IsChecked = false;
				MessageBox.Show("Introduce datos para guardar la información");
			}
			else
			{
				if (ChkBoxRememberData.IsChecked == true)
				{
					Properties.Settings.Default.User = TxtxBoxUser.Text;
					Properties.Settings.Default.Pass = TxtBoxPassword.Password;
					Properties.Settings.Default.SaveSession = true;
					Properties.Settings.Default.Save();
				}
				else if (ChkBoxRememberData.IsChecked == false)
				{
					Properties.Settings.Default.User = "";
					Properties.Settings.Default.Pass = "";
					Properties.Settings.Default.SaveSession = false;
					Properties.Settings.Default.Save();
				}
			}
		}
	}
}
