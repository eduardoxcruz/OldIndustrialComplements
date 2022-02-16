using Microsoft.EntityFrameworkCore;
using Mux.Model;

namespace Mux.Relationships
{
	internal class ShoppingCartRelationships : RelationshipsConfiguration
	{
		public void Configure(ref ModelBuilder modelBuilder)
		{
			modelBuilder
				.Entity<ProductToBuy>()
				.HasOne(productForBuy => productForBuy.Product)
				.WithMany(product => product.ShoppingCart)
				.HasForeignKey(productForBuy => productForBuy.ProductId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder
				.Entity<ProductToBuy>()
				.HasOne(productForBuy => productForBuy.Employee)
				.WithMany(employee => employee.ShoppingCart)
				.HasForeignKey(productForBuy => productForBuy.EmployeeId)
				.OnDelete(DeleteBehavior.SetNull);
		}
	}
}
