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
    /// Interaction logic for OrderDetailManagement.xaml
    /// </summary>
    public partial class OrderDetailManagement : Window
    {
        IOrderDetailRepository _orderDetailRepository = new OrderDetailRepository();
        private int _orderId;
        private bool _isAdmin;
        public OrderDetailManagement(int orderId, bool isAdmin)
        {
            _orderId = orderId;
            InitializeComponent();
            if (!isAdmin)
            {
                CheckAdmin();
            }
            LoadOrderDetailList();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtOrderId.Text))
            {
                OrderDetailPopup orderDetailPopup = new OrderDetailPopup(_orderId, int.Parse(txtProductId.Text), this);
                orderDetailPopup.Show();
            }
            else
            {
                MessageBox.Show("Please select an order detail");
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            OrderDetailPopup orderDetailPopup = new OrderDetailPopup( _orderId,this);
            orderDetailPopup.Show();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            
            if (!String.IsNullOrWhiteSpace(txtOrderId.Text))
            {
                MessageBoxResult dialogResult = MessageBox.Show("This action can't be reversed", "Are you sure?", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    _orderDetailRepository.Delete(int.Parse(txtOrderId.Text), int.Parse(txtProductId.Text));
                    LoadOrderDetailList();
                }
            }
            else
            {
                MessageBox.Show("Please select an order detail");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void LoadOrderDetailList()
        {
            lsvOrderDetails.ItemsSource = _orderDetailRepository.GetOrderDetails(_orderId).ToList();
        }

        private void CheckAdmin()
        {
            btnUpdate.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnAdd.IsEnabled = false;
        }
    }
}
