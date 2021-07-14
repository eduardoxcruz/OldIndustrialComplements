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
			Task = task;
			AutoAssignTextToTxtBlockProductTask();
		}

		private void AutoAssignTextToTxtBlockProductTask()
		{
			switch (Task)
			{
				case (int)ProductWindowTasks.Modify:
					TxtBlockProductTask.Text = "Modificar Producto";
					return;
				case (int)ProductWindowTasks.AddNewProduct:
					TxtBlockProductTask.Text = "Nuevo Producto";
					return;
				default:
					TxtBlockProductTask.Text = "Detalles del Producto";
					break;
			}
		}
	}
}
