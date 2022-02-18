using Microsoft.EntityFrameworkCore;
using Mux.Model;

namespace Mux.Relationships
{
	internal class ProductRelationships : RelationshipsConfiguration
	{
		public void Configure(ref ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product>()
				.HasOne(product => product.EncapsulationType)
				.WithMany(encapsulationType => encapsulationType.Products)
				.HasForeignKey(product => product.EncapsulationTypeId)
				.OnDelete(DeleteBehavior.ClientCascade);
		}
	}
}
