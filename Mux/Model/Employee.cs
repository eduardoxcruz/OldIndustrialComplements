using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mux.Model
{
	public class Employee : INotifyPropertyChanged
	{
#pragma warning disable 8632
		public event PropertyChangedEventHandler? PropertyChanged;
		
		private int _id;
		private string? _type;
		private string _fullName;
		private string? _password;

		public int Id
		{
			get => _id;
			set
			{
				_id = value;
				OnPropertyChanged(nameof(Id));
			}
		}
		public string? Type
		{
			get => _type;
			set
			{
				_type = value;
				OnPropertyChanged(nameof(Type));
			}
		}
		public string FullName
		{
			get => _fullName;
			set
			{
				_fullName = value;
				OnPropertyChanged(nameof(FullName));
			}
		}
		public string? Password
		{
			get => _password;
			set
			{
				_password = value;
				OnPropertyChanged(nameof(Password));
			}
		}
		public List<ProductToBuy> ShoppingCart { get; set; }
		public List<ProductRequest> ProductRequests { get; set; }
		public List<ProductChangeLog> ProductChangeLogs { get; set; }
#pragma warning restore 8632
		
		public Employee()
		{
			Id = 0;
			Type = "";
			FullName = "";
			Password = "";
		}

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
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
