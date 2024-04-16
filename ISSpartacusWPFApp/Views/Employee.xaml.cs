using ConfigurationLoader;
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

namespace ISSpartacusWPFApp.Views
{
    /// <summary>
    /// Interaction logic for Employee.xaml
    /// </summary>
    public partial class Employee : Window
    {
        public int EmployeeID { get; set; }
        public Employee(int employeeID)
        {
            InitializeComponent();
            EmployeeID = employeeID;    
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
            Configuration config = new Configuration();
            config.LoadFromJson("ConfigurationFile.json");
            MatchRepository matchRepository = new MatchRepository(config);
            MatchService matchService = new MatchService(matchRepository);

            int matchId = 7;
            MainFight fight = new MainFight(matchId, EmployeeID, matchService);
            fight.Show();
        }

        private void Buy_Weapon_Button_Click(object sender, RoutedEventArgs e)
        {
            Buy_Weapons weapon_page= new Buy_Weapons();
            weapon_page.Show();
        }
    }
}
