namespace Inventory.model
{
	public class Product
	{
		public int ProductId { get; set; }
		public string DebugCode { get; set; }
		public string State { get; set; }
		public string Enrollment { get; set; }
		public string MountingTechnology { get; set; }
		public string EncapsulationType { get; set; }
		public string Category { get; set; }
		public string ShortDescription { get; set; }
		public string FullDescription { get; set; }
		public bool TheProductUsesInventory { get; set; }
		public string CurrentProductStock { get; set; }
		public string MinProductStock { get; set; }
		public string MaxProductStock { get; set; }
		public string Container { get; set; }
		public string Location { get; set; }
		public string BranchOffice { get; set; }
		public string Rack { get; set; }
		public string Shelf { get; set; }
		public string PurchasePrice { get; set; }
		public string Unit { get; set; }
		public string ProductType { get; set; }
		public string Manufacturer { get; set; }
		public string ManufacturerPartNumber { get; set; }
		public bool ManualProfit { get; set; }
		public string PercentageOfProfit { get; set; }
		public string SalePrice { get; set; }
		public string Utility { get; set; }
		public string DiscountRate { get; set; }
		public string PriceWithDiscount { get; set; }
		public string ProfitWithDiscount { get; set; }
		public string Provider { get; set; }
		public string Memo { get; set; }
		public Product()
		{
			AssignDefaultData();
		}
		public Product(string id)
		{
			GetDataFromSqlDatabase(id);
		}
		private void AssignDefaultData()
		{
			this.ProductId = 0;
			this.DebugCode = "";
			this.State = "";
			this.Enrollment = "";
			this.MountingTechnology = "";
			this.EncapsulationType = "";
			this.Category = "";
			this.ShortDescription = "";
			this.FullDescription = "";
			this.TheProductUsesInventory = false;
			this.CurrentProductStock = "";
			this.MinProductStock = "";
			this.MaxProductStock = "";
			this.Container = "";
			this.Location = "";
			this.BranchOffice = "";
			this.Rack = "";
			this.Shelf = "";
			this.PurchasePrice = "";
			this.Unit = "";
			this.ProductType = "";
			this.Manufacturer = "";
			this.ManufacturerPartNumber = "";
			this.ManualProfit = false;
			this.PercentageOfProfit = "";
			this.SalePrice = "";
			this.Utility = "";
			this.DiscountRate = "";
			this.PriceWithDiscount = "";
			this.ProfitWithDiscount = "";
			this.Provider = "";
			this.Memo = "";
		}
		public void GetDataFromSqlDatabase(string id)
		{
		}
	}
}
