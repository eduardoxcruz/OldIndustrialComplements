using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using Inventory.database;

namespace Inventory.ui
{
	public partial class SearchProductWindow : Window
	{
		public SearchProductWindow()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			var query = "SELECT * FROM dbo.productos2";
			var conexion = new SqlDatabase().StartSqlClient();
			var comando = new SqlCommand(query, conexion);
			conexion.Open();

			var dataAdapter = new SqlDataAdapter(comando);
			var dataTable = new DataTable("dbo.productos2");
			dataAdapter.Fill(dataTable);
			DataGridProductos.ItemsSource = dataTable.DefaultView;
			dataAdapter.Update(dataTable);
		}


		private void DataGridProductos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var productWindow = new ProductWindow((int)ProductWindowTasks.ShowDetails);
			productWindow.Show();
		}
	}
}
