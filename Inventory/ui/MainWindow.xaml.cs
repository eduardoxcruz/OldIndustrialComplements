using System.Linq;
using System.Windows;
using Inventory.enums;

namespace Inventory.ui
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private ProductWindow _productWindowInstance;
		private ProductWindow ProductWindowInstance
		{
			get => _productWindowInstance;
			set => _productWindowInstance = value;
		}
		
		public MainWindow()
		{
			InitializeComponent();
		}

		private void BtnOpenTasksWindow_Click(object sender, RoutedEventArgs e)
		{
			new TasksWindow((int) TasksWindowTasks.Entrance).Show();
		}

		private void BtnOpenSearchProductWindow_Click(object sender, RoutedEventArgs e)
		{
			new SearchProductWindow().Show();
		}

		private void BtnOpenConecctionsWindow_Click(object sender, RoutedEventArgs e)
		{
			new ConnectionsWindow().Show();
		}

		private void BtnOpenSettingWindow_Click(object sender, RoutedEventArgs e)
		{
			new SettingsWindow().Show();
		}

		private void BtnOpenModifyWindow_Click(object sender, RoutedEventArgs e)
		{
			ProductWindowInstance = Application.Current.Windows.OfType<ProductWindow>().SingleOrDefault();
			
			if (ProductWindowInstance == null)
			{
				ProductWindowInstance = new ProductWindow((int)ProductWindowTasks.Modify);
				ProductWindowInstance.Show();
			}
			
			ProductWindowInstance.BringWindowToFront(ProductWindowInstance, (int)ProductWindowTasks.Modify);
		}

		private void BtnOpenAddProductWindow_Click(object sender, RoutedEventArgs e)
		{
			new ProductWindow((int)ProductWindowTasks.AddNewProduct).Show();
		}
	}
}
