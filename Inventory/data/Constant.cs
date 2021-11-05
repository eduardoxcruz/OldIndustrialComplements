namespace Inventory.data
{
	public class Sql
	{
		private readonly string _serverIp = string.IsNullOrEmpty(Properties.Settings.Default.DatabaseIp) ? 
			"192.168.0.254" : 
			Properties.Settings.Default.DatabaseIp;
		private const string ServerPort = "1433";
		private const string DatabaseName = "inventario";
		private const string IntegratedSecurity = "False";
		private const string UserId = "sa";
		private const string Password = "Tlacua015";
		private const string MultipleActiveResultSets = "True";
		private const string ConnectionTimeoutInSeconds = "10";
	}
}
