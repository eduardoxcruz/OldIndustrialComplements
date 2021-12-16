using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Inventory.data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.model
{
	public class Product : INotifyPropertyChanged
	{
#pragma warning disable 8632
		public event PropertyChangedEventHandler? PropertyChanged;

		private int _id;
		private string? _debugCode;
		private string? _status;
		private string? _enrollment;
		private string? _mountingTechnology;
		private string? _encapsulationType;
		private string? _shortDescription;
		private string? _category;
		private bool? _isUsingInventory;
		private int? _currentAmount;
		private int? _minAmount;
		private int? _maxAmount;
		private string? _container;
		private string? _location;
		private string? _branchOffice;
		private string? _rack;
		private string? _shelf;
		private decimal? _buyPrice;
		private string? _unitType;
		private string? _manufacturer;
		private string? _partNumber;
		private string? _typeOfStock;
		private bool? _isManualProfit;
		private decimal? _percentageOfProfit;
		private decimal? _percentageOfDiscount;
		private decimal? _salePriceWithoutDiscount;
		private decimal? _salePriceWithDiscount;
		private decimal? _profitWithoutDiscount;
		private decimal? _profitWithDiscount;
		private string? _fullDescription;
		private string? _memo;

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

		public string? DebugCode
		{
			get
			{
				return _debugCode;
			}
			set
			{
				_debugCode = value;
				OnPropertyChanged(nameof(DebugCode));
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

		public string? Enrollment
		{
			get
			{
				return _enrollment;
			}
			set
			{
				_enrollment = value;
				OnPropertyChanged(nameof(Enrollment));
			}
		}

		public string? MountingTechnology
		{
			get
			{
				return _mountingTechnology;
			}
			set
			{
				_mountingTechnology = value;
				OnPropertyChanged(nameof(MountingTechnology));
			}
		}

		public string? EncapsulationType
		{
			get
			{
				return _encapsulationType;
			}
			set
			{
				_encapsulationType = value;
				OnPropertyChanged(nameof(EncapsulationType));
			}
		}

		public string? ShortDescription
		{
			get
			{
				return _shortDescription;
			}
			set
			{
				_shortDescription = value;
				OnPropertyChanged(nameof(ShortDescription));
			}
		}

		public string? Category
		{
			get
			{
				return _category;
			}
			set
			{
				_category = value;
				OnPropertyChanged(nameof(Category));
			}
		}

		public bool? IsUsingInventory
		{
			get
			{
				return _isUsingInventory;
			}
			set
			{
				_isUsingInventory = value;
				OnPropertyChanged(nameof(IsUsingInventory));
			}
		}

		public int? CurrentAmount
		{
			get
			{
				return _currentAmount;
			}
			set
			{
				_currentAmount = value;
				OnPropertyChanged(nameof(CurrentAmount));
			}
		}

		public int? MinAmount
		{
			get
			{
				return _minAmount;
			}
			set
			{
				if (value > MaxAmount)
				{
					_minAmount = MaxAmount;
					return;
				}

				_minAmount = value;
				OnPropertyChanged(nameof(MinAmount));
			}
		}

		public int? MaxAmount
		{
			get
			{
				return _maxAmount;
			}
			set
			{
				_maxAmount = value;
				OnPropertyChanged(nameof(MaxAmount));
			}
		}

		public string? Container
		{
			get
			{
				return _container;
			}
			set
			{
				_container = value;
				OnPropertyChanged(nameof(Container));
			}
		}

		public string? Location
		{
			get
			{
				return _location;
			}
			set
			{
				_location = value;
				OnPropertyChanged(nameof(Location));
			}
		}

		public string? BranchOffice
		{
			get
			{
				return _branchOffice;
			}
			set
			{
				_branchOffice = value;
				OnPropertyChanged(nameof(BranchOffice));
			}
		}

		public string? Rack
		{
			get
			{
				return _rack;
			}
			set
			{
				_rack = value;
				OnPropertyChanged(nameof(Rack));
			}
		}

		public string? Shelf
		{
			get
			{
				return _shelf;
			}
			set
			{
				_shelf = value;
				OnPropertyChanged(nameof(Shelf));
			}
		}

		public decimal? BuyPrice
		{
			get
			{
				return _buyPrice;
			}
			set
			{
				_buyPrice = Math.Round(value ?? 0.00M, 2, MidpointRounding.ToNegativeInfinity);
				OnPropertyChanged(nameof(BuyPrice));
			}
		}

		public string? UnitType
		{
			get
			{
				return _unitType;
			}
			set
			{
				_unitType = value;
				OnPropertyChanged(nameof(UnitType));
			}
		}

		public string? Manufacturer
		{
			get
			{
				return _manufacturer;
			}
			set
			{
				_manufacturer = value;
				OnPropertyChanged(nameof(Manufacturer));
			}
		}

		public string? PartNumber
		{
			get
			{
				return _partNumber;
			}
			set
			{
				_partNumber = value;
				OnPropertyChanged(nameof(PartNumber));
			}
		}

		public string? TypeOfStock
		{
			get
			{
				return _typeOfStock;
			}
			set
			{
				_typeOfStock = value;
				OnPropertyChanged(nameof(TypeOfStock));
			}
		}

		public bool? IsManualProfit
		{
			get
			{
				return _isManualProfit;
			}
			set
			{
				_isManualProfit = value;
				OnPropertyChanged(nameof(IsManualProfit));
			}
		}

		public decimal? PercentageOfProfit
		{
			get
			{
				return _percentageOfProfit;
			}
			set
			{
				_percentageOfProfit = Math.Round(value ?? 0.00M, 2, MidpointRounding.ToNegativeInfinity);
				OnPropertyChanged(nameof(PercentageOfProfit));
			}
		}

		public decimal? PercentageOfDiscount
		{
			get
			{
				return _percentageOfDiscount;
			}
			set
			{
				_percentageOfDiscount = Math.Round(value ?? 0.00M, 2, MidpointRounding.ToNegativeInfinity);
				OnPropertyChanged(nameof(PercentageOfDiscount));
			}
		}

		public decimal? SalePriceWithoutDiscount
		{
			get
			{
				return _salePriceWithoutDiscount;
			}
			set
			{
				_salePriceWithoutDiscount = Math.Round(value ?? 0.00M, 2, MidpointRounding.ToNegativeInfinity);
				OnPropertyChanged(nameof(SalePriceWithoutDiscount));
			}
		}

		public decimal? SalePriceWithDiscount
		{
			get
			{
				return _salePriceWithDiscount;
			}
			set
			{
				_salePriceWithDiscount = Math.Round(value ?? 0.00M, 2, MidpointRounding.ToNegativeInfinity);
				OnPropertyChanged(nameof(SalePriceWithDiscount));
			}
		}

		public decimal? ProfitWithoutDiscount
		{
			get
			{
				return _profitWithoutDiscount;
			}
			set
			{
				_profitWithoutDiscount = Math.Round(value ?? 0.00M, 2, MidpointRounding.ToNegativeInfinity);
				OnPropertyChanged(nameof(ProfitWithoutDiscount));
			}
		}

		public decimal? ProfitWithDiscount
		{
			get
			{
				return _profitWithDiscount;
			}
			set
			{
				_profitWithDiscount = Math.Round(value ?? 0.00M, 2, MidpointRounding.ToNegativeInfinity);
				OnPropertyChanged(nameof(ProfitWithDiscount));
			}
		}

		public string? FullDescription
		{
			get
			{
				return _fullDescription;
			}
			set
			{
				_fullDescription = value;
				OnPropertyChanged(nameof(FullDescription));
			}
		}

		public string? Memo
		{
			get
			{
				return _memo;
			}
			set
			{
				_memo = value;
				OnPropertyChanged(nameof(Memo));
			}
		}

		public List<ProductForBuy> ProductForBuys { get; set; }
		public List<ProductRequest> ProductRequests { get; set; }
		public List<RecordOfProductMovement> RecordOfProductMovements { get; set; }

		public Product()
		{
			this.Id = 0;
			this.DebugCode = "";
			this.Status = "";
			this.Enrollment = "";
			this.MountingTechnology = "";
			this.EncapsulationType = "";
			this.Category = "";
			this.ShortDescription = "";
			this.FullDescription = "";
			this.IsUsingInventory = true;
			this.CurrentAmount = 0;
			this.MinAmount = 0;
			this.MaxAmount = 0;
			this.Container = "";
			this.Location = "";
			this.BranchOffice = "";
			this.Rack = "";
			this.Shelf = "";
			this.BuyPrice = 0.0M;
			this.UnitType = "";
			this.TypeOfStock = "";
			this.Manufacturer = "";
			this.PartNumber = "";
			this.IsManualProfit = true;
			this.PercentageOfProfit = 0.0M;
			this.SalePriceWithoutDiscount = 0.0M;
			this.ProfitWithoutDiscount = 0.0M;
			this.PercentageOfDiscount = 0.0M;
			this.SalePriceWithDiscount = 0.0M;
			this.ProfitWithDiscount = 0.0M;
			this.Memo = "";
		}

		public static Product GetDataFromSqlDatabase(int id)
		{
			using InventoryDbContext inventoryDb = new();
			int lastId = inventoryDb.Products.OrderByDescending(searchProduct => searchProduct.Id).FirstOrDefault()!.Id;

			if (id > lastId)
			{
				MessageBox.Show("El Id solicitado es mayor a la cantidad de productos guardados", "Error");
				return new Product();
			}

			Product? product = inventoryDb.Products.SingleOrDefault(searchProduct => searchProduct.Id == id);

			return product ?? new Product();
		}
#pragma warning restore 8632
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

			switch (propertyName)
			{
				case nameof(Enrollment):
					goto case "ChangeFullDescription";
				case nameof(Manufacturer):
					goto case "ChangeFullDescription";
				case nameof(PartNumber):
					goto case "ChangeFullDescription";
				case nameof(ShortDescription):
					goto case "ChangeFullDescription";
				case nameof(MountingTechnology):
					goto case "ChangeFullDescription";
				case nameof(EncapsulationType):
					goto case "ChangeFullDescription";
				case "ChangeFullDescription":
					FullDescription = Id + " - " + Manufacturer + ", " + Enrollment + ", " + ShortDescription +
					                  ", " + MountingTechnology + ", " + EncapsulationType;
					break;
				case nameof(IsUsingInventory):
					if (!IsUsingInventory ?? true)
					{
						CurrentAmount = 0;
						MinAmount = 0;
						MaxAmount = 0;
					}

					break;
				case nameof(IsManualProfit):
					if (!IsManualProfit ?? true)
					{
						PercentageOfProfit = 25.0M;
						PercentageOfDiscount = 10.0M;
					}

					break;
				case nameof(BuyPrice):
					goto case nameof(PercentageOfProfit);
				case nameof(PercentageOfProfit):
					ProfitWithoutDiscount = (PercentageOfProfit * 0.01M) * BuyPrice;
					break;
				case nameof(ProfitWithoutDiscount):
					SalePriceWithoutDiscount = BuyPrice + ProfitWithoutDiscount;
					break;
				case nameof(SalePriceWithoutDiscount):
					goto case nameof(PercentageOfDiscount);
				case nameof(PercentageOfDiscount):
					if (PercentageOfDiscount > 50)
					{
						PercentageOfDiscount = 50.00M;
					}
					SalePriceWithDiscount = SalePriceWithoutDiscount - ((PercentageOfDiscount * 0.01M) * SalePriceWithoutDiscount);
					ProfitWithDiscount = SalePriceWithDiscount - BuyPrice;
					break;
			}
		}
	}

	public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.HasKey(product => product.Id);

			builder
				.Property(product => product.Id)
				.ValueGeneratedNever();

			builder.Property(product => product.IsManualProfit);

			builder.Property(product => product.IsUsingInventory);

			builder.Property(product => product.CurrentAmount);

			builder.Property(product => product.MaxAmount);

			builder.Property(product => product.MinAmount);

			builder
				.Property(product => product.BuyPrice)
				.HasPrecision(6, 2);

			builder
				.Property(product => product.PercentageOfDiscount)
				.HasPrecision(6, 2);

			builder
				.Property(product => product.PercentageOfProfit)
				.HasPrecision(6, 2);

			builder
				.Property(product => product.SalePriceWithDiscount)
				.HasPrecision(6, 2);

			builder
				.Property(product => product.SalePriceWithoutDiscount)
				.HasPrecision(6, 2);

			builder
				.Property(product => product.ProfitWithoutDiscount)
				.HasPrecision(6, 2);

			builder
				.Property(product => product.ProfitWithDiscount)
				.HasPrecision(6, 2);

			builder
				.Property(product => product.Category)
				.HasMaxLength(50)
				.IsUnicode(false);

			builder
				.Property(product => product.DebugCode)
				.HasMaxLength(30)
				.IsUnicode(false);

			builder
				.Property(product => product.Container)
				.HasMaxLength(60)
				.IsUnicode(false);

			builder
				.Property(product => product.FullDescription)
				.HasMaxLength(200)
				.IsUnicode(false);

			builder
				.Property(product => product.ShortDescription)
				.HasMaxLength(200)
				.IsUnicode(false);

			builder
				.Property(product => product.Rack)
				.HasMaxLength(10)
				.IsUnicode(false);

			builder
				.Property(product => product.EncapsulationType)
				.HasMaxLength(20)
				.IsUnicode(false);

			builder
				.Property(product => product.Status)
				.HasMaxLength(10)
				.IsUnicode(false);

			builder
				.Property(product => product.Enrollment)
				.HasMaxLength(60)
				.IsUnicode(false);

			builder
				.Property(product => product.Memo)
				.HasMaxLength(70)
				.IsUnicode(false);

			builder
				.Property(product => product.PartNumber)
				.HasMaxLength(30)
				.IsUnicode(false);

			builder
				.Property(product => product.Manufacturer)
				.HasMaxLength(50)
				.IsUnicode(false);

			builder
				.Property(product => product.Shelf)
				.HasMaxLength(10)
				.IsUnicode(false);

			builder
				.Property(product => product.BranchOffice)
				.HasMaxLength(10)
				.IsUnicode(false);

			builder
				.Property(product => product.MountingTechnology)
				.HasMaxLength(16)
				.IsUnicode(false);

			builder
				.Property(product => product.TypeOfStock)
				.HasMaxLength(20)
				.IsUnicode(false);

			builder
				.Property(product => product.Location)
				.HasMaxLength(40)
				.IsUnicode(false);

			builder
				.Property(product => product.UnitType)
				.HasMaxLength(10)
				.IsUnicode(false);
		}
	}
}
