using DataAccessLibrary.Model;
using ISSpartacusWPFApp.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for MainFight.xaml
    /// </summary>
    public partial class MainFight : Window
    {
        private readonly int matchId;
        private int EmployeeID;
        private bool isPlayer1 = false;
        private int player1HP = 100;
        private int player2HP = 100;

        private readonly MatchService matchService;

        public MainFight(int matchId, int EmployeeID, MatchService matchService)
        {
            InitializeComponent();
            this.matchService = matchService;
            this.matchId = matchId;
            this.EmployeeID = EmployeeID;
            LoadPlayerNames();
            updateTheId();
            checkPlayer1();


            EventAggregator.OnPlayerHPChanged += OnPlayerHPChangedHandler;
        }

        private void OnPlayerHPChangedHandler(PlayerUpdateEventArgs args)
        {
            // Update the UI elements only if the event is for this match
            if (this.matchId == args.MatchId)
            {
                Dispatcher.Invoke(() =>
                {
                    labelFirstPlayerHPValue.Content = args.Player1HP;
                    labelSecondPlayerHPValue.Content = args.Player2HP;
                });
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            EventAggregator.OnPlayerHPChanged -= OnPlayerHPChangedHandler; // Unsubscribe to prevent memory leaks
        }


        private void checkPlayer1() {
            var match = GetMatchFromDatabase(matchId);
            if (EmployeeID == match.Employee1Id)
            {
                isPlayer1 = true;
            }
            
        }


        private void LoadPlayerNames()
        {
            var match = GetMatchFromDatabase(matchId);
            if (match != null)
            {
                var firstPlayerUsername = GetUsernameFromDatabase(match.Employee1Id);
                var secondPlayerUsername = GetUsernameFromDatabase(match.Employee2Id);

                labelFirstPlayerName.Content = firstPlayerUsername;
                labelSecondPlayerName.Content = secondPlayerUsername;
            }
        }

        private void updateTheId()
        {
            Trace.WriteLine(EmployeeID);
            EmployeeID = matchService.accountIdFromEmployeeId(EmployeeID);
            Trace.WriteLine(EmployeeID);
        }

        private DataAccessLibrary.Model.Match GetMatchFromDatabase(int id)
        {
            return matchService.GetMatchById(id);

        }

        private string GetUsernameFromDatabase(int employeeId)
        {
            return matchService.GetEmployeeFullNameForMatch(employeeId);
        }


        private void buttonWeakHit_Click(object sender, RoutedEventArgs e)
        {
            var match = GetMatchFromDatabase(matchId);
            if (match != null)
            {
                var firstPlayerUsername = GetUsernameFromDatabase(match.Employee1Id);
                var secondPlayerUsername = GetUsernameFromDatabase(match.Employee2Id);
            }
            int damage = 10;
            Random random = new Random();
            int chance = random.Next(1, 101);

            var (currentPlayer1HP, currentPlayer2HP) = MatchState.GetHP(matchId);


            bool player2turn = matchService.getTurn(matchId);

            if (isPlayer1 && !player2turn)
            {
                if (chance <= 80){
                    currentPlayer2HP -= damage;
                    AddMessage($"{labelFirstPlayerName.Content} deals {damage} damage");
                }
                else AddMessage(labelFirstPlayerName.Content + " misses");
                matchService.flipTurn(matchId);
            } else
            {
                if (!isPlayer1 && player2turn)
                {
                    if (chance <= 80)
                    {
                        currentPlayer1HP -= damage;
                        AddMessage($"{labelSecondPlayerName.Content} deals {damage} damage");
                    }
                    else AddMessage(labelSecondPlayerName.Content + " misses");
                    matchService.flipTurn(matchId);
                }
            }
            MatchState.SetHP(matchId, currentPlayer1HP, currentPlayer2HP); // Update the central state
            UpdateHP(currentPlayer1HP, currentPlayer2HP); // Update UI via event aggregator
        }

        private void buttonMediumHit_Click(object sender, RoutedEventArgs e)
        {
            var match = GetMatchFromDatabase(matchId);
            if (match != null)
            {
                var firstPlayerUsername = GetUsernameFromDatabase(match.Employee1Id);
                var secondPlayerUsername = GetUsernameFromDatabase(match.Employee2Id);
            }
            int damage = 20;
            Random random = new Random();
            int chance = random.Next(1, 101);

            var (currentPlayer1HP, currentPlayer2HP) = MatchState.GetHP(matchId);


            bool player2turn = matchService.getTurn(matchId);

            if (isPlayer1 && !player2turn)
            {
                if (chance <= 50)
                {
                    currentPlayer2HP -= damage;
                    AddMessage($"{labelFirstPlayerName.Content} deals {damage} damage");
                }
                else AddMessage(labelFirstPlayerName.Content + " misses");
                matchService.flipTurn(matchId);
            }
            else
            {
                if (!isPlayer1 && player2turn)
                {
                    if (chance <= 50)
                    {
                        currentPlayer1HP -= damage;
                        AddMessage($"{labelSecondPlayerName.Content} deals {damage} damage");
                    }
                    else AddMessage(labelSecondPlayerName.Content + " misses");
                    matchService.flipTurn(matchId);
                }
            }
            MatchState.SetHP(matchId, currentPlayer1HP, currentPlayer2HP); // Update the central state
            UpdateHP(currentPlayer1HP, currentPlayer2HP); // Update UI via event aggregator

        }

        private void buttonPowerfulHit_Click(object sender, RoutedEventArgs e)
        {
            var match = GetMatchFromDatabase(matchId);
            if (match != null)
            {
                var firstPlayerUsername = GetUsernameFromDatabase(match.Employee1Id);
                var secondPlayerUsername = GetUsernameFromDatabase(match.Employee2Id);
            }
            int damage = 30;
            Random random = new Random();
            int chance = random.Next(1, 101);
            var (currentPlayer1HP, currentPlayer2HP) = MatchState.GetHP(matchId);


            bool player2turn = matchService.getTurn(matchId);

            if (isPlayer1 && !player2turn)
            {
                if (chance <= 30)
                {
                    currentPlayer2HP -= damage;
                    AddMessage($"{labelFirstPlayerName.Content} deals {damage} damage");

                } else AddMessage(labelFirstPlayerName.Content + " misses");
                matchService.flipTurn(matchId);
            }
            else
            {
                if (!isPlayer1 && player2turn)
                {
                    if (chance <= 30)
                    {
                        currentPlayer1HP -= damage;
                        AddMessage($"{labelSecondPlayerName.Content} deals {damage} damage");
                    } else AddMessage(labelSecondPlayerName.Content + " misses");

                    matchService.flipTurn(matchId);
                }

            }
            MatchState.SetHP(matchId, currentPlayer1HP, currentPlayer2HP); // Update the central state
            UpdateHP(currentPlayer1HP, currentPlayer2HP); // Update UI via event aggregator

        }

        private void AddMessage(string message)
        {
            listBoxMessages.Items.Add(message);
            listBoxMessages.ScrollIntoView(listBoxMessages.Items[listBoxMessages.Items.Count - 1]); // Scrolls to the last item
        }


        private void UpdateHP(int player1HP, int player2HP)
        {
            EventAggregator.RaisePlayerHPChanged(new PlayerUpdateEventArgs
            {
                Player1HP = player1HP,
                Player2HP = player2HP,
                MatchId = this.matchId
            });

            if (player1HP <= 0 || player2HP <= 0)
            {
                string winner = player1HP <= 0 ? labelSecondPlayerName.Content.ToString() : labelFirstPlayerName.Content.ToString();
                matchService.updateWinner(matchId, EmployeeID);
                MessageBox.Show($"{winner} WINS!", "Game Over", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close(); // Optionally close the fight window
            }
        }

    }
}
