using System.Windows;
using System.Windows.Controls;
using Inventory.data;

namespace Inventory
{
	/// <summary>
	///     Interaction logic for App.xaml
	/// </summary>
	public partial class App
	{
		public static bool CanConnectToDatabase()
		{
			using InventoryDbContext inventoryDb = new();

			if (!inventoryDb.Database.CanConnect())
			{
				MessageBox.Show("No se puede conectar a la base de datos, vuelva a intentarlo.", "Error");
				return false;
			}

			return true;
		}
		public static int GetItemIndexFromComboBoxItems(ComboBox comboBox,string item)
		{
			if (string.IsNullOrEmpty(item))
			{
				return 0;
			}
			
			for (int index = 0; index < comboBox.Items.Count; index++)
			{
				if (comboBox.Items[index].ToString() == item)
				{
					return index;
				}
			}

			return 0;
		}
	}
}
