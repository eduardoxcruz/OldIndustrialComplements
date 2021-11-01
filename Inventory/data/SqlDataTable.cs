using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Inventory.data
{
	public class SqlDataTable
	{
		private SqlDatabase SqlDatabase { get; }
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
