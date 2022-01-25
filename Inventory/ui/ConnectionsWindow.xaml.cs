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
					LblConnectionResult.Content = "No es posible conectar a la Base de Datos";
					return;
				}

				LblConnectionResult.Content = "Existe conexión a la Base de Datos";
			});
		}
	}
}
