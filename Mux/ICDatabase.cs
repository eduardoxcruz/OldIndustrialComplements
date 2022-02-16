using Microsoft.EntityFrameworkCore;
using Mux.EntityTypes;
using Mux.IndexProperties;
using Mux.Model;
using Mux.NavigationProperties;
using Mux.Relationships;

#nullable disable

namespace Mux
{
	public class ICDatabase : DbContext
	{
		private readonly string _connectionString;
		
		public virtual DbSet<ProductToBuy> ShoppingCart { get; set; }
		public virtual DbSet<ProductChangeLog> ProductChangeLogs { get; set; }
		public virtual DbSet<Product> Products { get; set; }
		public virtual DbSet<ProductRequest> ProductRequests { get; set; }
		public virtual DbSet<Employee> Employees { get; set; }
		public virtual DbSet<Category> Categories { get; set; }

		public ICDatabase(string connectionString = "Server=192.168.0.254;Database=Testing;User Id=sa;Password=Tlacua015;")
		{
			_connectionString = connectionString;
		}
		
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(_connectionString);
			optionsBuilder.EnableDetailedErrors();
			optionsBuilder.EnableSensitiveDataLogging();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

			StartEntityTypesConfiguration(ref modelBuilder);
			StartRelationshipsConfiguration(ref modelBuilder);
			StartNavigationPropertiesConfiguration(ref modelBuilder);
			StartIndexPropertiesConfiguration(ref modelBuilder);
		}

		private static void StartEntityTypesConfiguration(ref ModelBuilder modelBuilder)
		{
			new ProductToBuyEntityType().Configure(modelBuilder.Entity<ProductToBuy>());
			new ProductChangelogEntityType().Configure(modelBuilder.Entity<ProductChangeLog>());
			new ProductEntityType().Configure(modelBuilder.Entity<Product>());
			new ProductRequestEntityType().Configure(modelBuilder.Entity<ProductRequest>());
			new EmployeeEntityType().Configure(modelBuilder.Entity<Employee>());
			new CategoryEntityType().Configure(modelBuilder.Entity<Category>());
		}

		private static void StartRelationshipsConfiguration(ref ModelBuilder modelBuilder)
		{
			new ProductCategoriesRelationships().Configure(ref modelBuilder);
			new ProductChangelogRelationships().Configure(ref modelBuilder);
			new ProductRequestRelationships().Configure(ref modelBuilder);
			new ShoppingCartRelationships().Configure(ref modelBuilder);
		}

		private static void StartNavigationPropertiesConfiguration(ref ModelBuilder modelBuilder)
		{
			new ProductNavigationProperties().Configure(ref modelBuilder);
			new EmployeeNavigationProperties().Configure(ref modelBuilder);
		}

		private static void StartIndexPropertiesConfiguration(ref ModelBuilder modelBuilder)
		{
			new ProductChangelogIndexProperties().Configure(ref modelBuilder);
			new ProductRequestIndexProperties().Configure(ref modelBuilder);
			new ProductToBuyIndexProperties().Configure(ref modelBuilder);
		}
	}
}
