using Microsoft.EntityFrameworkCore;
using Mux.Model;

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

		public ICDatabase(string connectionString)
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

			new ProductToBuyEntityTypeConfiguration().Configure(modelBuilder.Entity<ProductToBuy>());
			new ProductChangeLogEntityTypeConfiguration().Configure(
				modelBuilder.Entity<ProductChangeLog>());
			new ProductEntityTypeConfiguration().Configure(modelBuilder.Entity<Product>());
			new ProductRequestEntityTypeConfiguration().Configure(modelBuilder.Entity<ProductRequest>());
			new EmployeeEntityTypeConfiguration().Configure(modelBuilder.Entity<Employee>());

			StartRelationshipsConfiguration(ref modelBuilder);
			StartNavigationPropertiesConfiguration(ref modelBuilder);
			StartIndexPropertiesConfiguration(ref modelBuilder);
		}

		private static void StartRelationshipsConfiguration(ref ModelBuilder modelBuilder)
		{
			ConfigureShoppingCartRelationships(ref modelBuilder);
			ConfigureProductRequestRelationships(ref modelBuilder);
			ConfigureProductChangelogRelationships(ref modelBuilder);
		}

		private static void ConfigureShoppingCartRelationships(ref ModelBuilder modelBuilder)
		{
			modelBuilder
				.Entity<ProductToBuy>()
				.HasOne(productForBuy => productForBuy.Product)
				.WithMany(product => product.ShoppingCart)
				.HasForeignKey(productForBuy => productForBuy.ProductId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder
				.Entity<ProductToBuy>()
				.HasOne(productForBuy => productForBuy.Employee)
				.WithMany(employee => employee.ShoppingCart)
				.HasForeignKey(productForBuy => productForBuy.EmployeeId)
				.OnDelete(DeleteBehavior.SetNull);
		}

		private static void ConfigureProductRequestRelationships(ref ModelBuilder modelBuilder)
		{
			modelBuilder
				.Entity<ProductRequest>()
				.HasOne(productRequest => productRequest.Product)
				.WithMany(product => product.ProductRequests)
				.HasForeignKey(productRequest => productRequest.ProductId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder
				.Entity<ProductRequest>()
				.HasOne(productRequest => productRequest.Employee)
				.WithMany(employee => employee.ProductRequests)
				.HasForeignKey(productRequest => productRequest.EmployeeId)
				.OnDelete(DeleteBehavior.SetNull);
		}

		private static void ConfigureProductChangelogRelationships(ref ModelBuilder modelBuilder)
		{
			modelBuilder
				.Entity<ProductChangeLog>()
				.HasOne(recordOfProductMovement => recordOfProductMovement.Product)
				.WithMany(product => product.ProductChangeLogs)
				.HasForeignKey(recordOfProductMovement => recordOfProductMovement.ProductId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder
				.Entity<ProductChangeLog>()
				.HasOne(recordOfProductMovement => recordOfProductMovement.Employee)
				.WithMany(employee => employee.ProductChangeLogs)
				.HasForeignKey(recordOfProductMovement => recordOfProductMovement.EmployeeId)
				.OnDelete(DeleteBehavior.SetNull);
		}

		private static void StartNavigationPropertiesConfiguration(ref ModelBuilder modelBuilder)
		{
			ConfigureProductNavigationProperties(ref modelBuilder);
			ConfigureEmployeeNavigationProperties(ref modelBuilder);
		}

		private static void ConfigureProductNavigationProperties(ref ModelBuilder modelBuilder)
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

		private static void ConfigureEmployeeNavigationProperties(ref ModelBuilder modelBuilder)
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

		private static void StartIndexPropertiesConfiguration(ref ModelBuilder modelBuilder)
		{
			ConfigureProductToBuyIndexPropertie(ref modelBuilder);
			ConfigureProductRequestIndexPropertie(ref modelBuilder);
			ConfigureProductChangelogIndexPropertie(ref modelBuilder);
		}

		private static void ConfigureProductToBuyIndexPropertie(ref ModelBuilder modelBuilder)
		{
			modelBuilder
				.Entity<ProductToBuy>()
				.HasIndex(productForBuy => productForBuy.ProductId)
				.IsUnique(false);

			modelBuilder
				.Entity<ProductToBuy>()
				.HasIndex(productForBuy => productForBuy.EmployeeId)
				.IsUnique(false);
		}

		private static void ConfigureProductRequestIndexPropertie(ref ModelBuilder modelBuilder)
		{
			modelBuilder
				.Entity<ProductRequest>()
				.HasIndex(productRequest => productRequest.ProductId)
				.IsUnique(false);

			modelBuilder
				.Entity<ProductRequest>()
				.HasIndex(productRequest => productRequest.EmployeeId)
				.IsUnique(false);
		}

		private static void ConfigureProductChangelogIndexPropertie(ref ModelBuilder modelBuilder)
		{
			modelBuilder
				.Entity<ProductChangeLog>()
				.HasIndex(recordOfProductMovement => recordOfProductMovement.ProductId)
				.IsUnique(false);

			modelBuilder
				.Entity<ProductChangeLog>()
				.HasIndex(recordOfProductMovement => recordOfProductMovement.EmployeeId)
				.IsUnique(false);
		}
	}
}
