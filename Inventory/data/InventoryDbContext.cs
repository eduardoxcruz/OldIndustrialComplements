using System;
using System.Windows;
using Inventory.model;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Inventory.data
{
	public delegate void DatabaseRequest();

	public class InventoryDbContext : DbContext
	{
		public virtual DbSet<ProductToBuy> ShoppingCart { get; set; }
		public virtual DbSet<ProductChangeLog> ProductChangeLogs { get; set; }
		public virtual DbSet<Product> Products { get; set; }
		public virtual DbSet<ProductRequest> ProductRequests { get; set; }
		public virtual DbSet<Employee> Employees { get; set; }
		public virtual DbSet<ProductChangeCount> ProductChangeCounts { get; set; }

		private static string ServerIp =
			string.IsNullOrEmpty(Properties.Settings.Default.DatabaseIp)
				? "192.168.0.254"
				: Properties.Settings.Default.DatabaseIp;

		private const string ServerPort = "1433";
		private const string DatabaseName = "inventory";
		private const string IntegratedSecurity = "False";
		private const string UserId = "sa";
		private const string Password = "Tlacua015";
		private const string MultipleActiveResultSets = "True";
		private const string ConnectionTimeoutInSeconds = "10";

		private static string ConnectionString = "Server=" + ServerIp + "," + ServerPort +
		                                         ";Database=" + DatabaseName +
		                                         ";Integrated Security=" + IntegratedSecurity +
		                                         ";User Id=" + UserId +
		                                         ";Password=" + Password +
		                                         ";MultipleActiveResultSets=" + MultipleActiveResultSets +
		                                         ";Connection Timeout=" + ConnectionTimeoutInSeconds;

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(ConnectionString);
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
			new ProductChangeCountEntityTypeConfiguration().Configure(modelBuilder.Entity<ProductChangeCount>());

			modelBuilder = ConfigureRelationships(modelBuilder);
		}

		private static ModelBuilder ConfigureRelationships(ModelBuilder modelBuilder)
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

			modelBuilder
				.Entity<ProductChangeCount>()
				.HasOne(productChangeCount => productChangeCount.Product)
				.WithMany(product => product.ProductChangeCounts)
				.HasForeignKey(productChangeCount => productChangeCount.ProductId)
				.OnDelete(DeleteBehavior.SetNull);

			modelBuilder
				.Entity<Product>()
				.Navigation(product => product.ShoppingCart)
				.UsePropertyAccessMode(PropertyAccessMode.Property);

			modelBuilder
				.Entity<Employee>()
				.Navigation(employee => employee.ShoppingCart)
				.UsePropertyAccessMode(PropertyAccessMode.Property);

			modelBuilder
				.Entity<Product>()
				.Navigation(product => product.ProductRequests)
				.UsePropertyAccessMode(PropertyAccessMode.Property);

			modelBuilder
				.Entity<Employee>()
				.Navigation(employee => employee.ProductRequests)
				.UsePropertyAccessMode(PropertyAccessMode.Property);

			modelBuilder
				.Entity<Product>()
				.Navigation(product => product.ProductChangeLogs)
				.UsePropertyAccessMode(PropertyAccessMode.Property);

			modelBuilder
				.Entity<Employee>()
				.Navigation(employee => employee.ProductChangeLogs)
				.UsePropertyAccessMode(PropertyAccessMode.Property);

			modelBuilder
				.Entity<Product>()
				.Navigation(product => product.ProductChangeCounts)
				.UsePropertyAccessMode(PropertyAccessMode.Property);

			modelBuilder
				.Entity<ProductToBuy>()
				.HasIndex(productForBuy => productForBuy.ProductId)
				.IsUnique(false);

			modelBuilder
				.Entity<ProductToBuy>()
				.HasIndex(productForBuy => productForBuy.EmployeeId)
				.IsUnique(false);

			modelBuilder
				.Entity<ProductRequest>()
				.HasIndex(productRequest => productRequest.ProductId)
				.IsUnique(false);

			modelBuilder
				.Entity<ProductRequest>()
				.HasIndex(productRequest => productRequest.EmployeeId)
				.IsUnique(false);

			modelBuilder
				.Entity<ProductChangeLog>()
				.HasIndex(recordOfProductMovement => recordOfProductMovement.ProductId)
				.IsUnique(false);

			modelBuilder
				.Entity<ProductChangeLog>()
				.HasIndex(recordOfProductMovement => recordOfProductMovement.EmployeeId)
				.IsUnique(false);

			modelBuilder
				.Entity<ProductChangeCount>()
				.HasIndex(productChangeLog => productChangeLog.ProductId)
				.IsUnique(false);

			return modelBuilder;
		}

		public static void ExecuteDatabaseRequest(DatabaseRequest request)
		{
			try
			{
				request();
			}
			catch (Exception exception)
			{
				MessageBox.Show("Ha ocurrido un error al procesar su solicitud. Intentelo de nuevo.\nMas info: \n\n"
				                + exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
	}
}
