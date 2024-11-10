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
    /// Interaction logic for OrderDetailPopup.xaml
    /// </summary>
    public partial class OrderDetailPopup : Window
    {
        private int _orderId, _productId;
        private bool _isAddOrUpdate;
        private IOrderDetailRepository _orderDetailRepository = new OrderDetailRepository();
        OrderDetailManagement _orderDetailManagement;
        public OrderDetailPopup(int orderId, int productId, OrderDetailManagement orderDetailManagement)
        {
            _orderId = orderId;
            _productId = productId;
            _isAddOrUpdate = false;
            InitializeComponent();
            SetUpdateForm();
        }

        public OrderDetailPopup(int orderId, OrderDetailManagement orderDetailManagement)
        {
            _isAddOrUpdate = true;
            _orderId = orderId;
            _orderDetailManagement = orderDetailManagement;
            InitializeComponent();
            SetAddForm();
        }

        private void SetUpdateForm()
        {
            try
            {
                var orderDetail = _orderDetailRepository.GetOrderDetail(_orderId, _productId);
                txtProductId.Text = orderDetail.ProductId.ToString();
                txtOrderId.Text = orderDetail.OrderId.ToString();
                txtQuantity.Text = orderDetail.Quantity.ToString();
                txtUnitPrice.Text = orderDetail.UnitPrice.ToString();
                txtDiscount.Text = orderDetail.Discount.ToString();
                txtProductId.IsEnabled = false;
                txtOrderId.IsEnabled = false;
                lbAdd_Update.Content = "Update";
                btnAdd_Update.Content = "Update";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private OrderDetail GetOrderDetail()
        {
            return new OrderDetail
            {
                OrderId = int.Parse(txtOrderId.Text),
                ProductId = int.Parse(txtProductId.Text),
                Discount = double.Parse(txtDiscount.Text),
                Quantity = int.Parse(txtQuantity.Text),
                UnitPrice = decimal.Parse(txtUnitPrice.Text)
            };
        }

        private void btnAdd_Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_isAddOrUpdate)
                {
                    _orderDetailRepository.Add(GetOrderDetail());
                }
                else
                {
                    _orderDetailRepository.Update(GetOrderDetail());
                }
                _orderDetailManagement.LoadOrderDetailList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SetAddForm()
        {
            try
            {
                txtProductId.IsEnabled = true;
                txtOrderId.IsEnabled = false;
                txtOrderId.Text = _orderId.ToString();
                btnAdd_Update.Content = "Add";
                lbAdd_Update.Content = "Add";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
