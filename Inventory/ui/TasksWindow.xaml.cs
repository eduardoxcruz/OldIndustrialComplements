using System.Windows;
using Inventory.model;

namespace Inventory.ui
{
	public partial class TasksWindow : Window
	{
		private int _currentTask;
		private Product _product;
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
		private Product Product
		{
			get => _product;
			set => _product = value;
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
			Product = product;
		}
		private void BtnExit_OnClick(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}

