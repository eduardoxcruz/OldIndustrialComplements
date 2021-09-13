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
	}
}
