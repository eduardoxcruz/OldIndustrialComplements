using Microsoft.EntityFrameworkCore;
using Mux.Model;

namespace Mux.NavigationProperties
{
	internal class EncapsulationTypeNavigationProperties : NavigationPropertiesConfiguration
	{
		public void Configure(ref ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<EncapsulationType>()
				.Navigation(encapsulationType => encapsulationType.Products)
				.UsePropertyAccessMode(PropertyAccessMode.Property);
		}
	}
}
