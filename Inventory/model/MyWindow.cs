using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Inventory.model
{
	public class MyWindow : Window
	{
		private static readonly Regex IntegersRegex = new Regex("[^0-9]+");
		public void BringWindowToFront()
		{
			if (this.Visibility == Visibility.Collapsed)
			{
				this.Show();
			}

			if (this.WindowState == WindowState.Minimized || this.Visibility == Visibility.Hidden)
			{
				this.Visibility = Visibility.Visible;
				this.WindowState = WindowState.Normal;
			}

			this.Activate();
		}
		protected override void OnClosing(CancelEventArgs cancelEventArgs)
		{
			cancelEventArgs.Cancel = true;
			this.Hide();
		}
		protected void CloseWindow(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
		protected void AllowOnlyIntegers(object sender, TextCompositionEventArgs e)
		{
			e.Handled = TextMatchIntegers(e.Text);
		}
		private static bool TextMatchIntegers(string text)
		{
			return IntegersRegex.IsMatch(text);
		}
	}
}
