using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using Inventory.model;
using static System.DateTime;

namespace Inventory.ui
{
	public partial class TasksWindow
	{
		private static List<string> Tasks { get; set; }
		public static readonly TasksWindow Instance = new();
		private Product Product { get; set; }

		private TasksWindow()
		{
			InitializeComponent();
			Product = new Product();
			this.DataContext = Product;
			CmbBoxTask.SelectedIndex = 0;
			CmbBoxIdOrDebugCode.SelectedIndex = 0;
			AssignItemSourceToComboBox();
		}
		private TasksWindow(string task)
		{
			InitializeComponent();
			Product = new Product();
			this.DataContext = Product;
			AssignItemSourceToComboBox();
			CmbBoxTask.SelectedItem = task;
			CmbBoxIdOrDebugCode.SelectedIndex = 0;
		}
		private void AssignItemSourceToComboBox()
		{
			Tasks = new List<string>() {"ENTRADA", "SALIDA", "DEVOLUCION", "AJUSTE", "SOLICITAR", "COMPRAR"};
			CmbBoxTask.ItemsSource = Tasks;
		}
		public void BringWindowToFront(Product product = null)
		{
			RefresthDateTime();
			
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
		private void VerifySearch(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(TxtBoxIdOrDebugCode.Text))
			{
				MessageBox.Show("Ingresa un Id o Codigo Debug de un producto.", "Error");
				return;
			}

			SearchProductById(int.Parse(TxtBoxIdOrDebugCode.Text));
		}
		private void SearchProductById(int id)
		{
			this.DataContext = Product = Product.GetDataFromSqlDatabase(id);
		}
		private void RefresthDateTime()
		{
			TxtBlckDateTime.Text = Now.ToString(CultureInfo.CurrentCulture);
		}
	}
}

