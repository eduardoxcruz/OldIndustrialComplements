using Microsoft.EntityFrameworkCore;
using Mux.Model;

namespace Mux.NavigationProperties
{
	internal class ProductNavigationProperties : NavigationPropertiesConfiguration
	{
		public void Configure(ref ModelBuilder modelBuilder)
		{
			modelBuilder
				.Entity<Product>()
				.Navigation(product => product.ShoppingCart)
				.UsePropertyAccessMode(PropertyAccessMode.Property);
			
			modelBuilder
				.Entity<Product>()
				.Navigation(product => product.ProductRequests)
				.UsePropertyAccessMode(PropertyAccessMode.Property);
			
			modelBuilder
				.Entity<Product>()
				.Navigation(product => product.ProductChangeLogs)
				.UsePropertyAccessMode(PropertyAccessMode.Property);
		}
	}
}
