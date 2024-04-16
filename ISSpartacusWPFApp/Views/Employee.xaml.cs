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

namespace ISSpartacusWPFApp.Views
{
    /// <summary>
    /// Interaction logic for Employee.xaml
    /// </summary>
    public partial class Employee : Window
    {
        public Employee()
        {
            InitializeComponent();
        }


        private void bribeManagerButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Play_Button_Click(object sender, RoutedEventArgs e)
        {
            //TODO if to check the employee is really in a fight
            MainFight fight = new MainFight();
            fight.Show();
        }
    }
}
