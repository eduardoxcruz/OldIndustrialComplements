using System.ComponentModel;
using System.Windows;

namespace Inventory.model
{
	public class MyWindow : Window
	{
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
	}
}
