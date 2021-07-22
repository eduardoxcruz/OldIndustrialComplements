using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
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

		private void LoadDataFromDatabaseToDataTable()
		{
			var query = "SELECT * FROM dbo.productos2";
			var sqlDatabase = new SqlDatabase();
			var dataAdapter = new SqlDataAdapter(sqlDatabase.GetSqlCommandWithQuery(query));
			var dataTable = new DataTable("dbo.productos2");
			dataAdapter.Fill(dataTable);
			DataGridProducts.ItemsSource = dataTable.DefaultView;
			dataAdapter.Update(dataTable);
		}

		private void DataGridProductos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var productWindow = new ProductWindow((int)ProductWindowTasks.ShowDetails);
			productWindow.Show();
		}

		private void TxtBoxId_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			var regex = new Regex("[^0-9]+");
			e.Handled = regex.IsMatch(e.Text);
		}

		private void TxtBoxQuickSearch_TextChanged(object sender, TextChangedEventArgs e)
		{
			var numeroDeCaracteres = TxtBoxQuickSearch.Text;
			if (numeroDeCaracteres.Length > 3)
			{
				MessageBox.Show("Nueva busqueda en base de datos #");
			}
		}

		private void TxtBoxId_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
			}
		}

		private void TxtBoxEnrollment_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
			}
		}
	}
}
