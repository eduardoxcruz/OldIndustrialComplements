using System.Linq;
using System.Windows;
using Inventory.data;
using Inventory.model;

namespace Inventory.ui
{
	public partial class LoginWindow
	{
		public LoginWindow()
		{
			InitializeComponent();
			LoadUsersFromDatabaseToComboBox();
		}
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			ChkBoxRememberData.IsChecked = Properties.Settings.Default.SaveSession;
			
			if (Properties.Settings.Default.SaveSession)
			{
				TxtBoxPassword.Password = Properties.Settings.Default.User.Password ?? "";
			}
		}
		private void LoadUsersFromDatabaseToComboBox()
		{
			using InventoryDbContext inventoryDb = new InventoryDbContext();
			CmbBoxEmployees.ItemsSource = inventoryDb.Employees.ToList();
			CmbBoxEmployees.DisplayMemberPath = "FullName";
			CmbBoxEmployees.SelectedItem =  inventoryDb.Employees
				.FirstOrDefault(employee => employee == Properties.Settings.Default.User);
		}
		private void TryToLogin(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(TxtBoxPassword.Password))
			{
				ChkBoxRememberData.IsChecked = false;
				MessageBox.Show("El campo de contraseña esta vacío");
				return;
			}

			if (ChkBoxRememberData.IsChecked == true)
			{
				Properties.Settings.Default.SaveSession = true;
			}
			else
			{
				Properties.Settings.Default.SaveSession = false;
			}

			Properties.Settings.Default.User = (Employee)CmbBoxEmployees.SelectedItem;
			Properties.Settings.Default.Save();
			
			if (!CredentialsAreCorrect())
			{
				TxtBoxPassword.Password = "";
				ChkBoxRememberData.IsChecked = false;
				MessageBox.Show("Usuario o contraseña incorrectos.");
				return;
			}
			
			new MainWindow().Show();
			this.Close();
		}
		private bool CredentialsAreCorrect()
		{
			using InventoryDbContext inventoryDb = new InventoryDbContext();
			Employee employee = inventoryDb.Employees
				.FirstOrDefault(employee => employee == CmbBoxEmployees.SelectedItem);

			if (employee == null || !employee.Password.Equals(TxtBoxPassword.Password)) 
				return false;

			return true;
		}
		private void Exit(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
		private void RememberDataIsChecked(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(TxtBoxPassword.Password))
			{
				ChkBoxRememberData.IsChecked = false;
				MessageBox.Show("Introduce datos para guardar la información");
			}
		}
	}
}
