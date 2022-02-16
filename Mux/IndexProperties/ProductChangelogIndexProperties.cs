using Microsoft.EntityFrameworkCore;
using Mux.Model;

namespace Mux.IndexProperties
{
	internal class ProductChangelogIndexProperties : IndexPropertiesConfiguration
	{
		public void Configure(ref ModelBuilder modelBuilder)
		{
			modelBuilder
				.Entity<ProductChangeLog>()
				.HasIndex(recordOfProductMovement => recordOfProductMovement.ProductId)
				.IsUnique(false);

			modelBuilder
				.Entity<ProductChangeLog>()
				.HasIndex(recordOfProductMovement => recordOfProductMovement.EmployeeId)
				.IsUnique(false);
		}
	}
}
