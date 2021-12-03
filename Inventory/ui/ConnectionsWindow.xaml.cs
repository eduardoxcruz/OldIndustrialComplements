using System;
using System.Windows;

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
			try
			{
				TxtBlockConnectionResult.Text = App.CanConnectToDatabase() ? "Conectado" : "No se puede conectar a la BD";
			}
			catch (Exception exception)
			{
				TxtBlockConnectionResult.Text = "Hubo un error al intentar conectar a la BD. Mas detalles: \n" + 
				                                exception.Message;
			}
		}
	}
}
