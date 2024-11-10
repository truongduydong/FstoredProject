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
    /// Interaction logic for MemberManagementPopup.xaml
    /// </summary>
    public partial class MemberManagementPopup : Window
    {
        private bool _isAddOrUpdate;
        private int _memberId;
        IMemberRepository _memberRepository = new MemberRepository();
        MemberManagement _parentForm;

        public MemberManagementPopup(int memberId)
        {
            _isAddOrUpdate = false;
            _memberId = memberId;
            InitializeComponent();
            CheckAddOrUpdate();
        }

        public MemberManagementPopup(bool isAddOrUpdate, MemberManagement parentForm)
        {
            _isAddOrUpdate = isAddOrUpdate;
            _parentForm = parentForm;
            InitializeComponent();
            CheckAddOrUpdate();
        }

        public MemberManagementPopup(int memberId, MemberManagement parentForm)
        {
            _isAddOrUpdate = false;
            _memberId = memberId;
            _parentForm = parentForm;
            InitializeComponent();
            CheckAddOrUpdate();
        }

        private Member GetMemberInfor()
        {
            Member info = new Member {
                MemberId = int.Parse(txtMemberId.Text),
                Email = txtEmail.Text,
                City = txtCity.Text,
                Country = txtCountry.Text,
                CompanyName = txtCompanyName.Text,
                Password = txtPassword.Password
            };
            return info;
        }

        private void CheckAddOrUpdate()
        {
            txtMemberId.IsEnabled = _isAddOrUpdate;
            if (_isAddOrUpdate)
            {
                lbAdd_Update.Content = "Add";
                btnAdd_Update.Content = "Add";
            }
            else
            {
                var info = _memberRepository.GetMemberByID(_memberId);
                lbAdd_Update.Content = "Update";
                btnAdd_Update.Content = "Update";
                txtCity.Text = info.City;
                txtCompanyName.Text = info.CompanyName;
                txtCountry.Text = info.Country;
                txtEmail.Text = info.Email;
                txtPassword.Password = info.Password;
                txtMemberId.Text = info.MemberId.ToString();
            }
        }

        private void btnAdd_Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_isAddOrUpdate)
                {
                    _memberRepository.InsertMember(GetMemberInfor());
                    MessageBox.Show("Add success");
                }
                else
                {
                    _memberRepository.UpdateMember(GetMemberInfor());
                    MessageBox.Show("Update success");
                }
                if(_parentForm != null)
                {
                    _parentForm.LoadMemberList();
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
