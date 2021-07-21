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
			SqlDatabase sqlDatabase = new SqlDatabase();
			try
			{
				
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
