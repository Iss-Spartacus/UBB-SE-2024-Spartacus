﻿using ConfigurationLoader;
using DataAccessLibrary.Model;
using DataAccessLibrary.Repository;
using ISSpartacusWPFApp.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Interaction logic for Manager.xaml
    /// </summary>
    public partial class Manager : Window
    {
        private EmployeeService employeeService;
        DataAccessLibrary.Model.Employee fighterOne;
        DataAccessLibrary.Model.Employee fighterTwo;
        private readonly MatchService matchService;

        public Manager()
        {
            InitializeComponent();
            ConfigurationLoader.Configuration config = new ConfigurationLoader.Configuration();
            config.LoadFromJson("ConfigurationFile.json");
            employeeService = new EmployeeService(new EmployeeRepository(config));
            matchService = new MatchService(new MatchRepository(config));
            PopulateFighterComboBoxes();
        }

        private void PopulateFighterComboBoxes()
        {
            IEnumerable<DataAccessLibrary.Model.Employee> allEmployees = employeeService.GetAllEntitiesService();

            // Set the ItemsSource of the ComboBox to the list of Employee objects
            fighterOneComboBox.ItemsSource = allEmployees;
            fighterTwoComboBox.ItemsSource = allEmployees;

            // Set the DisplayMemberPath to the property you want to display (FullName in this case)
            fighterOneComboBox.DisplayMemberPath = "FullName";
            fighterTwoComboBox.DisplayMemberPath = "FullName";
        }
        
        private void spectateButton_Click(object sender, RoutedEventArgs e)
        {
            Spectator spectatorWindow = new Spectator();
            spectatorWindow.Show();
        }

        private void startFightButton_Click(object sender, RoutedEventArgs e)
        {
            if (fighterOneComboBox.SelectedItem != null && fighterTwoComboBox.SelectedItem != null)
            {
                fighterOne = (DataAccessLibrary.Model.Employee)fighterOneComboBox.SelectedItem;
                fighterTwo = (DataAccessLibrary.Model.Employee)fighterTwoComboBox.SelectedItem;
                
                if(fighterOne == fighterTwo)
                {
                    MessageBox.Show("Please select different fighters.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                 Match newMatch = new Match(tournamentId: 1,
                                             employee1Id: fighterOne.Id,
                                             employee2Id: fighterTwo.Id,
                                             registrationDate: DateTime.Now,
                                             winnerId: fighterOne.Id);

                  int newMatchId = matchService.AddEntityService(newMatch);

                  if (newMatchId > 0)
                  {
                      MessageBox.Show("Match added successfully.");
                      fighterOne = null;
                      fighterTwo = null;
                      fighterOneComboBox.SelectedItem = null;
                      fighterTwoComboBox.SelectedItem = null;
                  }

                  else
                  {
                      MessageBox.Show("Failed to add match to the database.");
                  }
                  

            }
            else
            {
                MessageBox.Show("Please select fighters.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
