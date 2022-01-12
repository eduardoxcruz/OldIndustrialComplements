using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.model
{
	public class ProductChangeCount : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private int _id;
		private int _entrys;
		private int _devolutions;
		private int _egresses;
		private int _amountAdjustments;
		private int _priceAdjustments;

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

		public int Entrys
		{
			get { return _entrys; }
			set
			{
				_entrys = value;
				OnPropertyChanged(nameof(Entrys));
			}
		}

		public int Devolutions
		{
			get { return _devolutions; }
			set
			{
				_devolutions = value;
				OnPropertyChanged(nameof(Devolutions));
			}
		}

		public int Egresses
		{
			get { return _egresses; }
			set
			{
				_egresses = value;
				OnPropertyChanged(nameof(Egresses));
			}
		}

		public int AmountAdjustments
		{
			get { return _amountAdjustments; }
			set
			{
				_amountAdjustments = value;
				OnPropertyChanged(nameof(AmountAdjustments));
			}
		}

		public int PriceAdjustments
		{
			get { return _priceAdjustments; }
			set
			{
				_priceAdjustments = value;
				OnPropertyChanged(nameof(PriceAdjustments));
			}
		}

		public Product Product { get; set; }
		public int? ProductId { get; set; }

		public ProductChangeCount()
		{
			Id = 0;
			Entrys = 0;
			Devolutions = 0;
			Egresses = 0;
			AmountAdjustments = 0;
			PriceAdjustments = 0;
		}

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}

	public class ProductChangeCountEntityTypeConfiguration : IEntityTypeConfiguration<ProductChangeCount>
	{
		public void Configure(EntityTypeBuilder<ProductChangeCount> builder)
		{
			builder.HasKey(productChangeCount => productChangeCount.Id);
			builder
				.Property(productChangeCount => productChangeCount.Id)
				.ValueGeneratedOnAdd();
			builder.Property(productChangeCount => productChangeCount.Entrys);
			builder.Property(productChangeCount => productChangeCount.Devolutions);
			builder.Property(productChangeCount => productChangeCount.Egresses);
			builder.Property(productChangeCount => productChangeCount.AmountAdjustments);
			builder.Property(productChangeCount => productChangeCount.PriceAdjustments);
		}
	}
}
