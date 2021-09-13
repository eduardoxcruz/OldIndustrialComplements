using System.Data;
using System.Windows.Controls;

namespace Inventory.data
{
	public class Table
	{
		private SqlDatabase _sqlDatabase;
		private SqlDatabase SqlDatabase
		{
			get => _sqlDatabase;
			set => _sqlDatabase = value;
		}
		public Table()
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
