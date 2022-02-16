using Microsoft.EntityFrameworkCore;

namespace Mux.IndexProperties
{
	public interface IndexPropertiesConfiguration
	{
		void Configure(ref ModelBuilder modelBuilder);
	}
}
