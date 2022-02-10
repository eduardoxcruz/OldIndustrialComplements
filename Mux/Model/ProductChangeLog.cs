using System;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mux.Model
{
	public class ProductChangeLog : INotifyPropertyChanged
	{
#pragma warning disable 8632
		public event PropertyChangedEventHandler? PropertyChanged;
		
		public int? _id;
		public DateTime? _date;
		public string? _type;
		public int? _amount;
		public int? _previousAmount;
		public int? _newAmount;
		public decimal? _purchasePrice;
		public string? _provider;
		public string? _productFullDescription;
		public string? _employeeName;

		public int? Id
		{
			get => _id;
			set
			{
				_id = value;
				OnPropertyChanged(nameof(Id));
			}
		}
		public DateTime? Date
		{
			get => _date;
			set
			{
				_date = value;
				OnPropertyChanged(nameof(Date));
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
		public int? Amount
		{
			get => _amount;
			set
			{
				_amount = value;
				OnPropertyChanged(nameof(Amount));
			}
		}
		public int? PreviousAmount
		{
			get => _previousAmount;
			set
			{
				_previousAmount = value;
				OnPropertyChanged(nameof(PreviousAmount));
			}
		}
		public int? NewAmount
		{
			get => _newAmount;
			set
			{
				_newAmount = value;
				OnPropertyChanged(nameof(NewAmount));
			}
		}
		public decimal? PurchasePrice
		{
			get => _purchasePrice;
			set
			{
				_purchasePrice = value;
				OnPropertyChanged(nameof(PurchasePrice));
			}
		}
		public string? Provider
		{
			get => _provider;
			set
			{
				_provider = value;
				OnPropertyChanged(nameof(Provider));
			}
		}
		public string? ProductFullDescription
		{
			get => _productFullDescription;
			set
			{
				_productFullDescription = value;
				OnPropertyChanged(nameof(ProductFullDescription));
			}
		}
		public string? EmployeeName
		{
			get => _employeeName;
			set
			{
				_employeeName = value;
				OnPropertyChanged(nameof(EmployeeName));
			}
		}
		public Employee Employee { get; set; }
		public int? EmployeeId { get; set; }
		public Product Product { get; set; }
		public int? ProductId { get; set; }
#pragma warning restore 8632
		
		public ProductChangeLog()
		{
			Id = 0;
			Date = DateTime.Now;
			Type = "";
			Amount = 0;
			PreviousAmount = 0;
			NewAmount = 0;
			PurchasePrice = 0;
			Provider = "";
			ProductFullDescription = "";
			EmployeeName = "";
		}

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}

	public class ProductChangeLogEntityTypeConfiguration : IEntityTypeConfiguration<ProductChangeLog>
	{
		public void Configure(EntityTypeBuilder<ProductChangeLog> builder)
		{
			builder.HasKey(recordOfProductMovement => recordOfProductMovement.Id);

			builder
				.Property(recordOfProductMovement => recordOfProductMovement.Id)
				.ValueGeneratedNever();

			builder.Property(recordOfProductMovement => recordOfProductMovement.Amount);

			builder.Property(recordOfProductMovement => recordOfProductMovement.PreviousAmount);

			builder.Property(recordOfProductMovement => recordOfProductMovement.NewAmount);

			builder.Property(recordOfProductMovement => recordOfProductMovement.Date);

			builder
				.Property(recordOfProductMovement => recordOfProductMovement.PurchasePrice)
				.HasPrecision(6, 2);

			builder
				.Property(recordOfProductMovement => recordOfProductMovement.EmployeeName)
				.HasMaxLength(35)
				.IsUnicode(false);

			builder
				.Property(recordOfProductMovement => recordOfProductMovement.ProductFullDescription)
				.HasMaxLength(200)
				.IsUnicode(false);

			builder.Property(recordOfProductMovement => recordOfProductMovement.Provider)
				.HasMaxLength(50)
				.IsUnicode(false);

			builder.Property(recordOfProductMovement => recordOfProductMovement.Type)
				.HasMaxLength(10)
				.IsUnicode(false);
		}
	}
}
