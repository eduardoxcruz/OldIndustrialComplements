﻿using System.Windows;
using Inventory.enums;
using Inventory.model;

namespace Inventory.ui
{
	public partial class TasksWindow
	{
		public static readonly TasksWindow Instance = new(TasksWindowTasks.Entrance);
		private TasksWindowTasks CurrentTask { get; set; }
		private Product Product { get; set; }

		private TasksWindow()
		{
			InitializeComponent();
		}
		private TasksWindow(TasksWindowTasks task)
		{
			InitializeComponent();
			Product = new Product();
			this.DataContext = Product;
			CurrentTask = task;
			CmbBoxTask.SelectedIndex = (int)CurrentTask;
			CmbBoxIdOrDebugCode.SelectedIndex = 0;
		}
		public void BringWindowToFront(Product product = null)
		{
			if (product != null)
			{
				SearchProductById(product.Id);
			}

			if (this.Visibility == Visibility.Collapsed)
			{
				this.Show();
			}

			if (this.WindowState == WindowState.Minimized || this.Visibility == Visibility.Hidden)
			{
				this.Visibility = Visibility.Visible;
				this.WindowState = WindowState.Normal;
			}

			this.Activate();
		}
		private void SearchProductById(int id)
		{
			this.DataContext = Product = Product.GetDataFromSqlDatabase(id);
		}
	}
}

