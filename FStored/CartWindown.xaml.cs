using DataAccess;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace FStored
{
    /// <summary>
    /// Interaction logic for CartWindown.xaml
    /// </summary>
    public partial class CartWindown : Window
    {
        IProductRepository _productRepository = new ProductRepository();
        IOrderDetailRepository _orderDetailRepository = new OrderDetailRepository();
        IOrderRepository _orderRepository = new OrderRepository();
        Product product;
        Order order;
        OrderDetail orderDetail;
        int memberId;
        Dictionary<Product, int> cart;
        OrderProduct orderProduct;
        CartPopup popup;
        public CartWindown(int memberId, Dictionary<Product, int> cart)
        {
            InitializeComponent();
            this.memberId = memberId;
            this.cart = new Dictionary<Product, int>();
            this.cart = cart;
            LoadListView(cart);
        }
        public CartWindown(int memberId, Dictionary<Product, int> cart, Product product, int quantity)
        {
            InitializeComponent();
            this.memberId = memberId;
            this.cart = new Dictionary<Product, int>();
            this.cart = cart;
            this.product = _productRepository.GetProductById(product.ProductId);
            if (quantity == 0)
            {
                cart.Remove(product);
            }
            else
            {
                cart.Remove(product);
                cart.Add(product, quantity);
            }
            LoadListView(cart);
        }


        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            if (cart != null)
            {
                List<Product> products = _productRepository.GetProducts().ToList();
                List<Order> orderlist = _orderRepository.GetOrders().ToList();
                order = new Order()
                {
                    OrderId = orderlist.Last().OrderId + 1,
                    MemberId = memberId,
                    OrderDate = DateTime.Now
                };
                _orderRepository.InsertOrder(order);
                foreach (var item in cart)
                {
                    product = item.Key;
                    orderDetail = new OrderDetail()
                    {
                        OrderId = order.OrderId,
                        ProductId = item.Key.ProductId,
                        UnitPrice = item.Key.UnitPrice,
                        Quantity = item.Value,
                        Discount = 0
                    };
                    if (orderDetail.Quantity <= product.UnitslnStock)
                    {
                        product.UnitslnStock -= item.Value;
                        _productRepository.Update(product);
                        _orderDetailRepository.Add(orderDetail);
                    }
                    else
                    {
                        MessageBox.Show("Not enough item for your order!");
                    }
                }
                MessageBox.Show("Pay successfully!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Nothing in your cart!");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtSelectedId.Text))
            {
                product = _productRepository.GetProductById(int.Parse(txtSelectedId.Text));
                popup = new CartPopup(memberId, cart, product, cart[product]);
                this.Close();
                popup.Show();
            }
            else
            {
                MessageBox.Show("Please select a product");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            orderProduct = new OrderProduct(memberId, cart);
            orderProduct.Show();
            this.Close();
        }
        void LoadListView(Dictionary<Product, int> cart)
        {
            if (cart.Count == 0)
            {
                lsvProducts.ItemsSource = new Dictionary<Product, int>();
            }
            else
            {
                lsvProducts.ItemsSource = cart;
            }
        }


    }
}
