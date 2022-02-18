using Microsoft.EntityFrameworkCore;
using Mux.Model;

namespace Mux.IndexProperties
{
	internal class ProductIndexProperties : IndexPropertiesConfiguration
	{
		public void Configure(ref ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product>()
				.HasIndex(product => product.EncapsulationTypeId)
				.IsUnique(false);
		}
	}
}
