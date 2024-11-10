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
    /// Interaction logic for OrderPopup.xaml
    /// </summary>
    public partial class OrderPopup : Window
    {
        private bool _isAddOrUpdate;
        private bool _isAdmin;
        private int? _memberId;
        IOrderRepository _orderRepository = new OrderRepository();
        public OrderPopup(bool isAdmin, int? memberId)
        {
            _isAddOrUpdate = true;
            _isAdmin = isAdmin;
            _memberId = memberId;
            InitializeComponent();
            SetAddForm();
        }

        public OrderPopup(int orderId, bool isAdmin)
        {
            _isAddOrUpdate = false;
            _isAdmin = isAdmin;
            InitializeComponent();
            SetUpdateForm(orderId);
        }

        private void SetUpdateForm(int orderId)
        {
            try
            {
                lbAdd_Update.Content = "Update";
                btnAdd_Update.Content = "Update";
                var order = _orderRepository.GetOrderById(orderId);
                txtOrderId.Text = order.OrderId.ToString();
                txtFreight.Text = order.Freight.ToString();
                txtMemberId.Text = order.MemberId.ToString();
                txtOrderDate.SelectedDate = order.OrderDate;
                txtRequiredDate.SelectedDate = order.RequiredDate;
                txtShippedDate.SelectedDate = order.ShippedDate;

                txtMemberId.IsEnabled = _isAdmin;
                txtOrderId.IsEnabled = false;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void SetAddForm()
        {
            try
            {
                lbAdd_Update.Content = "Add";
                btnAdd_Update.Content = "Add";
                if (!_isAdmin)
                {
                    txtMemberId.IsEnabled = false;
                    if(_memberId != null)
                    {
                        txtMemberId.Text = _memberId.ToString();
                    }
                }
                else
                {
                    txtMemberId.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void btnAdd_Update_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
                var order = GetOrderInput();
                if (_isAddOrUpdate)
                {
                    _orderRepository.InsertOrder(order);
                }
                else
                {
                    _orderRepository.UpdateOrder(order);
                }
                MessageBox.Show("Success");
                this.Close();
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private Order GetOrderInput()
        {
            return new Order { MemberId = int.Parse(txtMemberId.Text),
                OrderId = int.Parse(txtOrderId.Text),
                Freight = decimal.Parse(txtFreight.Text),
                OrderDate = DateTime.Parse(txtOrderDate.Text),
                RequiredDate = string.IsNullOrWhiteSpace(txtRequiredDate.Text) ? null : DateTime.Parse(txtRequiredDate.Text),
                ShippedDate = string.IsNullOrWhiteSpace(txtShippedDate.Text) ? null : DateTime.Parse(txtShippedDate.Text)};
        }
    }
}
