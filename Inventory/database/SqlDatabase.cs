using System.Data.SqlClient;

namespace Inventory.database
{
	public class SqlDatabase
	{
		const string ServerIp = "192.168.0.254";
		const string ServerPort = "1433";
		const string DatabaseName = "Pruebas";
		const string IntegratedSecurity = "False";
		const string UserId = "eduardo";
		const string Password = "Cruz0320";
		const string MultipleActiveResultSets = "True";

		public const string databaseConnectionString = "Server=" + serverIp + "," + serverPort + ";Database=" + databaseName + ";Integrated Security=" + integratedSecurity + ";User Id=" + userId + ";Password=" + password + ";MultipleActiveResultSets=" + multipleActiveResultSets;
		public SqlConnection StartSqlClient()
		{
			SqlConnection sqlDatabaseConnection = new SqlConnection(databaseConnectionString);
			sqlDatabaseConnection.Open();
			return sqlDatabaseConnection;
		}
	}
}	
