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
    /// Interaction logic for ManageWindow.xaml
    /// </summary>
    public partial class ManageWindow : Window
    {
        private bool _isAdmin;
        private int _memberId;
        public ManageWindow(bool isAdmin)
        {
            _isAdmin = isAdmin;
            InitializeComponent();
            SetEnable();
        }
        
        public ManageWindow(int memberId)
        {
            _isAdmin = false;
            _memberId = memberId;
            InitializeComponent();
            SetEnable();
        }

        private void SetEnable()
        {
            btnMemberManagement.Content = _isAdmin ? "Member Management" : "View Profile";
            if (!_isAdmin)
            {
                btnProductManagement.Visibility = Visibility.Hidden;
            }
            else
            {
                btnOrder.Visibility = Visibility.Hidden;
            }
        }

        private void btnMemberManagement_Click(object sender, RoutedEventArgs e)
        {
            if (_isAdmin)
            {
                MemberManagement memberManagementWindow = new MemberManagement();
                memberManagementWindow.Show();
            }
            else
            {
                MemberManagementPopup memberManagementPopup = new MemberManagementPopup(_memberId);
                memberManagementPopup.Show();
            }
        }

        private void btnProductManagement_Click(object sender, RoutedEventArgs e)
        {
            ProductManagementWindow productManagementWindow = new ProductManagementWindow();
            productManagementWindow.Show();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void btnOrderManagement_Click(object sender, RoutedEventArgs e)
        {
            OrderManagement orderManagement;
            if (_isAdmin)
            {
                orderManagement = new OrderManagement();
            }
            else
            {
                orderManagement = new OrderManagement(_memberId);
            }
            orderManagement.Show();
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            OrderProduct orderProduct = new OrderProduct(_memberId);
            orderProduct.Show();
        }
    }
}
