using System.Data;
using System.Windows.Controls;

namespace Inventory.data
{
	public class SqlDataGrid
	{
		private readonly SqlDatabase _sqlDatabase;
		private readonly SqlDataTable _sqlDataTable;
		private SqlDatabase SqlDatabase
		{
			get => _sqlDatabase;
			init => _sqlDatabase = value;
		}
		private SqlDataTable SqlDataTable
		{
			get => _sqlDataTable;
			init => _sqlDataTable = value;
		}

		public SqlDataGrid()
		{
			SqlDatabase = new SqlDatabase();
			SqlDataTable = new SqlDataTable();
		}
		public void FillDataGridWithQuery(string query, DataGrid dataGrid)
		{
			DataTable dataTable = SqlDatabase.GetFilledDataTableWithSqlDataAdapter(query);
			dataGrid.ItemsSource = dataTable.DefaultView;
			SqlDatabase.Dispose();
		}
	}
}
