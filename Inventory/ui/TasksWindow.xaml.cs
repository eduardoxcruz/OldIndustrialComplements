﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using Inventory.data;
using Inventory.model;
using Inventory.Properties;
using Microsoft.EntityFrameworkCore;
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
			if (Product == null || Product.Id == 0)
			{
				MessageBox.Show("Seleccione un producto valido.", "Error");
				return;
			}
			
			switch (CmbBoxTask.SelectedItem)
			{
				case "ENTRADA DE PRODUCTO":
					ExecuteProductAmountChange("ENTRADA DE PRODUCTO");
					break;
				case "SALIDA DE PRODUCTO":
					ExecuteProductAmountChange("SALIDA DE PRODUCTO");
					break;
				case "DEVOLUCION DE PRODUCTO":
					ExecuteProductAmountChange("DEVOLUCION DE PRODUCTO");
					break;
				case "AJUSTE DE CANTIDAD":
					ExecuteProductAmountChange("AJUSTE DE CANTIDAD");
					break;
				case "COMPRAR MAS PRODUCTO":
					break;
				case "SOLICITAR PARA VENTA":
					ExecuteProductRequestForWarehouse("SOLICITAR PARA VENTA");
					break;
				case "SOLICITAR PARA TIENDA":
					ExecuteProductRequestForWarehouse("SOLICITAR PARA TIENDA");
					break;
				case "SOLICITAR SIN SURTIR":
					ExecuteProductRequestForWarehouse("SOLICITAR SIN SURTIR");
					break;
				case "SOLICITAR PARA VERIFICAR":
					ExecuteProductRequestForWarehouse("SOLICITAR PARA VERIFICAR");
					break;
			}
			
			RefresthDateTime();
		}
		private void ExecuteProductAmountChange(string typeOfChange)
		{
			if (!IsProductAmountChangeValid(typeOfChange)) return;
			
			try
			{
				RecordOfProductMovement recordOfProductMovement = new();
				int totalPieces = 0;
				
				switch (typeOfChange)
				{
					case "ENTRADA DE PRODUCTO":
						totalPieces = (Product.CurrentAmount ?? default(int)) + int.Parse(TxtBoxInputQuantity.Text);
						recordOfProductMovement = AddPurchasePriceAndProvider(recordOfProductMovement, totalPieces);
						break;
					case "SALIDA DE PRODUCTO":
						totalPieces = (Product.CurrentAmount ?? default(int)) - int.Parse(TxtBoxInputQuantity.Text);
						recordOfProductMovement.Type = "SALIDA";
						break;
					case "DEVOLUCION DE PRODUCTO":
						totalPieces = (Product.CurrentAmount ?? default(int)) + int.Parse(TxtBoxInputQuantity.Text);
						recordOfProductMovement.Type = "DEVOLUCION";
						break;
					case "AJUSTE DE CANTIDAD":
						totalPieces = int.Parse(TxtBoxInputQuantity.Text);
						recordOfProductMovement.Type = "AJUSTE";
						break;
				}
				
				ConfigureRecordForProductAmountChange(recordOfProductMovement, totalPieces);
			}
			catch (Exception exception)
			{
				MessageBox.Show("Error al intentar enviar la solicitud. \nDetalles:\n\n" + exception.Message, "Error");
			}
		}
		private bool IsProductAmountChangeValid(string typeOfChange)
		{
			if (string.IsNullOrEmpty(TxtBoxInputQuantity.Text))
			{
				MessageBox.Show("Ingrese una cantidad valida.", "Error");
				return false;
			}

			switch (typeOfChange)
			{
				case "ENTRADA DE PRODUCTO":
					if (QuantityIsZero()) return false;
					
					if (string.IsNullOrEmpty(TxtBoxInputPrice.Text) || decimal.Parse(TxtBoxInputPrice.Text) <= 0)
					{
						MessageBox.Show("Ingrese un precio valido mayor a 0.", "Error");
						return false;
					}

					if (string.IsNullOrEmpty(CmbBoxProvider.Text))
					{
						MessageBox.Show("Ingrese proveedor.", "Error");
						return false;
					}
					
					break;
				case "SALIDA DE PRODUCTO":
					if (QuantityIsZero()) return false;

					break; 
				case "DEVOLUCION DE PRODUCTO":
					if (QuantityIsZero()) return false;
					
					break;
			}
			
			return true;
		}
		private RecordOfProductMovement AddPurchasePriceAndProvider(RecordOfProductMovement recordOfProductMovement, int totalPieces)
		{
			recordOfProductMovement.Type = "ENTRADA";
			recordOfProductMovement.PurchasePrice = GetNewBuyPrice(totalPieces);
			recordOfProductMovement.Provider = CmbBoxProvider.Text;

			return recordOfProductMovement;
		}
		private decimal GetNewBuyPrice(int totalPieces)
		{
			if (decimal.Parse(TxtBoxInputPrice.Text) >= Product.BuyPrice)
			{
				return decimal.Parse(TxtBoxInputPrice.Text);
			}
			
			return (
				       (
					       (Product.CurrentAmount ?? default(int)) * (Product.BuyPrice ?? default(decimal)) 
					       + 
					       (int.Parse(TxtBoxInputQuantity.Text)) * decimal.Parse(TxtBoxInputPrice.Text))
			       )
			       /
			       totalPieces;
		}
		private void ConfigureRecordForProductAmountChange(RecordOfProductMovement recordOfProductMovement, int totalPieces)
		{
			RecordOfProductMovement lastRecord = null;
			
			InventoryDbContext.ExecuteDatabaseRequest(() =>
			{
				using InventoryDbContext inventoryDb = new();
				
				 lastRecord = inventoryDb.RecordsOfProductMovements
					.OrderByDescending(record => record.Id)
					.FirstOrDefault();
			});
			
			
			string message = "¿Desea confirmar el siguiente movimiento?:\n\n";

			if ((recordOfProductMovement.PurchasePrice ?? 0.0m) != 0.0m) 
				message = message + "Nuevo precio de compra: " + recordOfProductMovement.PurchasePrice + "\n";
			
			recordOfProductMovement.Id = lastRecord == null ? 1 : lastRecord.Id + 1;
			recordOfProductMovement.Date = Now;
			recordOfProductMovement.Amount = int.Parse(TxtBoxInputQuantity.Text);
			recordOfProductMovement.PreviousAmount = Product.CurrentAmount;
			recordOfProductMovement.EmployeeId = Employee.Id;
			recordOfProductMovement.ProductId = Product.Id;
			recordOfProductMovement.NewAmount = totalPieces;

			message = message + "Cantidad de pzs anterior: " + Product.CurrentAmount + "\n" +
			          "Cantidad de pzs nueva: " + totalPieces;

			SaveProductAmountChangeInDatabase(recordOfProductMovement, message);
		}
		private void SaveProductAmountChangeInDatabase(RecordOfProductMovement recordOfProductMovement, string confirmationMessage)
		{
			if (MessageBox.Show(confirmationMessage, "Confirmacion", MessageBoxButton.OKCancel) != MessageBoxResult.OK)
			{
				return;
			}

			InventoryDbContext.ExecuteDatabaseRequest(() =>
			{
				using InventoryDbContext inventoryDb = new();
				
				if ((recordOfProductMovement.PurchasePrice ?? 0.0m) != 0.0m) 
					Product.BuyPrice = recordOfProductMovement.PurchasePrice;
			
				Product.CurrentAmount = recordOfProductMovement.NewAmount;
				inventoryDb.Entry(Product).State = EntityState.Modified;
				inventoryDb.RecordsOfProductMovements.Add(recordOfProductMovement);
				inventoryDb.SaveChanges();
					
				MessageBox.Show("Completado.", "Exito");
				CleanControls();
			});
		}
		private void ExecuteProductRequestForWarehouse(string type)
		{
			if (!IsProductRequestForWarehouseValid()) return;
			
			try
			{
				SaveProductRequestToWarehouseInDatabase(type);
			}
			catch (Exception exception)
			{
				MessageBox.Show("Error al intentar enviar la solicitud. \nDetalles:\n\n" + exception.Message, "Error");
			}
		}
		private bool IsProductRequestForWarehouseValid()
		{
			if (string.IsNullOrEmpty(TxtBoxInputQuantity.Text) || QuantityIsZero())
			{
				return false;
			}
			
			if (int.Parse(TxtBoxInputQuantity.Text) > Product.CurrentAmount)
			{
				MessageBox.Show("Cantidad solicitada es mayor a la cantidad disponible.", "Error");
				return false;
			}

			return true;
		}
		private void SaveProductRequestToWarehouseInDatabase(string type)
		{
			using InventoryDbContext inventoryDb = new();
			ProductRequest newRequest = new();
			ProductRequest lastRequest = inventoryDb.ProductRequests
				.OrderByDescending(productRequest => productRequest.Id)
				.FirstOrDefault();
			
			switch (type)
			{
				case "SOLICITAR PARA VENTA":
					newRequest.Type = "PARA VENTA";
					break;
				case "SOLICITAR PARA TIENDA":
					newRequest.Type = "PARA TIENDA";
					break;
				case "SOLICITAR SIN SURTIR":
					newRequest.Type = "NO SURTIR";
					break;
				case "SOLICITAR PARA VERIFICAR":
					newRequest.Type = "PARA VERIFICAR";
					break;
			}
			
			newRequest.Id = lastRequest == null ? 1 : lastRequest.Id + 1;
			newRequest.Status = "NO SURTIDO";
			newRequest.Amount = int.Parse(TxtBoxInputQuantity.Text);
			newRequest.Date = Now;
			newRequest.EmployeeId = Employee.Id;
			newRequest.ProductId = Product.Id;

			inventoryDb.ProductRequests.Add(newRequest);
			inventoryDb.SaveChanges();

			MessageBox.Show("Completado.", "Exito");
			CleanControls();
		}
		private bool QuantityIsZero()
		{
			if (int.Parse(TxtBoxInputQuantity.Text) != 0)
			{
				return false;
			}

			MessageBox.Show("Ingrese una cantidad valida mayor a 0.", "Error");
			return true;
		}
		private void RefresthDateTime()
		{
			TxtBlckDateTime.Text = Now.ToString(CultureInfo.CurrentCulture);
		}
		private void CleanControls()
		{
			TxtBoxInputPrice.Text = "";
			TxtBoxInputQuantity.Text = "";
			CmbBoxProvider.Text = "";
		}
	}
}

