using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.model
{
	public class ProductToBuy
	{
#pragma warning disable 8632
		public int? Id { get; set; }
		public string? Provider { get; set; }
		public DateTime? Date { get; set; }
		public string? Status { get; set; }
		public int? RequestedAmount { get; set; }
		public string? EmployeeName { get; set; }

		public Employee Employee { get; set; }
		public int? EmployeeId { get; set; }
		public Product Product { get; set; }
		public int? ProductId { get; set; }
#pragma warning restore 8632

		public ProductToBuy()
		{
			this.Id = 0;
			this.Provider = "";
			this.Date = DateTime.Now;
			this.Status = "";
			this.RequestedAmount = 0;
			this.EmployeeName = "";
		}
	}

	public class ProductToBuyEntityTypeConfiguration : IEntityTypeConfiguration<ProductToBuy>
	{
		public void Configure(EntityTypeBuilder<ProductToBuy> builder)
		{
			builder.HasKey(productForBuy => productForBuy.Id);

			builder
				.Property(productForBuy => productForBuy.Id)
				.ValueGeneratedNever();

			builder.Property(productForBuy => productForBuy.RequestedAmount);

			builder.Property(productForBuy => productForBuy.Date);

			builder
				.Property(productForBuy => productForBuy.EmployeeName)
				.HasMaxLength(35)
				.IsUnicode(false);

			builder
				.Property(productForBuy => productForBuy.Provider)
				.HasMaxLength(35)
				.IsUnicode(false);

			builder
				.Property(productForBuy => productForBuy.Status)
				.HasMaxLength(10)
				.IsUnicode(false);
		}
	}
}
