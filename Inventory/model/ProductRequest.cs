using System;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#nullable disable

namespace Inventory.model
{
	public class ProductRequest : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		private int? _id;
		private DateTime? _date;
		private int? _amount;
		private string? _employeeName;
		private string? _type;
		private string? _status;

		public int? Id
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

		public DateTime? Date
		{
			get
			{
				return _date;
			}
			set
			{
				_date = value;
				OnPropertyChanged(nameof(Date));
			}
		}

		public int? Amount
		{
			get
			{
				return _amount;
			}
			set
			{
				_amount = value;
				OnPropertyChanged(nameof(Amount));
			}
		}

		public string? EmployeeName
		{
			get
			{
				return _employeeName;
			}
			set
			{
				_employeeName = value;
				OnPropertyChanged(nameof(EmployeeName));
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

		public string? Status
		{
			get
			{
				return _status;
			}
			set
			{
				_status = value;
				OnPropertyChanged(nameof(Status));
			}
		}

		public Employee Employee { get; set; }
		public int? EmployeeId { get; set; }
		public Product Product { get; set; }
		public int? ProductId { get; set; }

		public ProductRequest()
		{
			this.Id = 0;
			this.Date = DateTime.Now;
			this.Amount = 0;
			this.EmployeeName = null;
			this.Type = "";
			this.Status = "";
		}

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}

	public class ProductRequestEntityTypeConfiguration : IEntityTypeConfiguration<ProductRequest>
	{
		public void Configure(EntityTypeBuilder<ProductRequest> builder)
		{
			builder.HasKey(productRequest => productRequest.Id);

			builder
				.Property(productRequest => productRequest.Id)
				.ValueGeneratedNever();

			builder.Property(productRequest => productRequest.Amount);

			builder.Property(productRequest => productRequest.Date);

			builder
				.Property(productRequest => productRequest.EmployeeName)
				.HasMaxLength(35)
				.IsUnicode(false);

			builder
				.Property(productRequest => productRequest.Type)
				.HasMaxLength(35)
				.IsUnicode(false);

			builder
				.Property(productRequest => productRequest.Status)
				.HasMaxLength(35)
				.IsUnicode(false);
		}
	}
}
