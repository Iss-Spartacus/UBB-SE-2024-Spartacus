using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Threading;
using DataAccessLibrary.Model;
using DataAccessLibrary.Repository;
using ISSpartacusWPFApp.Service;

namespace ISSpartacusWPFApp.Views
{
    public partial class Spectator : Window, INotifyPropertyChanged
    {
        private readonly Account _spectator;
        private readonly DataAccessLibrary.Model.Match _currentMatch;
        private DataAccessLibrary.Model.Employee firstFighter;
        private DataAccessLibrary.Model.Employee secondFighter;
        private DispatcherTimer _timer;

        private int _firstPlayerHP;
        private int _secondPlayerHP;

        public int FirstPlayerHP
        {
            get { return _firstPlayerHP; }
            set
            {
                _firstPlayerHP = value;
                OnPropertyChanged(nameof(FirstPlayerHP));
            }
        }

        public int SecondPlayerHP
        {
            get { return _secondPlayerHP; }
            set
            {
                _secondPlayerHP = value;
                OnPropertyChanged(nameof(SecondPlayerHP));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Spectator(Account spectator, DataAccessLibrary.Model.Match currentMatch)
        {
            InitializeComponent();
            _spectator = spectator;
            _currentMatch = currentMatch;

            // Load configuration and services
            LoadConfiguration();

            // Initialize UI bindings
            this.DataContext = this;

            // Setup and start the update timer
            SetupTimer();
        }

        private void LoadConfiguration()
        {
            ConfigurationLoader.Configuration config = new ConfigurationLoader.Configuration();
            config.LoadFromJson("ConfigurationFile.json");
            var employeeRepository = new EmployeeRepository(config);
            var employeeService = new EmployeeService(employeeRepository);

            Trace.WriteLine(_currentMatch.Employee1Id);


            firstFighter = employeeService.GetEntityService(_currentMatch.Employee1Id);
            secondFighter = employeeService.GetEntityService(_currentMatch.Employee2Id);

            var hpValues = MatchState.GetHP(_currentMatch.Id);
            FirstPlayerHP = hpValues.Player1HP;
            SecondPlayerHP = hpValues.Player2HP;

            labelFirstPlayerName.Content = firstFighter.FullName;
            labelSecondPlayerName.Content = secondFighter.FullName;
            labelFirstPlayerPower.Content = $"{firstFighter.Power} POWER";
            labelSecondPlayerPower.Content = $"{secondFighter.Power} POWER";

            labelCotaFirstPlayer.Content = "1.25";
            labelCotaSecondPlayer.Content = "1.25";
        }

        private void SetupTimer()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var hpValues = MatchState.GetHP(_currentMatch.Id);
            FirstPlayerHP = hpValues.Player1HP;
            SecondPlayerHP = hpValues.Player2HP;
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void betOnFirstPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            // Betting logic
        }

        private void betOnSecondPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            // Betting logic
        }

        private void NumericTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            // Regex to allow only numeric input
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
