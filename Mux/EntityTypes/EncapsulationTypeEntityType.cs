using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mux.Model;

namespace Mux.EntityTypes
{
	public class EncapsulationTypeEntityType : IEntityTypeConfiguration<EncapsulationType>
	{
		public void Configure(EntityTypeBuilder<EncapsulationType> builder)
		{
			builder.HasKey(encapsulationType => encapsulationType.Id);

			builder.Property(e => e.Id)
				.ValueGeneratedOnAdd();

			builder.Property(e => e.Name)
				.HasMaxLength(30)
				.IsUnicode(false);

			builder.Property(e => e.BodyWidth)
				.HasMaxLength(30)
				.IsUnicode();

			builder.Property(e => e.FullDescription)
				.HasComputedColumnSql("[Name] + ', ' + [BodyWidth]", stored: true);
		}
	}
}
