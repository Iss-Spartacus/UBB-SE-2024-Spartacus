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
            string email = txtEmail.Text;
            string password = txtPassword.Password;

            if(Validator.ValidateEmail(email) == false)
            {
                MessageBox.Show("Invalid email", "Authentication problem", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(Validator.ValidatePassword(password) == false)
            {
                MessageBox.Show("Invalid password", "Authentication problem", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            ConfigurationLoader.Configuration config = new ConfigurationLoader.Configuration();
            config.LoadFromJson("ConfigurationFile.json");
            AccountRepository accountRepository = new AccountRepository(config);
            AccountService accountService = new AccountService(accountRepository);

            var accounts = accountService.GetAllEntitiesService();

            bool accountWasFound = false;
            DataAccessLibrary.Model.Account foundAccount;
            foreach(var account in accounts)
            {
                if(account.Email == email && account.Password == password)
                {
                    accountWasFound = true;
                    foundAccount = account;
                }
            }

            if(accountWasFound == true)
            {
                AccountType accountType = AccountVerifier.VerifyAccountType(email);

                if (accountType == AccountType.User)
                {
                    ISSpartacusWPFApp.Views.User userView = new User();
                    userView.Show();
                    return;
                }
                else if (accountType == AccountType.Manager)
                {
                    ISSpartacusWPFApp.Views.Manager managerView = new Manager();
                    managerView.Show();
                    return;
                }
                else if (accountType == AccountType.Employee)
                {
                    ISSpartacusWPFApp.Views.Employee employeeView = new Employee();
                    employeeView.Show();
                    return;
                }
            }
            else
            {
                MessageBox.Show("Invalid email and password combination", "Authentication problem", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
