using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mux.Model;

namespace Mux.EntityTypes
{
	internal class ProductEntityType : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.HasKey(product => product.Id);

			builder
				.Property(product => product.Id)
				.ValueGeneratedNever();

			builder.Property(product => product.IsManualProfit);

			builder.Property(product => product.IsUsingInventory);

			builder.Property(product => product.CurrentAmount);

			builder.Property(product => product.MaxAmount);

			builder.Property(product => product.MinAmount);

			builder.Property(product => product.Entrys);

			builder.Property(product => product.Devolutions);

			builder.Property(product => product.Egresses);

			builder.Property(product => product.AmountAdjustments);

			builder.Property(product => product.PriceAdjustments);

			builder
				.Property(product => product.BuyPrice)
				.HasPrecision(6, 2);

			builder
				.Property(product => product.PercentageOfDiscount)
				.HasPrecision(6, 2);

			builder
				.Property(product => product.PercentageOfProfit)
				.HasPrecision(6, 2);

			builder
				.Property(product => product.SalePriceWithDiscount)
				.HasPrecision(6, 2);

			builder
				.Property(product => product.SalePriceWithoutDiscount)
				.HasPrecision(6, 2);

			builder
				.Property(product => product.ProfitWithoutDiscount)
				.HasPrecision(6, 2);

			builder
				.Property(product => product.ProfitWithDiscount)
				.HasPrecision(6, 2);

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
