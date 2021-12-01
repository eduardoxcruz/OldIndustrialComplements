using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Inventory.model
{
	public class MyWindow : Window
	{
		private static readonly Regex Regex = new Regex("[^0-9]+");
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
		protected void AllowOnlyNumbers(object sender, TextCompositionEventArgs e)
		{
			e.Handled = TextMatchNumbers(e.Text);
		}
		private static bool TextMatchNumbers(string text)
		{
			return Regex.IsMatch(text);
		}
	}
}
