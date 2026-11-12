using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class TicTacToeGame
    {
        public string currentPlayer { get; set; } = X;
        private const string X = "X";
        private const string O = "O";
        private string[,] board = new string[3, 3];

        private bool gameOver;

        private int playerWins;
        private int computerWins;
        private int draws;

        public void SetNextPlayer()
        {
            if(currentPlayer == X)
            {
                currentPlayer = O;
            }
            else
            {
                currentPlayer = X;
            }
        }

        public bool CheckForWinner()
        {
            //provjeravamo horizontalno
            for(var i = 0; i < 3; i++)
            {
                if (!string.IsNullOrWhiteSpace(board[i, 0]))
                {
                    if(board[i,0] == board[i,1] && board[i,0] == board[i, 2])
                    {
                        return true;
                    }
                }
            }

            //provjeravamo vertikalno
            for (var i = 0; i < 3; i++)
            {
                if (!string.IsNullOrWhiteSpace(board[0, i]))
                {
                    if (board[0, i] == board[1, i] && board[0, i] == board[2, i])
                    {
                        return true;
                    }
                }
            }

            //provjeravamo dijagonalno
                if (!string.IsNullOrWhiteSpace(board[1, 1]))
                {
                    if (board[0, 0] == board[1, 1] && board[1,1] == board[2, 2])
                    {
                        return true;
                    }

                    if(board[0,2] == board[1,1] && board[1,1] == board[2,0])
                    {
                    return true;
                    }
                }   
            return false;
        }

        internal void UpdateBoard(Position position,string value)
        {
            board[position.x, position.y] = value;
        }


        public bool IsBoardFull()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (string.IsNullOrEmpty(board[i, j]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool IsGameOver()
        {
            if (CheckForWinner() || IsBoardFull())
            {
                gameOver = true;

                if (CheckForWinner())
                {
                    if (currentPlayer == "X") playerWins++;
                    else computerWins++;
                }
                else
                {
                    draws++;
                }
            }
            return gameOver;
        }


        public string GetGameResult()
        {
            if (CheckForWinner())
            {
                return $"Player {currentPlayer} wins!";
            }
            else if (IsBoardFull())
            {
                return "It's a draw!";
            }
            return string.Empty;
        }


        public void IncrementPlayerWins()
        {
            playerWins++;
        }

        public void IncrementComputerWins()
        {
            computerWins++;
        }

        public void IncrementDraws()
        {
            draws++;
        }

        public void ResetBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = string.Empty;
                }
            }
            currentPlayer = "X"; // Resetujemo trenutnog igrača na X
        }

        public string GetScore()
        {
            return $"Player X Wins: {playerWins}\nPlayer O Wins: {computerWins}\nDraws: {draws}";
        }



    }
}
