using System.Collections.Generic;
using System.Data;
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

			if (CredentialsAreCorrect())
			{
				MainWindow mainWindow = new MainWindow();
				mainWindow.Show();
				this.Close();
				return;
			}

			MessageBox.Show("Usuario o contraseña incorrectos.");
			Properties.Settings.Default.User = "";
			Properties.Settings.Default.Pass = "";
			Properties.Settings.Default.SaveSession = false;
			Properties.Settings.Default.Save();
		}

		private void LoadUsersFromDatabaseToComboBox()
		{
			string queryUserNamesFromUsersTable = "SELECT nombre FROM dbo.usuarios";
			using SqlDataReader dataReader = SqlDatabase.Read(queryUserNamesFromUsersTable);

			while (dataReader.Read())
			{
				CmbBoxUsers.Items.Add(dataReader["nombre"].ToString());
				CmbBoxUsers.SelectedIndex = App.GetItemIndexFromComboBoxItems(CmbBoxUsers, Properties.Settings.Default.User);
			}
		}

		private bool CredentialsAreCorrect()
		{
			string queryCredentials = "SELECT * FROM dbo.usuarios WHERE nombre = @nombre AND contraseña = @pass";
			Dictionary<string, string> sqlCommandParams = new Dictionary<string, string>();
			sqlCommandParams.Add("@nombre", CmbBoxUsers.Text);
			sqlCommandParams.Add("@pass", TxtBoxPassword.Password);

			SqlDatabase sqlDatabase = new SqlDatabase();
			DataTable dataTable = sqlDatabase.GetFilledDataTableWithSqlDataAdapter(queryCredentials, sqlCommandParams);

			if (dataTable.Rows.Count == 1)
			{
				return true;
			}

			return false;
		}
		private void BtnExit_OnClick(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
