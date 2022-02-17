using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mux.Model;

namespace Mux.EntityTypes
{
	internal class ProductRequestEntityType : IEntityTypeConfiguration<ProductRequest>
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

			builder
				.Property(productRequest => productRequest.Type)
				.HasMaxLength(35)
				.IsUnicode(false);

			builder
				.Property(productRequest => productRequest.Status)
				.HasMaxLength(35)
				.IsUnicode(false);
		}
	}
}
