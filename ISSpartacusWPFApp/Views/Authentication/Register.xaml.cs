using ConfigurationLoader;
using DataAccessLibrary.Model;
using DataAccessLibrary.Repository;
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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            Configuration config = new Configuration();
            config.LoadFromJson("ConfigurationFile.json");
            AccountRepository accountRepo = new AccountRepository(config);

            string email = txtFullName.Text;
            string username = txtUsername.Text;
            string password = txtPassword.Password;
            string confirmPassword = txtConfirmPassword.Password;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                txtMessage.Text = "Please fill in all fields.";
                return;
            }

            if (password != confirmPassword)
            {
                txtMessage.Text = "Passwords do not match.";
                return;
            }


            Account toBeAdded = new Account(email, username, password, true);
            accountRepo.AddEntity(toBeAdded);
            txtMessage.Text = "Successful!";

        }

        private void ageCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
