using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Inventory.data
{
	public class SqlDatabase : IDisposable
	{
		private string _databaseConnectionString;
		private SqlConnection _databaseConnection;
		private string DatabaseConnectionString
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
		
		public SqlDatabase()
		{
			DatabaseConnectionString = "Server=" + Sql.ServerIp + "," + Sql.ServerPort + 
			                           ";Database=" + Sql.DatabaseName + 
			                           ";Integrated Security=" + Sql.IntegratedSecurity + 
			                           ";User Id=" + Sql.UserId + 
			                           ";Password=" + Sql.Password + 
			                           ";MultipleActiveResultSets=" + Sql.MultipleActiveResultSets + 
			                           ";Connection Timeout=" + Sql.ConnectionTimeoutInSeconds;
			DatabaseConnection = new SqlConnection(DatabaseConnectionString);
		}
		public SqlDataReader Read(string query)
		{
			return GetSqlCommandWithQuery(query).ExecuteReader();
		}
		public SqlDataReader Read(string query, Dictionary<string,string> sqlCommandParams)
		{
			return GetSqlCommandWithQuery(query, sqlCommandParams).ExecuteReader();
		}
		public SqlCommand GetSqlCommandWithQuery(string query)
		{
			DatabaseConnection.Open();
			return new SqlCommand(query, DatabaseConnection);
		}
		public SqlCommand GetSqlCommandWithQuery(string query, Dictionary<string,string> sqlCommandParams)
		{
			DatabaseConnection.Open();
			SqlCommand sqlCommand = new SqlCommand(query, DatabaseConnection);
			foreach (KeyValuePair<string, string> parameter in sqlCommandParams)
			{
				sqlCommand.Parameters.AddWithValue(parameter.Key, parameter.Value);
			}
			return sqlCommand;
		}
		public void Dispose()
		{
			DatabaseConnection.Close();
		}
	}
}	
