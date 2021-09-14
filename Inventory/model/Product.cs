using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using Inventory.data;

namespace Inventory.model
{
	public class Product
	{
		public int ProductId { get; set; }
		public string DebugCode { get; set; }
		public string State { get; set; }
		public string Enrollment { get; set; }
		public string MountingTechnology { get; set; }
		public string EncapsulationType { get; set; }
		public string Category { get; set; }
		public string ShortDescription { get; set; }
		public string FullDescription { get; set; }
		public bool TheProductUsesInventory { get; set; }
		public string CurrentProductStock { get; set; }
		public string MinProductStock { get; set; }
		public string MaxProductStock { get; set; }
		public string Container { get; set; }
		public string Location { get; set; }
		public string BranchOffice { get; set; }
		public string Rack { get; set; }
		public string Shelf { get; set; }
		public string PurchasePrice { get; set; }
		public string Unit { get; set; }
		public string ProductType { get; set; }
		public string Manufacturer { get; set; }
		public string ManufacturerPartNumber { get; set; }
		public bool ManualProfit { get; set; }
		public string PercentageOfProfit { get; set; }
		public string SalePrice { get; set; }
		public string Utility { get; set; }
		public string DiscountRate { get; set; }
		public string PriceWithDiscount { get; set; }
		public string ProfitWithDiscount { get; set; }
		public string Provider { get; set; }
		public string Memo { get; set; }
		public Product()
		{
			AssignDefaultData();
		}
		public Product(string id)
		{
			GetDataFromSqlDatabase(id);
		}
		private void AssignDefaultData()
		{
			this.ProductId = 0;
			this.DebugCode = "";
			this.State = "";
			this.Enrollment = "";
			this.MountingTechnology = "";
			this.EncapsulationType = "";
			this.Category = "";
			this.ShortDescription = "";
			this.FullDescription = "";
			this.TheProductUsesInventory = false;
			this.CurrentProductStock = "";
			this.MinProductStock = "";
			this.MaxProductStock = "";
			this.Container = "";
			this.Location = "";
			this.BranchOffice = "";
			this.Rack = "";
			this.Shelf = "";
			this.PurchasePrice = "";
			this.Unit = "";
			this.ProductType = "";
			this.Manufacturer = "";
			this.ManufacturerPartNumber = "";
			this.ManualProfit = false;
			this.PercentageOfProfit = "";
			this.SalePrice = "";
			this.Utility = "";
			this.DiscountRate = "";
			this.PriceWithDiscount = "";
			this.ProfitWithDiscount = "";
			this.Provider = "";
			this.Memo = "";
		}
		public void GetDataFromSqlDatabase(string id)
		{
			try
			{
				string queryDataFromProductId = "SELECT * FROM dbo.productos2 WHERE id=@id";

				using SqlDatabase sqlDatabase = new SqlDatabase();
				using SqlDataReader sqlDataReader = sqlDatabase.Read(queryDataFromProductId, new Dictionary<string, string> {{"@id", id}});
				
				if (sqlDataReader.Read())
				{
					this.ProductId = int.Parse(sqlDataReader["id"].ToString());
					this.DebugCode = sqlDataReader["codigo"].ToString();
					this.State = sqlDataReader["estado"].ToString();
					this.Enrollment = sqlDataReader["matricula"].ToString();
					this.MountingTechnology = sqlDataReader["tecmon"].ToString();
					this.EncapsulationType = sqlDataReader["encapsulado"].ToString();
					this.ShortDescription = sqlDataReader["descripcion"].ToString();
					this.Category = sqlDataReader["categoria"].ToString();
					this.TheProductUsesInventory = bool.Parse(sqlDataReader["inventario"].ToString());
					this.CurrentProductStock = sqlDataReader["existencia"].ToString();
					this.MinProductStock = sqlDataReader["minimo"].ToString();
					this.MaxProductStock = sqlDataReader["maximo"].ToString();
					this.Container = sqlDataReader["contenedor"].ToString();
					this.Location = sqlDataReader["ubicacion"].ToString();
					this.BranchOffice = sqlDataReader["s"].ToString();
					this.Rack = sqlDataReader["e"].ToString();
					this.Shelf = sqlDataReader["r"].ToString();
					this.PurchasePrice = sqlDataReader["preciocomp"].ToString();
					this.Unit = sqlDataReader["unidad"].ToString();
					this.Manufacturer = sqlDataReader["proveedor"].ToString();
					this.ManufacturerPartNumber = sqlDataReader["parte"].ToString();
					this.ProductType = sqlDataReader["tipo"].ToString();
					this.ManualProfit = bool.Parse(sqlDataReader["ajuste_manual"].ToString());
					this.PercentageOfProfit = sqlDataReader["ganancia"].ToString();
					this.DiscountRate = sqlDataReader["descuento"].ToString();
					this.SalePrice = sqlDataReader["preciovent"].ToString();
					this.PriceWithDiscount = sqlDataReader["preciodesc"].ToString();
					this.Utility = sqlDataReader["utilidad"].ToString();
					this.PriceWithDiscount = sqlDataReader["utilidaddesc"].ToString();
					this.FullDescription = sqlDataReader["descfull"].ToString();
					this.Memo = sqlDataReader["memo"].ToString();
				}
			}
			catch(Exception exception)
			{
				MessageBox.Show("Error al obtener el producto de la base de datos.\n\nDetalles:\n" + exception , "Error");
				AssignDefaultData();
			}
		}
	}
}
