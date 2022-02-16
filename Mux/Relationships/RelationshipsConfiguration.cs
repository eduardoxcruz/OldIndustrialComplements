using Microsoft.EntityFrameworkCore;

namespace Mux.Relationships
{
	internal interface RelationshipsConfiguration
	{
		void Configure(ref ModelBuilder modelBuilder);
	}
}
