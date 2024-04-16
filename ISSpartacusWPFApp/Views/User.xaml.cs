using DataAccessLibrary.Model;
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
    /// Interaction logic for User.xaml
    /// </summary>
    public partial class User : Window
    {
        private readonly Account _account;

        public User(Account account)
        {
            InitializeComponent();
            _account = account;
        }

        private void spectateButton_Click(object sender, RoutedEventArgs e)
        {
            // Get current on-going match
            ConfigurationLoader.Configuration config = new ConfigurationLoader.Configuration();
            config.LoadFromJson("ConfigurationFile.json");
            var matchRepository = new MatchRepository(config);
            var matchService = new MatchService(matchRepository);

            var currentMatch = matchService.GetOnGoingMatchService();
            if(currentMatch == null) 
            {
                MessageBox.Show("No on-going match for now", "Sorry", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            Spectator spectatorWindow = new Spectator(_account, currentMatch);
            spectatorWindow.Show();
        }
    }
}
