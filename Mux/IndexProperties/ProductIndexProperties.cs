using Microsoft.EntityFrameworkCore;
using Mux.Model;

namespace Mux.IndexProperties
{
	public class ProductIndexProperties : IndexPropertiesConfiguration
	{
		public void Configure(ref ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product>()
				.HasIndex(product => product.EncapsulationTypeId)
				.IsUnique(false);
		}
	}
}
