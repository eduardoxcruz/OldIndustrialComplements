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
		private void AssignItemSourceToComboBox()
		{
			Tasks = new List<string>() {
				"ENTRADA DE PRODUCTO", 
				"SALIDA DE PRODUCTO", 
				"DEVOLUCION DE PRODUCTO", 
				"AJUSTE DE CANTIDAD",
				"COMPRAR MAS PRODUCTO",
				"SOLICITAR PARA VENTA",
				"SOLICITAR PARA TIENDA",
				"SOLICITAR SIN SURTIR",
				"SOLICITAR PARA VERIFICAR"
			};
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
		private void ExecuteTask(object sender, RoutedEventArgs e)
		{
			switch (CmbBoxTask.SelectedItem)
			{
				case "ENTRADA DE PRODUCTO":
					break;
				case "SALIDA DE PRODUCTO":
					break;
				case "DEVOLUCION DE PRODUCTO":
					break;
				case "AJUSTE DE CANTIDAD":
					break;
				case "COMPRAR MAS PRODUCTO":
					break;
				case "SOLICITAR PARA VENTA":
					break;
				case "SOLICITAR PARA TIENDA":
					break;
				case "SOLICITAR SIN SURTIR":
					break;
				case "SOLICITAR PARA VERIFICAR":
					break;
			}
		}
		private void RefresthDateTime()
		{
			TxtBlckDateTime.Text = Now.ToString(CultureInfo.CurrentCulture);
		}
	}
}

