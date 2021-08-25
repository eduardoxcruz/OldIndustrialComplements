using System.Windows;

namespace Inventory.ui
{
	public partial class TasksWindow : Window
	{
		public TasksWindow()
		{
			InitializeComponent();
		}
		private void BtnExit_OnClick(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}

