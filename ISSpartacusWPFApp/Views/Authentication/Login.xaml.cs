using DataAccessLibrary.Modules;
using DataAccessLibrary.Repository;
using ISSpartacusWPFApp.Service;
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

namespace ISSpartacusWPFApp.Views.Authentication
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string email = txtUsername.Text;
            string password = txtPassword.Password;

            LoginService loginService = new LoginService();

            string result = loginService.ValidateLogin(email, password);

            if (result == "Success")
            {
                OpenUserInterface(loginService.AccountType);
            }
            else
            {
                MessageBox.Show(result, "Authentication problem", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenUserInterface(AccountType accountType)
        {
            if (accountType == AccountType.User)
            {
                ISSpartacusWPFApp.Views.User userView = new User();
                userView.Show();
            }
            else if (accountType == AccountType.Manager)
            {
                ISSpartacusWPFApp.Views.Manager managerView = new Manager();
                managerView.Show();
            }
            else if (accountType == AccountType.Employee)
            {
                ISSpartacusWPFApp.Views.Employee employeeView = new Employee();
                employeeView.Show();
            }
        }

    }
}
