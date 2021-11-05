namespace Inventory.data
{
	public static class Sql
	{
		public static readonly string ServerIp = 
			string.IsNullOrEmpty(Properties.Settings.Default.DatabaseIp) ? 
			"192.168.0.254" : 
			Properties.Settings.Default.DatabaseIp;
		public const string ServerPort = "1433";
		public const string DatabaseName = "inventario";
		public const string IntegratedSecurity = "False";
		public const string UserId = "sa";
		public const string Password = "Tlacua015";
		public const string MultipleActiveResultSets = "True";
		public const string ConnectionTimeoutInSeconds = "10";
		public const string ProductsTableName = "dbo.productos";
	}
}
