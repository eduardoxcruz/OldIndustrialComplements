using System;
using System.Data.SqlClient;

namespace Inventory.database
{
	public class SqlDatabase : IDisposable
	{
		private const string ServerIp = "192.168.0.254";
		private const string ServerPort = "1433";
		private const string DatabaseName = "pruebas";
		private const string IntegratedSecurity = "False";
		private const string UserId = "eduardo";
		private const string Password = "Cruz0320";
		private const string MultipleActiveResultSets = "True";
		private const string ConnectionTimeoutInSeconds = "10";
		private string _databaseConnectionString;
		private SqlConnection _databaseConnection;
		public SqlDatabase()
		{
			DatabaseConnectionString = "Server=" + ServerIp + "," + ServerPort + 
			                           ";Database=" + DatabaseName + 
			                           ";Integrated Security=" + IntegratedSecurity + 
			                           ";User Id=" + UserId + 
			                           ";Password=" + Password + 
			                           ";MultipleActiveResultSets=" + MultipleActiveResultSets + 
			                           ";Connection Timeout=" + ConnectionTimeoutInSeconds;
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
		public SqlDataReader Read(string query)
		{
			SqlCommand sqlCommand = new SqlCommand(query, DatabaseConnection);
			DatabaseConnection.Open();
			SqlDataReader dataReader = sqlCommand.ExecuteReader();
			return dataReader;
		}
		public void Dispose()
		{
			DatabaseConnection.Close();
		}
	}
}	
