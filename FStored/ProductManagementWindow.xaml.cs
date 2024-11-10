using DataAccess;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FStored
{
    /// <summary>
    /// Interaction logic for ProductManagementWindow.xaml
    /// </summary>
    public partial class ProductManagementWindow : Window
    {
        IProductRepository _productRepository = new ProductRepository();
        public ProductManagementWindow()
        {
            InitializeComponent();
            LoadProductsList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ProductManagementPopup productManagementPopup = new ProductManagementPopup(this);
            productManagementPopup.Show();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtSelectedId.Text))
            {
                ProductManagementPopup productManagementPopup = new ProductManagementPopup(int.Parse(txtSelectedId.Text), this);
                productManagementPopup.Show();
            }
            else
            {
                MessageBox.Show("Please select a product");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtSelectedId.Text))
            {
                MessageBox.Show("Please select the product you want to delete");
            }
            else
            {
                MessageBoxResult dialogResult = MessageBox.Show("This action can't be reversed", "Are you sure?", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    _productRepository.Delete(int.Parse(txtSelectedId.Text));
                    LoadProductsList();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void LoadProductsList()
        {
            lsvProducts.ItemsSource = _productRepository.GetProducts();
        }

        private void LoadProductsWithConstraints(IEnumerable<Product> productList)
        {
            lsvProducts.ItemsSource = productList.ToList();
        }

        private IEnumerable<Product> SearchProductsByPrice(decimal min, decimal max)
        {
            var result = new List<Product>();
            try
            {
                result = _productRepository.GetProductByPrice(min, max).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during finding section!");
            }
            return result;
        }

        private IEnumerable<Product> SearchProductsByName(string name)
        {
            var result = new List<Product>();
            try
            {
                result = _productRepository.GetProductByName(name).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during finding section!");
            }
            return result;
        }

        private void btnSearchByUnitPrice_Click(object sender, RoutedEventArgs e)
        {
            decimal minPriceInput = 0;
            decimal maxPriceInput = 9999999999999;
            if (!String.IsNullOrWhiteSpace(txtMinPriceInput.Text))
            {
                minPriceInput = decimal.Parse(txtMinPriceInput.Text);
            }
            if (!String.IsNullOrWhiteSpace(txtMaxPriceInput.Text))
            {
                maxPriceInput = decimal.Parse(txtMaxPriceInput.Text);
            }
            try
            {
                if (minPriceInput > maxPriceInput)
                {
                    MessageBox.Show("Invalid input");
                }
                else
                {
                    var products = SearchProductsByPrice(minPriceInput, maxPriceInput);
                    LoadProductsWithConstraints(products);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private IEnumerable<Product> SearchProductById(int id)
        {
            var result = new List<Product>();
            try
            {
                var product = _productRepository.GetProductById(id);
                if (product != null)
                {
                    result.Add(product);
                }
                else
                {
                    MessageBox.Show("Can't find");
                    LoadProductsList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return result;
        }

        private IEnumerable<Product> SearchProductByUnitInStock(int unitInStock)
        {
            var result = new List<Product>();
            try
            {
                result = _productRepository.GetProductsByUnitInStock(unitInStock).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during finding by quantity");
            } 
            return result;
        }

        private void btnSearchByName_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(txtProductIdInput.Text))
                {
                    if (int.TryParse(txtProductIdInput.Text, out int parsedId))
                    {
                        var products = SearchProductById(int.Parse(txtProductIdInput.Text));
                        LoadProductsWithConstraints(products);
                    }
                    else
                    {
                        MessageBox.Show("Invalid id input");
                    }
                }
                else if (!String.IsNullOrWhiteSpace(txtProductNameInput.Text))
                {
                    var products = SearchProductsByName(txtProductNameInput.Text);
                    LoadProductsWithConstraints(products);
                }
                else if (!String.IsNullOrWhiteSpace(txtUnitInStock.Text))
                {
                    if(int.TryParse(txtUnitInStock.Text, out int parseUnitInStock))
                    {
                        var products = SearchProductByUnitInStock(int.Parse(txtUnitInStock.Text));
                        LoadProductsWithConstraints(products);
                    }
                    else
                    {
                        MessageBox.Show("Invalid quantity input");
                    }
                }
                else
                {
                    LoadProductsList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
