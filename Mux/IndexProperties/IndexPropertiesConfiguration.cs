using Microsoft.EntityFrameworkCore;

namespace Mux.IndexProperties
{
	internal interface IndexPropertiesConfiguration
	{
		void Configure(ref ModelBuilder modelBuilder);
	}
}
