using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#nullable disable

namespace Inventory.model
{
	public class ProductRequest
	{
		public int? Id { get; set; }
		public DateTime? Date { get; set; }
		public int? Amount { get; set; }
		public string? EmployeeName { get; set; }

		public Employee Employee { get; set; }
		public int? EmployeeId { get; set; }
		public Product Product { get; set; }
		public int? ProductId { get; set; }
	}

	public class ProductRequestEntityTypeConfiguration : IEntityTypeConfiguration<ProductRequest>
	{
		public void Configure(EntityTypeBuilder<ProductRequest> builder)
		{
			builder.HasKey(productRequest => productRequest.Id);

			builder
				.Property(productRequest => productRequest.Id)
				.ValueGeneratedNever();

			builder.Property(productRequest => productRequest.Amount);

			builder.Property(productRequest => productRequest.Date);

			builder
				.Property(productRequest => productRequest.EmployeeName)
				.HasMaxLength(35)
				.IsUnicode(false);
		}
	}
}
