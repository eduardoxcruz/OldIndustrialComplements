using Microsoft.EntityFrameworkCore;
using Mux.Model;

namespace Mux.Relationships
{
	internal class ProductRequestRelationships : RelationshipsConfiguration
	{
		public void Configure(ref ModelBuilder modelBuilder)
		{
			modelBuilder
				.Entity<ProductRequest>()
				.HasOne(productRequest => productRequest.Product)
				.WithMany(product => product.ProductRequests)
				.HasForeignKey(productRequest => productRequest.ProductId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder
				.Entity<ProductRequest>()
				.HasOne(productRequest => productRequest.Employee)
				.WithMany(employee => employee.ProductRequests)
				.HasForeignKey(productRequest => productRequest.EmployeeId)
				.OnDelete(DeleteBehavior.SetNull);
		}
	}
}
