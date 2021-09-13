using System.Data;
using System.Windows.Controls;

namespace Inventory.data
{
	public class SqlDataGrid
	{
		private readonly SqlDatabase _sqlDatabase;
		private SqlDatabase SqlDatabase
		{
			get => _sqlDatabase;
			init => _sqlDatabase = value;
		}

		public SqlDataGrid()
		{
			SqlDatabase = new SqlDatabase();
		}
		public void FillDataGridWithQuery(string query, DataGrid dataGrid)
		{
			DataTable dataTable = SqlDatabase.GetFilledDataTableWithSqlDataAdapter(query);
			dataGrid.ItemsSource = dataTable.DefaultView;
			SqlDatabase.Dispose();
		}
	}
}
