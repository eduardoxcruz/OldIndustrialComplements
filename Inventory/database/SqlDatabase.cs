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

		public const string DatabaseConnectionString = "Server=" + ServerIp + "," + ServerPort + ";Database=" + DatabaseName + ";Integrated Security=" + IntegratedSecurity + ";User Id=" + UserId + ";Password=" + Password + ";MultipleActiveResultSets=" + MultipleActiveResultSets;
		public SqlConnection StartSqlClient()
		{
			SqlConnection sqlDatabaseConnection = new SqlConnection(DatabaseConnectionString);
			//sqlDatabaseConnection.Open();
			return sqlDatabaseConnection;
		}
	}
}	
