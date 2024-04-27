using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.Design;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Channels;

namespace TicTacToeGame
{
    internal class Program
    {
        /*     Use a loop for horizontal and vertical checks: To check for horizontal and vertical wins, 
               you can use a single loop that iterates through the range of 0 to 2 (inclusive). 
               This way, you can efficiently compare the elements in the 2D array.

               Nested if conditions for horizontal and vertical checks: When checking for horizontal and vertical wins,
               you can use nested if conditions inside the loop. Make sure you compare the elements in the same 
               row for horizontal checks and in the same column for vertical checks.

               Comparing elements for a win: To determine a win in either horizontal or vertical cases,
               you need to check if the elements are equal. For example, for a horizontal win,
               you should compare board[i,0], board[i,1], and board[i,2] (where i is the row index).
               For a vertical win, compare board[0,i], board[1,i], and board[2,i] (where i is the column index).

               Diagonal checks: For diagonal checks, you don't need a loop, as there are only two possible diagonal wins.
               You can use two separate if conditions to check for these cases. Remember to compare the diagonal elements,
               such as board[0,0], board[1,1], and board[2,2] for one diagonal, and board[0,2], board[1,1], and board[2,0] for the other diagonal.

               Returning the result: If a winning condition is met in any of the cases (horizontal, vertical, or diagonal), 
               the Checker method should return true, indicating a winner. If none of the conditions are met after all checks are complete,
               the method should return false, indicating no winner.
        */
        

    static string[,] board = {{"1","2","3"},
                              {"4","5","6"},
                              {"7","8","9"}};


        
        static void Main(string[] args)
        {
            bool playAgain;
            string startingPlayer = "Player 1";
            PrintBoard(board);

            do
            {
                playAgain = PlayGame(startingPlayer);
                ResetBoard();
                // for changing player after each game
                startingPlayer = (startingPlayer == "Player 1") ? "Player 2" : "Player 1";

            } while (playAgain);
        }



        public static bool PlayGame(string startingPlayer)
        {
            
            bool gameOver = false;

            do
            {
                string input = GetValidInput(board, startingPlayer);
                board = InputBoard(board, input, startingPlayer);
                PrintBoard(board);

                if (Checker(board))
                {
                    gameOver = true;
                    Console.WriteLine($"{startingPlayer} wins!");
                }
                else if (IsBoardFull(board))
                {
                    gameOver = true;
                    Console.WriteLine("It's a draw!");
                }

                if (!gameOver)
                {   // for changing player after each move
                    startingPlayer = (startingPlayer == "Player 1") ? "Player 2" : "Player 1";
                }

            } while (!gameOver);

            Console.Write("Do you want to play again? (y/n): ");
            string playAgainInput = Console.ReadLine().ToLower();

            return playAgainInput == "y";
        }

        public static void ResetBoard()
        {
            string[,] boardInitial = {{"1","2","3"},
                                      {"4","5","6"},
                                      {"7","8","9"}};
            board = boardInitial;
            PrintBoard(board);
        }

        public static string GetValidInput(string[,] board, string playerName)
        {
            string input;

            do
            {
                Console.Write($"{playerName}: Choose your field! ");
                input = Console.ReadLine();

            } while (!InputManager(board, input));

            return input;
        }


        public static bool IsBoardFull(string[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] != "X" && board[i, j] != "O")
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        public static void PrintBoard(string[,] board)
        {
            Console.Clear();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Console.Write(" | ");
                    Console.Write($" {board[i, j]} ");
                }
                Console.Write(" | ");
                Console.WriteLine();

                if (i < 2)
                {
                    Console.WriteLine(" |-----|-----|-----|");

                }
            }

        }

        public static bool InputManager(string[,] board, string input)
        {

            if (Int32.TryParse(input, out int number) != true)
            {
                Console.WriteLine("Enter a number that matches non taken fields!");
                return false;
            }
            else if (int.Parse(input) < 1 || int.Parse(input) > 9)
            {
                Console.WriteLine("Number out of the bounds!!");
                return false;
            }
            else
            {
                for (int i = 0; i < board.GetLength(0); i++)
                {
                    for (int j = 0; j < board.GetLength(1); j++)
                    {
                        if (input == board[i, j])
                        {
                            if (board[i, j] == "X" || board[i, j] == "O")
                            {
                                Console.WriteLine("Field already used!!");
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                }
                Console.WriteLine("Invalid input! Please try again.");
                return false;
            }
        }

        public static string[,] InputBoard(string[,] board, string input, string player)
        {
            string playerSymbol;
            if (player == "Player 1")
            {
                playerSymbol = "O";
            }
            else
            {
                playerSymbol = "X";
            }
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (input == board[i, j])
                        board[i, j] = playerSymbol;

                }
            }

            return board;
        }

        public static bool Checker(string[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                {
                    return true;
                }
                else if (board[0, i] == board[1, i] && board[1, i] == board[2, i])
                {
                    return true;
                }
            }
            if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
            {
                return true;
            }
            else if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
            {
                return true;
            }
            return false;


        }


    }
}