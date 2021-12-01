using Inventory.enums;
using Inventory.model;

namespace Inventory.ui
{
	public partial class TasksWindow
	{
		public static readonly TasksWindow Instance = new(TasksWindowTasks.Entrance);
		private TasksWindowTasks CurrentTask { get; set; }
		private Product Product { get; set; }

		private TasksWindow(TasksWindowTasks task)
		{
			InitializeComponent();
			CurrentTask = task;
			Product = new Product();
			CmbBoxTask.SelectedIndex = (int)CurrentTask;
			CmbBoxIdOrDebugCode.SelectedIndex = 0;
		}
	}
}

