using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mux.Model;

namespace Mux.EntityTypes
{
	internal class ProductChangelogEntityType : IEntityTypeConfiguration<ProductChangeLog>
	{
		public void Configure(EntityTypeBuilder<ProductChangeLog> builder)
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
