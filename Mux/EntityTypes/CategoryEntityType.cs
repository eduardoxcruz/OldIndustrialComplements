using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mux.Model;

namespace Mux.EntityTypes
{
	internal class CategoryEntityType : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.HasKey(category => category.Id);

			builder
				.Property(category => category.Id)
				.ValueGeneratedOnAdd();

			builder
				.Property(category => category.Name)
				.HasMaxLength(50)
				.IsUnicode();
		}
	}
}
