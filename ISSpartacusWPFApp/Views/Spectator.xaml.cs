using DataAccessLibrary.Model;
using DataAccessLibrary.Repository;
using ISSpartacusWPFApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Spectator.xaml
    /// </summary>
    public partial class Spectator : Window
    {
        private readonly Account _spectator;
        private readonly DataAccessLibrary.Model.Match _currentMatch;
        private DataAccessLibrary.Model.Employee firstFighter;
        private DataAccessLibrary.Model.Employee secondFighter;
        private double firstPlayerOdd;
        private double secondPlayerOdd;
        public Spectator(Account spectator, DataAccessLibrary.Model.Match currentMatch)
        {
            InitializeComponent();
            _spectator = spectator;
            _currentMatch = currentMatch;

            ConfigurationLoader.Configuration config = new ConfigurationLoader.Configuration();
            config.LoadFromJson("ConfigurationFile.json");
            var employeeRepository = new EmployeeRepository(config);
            var employeeService = new EmployeeService(employeeRepository);

            firstFighter = employeeService.GetEntityService(currentMatch.Employee1Id);
            secondFighter = employeeService.GetEntityService(currentMatch.Employee2Id);


            labelFirstPlayerName.Content = firstFighter.FullName;
            labelSecondPlayerName.Content = secondFighter.FullName;
            labelFirstPlayerLife.Content = "100HP";
            labelSecondPlayerLife.Content = "100HP";
            labelFirstPlayerPower.Content = firstFighter.Power;
            labelSecondPlayerPower.Content = secondFighter.Power;

            labelFirstPlayerOdd.Content = "1.25";
            labelSecondPlayerOdd.Content = "1.25";
        }

        private void betOnFirstPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            //Betting bet = new Betting(_spectator.Id, ;
        }

        private void betOnSecondPlayerButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NumericTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            // Regex to allow only numeric input
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
