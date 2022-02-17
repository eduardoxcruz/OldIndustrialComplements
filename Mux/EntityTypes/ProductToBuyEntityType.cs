using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mux.Model;

namespace Mux.EntityTypes
{
	internal class ProductToBuyEntityType : IEntityTypeConfiguration<ProductToBuy>
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
