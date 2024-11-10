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
    /// Interaction logic for OrderManagement.xaml
    /// </summary>
    public partial class OrderManagement : Window
    {
        private int _memberId;
        private bool _isAdmin;
        private IOrderRepository _orderRepository = new OrderRepository();
        private IMemberRepository _memberRepository = new MemberRepository();

        public OrderManagement()
        {
            _isAdmin = true;
            InitializeComponent();
            LoadOrderList();
            CheckAdmin();
        }
        
        public OrderManagement(int memberId)
        {
            _memberId = memberId;
            _isAdmin = false;
            InitializeComponent();
            LoadOrderListForUser(memberId);
            CheckAdmin();
        }

        public void LoadOrderList()
        {
            lsvOrder.ItemsSource = _orderRepository.GetOrders();
        }

        public void LoadOrderListForUser(int memberId)
        {
            var result = _memberRepository.GetMemberByID(memberId).Orders.ToList();
            lsvOrder.ItemsSource = result;
        }

        private void lsvOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtSelectedId.Text))
                {
                    OrderDetailManagement orderDetailManagement = new OrderDetailManagement(int.Parse(txtSelectedId.Text), _isAdmin);
                    orderDetailManagement.Show();
                }
                else
                {
                    throw new Exception("Please select an order");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            OrderPopup orderPopup = new OrderPopup(_isAdmin, _memberId);
            orderPopup.Show();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtSelectedId.Text))
            {
                OrderPopup orderPopup = new OrderPopup(int.Parse(txtSelectedId.Text), _isAdmin);
                orderPopup.Show();
            }
            else
            {
                MessageBox.Show("Please select an order");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtSelectedId.Text))
            {
                MessageBox.Show("Please select a order you want to delete");
            }
            else
            {
                MessageBoxResult dialogResult = MessageBox.Show("This may cause delete some your order details. Are you sure?","This action can't be reversed", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    _orderRepository.DeleteOrder(int.Parse(txtSelectedId.Text));
                    if (_isAdmin)
                    {
                        LoadOrderList();
                    }
                    else
                    {
                        LoadOrderListForUser(_memberId);
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CheckAdmin()
        {
            if (!_isAdmin)
            {
                btnAdd.IsEnabled = false;
                btnDelete.IsEnabled = false;
                btnUpdate.IsEnabled = false;
            }
        }
    }
}
