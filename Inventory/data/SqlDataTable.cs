namespace Inventory.data
{
	public class SqlDataTable
	{
		private readonly SqlDatabase _sqlDatabase;
		private SqlDatabase SqlDatabase
		{
			get => _sqlDatabase;
			init => _sqlDatabase = value;
		}
		public SqlDataTable()
		{
			SqlDatabase = new SqlDatabase();
		}
	}
}
