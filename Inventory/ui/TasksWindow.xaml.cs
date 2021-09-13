using System.Windows;

namespace Inventory.ui
{
	public partial class TasksWindow : Window
	{
		private int _currentTask;
		private int CurrentTask
		{
			get => _currentTask;
			set {
				if (value is >= 0 or <= 6)
				{
					_currentTask = value;
				}
			}
	}
		
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

