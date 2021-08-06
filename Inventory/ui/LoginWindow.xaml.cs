using System.Data.SqlClient;
using System.Windows;
using Inventory.database;

namespace Inventory.ui
{
	public partial class LoginWindow : Window
	{

		private SqlDataReader _dataReader;
		private SqlDataReader DataReader
		{
			get => _dataReader;
			set => _dataReader = value;
		}

		public LoginWindow()
		{
			InitializeComponent();
			MainWindow mainWindow = new MainWindow();
			mainWindow.Show();
			AssignUsersData();
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
			while (DataReader.Read())
			{
				CmbBoxUsers.Items.Add(DataReader["nombre"].ToString());
				CmbBoxUsers.SelectedIndex = App.GetItemIndexFromComboBoxItems(CmbBoxUsers, Properties.Settings.Default.User);
			}
		}

		private void GetUsersData()
		{
			string queryDataFromProductId = "SELECT * FROM dbo.usuarios";
			SqlDatabase sqlDatabase = new SqlDatabase();
			DataReader = sqlDatabase.Read(queryDataFromProductId);
		}
	}
}
