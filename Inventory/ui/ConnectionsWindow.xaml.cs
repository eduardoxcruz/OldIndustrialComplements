using System.Data.SqlClient;
using System.Windows;
using Inventory.data;

namespace Inventory.ui
{
	public partial class ConnectionsWindow
	{
		public static readonly ConnectionsWindow Instance = new();
		private ConnectionsWindow()
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
