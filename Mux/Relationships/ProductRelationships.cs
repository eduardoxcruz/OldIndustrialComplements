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
				.WithMany(e => e.Products)
				.HasForeignKey(p => p.EncapsulationTypeId)
				.OnDelete(DeleteBehavior.ClientCascade);
		}
	}
}
