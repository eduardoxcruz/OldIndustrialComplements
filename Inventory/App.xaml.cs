using System.Windows.Controls;

namespace Inventory
{
	/// <summary>
	///     Interaction logic for App.xaml
	/// </summary>
	public partial class App
	{
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
