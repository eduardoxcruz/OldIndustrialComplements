using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mux.Model;

namespace Mux.EntityTypes
{
	internal class EmployeeEntityType : IEntityTypeConfiguration<Employee>
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
