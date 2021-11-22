using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using Inventory.data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.model
{
	public class Product
	{
		public int Id { get; set; }
		public string? DebugCode { get; set; }
		public string? Status { get; set; }
		public string? Enrollment { get; set; }
		public string? MountingTechnology { get; set; }
		public string? EncapsulationType { get; set; }
		public string? ShortDescription { get; set; }
		public string? Category { get; set; }
		public bool? ProductUseInventory { get; set; }
		public int? CurrentAmount { get; set; }
		public int? MinAmount { get; set; }
		public int? MaxAmount { get; set; }
		public string? Container { get; set; }
		public string? Location { get; set; }
		public string? BranchOffice { get; set; }
		public string? Rack { get; set; }
		public string? Shelf { get; set; }
		public decimal? BuyPrice { get; set; }
		public string? UnitType { get; set; }
		public string? Manufacturer { get; set; }
		public string? PartNumber { get; set; }
		public string? TypeOfStock { get; set; }
		public bool? ManualProfit { get; set; }
		public decimal? PercentageOfProfit { get; set; }
		public decimal? PercentageOfDiscount { get; set; }
		public decimal? SalePriceWithoutDiscount { get; set; }
		public decimal? PriceWithDiscount { get; set; }
		public decimal? ProfitWithoutDiscount { get; set; }
		public decimal? ProfitWithDiscount { get; set; }
		public string? FullDescription { get; set; }
		public string? Memo { get; set; }
		
		public ProductForBuy ProductForBuy { get; set; }
		public ProductRequest ProductRequest { get; set; }
		public RecordOfProductMovement RecordOfProductMovement { get; set; }
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
			this.Id = 0;
			this.DebugCode = "";
			this.Status = "";
			this.Enrollment = "";
			this.MountingTechnology = "";
			this.EncapsulationType = "";
			this.Category = "";
			this.ShortDescription = "";
			this.FullDescription = "";
			this.ProductUseInventory = false;
			this.CurrentAmount = 0;
			this.MinAmount = 1;
			this.MaxAmount = 0;
			this.Container = "";
			this.Location = "";
			this.BranchOffice = "";
			this.Rack = "";
			this.Shelf = "";
			this.BuyPrice = 0.0M;
			this.UnitType = "";
			this.TypeOfStock = "";
			this.Manufacturer = "";
			this.PartNumber = "";
			this.ManualProfit = false;
			this.PercentageOfProfit = 0.0M;
			this.SalePriceWithoutDiscount = 0.0M;
			this.ProfitWithoutDiscount = 0.0M;
			this.PercentageOfDiscount = 0.0M;
			this.PriceWithDiscount = 0.0M;
			this.ProfitWithDiscount = 0.0M;
			this.Memo = "";
		}
		public void GetDataFromSqlDatabase(string id)
		{
			try
			{
				string queryDataFromProductId = "SELECT * FROM " + Sql.ProductsTableName + " WHERE id=@id";

				using SqlDatabase sqlDatabase = new SqlDatabase();
				using SqlDataReader sqlDataReader = sqlDatabase.Read(queryDataFromProductId, new Dictionary<string, string> {{"@id", id}});
				
				if (sqlDataReader.Read())
				{
					this.Id = int.Parse(sqlDataReader["id"].ToString());
					this.DebugCode = sqlDataReader["codigo"].ToString();
					this.Status = sqlDataReader["estado"].ToString();
					this.Enrollment = sqlDataReader["matricula"].ToString();
					this.MountingTechnology = sqlDataReader["tecmon"].ToString();
					this.EncapsulationType = sqlDataReader["encapsulado"].ToString();
					this.ShortDescription = sqlDataReader["descripcion"].ToString();
					this.Category = sqlDataReader["categoria"].ToString();
					this.ProductUseInventory = bool.Parse(sqlDataReader["inventario"].ToString());
					this.CurrentAmount = int.Parse((sqlDataReader["existencia"].ToString()));
					this.MinAmount = int.Parse(sqlDataReader["minimo"].ToString());
					this.MaxAmount = int.Parse((sqlDataReader["maximo"].ToString()));
					this.Container = sqlDataReader["contenedor"].ToString();
					this.Location = sqlDataReader["ubicacion"].ToString();
					this.BranchOffice = sqlDataReader["s"].ToString();
					this.Rack = sqlDataReader["e"].ToString();
					this.Shelf = sqlDataReader["r"].ToString();
					this.BuyPrice = decimal.Parse((sqlDataReader["preciocomp"].ToString()));
					this.UnitType = sqlDataReader["unidad"].ToString();
					this.Manufacturer = sqlDataReader["proveedor"].ToString();
					this.PartNumber = sqlDataReader["parte"].ToString();
					this.TypeOfStock = sqlDataReader["tipo"].ToString();
					this.ManualProfit = bool.Parse(sqlDataReader["ajuste_manual"].ToString());
					this.PercentageOfProfit = decimal.Parse((sqlDataReader["ganancia"].ToString()));
					this.PercentageOfDiscount = decimal.Parse((sqlDataReader["descuento"].ToString()));
					this.SalePriceWithoutDiscount = decimal.Parse((sqlDataReader["preciovent"].ToString()));
					this.PriceWithDiscount = decimal.Parse((sqlDataReader["preciodesc"].ToString()));
					this.ProfitWithoutDiscount = decimal.Parse((sqlDataReader["utilidad"].ToString()));
					this.PriceWithDiscount = decimal.Parse((sqlDataReader["utilidaddesc"].ToString()));
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
	
	public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.HasKey(product => product.Id);
			
			builder
				.Property(product => product.Id)
				.ValueGeneratedNever();

			builder.Property(product => product.ManualProfit);

			builder.Property(product => product.PercentageOfDiscount);
			
			builder.Property(product => product.CurrentAmount);

			builder.Property(product => product.PercentageOfProfit);

			builder.Property(product => product.ProductUseInventory);
			
			builder.Property(product => product.MaxAmount);
			
			builder.Property(product => product.MinAmount);

			builder.Property(product => product.BuyPrice);

			builder.Property(product => product.PriceWithDiscount);

			builder.Property(product => product.SalePriceWithoutDiscount);
			
			builder.Property(product => product.ProfitWithoutDiscount);

			builder.Property(product => product.ProfitWithDiscount);
			
			builder
				.Property(product => product.Category)
				.HasMaxLength(50)
				.IsUnicode(false);

			builder
				.Property(product => product.DebugCode)
				.HasMaxLength(30)
				.IsUnicode(false);

			builder
				.Property(product => product.Container)
				.HasMaxLength(60)
				.IsUnicode(false);

			builder
				.Property(product => product.FullDescription)
				.HasMaxLength(200)
				.IsUnicode(false);

			builder
				.Property(product => product.ShortDescription)
				.HasMaxLength(200)
				.IsUnicode(false);
			
			builder
				.Property(product => product.Rack)
				.HasMaxLength(10)
				.IsUnicode(false);

			builder
				.Property(product => product.EncapsulationType)
				.HasMaxLength(20)
				.IsUnicode(false);

			builder
				.Property(product => product.Status)
				.HasMaxLength(10)
				.IsUnicode(false);
			
			builder
				.Property(product => product.Enrollment)
				.HasMaxLength(60)
				.IsUnicode(false);

			builder
				.Property(product => product.Memo)
				.HasMaxLength(70)
				.IsUnicode(false);
			
			builder
				.Property(product => product.PartNumber)
				.HasMaxLength(30)
				.IsUnicode(false);

			builder
				.Property(product => product.Manufacturer)
				.HasMaxLength(50)
				.IsUnicode(false);

			builder
				.Property(product => product.Shelf)
				.HasMaxLength(10)
				.IsUnicode(false);

			builder
				.Property(product => product.BranchOffice)
				.HasMaxLength(10)
				.IsUnicode(false);

			builder
				.Property(product => product.MountingTechnology)
				.HasMaxLength(16)
				.IsUnicode(false);

			builder
				.Property(product => product.TypeOfStock)
				.HasMaxLength(20)
				.IsUnicode(false);

			builder
				.Property(product => product.Location)
				.HasMaxLength(40)
				.IsUnicode(false);

			builder
				.Property(product => product.UnitType)
				.HasMaxLength(10)
				.IsUnicode(false);
		}
	}
}
