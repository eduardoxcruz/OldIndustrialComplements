using Microsoft.EntityFrameworkCore;
using Mux.Model;

namespace Mux.Relationships
{
	internal class ProductChangelogRelationships : RelationshipsConfiguration
	{
		public void Configure(ref ModelBuilder modelBuilder)
		{
			modelBuilder
				.Entity<ProductChangeLog>()
				.HasOne(recordOfProductMovement => recordOfProductMovement.Product)
				.WithMany(product => product.ProductChangeLogs)
				.HasForeignKey(recordOfProductMovement => recordOfProductMovement.ProductId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder
				.Entity<ProductChangeLog>()
				.HasOne(recordOfProductMovement => recordOfProductMovement.Employee)
				.WithMany(employee => employee.ProductChangeLogs)
				.HasForeignKey(recordOfProductMovement => recordOfProductMovement.EmployeeId)
				.OnDelete(DeleteBehavior.SetNull);
		}
	}
}
