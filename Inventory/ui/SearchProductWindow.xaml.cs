using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Inventory.database;

namespace Inventory.ui
{
	public partial class SearchProductWindow : Window
	{
		public SearchProductWindow()
		{
			InitializeComponent();
		}

		private void LoadDataFromDatabaseToDataTable()
		{
			var query = "SELECT * FROM dbo.productos2";
			var sqlDatabase = new SqlDatabase();
			var dataAdapter = new SqlDataAdapter(sqlDatabase.GetSqlCommandWithQuery(query));
			var dataTable = new DataTable("dbo.productos2");
			dataAdapter.Fill(dataTable);
			DataGridProducts.ItemsSource = dataTable.DefaultView;
			dataAdapter.Update(dataTable);
		}

		private void DataGridProductos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var productWindow = new ProductWindow((int)ProductWindowTasks.ShowDetails);
			productWindow.Show();
		}

		private void TxtBoxId_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			var regex = new Regex("[^0-9]+");
			e.Handled = regex.IsMatch(e.Text);
		}
	}
}
