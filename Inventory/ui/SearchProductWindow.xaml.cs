using System.Data;
using System.Data.SqlClient;
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
			string query = "SELECT * FROM dbo.productos2";
			SqlDatabase sqlDatabase = new SqlDatabase();
			SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlDatabase.GetSqlCommandWithQuery(query));
			DataTable dataTable = new DataTable("dbo.productos2");
			dataAdapter.Fill(dataTable);
			DataGridProductos.ItemsSource = dataTable.DefaultView;
			dataAdapter.Update(dataTable);
		}
		private void DataGridProductos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var productWindow = new ProductWindow((int)ProductWindowTasks.ShowDetails);
			productWindow.Show();
		}
	}
}
