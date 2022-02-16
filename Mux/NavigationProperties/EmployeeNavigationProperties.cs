using Microsoft.EntityFrameworkCore;
using Mux.Model;

namespace Mux.NavigationProperties
{
	public class EmployeeNavigationProperties : NavigationPropertiesConfiguration
	{
		public void Configure(ref ModelBuilder modelBuilder)
		{
			modelBuilder
				.Entity<Employee>()
				.Navigation(employee => employee.ShoppingCart)
				.UsePropertyAccessMode(PropertyAccessMode.Property);

			modelBuilder
				.Entity<Employee>()
				.Navigation(employee => employee.ProductRequests)
				.UsePropertyAccessMode(PropertyAccessMode.Property);
			
			modelBuilder
				.Entity<Employee>()
				.Navigation(employee => employee.ProductChangeLogs)
				.UsePropertyAccessMode(PropertyAccessMode.Property);
		}
	}
}
