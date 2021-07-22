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
			string query = "SELECT * FROM dbo.productos2";
			SqlDatabase sqlDatabase = new SqlDatabase();
			SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlDatabase.GetSqlCommandWithQuery(query));
			DataTable dataTable = new DataTable("dbo.productos2");
			dataAdapter.Fill(dataTable);
			DataGridProducts.ItemsSource = dataTable.DefaultView;
			dataAdapter.Update(dataTable);
		}

		private void DataGridProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
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
			string numeroDeCaracteres = TxtBoxQuickSearch.Text;
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

		private void CmbBoxStatus_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				//Busqueda
			}
		}

		private void TxtBoxDescription_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
			}
		}

		private void TxtBoxContainer_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
			}
		}

		private void CmbBoxMountingTechnology_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				//Busqueda
			}
		}

		private void CmbBoxEncapsulation_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				//Busqueda
			}
		}

		private void TxtBoxLocation_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
			}
		}

		private void TxtBoxDebugCode_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
			}
		}
	}
}
