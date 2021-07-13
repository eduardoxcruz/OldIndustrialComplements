using System.Data.SqlClient;

namespace Inventory.database
{
	public class SqlDatabase
	{
		const string serverIp = "192.168.0.254";
		const string serverPort = "1433";
		const string databaseName = "Pruebas";
		const string integratedSecurity = "False";
		const string userId = "eduardo";
		const string password = "Cruz0320";
		const string multipleActiveResultSets = "True";

		public const string databaseConnectionString = "Server=" + serverIp + "," + serverPort + ";Database=" + databaseName + ";Integrated Security=" + integratedSecurity + ";User Id=" + userId + ";Password=" + password + ";MultipleActiveResultSets=" + multipleActiveResultSets;
		public SqlConnection StartSqlClient()
		{
			SqlConnection sqlDatabaseConnection = new SqlConnection(databaseConnectionString);
			//sqlDatabaseConnection.Open();
			return sqlDatabaseConnection;
		}
	}
}	
