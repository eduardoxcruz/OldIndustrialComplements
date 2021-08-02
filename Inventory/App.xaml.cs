using System.Linq;
using System.Windows;

namespace Inventory
{
	/// <summary>
	///     Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private static bool IsWindowOpen<T>(string windowName = "") where T : Window
		{
			return string.IsNullOrEmpty(windowName)
				? Current.Windows.OfType<T>().Any()
				: Current.Windows.OfType<T>().Any(w => w.GetType().Name.Equals(windowName));
		}

		public static bool IsWindowOpen(string windowName = "")
		{
			return string.IsNullOrEmpty(windowName)
				? Current.Windows.OfType<Window>().Any()
				: Current.Windows.OfType<Window>().Any(w => w.GetType().Name.Equals(windowName));
		}
	}
}
