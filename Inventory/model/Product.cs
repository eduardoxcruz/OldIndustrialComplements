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
		public int CurrentProductStock { get; set; }
		public int MinProductStock { get; set; }
		public int MaxProductStock { get; set; }
		public string Container { get; set; }
		public string Location { get; set; }
		public string BranchOffice { get; set; }
		public string Shelf { get; set; }
		public float PurchasePrice { get; set; }
		public string Unit { get; set; }
		public string ProductType { get; set; }
		public string Manufacturer { get; set; }
		public string ManufacturerPartNumber { get; set; }
		public bool AutomaticProfit { get; set; }
		public bool ManualProfit { get; set; }
		public float PercentageOfProfit { get; set; }
		public float SalePrice { get; set; }
		public float Utility { get; set; }
		public float DiscountRate { get; set; }
		public float PriceWithDiscount { get; set; }
		public float ProfitWithDiscount { get; set; }
		public string Memo { get; set; }
	}
}
