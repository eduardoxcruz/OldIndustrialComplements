using Inventory.model;

namespace Inventory.ui
{
	public partial class RequestsWindow
	{
		public static readonly RequestsWindow Instance = new();
		private static ProductRequest productRequests { get; set; }
		private RequestsWindow()
		{
			InitializeComponent();
		}
	}
}
