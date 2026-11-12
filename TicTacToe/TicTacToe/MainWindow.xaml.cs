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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private TicTacToeGame game = new TicTacToeGame();
        private bool vsComputer = false;

        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            foreach (Button button in gridBoard.Children.OfType<Button>())
            {
                button.Content = string.Empty;
                button.IsEnabled = true;
            }
            WinScreen.Visibility = Visibility.Collapsed;
            game.ResetBoard(); // Resetujemo samo tablu, rezultati ostaju sacuvani
            game.currentPlayer = "X"; // Pocetni igrac
        }



        private void btnVsComputer_Click(object sender, RoutedEventArgs e)
        {
            vsComputer = !vsComputer;
            (sender as Button).Content = vsComputer ? "Vs Player" : "Vs Computer";
            btnNewGame_Click(sender, e);
        }

        private void btnScores_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(game.GetScore(), "Scores");
        }

        private void PlayerClicksSpace(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button.Content.ToString() == string.Empty && !game.IsGameOver())
            {
                Position position = ParsePosition(button.Tag.ToString());
                string playerSymbol = game.currentPlayer;
                button.Content = playerSymbol;
                game.UpdateBoard(position, playerSymbol);

                if (game.CheckForWinner())
                {
                    UpdateScores(playerSymbol);
                    WinScreen.Text = $"{playerSymbol} wins!";
                    WinScreen.Visibility = Visibility.Visible;
                }
                else if (game.IsBoardFull())
                {
                    game.IncrementDraws();
                    WinScreen.Text = "It's a draw!";
                    WinScreen.Visibility = Visibility.Visible;
                }
                else
                {
                    if (vsComputer && game.currentPlayer == "X")
                    {
                        ComputerMove();
                    }
                    else
                    {
                        game.SetNextPlayer();
                    }
                }
            }
        }

        private void ComputerMove()
        {
            if (game.IsGameOver()) return;

            // Kompjuter uvijek igra sa "O"
            string computerSymbol = "O";

            // Pronadji slobodno dugme
            var availableButtons = gridBoard.Children
                .OfType<Button>()
                .Where(b => string.IsNullOrEmpty(b.Content?.ToString()))
                .ToList();

            if (availableButtons.Any())
            {
                Random random = new Random();
                Button button = availableButtons[random.Next(availableButtons.Count)];
                Position position = ParsePosition(button.Tag.ToString());
                button.Content = computerSymbol;
                game.UpdateBoard(position, computerSymbol);

                if (game.CheckForWinner())
                {
                    WinScreen.Text = $"{computerSymbol} wins!";
                    WinScreen.Visibility = Visibility.Visible;
                }
            }
        }

        private void UpdateScores(string winner)
        {
            if (winner == "X")
            {
                game.IncrementPlayerWins();
            }
            else if (winner == "O")
            {
                game.IncrementComputerWins();
            }
        }



        private Position ParsePosition(string tag)
        {
            var parts = tag.Split(',');
            return new Position { x = int.Parse(parts[0]), y = int.Parse(parts[1]) };
        }

    }
}
