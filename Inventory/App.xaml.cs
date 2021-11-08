using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Inventory
{
	/// <summary>
	///     Interaction logic for App.xaml
	/// </summary>
	public partial class App
	{
		private static bool IsWindowOpen<T>(string windowName = "") where T : Window
		{
			return string.IsNullOrEmpty(windowName)
				? Current.Windows.OfType<T>().Any()
				: Current.Windows.OfType<T>().Any(w => w.GetType().Name.Equals(windowName));
		}

		private static bool IsWindowOpen(string windowName = "")
		{
			return string.IsNullOrEmpty(windowName)
				? Current.Windows.OfType<Window>().Any()
				: Current.Windows.OfType<Window>().Any(w => w.GetType().Name.Equals(windowName));
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
