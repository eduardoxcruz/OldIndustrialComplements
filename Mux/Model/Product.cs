using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Mux.Model
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
		private string? _oldEncapsulationType;
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
		private int _entrys;
		private int _devolutions;
		private int _egresses;
		private int _amountAdjustments;
		private int _priceAdjustments;

		public int Id
		{
			get => _id;
			set
			{
				_id = value;
				OnPropertyChanged(nameof(Id));
			}
		}
		public string? DebugCode
		{
			get => _debugCode;
			set
			{
				_debugCode = value;
				OnPropertyChanged(nameof(DebugCode));
			}
		}
		public string? Status
		{
			get => _status;
			set
			{
				_status = value;
				OnPropertyChanged(nameof(Status));
			}
		}
		public string? Enrollment
		{
			get => _enrollment;
			set
			{
				_enrollment = value;
				OnPropertyChanged(nameof(Enrollment));
			}
		}
		public string? MountingTechnology
		{
			get => _mountingTechnology;
			set
			{
				_mountingTechnology = value;
				OnPropertyChanged(nameof(MountingTechnology));
			}
		}
		public string? OldEncapsulationType
		{
			get => _oldEncapsulationType;
			set
			{
				_oldEncapsulationType = value;
				OnPropertyChanged(nameof(OldEncapsulationType));
			}
		}
		public string? ShortDescription
		{
			get => _shortDescription;
			set
			{
				_shortDescription = value;
				OnPropertyChanged(nameof(ShortDescription));
			}
		}
		public string? Category
		{
			get => _category;
			set
			{
				_category = value;
				OnPropertyChanged(nameof(Category));
			}
		}
		public bool? IsUsingInventory
		{
			get => _isUsingInventory;
			set
			{
				_isUsingInventory = value;
				OnPropertyChanged(nameof(IsUsingInventory));
			}
		}
		public int? CurrentAmount
		{
			get => _currentAmount;
			set
			{
				_currentAmount = value;
				OnPropertyChanged(nameof(CurrentAmount));
			}
		}
		public int? MinAmount
		{
			get => _minAmount;
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
			get => _maxAmount;
			set
			{
				_maxAmount = value;
				OnPropertyChanged(nameof(MaxAmount));
			}
		}
		public string? Container
		{
			get => _container;
			set
			{
				_container = value;
				OnPropertyChanged(nameof(Container));
			}
		}
		public string? Location
		{
			get => _location;
			set
			{
				_location = value;
				OnPropertyChanged(nameof(Location));
			}
		}
		public string? BranchOffice
		{
			get => _branchOffice;
			set
			{
				_branchOffice = value;
				OnPropertyChanged(nameof(BranchOffice));
			}
		}
		public string? Rack
		{
			get => _rack;
			set
			{
				_rack = value;
				OnPropertyChanged(nameof(Rack));
			}
		}
		public string? Shelf
		{
			get => _shelf;
			set
			{
				_shelf = value;
				OnPropertyChanged(nameof(Shelf));
			}
		}
		public decimal? BuyPrice
		{
			get => _buyPrice;
			set
			{
				_buyPrice = Math.Round(value ?? 0.00M, 2, MidpointRounding.ToNegativeInfinity);
				OnPropertyChanged(nameof(BuyPrice));
			}
		}
		public string? UnitType
		{
			get => _unitType;
			set
			{
				_unitType = value;
				OnPropertyChanged(nameof(UnitType));
			}
		}
		public string? Manufacturer
		{
			get => _manufacturer;
			set
			{
				_manufacturer = value;
				OnPropertyChanged(nameof(Manufacturer));
			}
		}
		public string? PartNumber
		{
			get => _partNumber;
			set
			{
				_partNumber = value;
				OnPropertyChanged(nameof(PartNumber));
			}
		}
		public string? TypeOfStock
		{
			get => _typeOfStock;
			set
			{
				_typeOfStock = value;
				OnPropertyChanged(nameof(TypeOfStock));
			}
		}
		public bool? IsManualProfit
		{
			get => _isManualProfit;
			set
			{
				_isManualProfit = value;
				OnPropertyChanged(nameof(IsManualProfit));
			}
		}
		public decimal? PercentageOfProfit
		{
			get => _percentageOfProfit;
			set
			{
				_percentageOfProfit = Math.Round(value ?? 0.00M, 2, MidpointRounding.ToNegativeInfinity);
				OnPropertyChanged(nameof(PercentageOfProfit));
			}
		}
		public decimal? PercentageOfDiscount
		{
			get => _percentageOfDiscount;
			set
			{
				_percentageOfDiscount = Math.Round(value ?? 0.00M, 2, MidpointRounding.ToNegativeInfinity);
				OnPropertyChanged(nameof(PercentageOfDiscount));
			}
		}
		public decimal? SalePriceWithoutDiscount
		{
			get => _salePriceWithoutDiscount;
			set
			{
				_salePriceWithoutDiscount = Math.Round(value ?? 0.00M, 2, MidpointRounding.ToNegativeInfinity);
				OnPropertyChanged(nameof(SalePriceWithoutDiscount));
			}
		}
		public decimal? SalePriceWithDiscount
		{
			get => _salePriceWithDiscount;
			set
			{
				_salePriceWithDiscount = Math.Round(value ?? 0.00M, 2, MidpointRounding.ToNegativeInfinity);
				OnPropertyChanged(nameof(SalePriceWithDiscount));
			}
		}
		public decimal? ProfitWithoutDiscount
		{
			get => _profitWithoutDiscount;
			set
			{
				_profitWithoutDiscount = Math.Round(value ?? 0.00M, 2, MidpointRounding.ToNegativeInfinity);
				OnPropertyChanged(nameof(ProfitWithoutDiscount));
			}
		}
		public decimal? ProfitWithDiscount
		{
			get => _profitWithDiscount;
			set
			{
				_profitWithDiscount = Math.Round(value ?? 0.00M, 2, MidpointRounding.ToNegativeInfinity);
				OnPropertyChanged(nameof(ProfitWithDiscount));
			}
		}
		public string? FullDescription
		{
			get => _fullDescription;
			set
			{
				_fullDescription = value;
				OnPropertyChanged(nameof(FullDescription));
			}
		}
		public string? Memo
		{
			get => _memo;
			set
			{
				_memo = value;
				OnPropertyChanged(nameof(Memo));
			}
		}
		public int Entrys
		{
			get => _entrys;
			set
			{
				_entrys = value;
				OnPropertyChanged(nameof(Entrys));
			}
		}
		public int Devolutions
		{
			get => _devolutions;
			set
			{
				_devolutions = value;
				OnPropertyChanged(nameof(Devolutions));
			}
		}
		public int Egresses
		{
			get => _egresses;
			set
			{
				_egresses = value;
				OnPropertyChanged(nameof(Egresses));
			}
		}
		public int AmountAdjustments
		{
			get => _amountAdjustments;
			set
			{
				_amountAdjustments = value;
				OnPropertyChanged(nameof(AmountAdjustments));
			}
		}
		public int PriceAdjustments
		{
			get => _priceAdjustments;
			set
			{
				_priceAdjustments = value;
				OnPropertyChanged(nameof(PriceAdjustments));
			}
		}
#pragma warning restore 8632
		
		public int EncapsulationTypeId { get; set; }
		public EncapsulationType EncapsulationType { get; set; }
		
		public List<ProductToBuy> ShoppingCart { get; set; }
		public List<ProductRequest> ProductRequests { get; set; }
		public List<ProductChangeLog> ProductChangeLogs { get; set; }
		public List<Category> Categories { get; set; }

		public Product()
		{
			Id = 0;
			DebugCode = "";
			Status = "";
			Enrollment = "";
			MountingTechnology = "";
			OldEncapsulationType = "";
			Category = "";
			ShortDescription = "";
			FullDescription = "";
			IsUsingInventory = true;
			CurrentAmount = 0;
			MinAmount = 0;
			MaxAmount = 0;
			Container = "";
			Location = "";
			BranchOffice = "";
			Rack = "";
			Shelf = "";
			BuyPrice = 0.0M;
			UnitType = "";
			TypeOfStock = "";
			Manufacturer = "";
			PartNumber = "";
			IsManualProfit = true;
			PercentageOfProfit = 0.0M;
			SalePriceWithoutDiscount = 0.0M;
			ProfitWithoutDiscount = 0.0M;
			PercentageOfDiscount = 0.0M;
			SalePriceWithDiscount = 0.0M;
			ProfitWithDiscount = 0.0M;
			Entrys = 0;
			Egresses = 0;
			Devolutions = 0;
			AmountAdjustments = 0;
			PriceAdjustments = 0;
			Memo = "";
		}

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
