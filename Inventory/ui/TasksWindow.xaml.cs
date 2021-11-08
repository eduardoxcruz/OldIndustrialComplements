using System.Globalization;
using System.Windows;
using Inventory.enums;
using Inventory.model;
using Inventory.Properties;
using static System.DateTime;

namespace Inventory.ui
{
	public partial class TasksWindow
	{
		public static readonly TasksWindow Instance = new(TasksWindowTasks.Entrance);
		private TasksWindowTasks CurrentTask { get; set; }
		private Product Product { get; set; }

		private TasksWindow(TasksWindowTasks task)
		{
			InitializeComponent();
			CurrentTask = task;
			Product = new Product();
			CmbBoxTask.SelectedIndex = (int)CurrentTask;
			CmbBoxIdOrDebugCode.SelectedIndex = 0;
			AssignProductDataToControls(Product.ProductId.ToString());
		}
		private TasksWindow(TasksWindowTasks task, Product product)
		{
			InitializeComponent();
			CurrentTask = task;
			Product = product;
			CmbBoxTask.SelectedIndex = (int)CurrentTask;
			CmbBoxIdOrDebugCode.SelectedIndex = 0;
			AssignProductDataToControls(Product.ProductId.ToString());
		}
		private void AssignProductDataToControls(string id)
		{
			if (!id.Equals(Product.ProductId.ToString()))
			{
				Product.GetDataFromSqlDatabase(id);
			}

			TxtBlckId.Text = Product.ProductId.ToString();
			TxtBlckDateTime.Text = Now.ToString(CultureInfo.CurrentCulture);
			TxtBlckCurrentQuantityInStock.Text = Product.CurrentProductStock;
			TxtBlckContainer.Text = Product.Container;
			TxtBlckLocation.Text = Product.Location;
			TxtBlckBranchOffice.Text = Product.BranchOffice;
			TxtBlckShelf.Text = Product.Shelf;
			TxtBlckRack.Text = Product.Rack;
			TxtBlckPurchasePrice.Text = Product.PurchasePrice;
			TxtBlckFullDescription.Text = Product.FullDescription;
			TxtBlckEmployee.Text = Settings.Default.User;
			
			CmbBoxProvider.Items.Clear();
		}
		private void BtnSearch_OnClick(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(TxtBoxIdOrDebugCode.Text))
			{
				return;
			}
			
			AssignProductDataToControls(TxtBoxIdOrDebugCode.Text);
		}
	}
}

