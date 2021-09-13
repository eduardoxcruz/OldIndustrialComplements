using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Inventory.data
{
	public class SqlDataTable
	{
		private readonly SqlDatabase _sqlDatabase;
		private SqlDatabase SqlDatabase
		{
			get => _sqlDatabase;
			init => _sqlDatabase = value;
		}
		public SqlDataTable()
		{
			SqlDatabase = new SqlDatabase();
		}
		public DataTable GetFilledDataTableWithSqlDataAdapter(string query)
		{
			DataTable dataTable = new DataTable();
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(SqlDatabase.GetSqlCommandWithQuery(query));
			sqlDataAdapter.Fill(dataTable);
			SqlDatabase.Dispose();
			return dataTable;
		}
		public DataTable GetFilledDataTableWithSqlDataAdapter(string query, Dictionary<string, string> sqlCommandParams)
		{
			DataTable dataTable = new DataTable();
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(SqlDatabase.GetSqlCommandWithQuery(query, sqlCommandParams));
			sqlDataAdapter.Fill(dataTable);
			SqlDatabase.Dispose();
			return dataTable;
		}
	}
}
