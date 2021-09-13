﻿using System.Windows;
using Inventory.enums;

namespace Inventory.ui
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void BtnOpenTasksWindow_Click(object sender, RoutedEventArgs e)
		{
			TasksWindow tasksWindow = new TasksWindow();
			tasksWindow.Show();
		}

		private void BtnOpenSearchProductWindow_Click(object sender, RoutedEventArgs e)
		{
			SearchProductWindow searchProductWindow = new SearchProductWindow();
			searchProductWindow.Show();
		}

		private void BtnOpenConecctionsWindow_Click(object sender, RoutedEventArgs e)
		{
			ConnectionsWindow connectionsWindow = new ConnectionsWindow();
			connectionsWindow.Show();
		}

		private void BtnOpenSettingWindow_Click(object sender, RoutedEventArgs e)
		{
			SettingsWindow settingsWindow = new SettingsWindow();
			settingsWindow.Show();
		}

		private void BtnOpenLoginWindow_Click(object sender, RoutedEventArgs e)
		{
			LoginWindow loginWindow = new LoginWindow();
			loginWindow.Show();
		}

		private void BtnOpenDetailsWindow_Click(object sender, RoutedEventArgs e)
		{
			//DetailsWindow detailsWindow = new DetailsWindow();
			//detailsWindow.Show();
			ProductWindow productWindow = new ProductWindow((int)ProductWindowTasks.ShowDetails);
			productWindow.Show();
		}

		private void BtnOpenRequestsWindow_Click(object sender, RoutedEventArgs e)
		{
			RequestsWindow requestsWindow = new RequestsWindow();
			requestsWindow.Show();
		}

		private void BtnOpenModifyWindow_Click(object sender, RoutedEventArgs e)
		{
			//ModifyWindow modifyWindow = new ModifyWindow();
			//modifyWindow.Show();
			ProductWindow productWindow = new ProductWindow((int)ProductWindowTasks.Modify);
			productWindow.Show();
		}

		private void BtnOpenAddProductWindow_Click(object sender, RoutedEventArgs e)
		{
			//AddProductWindow addProductWindow = new AddProductWindow();
			//addProductWindow.Show();
			ProductWindow productWindow = new ProductWindow((int)ProductWindowTasks.AddNewProduct);
			productWindow.Show();
		}
	}
}
