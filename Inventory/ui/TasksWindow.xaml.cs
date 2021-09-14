using System.Windows;
using Inventory.model;

namespace Inventory.ui
{
	public partial class TasksWindow : Window
	{
		private int _currentTask;
		private int CurrentTask
		{
			get => _currentTask;
			set {
				if (value is >= 0 and <= 6)
				{
					_currentTask = value;
				}
			}
	}
		
		public TasksWindow(int task)
		{
			InitializeComponent();
			CurrentTask = task;
		}
		public TasksWindow(int task, Product product)
		{
			InitializeComponent();
			CurrentTask = task;
		}
		private void BtnExit_OnClick(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}

