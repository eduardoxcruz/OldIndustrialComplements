using System;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.model
{
	public class ProductToBuy : INotifyPropertyChanged
	{
#pragma warning disable 8632
		public event PropertyChangedEventHandler? PropertyChanged;

		private int? _id;
		private string? _provider;
		private DateTime? _date;
		private string? _status;
		private int? _requestedAmount;
		private string? _employeeName;

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

		public string? Provider
		{
			get
			{
				return _provider;
			}
			set
			{
				_provider = value;
				OnPropertyChanged(nameof(Provider));
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

		public int? RequestedAmount
		{
			get
			{
				return _requestedAmount;
			}
			set
			{
				_requestedAmount = value;
				OnPropertyChanged(nameof(RequestedAmount));
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

		public Employee Employee { get; set; }
		public int? EmployeeId { get; set; }
		public Product Product { get; set; }
		public int? ProductId { get; set; }
#pragma warning restore 8632

		public ProductToBuy()
		{
			this.Id = 0;
			this.Provider = "";
			this.Date = DateTime.Now;
			this.Status = "";
			this.RequestedAmount = 0;
			this.EmployeeName = "";
		}

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}

	public class ProductToBuyEntityTypeConfiguration : IEntityTypeConfiguration<ProductToBuy>
	{
		public void Configure(EntityTypeBuilder<ProductToBuy> builder)
		{
			builder.HasKey(productForBuy => productForBuy.Id);

			builder
				.Property(productForBuy => productForBuy.Id)
				.ValueGeneratedNever();

			builder.Property(productForBuy => productForBuy.RequestedAmount);

			builder.Property(productForBuy => productForBuy.Date);

			builder
				.Property(productForBuy => productForBuy.EmployeeName)
				.HasMaxLength(35)
				.IsUnicode(false);

			builder
				.Property(productForBuy => productForBuy.Provider)
				.HasMaxLength(35)
				.IsUnicode(false);

			builder
				.Property(productForBuy => productForBuy.Status)
				.HasMaxLength(10)
				.IsUnicode(false);
		}
	}
}
