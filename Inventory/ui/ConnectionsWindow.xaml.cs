using System;
using System.Data.SqlClient;
using System.Windows;

namespace Inventory.ui
{
	public partial class ConnectionsWindow : Window
	{
		public ConnectionsWindow()
		{
			InitializeComponent();
		}

		private void BtnTestConnection_OnClick(object sender, RoutedEventArgs routedEventArgs)
		{
			string connectionString = "Server=192.168.0.254,1433;Database=Pruebas;Integrated Security=False;User Id=eduardo;Password=Cruz0320;MultipleActiveResultSets=True";
			SqlConnection sqlDatabaseConnection = new SqlConnection(connectionString);
			try
			{
				sqlDatabaseConnection.Open();
				TxtBlockConnectionResult.Text = "Conectado";
			}
			catch (SqlException exception)
			{
				TxtBlockConnectionResult.Text = "Hubo un error: " + exception;
				throw;
			}
		}
	}
}
