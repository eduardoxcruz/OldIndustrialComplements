using System;
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
		private void TestConnection(object sender, RoutedEventArgs routedEventArgs)
		{
			using InventoryDbContext inventoryDb = new InventoryDbContext();

			try
			{
				if (inventoryDb.Database.CanConnect())
				{
					TxtBlockConnectionResult.Text = "Conectado";
				}
				else
				{
					TxtBlockConnectionResult.Text = "No se puede conectar a la BD";
				}
			}
			catch (Exception exception)
			{
				TxtBlockConnectionResult.Text = "Hubo un error al intentar conectar a la BD. Mas detalles: \n" + 
				                                exception.Message;
			}
		}
	}
}
