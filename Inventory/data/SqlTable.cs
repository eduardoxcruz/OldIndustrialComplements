using System.Data;
using System.Windows.Controls;

namespace Inventory.data
{
	public class SqlTable
	{
		private readonly SqlDatabase _sqlDatabase;
		private SqlDatabase SqlDatabase
		{
			get => _sqlDatabase;
			set => _sqlDatabase = value;
		}
		public SqlTable()
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
