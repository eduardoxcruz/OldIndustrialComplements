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
		public virtual DbSet<ProductForBuy> ProductsForBuy { get; set; }
		public virtual DbSet<RecordOfProductMovement> RecordsOfProductMovements { get; set; }
		public virtual DbSet<Product> Products { get; set; }
		public virtual DbSet<ProductRequest> ProductRequests { get; set; }
		public virtual DbSet<Employee> Employees { get; set; }

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

			new ProductForBuyEntityTypeConfiguration().Configure(modelBuilder.Entity<ProductForBuy>());
			new RecordOfProductMovementEntityTypeConfiguration().Configure(
				modelBuilder.Entity<RecordOfProductMovement>());
			new ProductEntityTypeConfiguration().Configure(modelBuilder.Entity<Product>());
			new ProductRequestEntityTypeConfiguration().Configure(modelBuilder.Entity<ProductRequest>());
			new EmployeeEntityTypeConfiguration().Configure(modelBuilder.Entity<Employee>());

			modelBuilder = ConfigureRelationships(modelBuilder);
		}

		private static ModelBuilder ConfigureRelationships(ModelBuilder modelBuilder)
		{
			modelBuilder
				.Entity<ProductForBuy>()
				.HasOne(productForBuy => productForBuy.Product)
				.WithMany(product => product.ProductForBuys)
				.HasForeignKey(productForBuy => productForBuy.ProductId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder
				.Entity<ProductForBuy>()
				.HasOne(productForBuy => productForBuy.Employee)
				.WithMany(employee => employee.ProductForBuys)
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
				.Entity<RecordOfProductMovement>()
				.HasOne(recordOfProductMovement => recordOfProductMovement.Product)
				.WithMany(product => product.RecordOfProductMovements)
				.HasForeignKey(recordOfProductMovement => recordOfProductMovement.ProductId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder
				.Entity<RecordOfProductMovement>()
				.HasOne(recordOfProductMovement => recordOfProductMovement.Employee)
				.WithMany(employee => employee.RecordOfProductMovements)
				.HasForeignKey(recordOfProductMovement => recordOfProductMovement.EmployeeId)
				.OnDelete(DeleteBehavior.SetNull);

			modelBuilder
				.Entity<Product>()
				.Navigation(product => product.ProductForBuys)
				.UsePropertyAccessMode(PropertyAccessMode.Property);

			modelBuilder
				.Entity<Employee>()
				.Navigation(employee => employee.ProductForBuys)
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
				.Navigation(product => product.RecordOfProductMovements)
				.UsePropertyAccessMode(PropertyAccessMode.Property);

			modelBuilder
				.Entity<Employee>()
				.Navigation(employee => employee.RecordOfProductMovements)
				.UsePropertyAccessMode(PropertyAccessMode.Property);

			modelBuilder
				.Entity<ProductForBuy>()
				.HasIndex(productForBuy => productForBuy.ProductId)
				.IsUnique(false);

			modelBuilder
				.Entity<ProductForBuy>()
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
				.Entity<RecordOfProductMovement>()
				.HasIndex(recordOfProductMovement => recordOfProductMovement.ProductId)
				.IsUnique(false);

			modelBuilder
				.Entity<RecordOfProductMovement>()
				.HasIndex(recordOfProductMovement => recordOfProductMovement.EmployeeId)
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
