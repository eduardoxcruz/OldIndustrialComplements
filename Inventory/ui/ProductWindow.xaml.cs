using System.Windows;

namespace Inventory.ui
{
	public enum ProductWindowTasks : int
	{
		ShowDetails = 0,
		Modify = 1,
		AddNewProduct = 2
	}

	public partial class ProductWindow : Window
	{
		private int _task = 0;

		private int Task
		{
			get
			{
				return _task;
			}
			set
			{
				if (value < 3)
				{
					_task = value;
				}
			}
		}

		public ProductWindow(int task)
		{
			InitializeComponent();
		}
	}
}
