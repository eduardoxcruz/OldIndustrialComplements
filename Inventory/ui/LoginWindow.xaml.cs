using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
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
			while (!new InventoryDbContext().Database.CanConnect())
			{
				MessageBox.Show("No hay conexion al servidor.", "Error");
				Thread.Sleep(5000);
			}

			InventoryDbContext.ExecuteDatabaseRequest(() =>
			{
				using InventoryDbContext inventoryDb = new InventoryDbContext();
				CmbBoxEmployees.ItemsSource = inventoryDb.Employees.ToList();
				CmbBoxEmployees.DisplayMemberPath = "FullName";
				CmbBoxEmployees.SelectedItem = inventoryDb.Employees
					.FirstOrDefault(employee => employee == Properties.Settings.Default.User);
			});
		}
		
		private void EnterKeyPressed(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
				TryToLogin(null, null);
		}

		private void TryToLogin(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(TxtBoxPassword.Password))
			{
				ChkBoxRememberData.IsChecked = false;
				MessageBox.Show("El campo de contraseña no puede estar vacio.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
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
			this.Hide();
		}

		private bool CredentialsAreCorrect()
		{
			bool credentialsAreCorrect = true;
			InventoryDbContext.ExecuteDatabaseRequest(() =>
			{
				using InventoryDbContext inventoryDb = new InventoryDbContext();
				Employee employee = inventoryDb.Employees
					.FirstOrDefault(employee => employee == CmbBoxEmployees.SelectedItem);

				if (employee == null || !employee.Password.Equals(TxtBoxPassword.Password))
					credentialsAreCorrect = false;
			});

			return credentialsAreCorrect;
		}

		private void Exit(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		protected override void OnClosing(CancelEventArgs cancelEventArgs)
		{
			Application.Current.Shutdown();
		}
	}
}
