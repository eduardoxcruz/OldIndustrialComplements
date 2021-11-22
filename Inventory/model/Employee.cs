using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#nullable disable

namespace MiPrimerEntityFramework.model
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public string FullName { get; set; }
        public string? Password { get; set; }
        
        public ProductForBuy ProductForBuy { get; set; }
        public ProductRequest ProductRequest { get; set; }
        public RecordOfProductMovement RecordOfProductMovement { get; set; }
    }

    public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(employee => employee.Id);

            builder.Property(employee => employee.Id)
                .ValueGeneratedOnAdd();
            
            builder
                .Property(employee => employee.FullName)
                .HasMaxLength(30)
                .IsUnicode(false);
            
            builder
                .Property(employee => employee.Password)
                .HasMaxLength(10)
                .IsUnicode(false);


            builder
                .Property(employee => employee.Type)
                .HasMaxLength(20)
                .IsUnicode(false);
        }
    }
}
