using Microsoft.EntityFrameworkCore;
using Mux.Model;

namespace Mux.IndexProperties
{
	internal class ProductRequestIndexProperties : IndexPropertiesConfiguration
	{
		public void Configure(ref ModelBuilder modelBuilder)
		{
			modelBuilder
				.Entity<ProductRequest>()
				.HasIndex(productRequest => productRequest.ProductId)
				.IsUnique(false);

			modelBuilder
				.Entity<ProductRequest>()
				.HasIndex(productRequest => productRequest.EmployeeId)
				.IsUnique(false);
		}
	}
}
