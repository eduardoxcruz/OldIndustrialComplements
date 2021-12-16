using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.model
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
			get
			{
				return _id;
			}
			set
			{
				_id = value;
				OnPropertyChanged(nameof(Id));
			}
		}

		public string? Type
		{
			get
			{
				return _type;
			}
			set
			{
				_type = value;
				OnPropertyChanged(nameof(Type));
			}
		}

		public string FullName
		{
			get
			{
				return _fullName;
			}
			set
			{
				_fullName = value;
				OnPropertyChanged(nameof(FullName));
			}
		}

		public string? Password
		{
			get
			{
				return _password;
			}
			set
			{
				_password = value;
				OnPropertyChanged(nameof(Password));
			}
		}
#pragma warning restore 8632
		public List<ProductForBuy> ProductForBuys { get; set; }
		public List<ProductRequest> ProductRequests { get; set; }
		public List<RecordOfProductMovement> RecordOfProductMovements { get; set; }

		public Employee()
		{
			this.Id = 0;
			this.Type = "";
			this.FullName = "";
			this.Password = "";
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
