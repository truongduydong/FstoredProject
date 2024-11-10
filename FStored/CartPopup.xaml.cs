using DataAccess;
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
    /// Interaction logic for CartPopup.xaml
    /// </summary>
    public partial class CartPopup : Window
    {
        Product product;
        int quantity;
        CartWindown cartWindown;
        int memberId;
        Dictionary<Product, int> cart;
        public CartPopup(int memberId, Dictionary<Product, int> cart, Product product, int quantity)
        {
            InitializeComponent();
            this.product = product;
            this.quantity = quantity;
            this.cart = cart;
            this.memberId = memberId;
            loadData();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                quantity = int.Parse(txtQuantity.Text.ToString());
                if (quantity < 0)
                {
                    throw new Exception();
                }
                this.Close();
                cartWindown = new CartWindown(memberId, cart, product, quantity);
                cartWindown.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Please input a valid number");
                return;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            cartWindown = new CartWindown(memberId, cart, product, quantity);
            cartWindown.Show();

        }
        void loadData()
        {
            txtProductId.Text = product.ProductId.ToString();
            txtProductName.Text = product.ProductName.ToString();
            txtWeight.Text = product.Weight.ToString();
            txtUnitPrice.Text = product.UnitPrice.ToString();
            txtQuantity.Text = quantity.ToString();
        }
    }
}
