using System.Data.SqlClient;

namespace Inventory.database
{
	public class SqlDatabase
	{
		private const string ServerIp = "192.168.0.254";
		private const string ServerPort = "1433";
		private const string DatabaseName = "Pruebas";
		private const string IntegratedSecurity = "False";
		private const string UserId = "eduardo";
		private const string Password = "Cruz0320";
		private const string MultipleActiveResultSets = "True";
		private string _databaseConnectionString;
		private SqlConnection _sqlDatabaseConnection;

		public SqlDatabase()
		{
			DatabaseConnectionString = "Server=" + ServerIp + "," + ServerPort + 
			                           ";Database=" + DatabaseName + 
			                           ";Integrated Security=" + IntegratedSecurity + 
			                           ";User Id=" + UserId + 
			                           ";Password=" + Password + 
			                           ";MultipleActiveResultSets=" + MultipleActiveResultSets;
		}
		public SqlDatabase(string databaseConnectionString)
		{
			DatabaseConnectionString = databaseConnectionString;
		}
		public string DatabaseConnectionString
		{
			get
			{
				return _databaseConnectionString;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					_databaseConnectionString = value;
				}
			}
		}
		private SqlConnection SqlDatabaseConnection
		{
			get
			{
				return _sqlDatabaseConnection;
			}
			set
			{
				_sqlDatabaseConnection = value;
			}
		}

		public SqlConnection StartSqlClient()
		{
			SqlConnection sqlDatabaseConnection = new SqlConnection(DatabaseConnectionString);
			//sqlDatabaseConnection.Open();
			return sqlDatabaseConnection;
		}
	}
}	
