using System.Globalization;
using System.Windows;
using Inventory.model;
using Inventory.Properties;
using static System.DateTime;

namespace Inventory.ui
{
	public partial class TasksWindow : Window
	{
		private int _currentTask;
		private Product _product;
		private int CurrentTask
		{
			get => _currentTask;
			set {
				if (value is < 0 or > 6)
				{
					_currentTask = 0;
					return;
				}

				_currentTask = value;
			}
	}
		private Product Product
		{
			get => _product;
			set => _product = value;
		}
		
		public TasksWindow(int task)
		{
			InitializeComponent();
			CurrentTask = task;
			Product = new Product();
			CmbBoxTask.SelectedIndex = CurrentTask;
			CmbBoxIdOrDebugCode.SelectedIndex = 0;
			AssignProductDataToControls(Product.ProductId.ToString());
		}
		public TasksWindow(int task, Product product)
		{
			InitializeComponent();
			CurrentTask = task;
			Product = product;
			CmbBoxTask.SelectedIndex = CurrentTask;
			CmbBoxIdOrDebugCode.SelectedIndex = 0;
			AssignProductDataToControls(Product.ProductId.ToString());
		}
		private void AssignProductDataToControls(string id)
		{
			if (!id.Equals(Product.ProductId.ToString()))
			{
				Product.GetDataFromSqlDatabase(id);
			}

			TxtBlckID.Text = Product.ProductId.ToString();
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
		private void BtnExit_OnClick(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void BtnSearch_OnClick(object sender, RoutedEventArgs e)
		{
			
		}
	}
}

