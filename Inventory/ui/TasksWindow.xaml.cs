using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using Inventory.data;
using Inventory.model;
using Inventory.Properties;
using static System.DateTime;

namespace Inventory.ui
{
	public partial class TasksWindow
	{
		private static List<string> Tasks { get; set; }
		public static readonly TasksWindow Instance = new();
		private static Product Product { get; set; }
		private static Employee Employee { get; set; } = Settings.Default.User;

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
		public void BringWindowToFront(Product product = null, string task = "ENTRADA DE PRODUCTO")
		{
			RefresthDateTime();
			CmbBoxTask.SelectedItem = task;
			
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
					ExecuteRequest("SOLICITAR PARA VENTA");
					break;
				case "SOLICITAR PARA TIENDA":
					ExecuteRequest("SOLICITAR PARA TIENDA");
					break;
				case "SOLICITAR SIN SURTIR":
					ExecuteRequest("SOLICITAR SIN SURTIR");
					break;
				case "SOLICITAR PARA VERIFICAR":
					ExecuteRequest("SOLICITAR PARA VERIFICAR");
					break;
			}
		}
		private void ExecuteRequest(string type)
		{
			if (string.IsNullOrEmpty(TxtBoxInputQuantity.Text) || int.Parse(TxtBoxInputQuantity.Text) <= 0)
			{
				MessageBox.Show("Ingrese una cantidad valida.", "Error");
				return;
			}
			
			if (int.Parse(TxtBoxInputQuantity.Text) > Product.CurrentAmount)
			{
				MessageBox.Show("Cantidad solicitada es mayor a la cantidad disponible.", "Error");
				return;
			}

			using InventoryDbContext inventoryDb = new();

			try
			{
				ProductRequest request = new();
			
				switch (type)
				{
					case "SOLICITAR PARA VENTA":
						request.Type = "PARA VENTA";
						break;
					case "SOLICITAR PARA TIENDA":
						request.Type = "PARA TIENDA";
						break;
					case "SOLICITAR SIN SURTIR":
						request.Type = "NO SURTIR";
						break;
					case "SOLICITAR PARA VERIFICAR":
						request.Type = "PARA VERIFICAR";
						break;
				}
			
				request.Id = inventoryDb.ProductRequests
					.OrderByDescending(productRequest => productRequest.Id)
					.FirstOrDefault().Id + 1;
				request.Status = "NO SURTIDO";
				request.Amount = int.Parse(TxtBoxInputQuantity.Text);
				request.Date = Now;
				request.EmployeeId = Employee.Id;
				request.ProductId = Product.Id;

				inventoryDb.ProductRequests.Add(request);
				inventoryDb.SaveChanges();

				MessageBox.Show("Completado.", "Exito");
			}
			catch (Exception exception)
			{
				MessageBox.Show("Error al intentar enviar la solicitud. \nDetalles:\n\n" + exception.Message, "Error");
			}
				
		}
		private void RefresthDateTime()
		{
			TxtBlckDateTime.Text = Now.ToString(CultureInfo.CurrentCulture);
		}
	}
}

