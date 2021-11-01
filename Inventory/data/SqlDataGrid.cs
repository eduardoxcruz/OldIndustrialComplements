using System.Data;
using System.Windows.Controls;

namespace Inventory.data
{
	public class SqlDataGrid
	{
		private SqlDataTable SqlDataTable { get; init; }

		public SqlDataGrid()
		{
			SqlDataTable = new SqlDataTable();
		}
		public void FillDataGridWithQuery(string query, DataGrid dataGrid)
		{
			DataTable dataTable = SqlDataTable.GetFilledDataTableWithSqlDataAdapter(query);
			dataGrid.ItemsSource = dataTable.DefaultView;
		}
	}
}
