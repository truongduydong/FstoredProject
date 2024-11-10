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
    /// Interaction logic for MemberManagement.xaml
    /// </summary>
    public partial class MemberManagement : Window
    {
        private IMemberRepository _memberRepository = new MemberRepository();
        public MemberManagement()
        {
            InitializeComponent();
            LoadMemberList(); 
        }

        public void LoadMemberList()
        {
            lsvMembers.ItemsSource = _memberRepository.GetMembers();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtSelectedId.Text))
            {
                MessageBoxResult dialogResult = MessageBox.Show("This action can't be reversed", "Are you sure?", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    _memberRepository.DeleteMember(int.Parse(txtSelectedId.Text));
                    LoadMemberList();
                }
            }
            else
            {
                MessageBox.Show("Please select a member");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtSelectedId.Text))
            {
                MemberManagementPopup memberManagementPopup = new MemberManagementPopup(int.Parse(txtSelectedId.Text), this);
                memberManagementPopup.Show();
            }
            else
            {
                MessageBox.Show("Please select a member");
            }
            
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            MemberManagementPopup memberManagementPopup = new MemberManagementPopup(true, this);
            memberManagementPopup.Show();
        }
    }
}
