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
	}
}
