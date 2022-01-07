using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;
using Inventory.data;
using Inventory.model;
using Microsoft.EntityFrameworkCore;

namespace Inventory.ui
{
	public partial class ShoppingCartWindow
	{
		public static readonly ShoppingCartWindow Instance = new();
		private DispatcherTimer NewProductToBuyLookupTimer { get; set; }
		private InventoryDbContext InventoryDb { get; set; }
		private ObservableCollection<ProductForBuy> ShoppingCartCollection { get; set; }
		private CollectionViewSource ShoppingCartView { get; set; }
		
		public ShoppingCartWindow()
		{
			InitializeComponent();
			GetAllProductsToBuy(null, null);
		}

		private void GetAllProductsToBuy(object sender, RoutedEventArgs e)
		{
			InventoryDb = new InventoryDbContext();

			InventoryDb.ProductsForBuy
				.Include(productRequest => productRequest.Employee)
				.Include(productRequest => productRequest.Product)
				.Load();
			ShoppingCartCollection = InventoryDb.ProductsForBuy.Local.ToObservableCollection();
			ShoppingCartView = new CollectionViewSource() {Source = ShoppingCartCollection};
			ShoppingCartView.SortDescriptions.Clear();
			ShoppingCartView.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Descending));
			DataGridShoppingCart.ItemsSource = ShoppingCartView.View;
		}
	}
}
