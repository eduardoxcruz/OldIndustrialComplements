namespace Inventory.ui
{
	public partial class RequestsWindow
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
