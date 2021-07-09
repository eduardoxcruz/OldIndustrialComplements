using System.Data.SqlClient;

namespace Inventory.database
{
	public class SqlDatabase
	{
		static string serverIp = "192.168.0.254";
		static string serverPort = "1433";
		static string databaseName = "Pruebas";
		static string integratedSecurity = "False";
		static string userId = "eduardo";
		static string password = "Cruz0320";
		static string multipleActiveResultSets = "True";

		static string databaseConnectionString = "Server=" + serverIp + "," + serverPort + ";Database=" + databaseName + ";Integrated Security=" + integratedSecurity + ";User Id=" + userId + ";Password=" + password + ";MultipleActiveResultSets=" + multipleActiveResultSets;
		public void StartSqlClient()
		{
			SqlConnection sqlDatabaseConnection = new SqlConnection(databaseConnectionString);
			sqlDatabaseConnection.Open();
		}
	}
}	
