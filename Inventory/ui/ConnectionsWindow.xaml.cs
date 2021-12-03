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
			InventoryDbContext.ExecuteDatabaseRequest(() =>
			{
				using InventoryDbContext inventoryDb = new();

				if (!inventoryDb.Database.CanConnect())
				{
					TxtBlockConnectionResult.Text = "No se puede conectar a la BD";
					return;
				}

				TxtBlockConnectionResult.Text = "Conectado";
			});
		}
	}
}
