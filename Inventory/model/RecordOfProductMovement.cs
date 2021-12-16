using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.model
{
	public class RecordOfProductMovement
	{
#pragma warning disable 8632
		public int? Id { get; set; }
		public DateTime? Date { get; set; }
		public string? Type { get; set; }
		public int? Amount { get; set; }
		public int? PreviousAmount { get; set; }
		public int? NewAmount { get; set; }
		public decimal? PurchasePrice { get; set; }
		public string? Provider { get; set; }
		public string? ProductFullDescription { get; set; }
		public string? EmployeeName { get; set; }

		public Employee Employee { get; set; }
		public int? EmployeeId { get; set; }
		public Product Product { get; set; }
		public int? ProductId { get; set; }
#pragma warning restore 8632
		
		public RecordOfProductMovement()
		{
			this.Id = 0;
			this.Date = DateTime.Now;
			this.Type = "";
			this.Amount = 0;
			this.PreviousAmount = 0;
			this.NewAmount = 0;
			this.PurchasePrice = 0;
			this.Provider = "";
			this.ProductFullDescription = "";
			this.EmployeeName = "";
		}
	}

	public class RecordOfProductMovementEntityTypeConfiguration : IEntityTypeConfiguration<RecordOfProductMovement>
	{
		public void Configure(EntityTypeBuilder<RecordOfProductMovement> builder)
		{
			builder.HasKey(recordOfProductMovement => recordOfProductMovement.Id);

			builder
				.Property(recordOfProductMovement => recordOfProductMovement.Id)
				.ValueGeneratedNever();

			builder.Property(recordOfProductMovement => recordOfProductMovement.Amount);

			builder.Property(recordOfProductMovement => recordOfProductMovement.PreviousAmount);

			builder.Property(recordOfProductMovement => recordOfProductMovement.NewAmount);

			builder.Property(recordOfProductMovement => recordOfProductMovement.Date);

			builder
				.Property(recordOfProductMovement => recordOfProductMovement.PurchasePrice)
				.HasPrecision(6, 2);

			builder
				.Property(recordOfProductMovement => recordOfProductMovement.EmployeeName)
				.HasMaxLength(35)
				.IsUnicode(false);

			builder
				.Property(recordOfProductMovement => recordOfProductMovement.ProductFullDescription)
				.HasMaxLength(200)
				.IsUnicode(false);

			builder.Property(recordOfProductMovement => recordOfProductMovement.Provider)
				.HasMaxLength(50)
				.IsUnicode(false);

			builder.Property(recordOfProductMovement => recordOfProductMovement.Type)
				.HasMaxLength(10)
				.IsUnicode(false);
		}
	}
}
