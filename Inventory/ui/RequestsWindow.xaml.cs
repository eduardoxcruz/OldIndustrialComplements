using System.Windows;

namespace Inventory.ui
{
	public partial class RequestsWindow : Window
	{
		public static readonly RequestsWindow Instance = new();
		private RequestsWindow()
		{
			InitializeComponent();
		}
		private void BtnExit_OnClick(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
