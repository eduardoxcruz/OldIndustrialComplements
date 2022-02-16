using Microsoft.EntityFrameworkCore;

namespace Mux.NavigationProperties
{
	internal interface NavigationPropertiesConfiguration
	{
		void Configure(ref ModelBuilder modelBuilder);
	}
}
