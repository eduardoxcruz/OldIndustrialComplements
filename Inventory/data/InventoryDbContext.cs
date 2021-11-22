using Inventory.model;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Inventory.data
{
    public partial class InventoryDbContext : DbContext
    {
        public virtual DbSet<ProductForBuy> ProductsForBuy { get; set; }
        public virtual DbSet<RecordOfProductMovement> RecordsOfProductMovements { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductRequest> ProductRequests { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        
        private const string ServerIp = "192.168.0.254";
        private const string ServerPort = "1433";
        private const string DatabaseName = "inventory";
        private const string IntegratedSecurity = "False";
        private const string UserId = "sa";
        private const string Password = "Tlacua015";
        private const string MultipleActiveResultSets = "True";
        private const string ConnectionTimeoutInSeconds = "10";
        private const string ConnectionString  = "Server=" + ServerIp + "," + ServerPort +
                                                 ";Database=" + DatabaseName +
                                                 ";Integrated Security=" + IntegratedSecurity +
                                                 ";User Id=" + UserId +
                                                 ";Password=" + Password +
                                                 ";MultipleActiveResultSets=" + MultipleActiveResultSets +
                                                 ";Connection Timeout=" + ConnectionTimeoutInSeconds;
        
        public InventoryDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");
            
            new ProductForBuyEntityTypeConfiguration().Configure(modelBuilder.Entity<ProductForBuy>());
            new RecordOfProductMovementEntityTypeConfiguration().Configure(modelBuilder.Entity<RecordOfProductMovement>());
            new ProductEntityTypeConfiguration().Configure(modelBuilder.Entity<Product>());
            new ProductRequestEntityTypeConfiguration().Configure(modelBuilder.Entity<ProductRequest>());
            new EmployeeEntityTypeConfiguration().Configure(modelBuilder.Entity<Employee>());

            modelBuilder = ConfigureRelationships(modelBuilder);
        }
        private static ModelBuilder ConfigureRelationships(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Product>()
                .HasOne(product => product.ProductForBuy)
                .WithOne(productForBuy => productForBuy.Product)
                .HasForeignKey<ProductForBuy>(productForBuy => productForBuy.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<Employee>()
                .HasOne(employee => employee.ProductForBuy)
                .WithOne(productForBuy => productForBuy.Employee)
                .HasForeignKey<ProductForBuy>(productForBuy => productForBuy.EmployeeId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder
                .Entity<Product>()
                .HasOne(product => product.ProductRequest)
                .WithOne(productRequest => productRequest.Product)
                .HasForeignKey<ProductRequest>(productRequest => productRequest.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<Employee>()
                .HasOne(employee => employee.ProductRequest)
                .WithOne(productRequest => productRequest.Employee)
                .HasForeignKey<ProductRequest>(productRequest => productRequest.EmployeeId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder
                .Entity<Product>()
                .HasOne(product => product.RecordOfProductMovement)
                .WithOne(recordOfProductMovement => recordOfProductMovement.Product)
                .HasForeignKey<RecordOfProductMovement>(recordOfProductMovement => recordOfProductMovement.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<Employee>()
                .HasOne(employee => employee.RecordOfProductMovement)
                .WithOne(recordOfProductMovement => recordOfProductMovement.Employee)
                .HasForeignKey<RecordOfProductMovement>(recordOfProductMovement => recordOfProductMovement.EmployeeId)
                .OnDelete(DeleteBehavior.SetNull);
            
            modelBuilder
                .Entity<Product>()
                .Navigation(product => product.ProductForBuy)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

            modelBuilder
                .Entity<Employee>()
                .Navigation(employee => employee.ProductForBuy)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

            modelBuilder
                .Entity<Product>()
                .Navigation(product => product.ProductRequest)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

            modelBuilder
                .Entity<Employee>()
                .Navigation(employee => employee.ProductRequest)
                .UsePropertyAccessMode(PropertyAccessMode.Property);
            
            modelBuilder
                .Entity<Product>()
                .Navigation(product => product.RecordOfProductMovement)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

            modelBuilder
                .Entity<Employee>()
                .Navigation(employee => employee.RecordOfProductMovement)
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
    }
}
