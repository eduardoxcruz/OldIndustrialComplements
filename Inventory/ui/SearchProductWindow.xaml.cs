using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Inventory.data;

namespace Inventory.ui
{
	public partial class SearchProductWindow
	{
		public static readonly SearchProductWindow Instance = new();
		private SqlDataGrid SqlDataGrid { get; }

		private SearchProductWindow()
		{
			InitializeComponent();
			SqlDataGrid = new SqlDataGrid();
		}
		private void SearchProductWithQuickSearch(string text)
		{
			string query = "SELECT TOP 30 * FROM " + Sql.ProductsTableName + " WHERE id like('%" + text + "%') OR " +
			               "matricula like ('%" + text + "%') OR " +
			               "descripcion like ('%" + text + "%') OR " +
			               "contenedor like ('%" + text + "%') OR " +
			               "ubicacion like ('%" + text + "%') OR " +
			               "estado like ('%"+ text + "%') OR " +
			               "tecmon like ('%" + text + "%') OR " +
			               "encapsulado like ('%" + text + "%')";
			SqlDataGrid.FillDataGridWithQuery(query, DataGridProducts);
			TxtBoxCount.Text = DataGridProducts.Items.Count.ToString();
		}

		private void SearchProductWithFilters()
		{
			if (string.IsNullOrEmpty(TxtBoxId.Text) & string.IsNullOrEmpty(TxtBoxStatus.Text) & string.IsNullOrEmpty(TxtBoxEnrollment.Text) & string.IsNullOrEmpty(TxtBoxDescription.Text) & string.IsNullOrEmpty(TxtBoxMountingTechnology.Text) & string.IsNullOrEmpty(TxtBoxEncapsulation.Text) & string.IsNullOrEmpty(TxtBoxContainer.Text) & string.IsNullOrEmpty(TxtBoxLocation.Text) & string.IsNullOrEmpty(TxtBoxDebugCode.Text))
			{
				MessageBox.Show("Llene al menos un campo para buscar.");
				return;
			}

			string query = "SELECT * FROM " + Sql.ProductsTableName + " WHERE id like('%" + TxtBoxId.Text + "%') AND " +
			               "matricula like ('%" + TxtBoxEnrollment.Text + "%') AND " +
			               "descripcion like ('%" + TxtBoxDescription.Text + "%') AND " +
			               "contenedor like ('%" + TxtBoxContainer.Text + "%') AND " +
			               "ubicacion like ('%" + TxtBoxLocation.Text + "%') AND " +
			               "estado like ('%" + TxtBoxStatus.Text + "%') AND " +
			               "tecmon like ('%" + TxtBoxMountingTechnology.Text + "%')AND " +
			               "encapsulado like ('%" + TxtBoxEncapsulation.Text + "%')AND " +
			               "codigo like ('%" + TxtBoxDebugCode.Text + "%')";
			SqlDataGrid.FillDataGridWithQuery(query, DataGridProducts);
			TxtBoxCount.Text = DataGridProducts.Items.Count.ToString();
		}

		private void DataGridProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (DataGridProducts.ItemsSource == null)
			{
				return;
			}
			
			DataRowView selectedRow = (DataRowView)DataGridProducts.SelectedItems[0];
		
			ProductWindow.ShowProductDetailsInstance.BringWindowToFront(
				selectedRow.Row[0].ToString()
				);
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
		private void EnterPressed(object sender, KeyEventArgs e)
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

		private void BtnClean_Click(object sender, RoutedEventArgs e)
		{
			DataGridProducts.ItemsSource = null;
			TxtBoxId.Text = null;
			TxtBoxStatus.Text = null;
			TxtBoxEnrollment.Text = null;
			TxtBoxDescription.Text = null;
			TxtBoxMountingTechnology.Text = null;
			TxtBoxEncapsulation.Text = null;
			TxtBoxContainer.Text = null;
			TxtBoxLocation.Text = null;
			TxtBoxQuickSearch.Text = null;
			TxtBoxDebugCode.Text = null;
			TxtBoxCount.Text = "00";
		}

		private void BtnTool_Click(object sender, RoutedEventArgs e)
		{
			new SettingsWindow().Show();
		}

		private void BtnBuy_Click(object sender, RoutedEventArgs e)
		{
			new RequestsWindow().Show();
		}

		private void BtnExit_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void BtnPlus_Click(object sender, RoutedEventArgs e)
		{
			ProductWindow.AddNewProductInstance.BringWindowToFront();
		}

		private void AddProductToDataGrid_Click(object sender, RoutedEventArgs e)
		{
			DataRowView selectedRow = (DataRowView)DataGridProducts.SelectedItems[0];
			DataGridAddedProducts.Items.Add(selectedRow);
		}

		private void RequestProduct_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Click Derecho Solicitar");
		}

		private void BtnCleanDataGridAddedProducts_Click(object sender, RoutedEventArgs e)
		{
			DataGridAddedProducts.Items.Clear();
		}
	}
}
