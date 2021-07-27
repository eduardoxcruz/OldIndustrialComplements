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
		private void SearchProductWithQuickSearch(string text)
		{
			string query = "SELECT * FROM dbo.productos2 WHERE id like('" + text + "%') OR matricula like ('" + text + "%') OR descripcion like ('" + text + "%') OR contenedor like ('" + text + "%') OR ubicacion like ('" + text + "%') OR estado like ('"+ text + "%')";
			using SqlDatabase sqlDatabase = new SqlDatabase();
			sqlDatabase.FillDataGridWithQuery(query, DataGridProducts);
		}

		private void SearchProductWithFilters()
		{
			string query = "SELECT * FROM dbo.productos2 WHERE id like('" + TxtBoxId.Text + "%') AND matricula like ('" + TxtBoxEnrollment.Text + "%') AND descripcion like ('" + TxtBoxDescription.Text + "%') AND contenedor like ('" + TxtBoxContainer.Text + "%') AND ubicacion like ('" + TxtBoxLocation.Text + "%') AND estado like ('" + CmbBoxStatus.Text + "%')";
			using SqlDatabase sqlDatabase = new SqlDatabase();
			sqlDatabase.FillDataGridWithQuery(query, DataGridProducts);
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
			if (numeroDeCaracteres.Length > 2)
			{
				SearchProductWithQuickSearch(TxtBoxQuickSearch.Text);
			}
		}

		private void TxtBoxId_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				SearchProductWithFilters();
			}
		}

		private void TxtBoxEnrollment_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				SearchProductWithFilters();
			}
		}

		private void TxtBoxStatus_KeyDown(object sender, KeyEventArgs e)
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
				SearchProductWithFilters();
			}
		}

		private void TxtBoxContainer_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				SearchProductWithFilters();
			}
		}

		private void TxtBoxMountingTechnology_KeyDown(object sender, KeyEventArgs e)
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
				SearchProductWithFilters();
			}
		}

		private void TxtBoxDebugCode_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				SearchProductWithFilters();
			}
		}

		private void BtnSearch_Click(object sender, RoutedEventArgs e)
		{
			SearchProductWithFilters();
		}
	}
}
