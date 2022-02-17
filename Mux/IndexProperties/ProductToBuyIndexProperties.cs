using Microsoft.EntityFrameworkCore;
using Mux.Model;

namespace Mux.IndexProperties
{
	internal class ProductToBuyIndexProperties : IndexPropertiesConfiguration
	{
		public void Configure(ref ModelBuilder modelBuilder)
		{
			modelBuilder
				.Entity<ProductToBuy>()
				.HasIndex(productForBuy => productForBuy.ProductId)
				.IsUnique(false);

			modelBuilder
				.Entity<ProductToBuy>()
				.HasIndex(productForBuy => productForBuy.EmployeeId)
				.IsUnique(false);
		}
	}
}
