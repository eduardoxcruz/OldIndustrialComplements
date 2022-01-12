using System.ComponentModel;

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
			}
		}
		
		public int Entrys
		{
			get { return _entrys; }
			set { _entrys = value; }
		}

		public int Devolutions
		{
			get { return _devolutions; }
			set
			{
				_devolutions = value;
			}
		}
		
		public int Egresses
		{
			get { return _egresses; } 
			set { _egresses = value; }
		}
		
		public int AmountAdjustments
		{
			get { return _amountAdjustments; }
			set { _amountAdjustments = value; }
		}
		
		public int PriceAdjustments
		{
			get { return _priceAdjustments; }
			set { _priceAdjustments = value; }
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
}
