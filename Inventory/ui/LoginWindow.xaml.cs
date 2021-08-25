using System.Data.SqlClient;
using System.Windows;
using Inventory.database;

namespace Inventory.ui
{
	public partial class LoginWindow : Window
	{
		private SqlDatabase _sqlDatabase;

		private SqlDatabase SqlDatabase
		{
			get
			{
				return _sqlDatabase;
			}
			set
			{
				_sqlDatabase = value;
			}
		}

		public LoginWindow()
		{
			InitializeComponent();
			SqlDatabase = new SqlDatabase();
			LoadUsersFromDatabaseToComboBox();
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
				return;
			}
			if (ChkBoxRememberData.IsChecked == true)
			{
				Properties.Settings.Default.User = CmbBoxUsers.Text;
				Properties.Settings.Default.Pass = TxtBoxPassword.Password;
				Properties.Settings.Default.SaveSession = true;
				Properties.Settings.Default.Save();
			}
			if (ChkBoxRememberData.IsChecked == false)
			{
				Properties.Settings.Default.User = "";
				Properties.Settings.Default.Pass = "";
				Properties.Settings.Default.SaveSession = false;
				Properties.Settings.Default.Save();
			}
		}

		private void LoadUsersFromDatabaseToComboBox()
		{
			string queryDataFromProductId = "SELECT * FROM dbo.usuarios";
			using SqlDataReader dataReader = SqlDatabase.Read(queryDataFromProductId);

			while (dataReader.Read())
			{
				CmbBoxUsers.Items.Add(dataReader["nombre"].ToString());
				CmbBoxUsers.SelectedIndex = App.GetItemIndexFromComboBoxItems(CmbBoxUsers, Properties.Settings.Default.User);
			}
		}
	}
}
