using System.Windows;

namespace Inventory.ui
{
	public partial class RequestsWindow : Window
	{
		public RequestsWindow()
		{
			InitializeComponent();
		}
		private void BtnExit_OnClick(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
