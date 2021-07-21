using System;
using System.Data.SqlClient;

namespace Inventory.database
{
	public class SqlDatabase : IDisposable
	{
		private const string ServerIp = "192.168.0.254";
		private const string ServerPort = "1433";
		private const string DatabaseName = "Pruebas";
		private const string IntegratedSecurity = "False";
		private const string UserId = "eduardo";
		private const string Password = "Cruz0320";
		private const string MultipleActiveResultSets = "True";
		private string _databaseConnectionString;
		private SqlConnection _databaseConnection;
		public SqlDatabase()
		{
			DatabaseConnectionString = "Server=" + ServerIp + "," + ServerPort + 
			                           ";Database=" + DatabaseName + 
			                           ";Integrated Security=" + IntegratedSecurity + 
			                           ";User Id=" + UserId + 
			                           ";Password=" + Password + 
			                           ";MultipleActiveResultSets=" + MultipleActiveResultSets;
			DatabaseConnection = new SqlConnection(DatabaseConnectionString);
		}
		public SqlDatabase(string databaseConnectionString)
		{
			DatabaseConnectionString = databaseConnectionString;
			DatabaseConnection = new SqlConnection(DatabaseConnectionString);
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
		public SqlConnection DatabaseConnection
		{
			get
			{
				return _databaseConnection;
			}
			set
			{
				_databaseConnection = value;
			}
		}
		public void Dispose()
		{
			DatabaseConnection.Close();
		}
	}
}	
