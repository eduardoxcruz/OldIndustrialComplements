using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using Inventory.database;

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
			if (string.IsNullOrEmpty(TxtBoxPassword.Password) && string.IsNullOrEmpty(CmbBoxUsers.Text))
			{
				ChkBoxRememberData.IsChecked = false;
				MessageBox.Show("Introduce datos para guardar la información");
			}
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			TxtBoxPassword.Password = Properties.Settings.Default.Pass;
			ChkBoxRememberData.IsChecked = Properties.Settings.Default.SaveSession;
		}

		private void BtnConnect_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(TxtBoxPassword.Password) || string.IsNullOrEmpty(CmbBoxUsers.Text))
			{
				ChkBoxRememberData.IsChecked = false;
				MessageBox.Show("El campo de contraseña esta vacío");
			}
			else
			{
				if (ChkBoxRememberData.IsChecked == true)
				{
					Properties.Settings.Default.User = CmbBoxUsers.Text;
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

		private void AssignUsersData()
		{
			string queryDataFromProductId = "SELECT * FROM dbo.usuarios";
			using SqlDatabase sqlDatabase = new SqlDatabase();
			using SqlDataReader registro = sqlDatabase.Read(queryDataFromProductId);
			if (registro.Read())
			{
				CmbBoxUsers.Items.Add(registro["nombre"].ToString());
				CmbBoxUsers.SelectedIndex = 0;
			}
		}
	}
}
