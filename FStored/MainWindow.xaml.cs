using DataAccess.Repository;
using Microsoft.Extensions.Configuration;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FStored
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IMemberRepository _memberRepository = new MemberRepository();
        IConfiguration _config;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ManageWindow manageWindow;
                var test = ConfirmInfo();
                switch (test)
                {
                    case 0:
                        var user = _memberRepository.GetMemberByEmail(txtUsername.Text);
                        manageWindow = new ManageWindow(user.MemberId);
                        manageWindow.Show();
                        this.Close();
                        break;
                    case 1:
                        manageWindow = new ManageWindow(true);
                        manageWindow.Show();
                        this.Close();

                        break;
                    default:
                        MessageBox.Show("Invalid username or password");
                        break;
                }
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

        private int ConfirmInfo()
        {
            var usernameInput = txtUsername.Text;
            var passwordInput = txtPassword.Password;
            try
            {
                var userInfo = _memberRepository.GetMemberByEmail(usernameInput);
                if (userInfo != null)
                {
                    if (userInfo.Password == passwordInput)
                    {
                        return 0;
                    }
                }
                if(usernameInput == GetAdminUsername() && passwordInput == GetAdminPassword())
                {
                    return 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return -1;
        }

        private string GetAdminUsername()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsetting.json", true, true).Build();
            return _config["admin:username"];
        }
        
        private string GetAdminPassword()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsetting.json", true, true).Build();
            return _config["admin:password"];
        }
    }
}
