using Microsoft.EntityFrameworkCore;

namespace Mux.NavigationProperties
{
	public interface NavigationPropertiesConfiguration
	{
		void Configure(ref ModelBuilder modelBuilder);
	}
}
