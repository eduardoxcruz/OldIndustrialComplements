using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Threading;
using Inventory.data;
using Inventory.model;
using Microsoft.EntityFrameworkCore;

namespace Inventory.ui
{
	public partial class RequestsWindow
	{
		public static readonly RequestsWindow Instance = new();
		private DispatcherTimer DispatcherTimer { get; set; }
		private ProductRequest LastProductRequest { get; set; }
		private InventoryDbContext InventoryDb { get; set; }
		private ObservableCollection<ProductRequest> ProductRequestsCollection { get; set; }
		private CollectionViewSource ProductRequestsView { get; set; }

		private RequestsWindow()
		{
			InitializeComponent();
			InventoryDb = new InventoryDbContext();
			GetAllProductRequests();
			StartDispatcherTimer();
		}

		private void GetAllProductRequests()
		{
			LastProductRequest =
				InventoryDb.ProductRequests.OrderByDescending(request => request.Id).FirstOrDefault() ??
				new ProductRequest();
			InventoryDb.ProductRequests
				.Include(productRequest => productRequest.Employee)
				.Include(productRequest => productRequest.Product)
				.Load();
			ProductRequestsCollection = InventoryDb.ProductRequests.Local.ToObservableCollection();
			ProductRequestsView = new CollectionViewSource() {Source = ProductRequestsCollection};
			ProductRequestsView.SortDescriptions.Clear();
			ProductRequestsView.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Descending));
			DataGridRequests.ItemsSource = ProductRequestsView.View;
		}

		private void StartDispatcherTimer()
		{
			DispatcherTimer = new DispatcherTimer {Interval = new TimeSpan(0, 0, 3)};
			DispatcherTimer.Tick += AddNewProductRequestToCollection;
			DispatcherTimer.Start();
		}

		private void AddNewProductRequestToCollection(object sender, EventArgs e)
		{
			ProductRequest nextRequest = InventoryDb
				.ProductRequests
				.Include(productRequest => productRequest.Employee)
				.Include(productRequest => productRequest.Product)
				.SingleOrDefault(request => request.Id == LastProductRequest.Id + 1);

			if (nextRequest == null)
			{
				return;
			}

			if (ProductRequestsCollection.Count < nextRequest.Id)
			{
				LastProductRequest = nextRequest;
				ProductRequestsCollection.Add(LastProductRequest);
			}

			DataGridRequests.Items.Refresh();
		}
	}
}
