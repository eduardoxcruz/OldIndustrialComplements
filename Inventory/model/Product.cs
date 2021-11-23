using System;
using System.Linq;
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
		public static Product GetDataFromSqlDatabase(int id)
		{
			Product product;
			try
			{
				using InventoryDbContext inventoryDb = new InventoryDbContext();
				product = inventoryDb.Products.Single(searchProduct => searchProduct.Id == id);
			}
			catch(Exception exception)
			{
				MessageBox.Show("Error al obtener el producto de la base de datos.\n\nDetalles:\n" + exception , "Error");
				product = new Product();
			}

			return product;
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
