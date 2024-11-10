using DataAccess;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace FStored
{
    /// <summary>
    /// Interaction logic for OrderProduct.xaml
    /// </summary>
    public partial class OrderProduct : Window
    {
        IProductRepository _productRepository = new ProductRepository();
        Product product;
        Dictionary<Product, int> cart = new Dictionary<Product, int>();
        CartWindown cartWindown;
        int memberId;
        int quantity;
        public OrderProduct(int memberId)
        {
            InitializeComponent();
            LoadProductsList();
            this.memberId = memberId;
        }
        public OrderProduct(int memberId, Dictionary<Product, int> cart)
        {
            InitializeComponent();
            LoadProductsList();
            this.cart = cart;
            this.memberId = memberId;
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            if (!String.IsNullOrWhiteSpace(txtSelectedId.Text))
            {
                product = _productRepository.GetProductById(int.Parse(txtSelectedId.Text));
            }
            else
            {
                MessageBox.Show("Please select a product");
            }
            if (product != null)
            {
                if (cart.ContainsKey(product))
                {
                    quantity= cart[product]+1;
                    cart.Remove(product);
                    cart.Add(product, quantity);
                }
                else
                {
                    cart.Add(product, 1);
                }
                MessageBox.Show("Add success to your cart!");
            }
            else
            {
                MessageBox.Show("Please select a product");
            }

        }

        private void btnViewCart_Click(object sender, RoutedEventArgs e)
        {
            cartWindown = new CartWindown(memberId,cart);
            cartWindown.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
                    if (int.TryParse(txtUnitInStock.Text, out int parseUnitInStock))
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
        public void LoadProductsList()
        {
            lsvProducts.ItemsSource = _productRepository.GetProducts();
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
        private void LoadProductsWithConstraints(IEnumerable<Product> productList)
        {
            lsvProducts.ItemsSource = productList.ToList();
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


    }

}
