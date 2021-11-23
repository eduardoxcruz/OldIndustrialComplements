using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Inventory.data;
using Microsoft.EntityFrameworkCore;

namespace Inventory.ui
{
	public partial class SearchProductWindow
	{
		public static readonly SearchProductWindow Instance = new();

		private SearchProductWindow()
		{
			InitializeComponent();
		}
		private void SearchProductWithQuickSearch(string text)
		{
			using InventoryDbContext inventoryDb = new InventoryDbContext();
			DataGridProducts.ItemsSource =
				inventoryDb
					.Products
					.Where(product => product.Id.Equals(text) ||
					                  product.DebugCode.Contains(text) ||
					                  product.Status.Contains(text) ||
					                  product.Enrollment.Contains(text) ||
					                  product.MountingTechnology.Contains(text) ||
					                  product.EncapsulationType.Contains(text) ||
					                  product.ShortDescription.Contains(text) ||
					                  product.Category.Contains(text) ||
					                  product.Container.Contains(text) ||
					                  product.Location.Contains(text) ||
					                  product.Manufacturer.Contains(text) ||
					                  product.PartNumber.Contains(text) ||
					                  product.TypeOfStock.Contains(text) ||
					                  product.Memo.Contains(text))
					.ToList();
			TxtBoxCount.Text = DataGridProducts.Items.Count.ToString();
		}

		private void SearchProductWithFilters()
		{
			if (string.IsNullOrEmpty(TxtBoxId.Text) & 
			    string.IsNullOrEmpty(TxtBoxStatus.Text) & 
			    string.IsNullOrEmpty(TxtBoxEnrollment.Text) & 
			    string.IsNullOrEmpty(TxtBoxDescription.Text) & 
			    string.IsNullOrEmpty(TxtBoxMountingTechnology.Text) & 
			    string.IsNullOrEmpty(TxtBoxEncapsulation.Text) & 
			    string.IsNullOrEmpty(TxtBoxContainer.Text) & 
			    string.IsNullOrEmpty(TxtBoxLocation.Text) & 
			    string.IsNullOrEmpty(TxtBoxDebugCode.Text))
			{
				MessageBox.Show("Llene al menos un campo para buscar.");
				return;
			}

			using InventoryDbContext inventoryDb = new InventoryDbContext();

			DataGridProducts.ItemsSource = inventoryDb.Products
				.FromSqlRaw("SELECT * FROM dbo.Products WHERE Id like('%" + TxtBoxId.Text + "%') AND " +
				            "Enrollment like ('%" + TxtBoxEnrollment.Text + "%') AND " +
				            "ShortDescription like ('%" + TxtBoxDescription.Text + "%') AND " +
				            "Container like ('%" + TxtBoxContainer.Text + "%') AND " +
				            "Location like ('%" + TxtBoxLocation.Text + "%') AND " +
				            "Status like ('%" + TxtBoxStatus.Text + "%') AND " +
				            "MountingTechnology like ('%" + TxtBoxMountingTechnology.Text + "%')AND " +
				            "EncapsulationType like ('%" + TxtBoxEncapsulation.Text + "%')AND " +
				            "DebugCode like ('%" + TxtBoxDebugCode.Text + "%')")
				.ToList();
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
			Regex regex = new Regex("[^0-9]+");
			e.Handled = regex.IsMatch(e.Text);
		}

		private void TxtBoxQuickSearch_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (TxtBoxQuickSearch.Text.Length > 2)
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
			SettingsWindow.Instance.BringWindowToFront();
		}

		private void BtnBuy_Click(object sender, RoutedEventArgs e)
		{
			RequestsWindow.Instance.BringWindowToFront();
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
