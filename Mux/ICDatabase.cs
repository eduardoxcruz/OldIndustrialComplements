using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Mux.IndexProperties;
using Mux.Model;
using Mux.NavigationProperties;

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
			ConfigureProductCategoriesRelationship(ref modelBuilder);
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

		private static void ConfigureProductCategoriesRelationship(ref ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product>()
				.HasMany(product => product.Categories)
				.WithMany(category => category.Products)
				.UsingEntity<Dictionary<string, object>>(
					"ProductCategories",
					entityTypeBuilder => entityTypeBuilder
						.HasOne<Category>()
						.WithMany()
						.HasForeignKey("CategoryId")
						.HasConstraintName("FK_ProductCategories_Categories_CategoryId")
						.OnDelete(DeleteBehavior.ClientSetNull),
					entityTypeBuilder => entityTypeBuilder
						.HasOne<Product>()
						.WithMany()
						.HasForeignKey("ProductId")
						.HasConstraintName("FK_ProductCategories_Products_ProductId")
						.OnDelete(DeleteBehavior.ClientSetNull)
				);
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
