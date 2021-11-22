using System;
using Inventory.model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#nullable disable

namespace MiPrimerEntityFramework.model
{
	public class ProductForBuy
	{
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
	}

	public class ProductForBuyEntityTypeConfiguration : IEntityTypeConfiguration<ProductForBuy>
	{
		public void Configure(EntityTypeBuilder<ProductForBuy> builder)
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
				.IsUnicode(false);;

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
