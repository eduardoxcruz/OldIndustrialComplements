using System;
using System.Data.SqlClient;
using System.Windows;
using Inventory.database;

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
			try
			{
				using SqlDatabase sqlDatabase = new SqlDatabase();
				sqlDatabase.DatabaseConnection.Open();
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
