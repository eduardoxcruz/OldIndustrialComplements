using Microsoft.EntityFrameworkCore;
using Mux.Model;

namespace Mux.NavigationProperties
{
	public class EncapsulationTypeNavigationProperties : NavigationPropertiesConfiguration
	{
		public void Configure(ref ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<EncapsulationType>()
				.Navigation(encapsulationType => encapsulationType.Products)
				.UsePropertyAccessMode(PropertyAccessMode.Property);
		}
	}
}
