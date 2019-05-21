namespace n_UI
{
    using System;
    using System.Linq;
    using System.Text;
    using Ex02.ConsoleUtils;
    using Reversi;
    using n_Utilities;
    using n_Square;

    public class UI
    {
        public static readonly char sr_BlackPlayer = 'O', sr_WhitePlayer = 'X', sr_EmptySquare = ' ';
        public static readonly char sr_YesUpperCase = 'Y', sr_YesLowerCase = 'y', sr_FirstLetter = 'A';
        public static readonly char sr_HorizontalSaperatorLine = '=', sr_VerticalSaperatorLine = '|', sr_QuitGameKey = 'Q';

        private enum eHowToPrint
        {
            SameSign = 0,
            AscendingSign = 1
        }

        public static int GetChoicePlayAgainst()
        {
            int value = 0;

            Screen.Clear();
            PrintLine(Strings.play_against_option_computer_vs_computer);
            PrintLine(Strings.play_against_option_computer);
            PrintLine(Strings.play_against_option_player);

            while (!int.TryParse(Console.ReadLine(), out value) || (value < 1 || value > 4))
            {
                ShowError(Strings.error_enter_valid_integer);
            }

            return value - 1;
        }

        public static int GetBoardSize()
        {
            Screen.Clear();
            PrintLine(Strings.choose_size_of_board);

            int playerChoose = GetInteger();

            while (playerChoose != 8 && playerChoose != 4)
            {
                ShowError(Strings.board_size_invalid);
                playerChoose = GetInteger();
            }

            return playerChoose;
        }

        public static int GetInteger()
        {
            bool validation;
            int inputNumber;
            validation = int.TryParse(Console.ReadLine(), out inputNumber);

            while (!validation)
            {
                ShowError(Strings.error_enter_valid_integer);
                validation = int.TryParse(Console.ReadLine(), out inputNumber);
            }

            return inputNumber;
        }

        public static void ShowError(string i_ErrorText)
        {
            Console.WriteLine(i_ErrorText);
        }

        public static void PrintLine(string i_Text)
        {
            Console.WriteLine(i_Text);
        }

        public static Square GetPlayerChoice(int i_MatrixSize, string i_NameOfPlayer, out bool io_KeepPlaying)
        {
            string choose;
            char column;
            int row;
            Square square;

            PrintLine(string.Format(Strings.player_is_playing, i_NameOfPlayer));
            choose = Console.ReadLine();
            choose = choose.ToUpper();

            while (true)
            {
                if (choose.Length != 2)
                {
                    if (choose.Equals(sr_QuitGameKey.ToString()))
                    {
                        io_KeepPlaying = !true;
                        square = new Square();
                        break;
                    }
                    else
                    {
                        ShowError(Strings.error_enter_invalid_cord);
                    }
                }
                else
                {
                    column = choose.ElementAt(0);
                    row = choose.ElementAt(1) - '0' - 1;
                    if (Utilities.CheckIfRowRight(row) && Utilities.CheckIfColumnRight(column))
                    {
                        io_KeepPlaying = true;
                        square = new Square(column, row);
                        break;
                    }
                    else
                    {
                        ShowError(Strings.error_enter_invalid_cord);
                    }
                }

                choose = Console.ReadLine();
                choose = choose.ToUpper();
            }

            return square;
        }

        public static Square.eSquareColor ChooseColor()
        {
            int choose = 0;

            Screen.Clear();
            PrintLine(Strings.choose_color);

            while (!int.TryParse(Console.ReadLine(), out choose) || (choose != 1 && choose != 2))
            {
                ShowError(Strings.error_enter_valid_integer);
            }

            return (Square.eSquareColor)choose;
        }

        public static void QuitGameMessage()
        {
            Screen.Clear();
            PrintLine(Strings.player_quit_game);
        }

        public static string GetPlayerName()
        {
            PrintLine(Strings.enter_name);
            return Console.ReadLine();
        }

        public static void PrintMatrix(Square[,] i_board)
        {
            StringBuilder buildBoard = new StringBuilder();
            Screen.Clear();
            buildBoard.Append("   ");
            PrintRowOfSignsWithSpaces(i_board.GetLength(0), 3, sr_FirstLetter, eHowToPrint.AscendingSign, ref buildBoard);
            for (int i = 1; i <= i_board.GetLength(0); i++)
            {
                buildBoard.Append("  ");
                PrintRowOfSignsWithSpaces(i_board.GetLength(0) * 4, 0, sr_HorizontalSaperatorLine, eHowToPrint.SameSign, ref buildBoard);
                PrintRowOfMatrix(i_board, i, ref buildBoard);
            }

            buildBoard.Append("  ");
            PrintRowOfSignsWithSpaces(i_board.GetLength(0) * 4, 0, sr_HorizontalSaperatorLine, eHowToPrint.SameSign, ref buildBoard);
            PrintLine(buildBoard.ToString());
        }

        public static void PrintPointStatus(int i_CounterBlack, int i_CounterWhite)
        {
            n_Player.Player blackPlayer, whitePlayer;

            if (n_Game.Game.GetPlayer(0).PlayerColor == Square.eSquareColor.Black)
            {
                blackPlayer = n_Game.Game.GetPlayer(0);
                whitePlayer = n_Game.Game.GetPlayer(1);
            }
            else
            {
                blackPlayer = n_Game.Game.GetPlayer(1);
                whitePlayer = n_Game.Game.GetPlayer(0);
            }

            PrintLine(string.Format(Strings.player_score, blackPlayer.NameOfUser, i_CounterBlack));
            PrintLine(string.Format(Strings.player_score, whitePlayer.NameOfUser, i_CounterWhite));
        }

        private static void PrintRowOfSignsWithSpaces(int i_Length, int i_numOfSpaces, char i_sign, eHowToPrint i_isItSameSign, ref StringBuilder io_BuildBoard)
        {
            for (int i = 0; i < i_Length; i++)
            {
                io_BuildBoard.Append((char)(i_sign + ((int)i_isItSameSign * i)));
                for (int j = 0; j < i_numOfSpaces; j++)
                {
                    io_BuildBoard.Append(" ");
                }
            }

            io_BuildBoard.Append("\n");
        }

        public static bool RestartGame()
        {
            PrintLine(Strings.play_again);
            char key = Console.ReadKey().KeyChar;

            Screen.Clear();

            return (key == sr_YesUpperCase) || (key == sr_YesLowerCase);
        }

        private static void PrintRowOfMatrix(Square[,] i_board, int i_numberOfRow, ref StringBuilder io_BuildBoard)
        {
            char charToPrint;
            io_BuildBoard.Append(string.Format("{0} ", i_numberOfRow));
            for (int i = 0; i < i_board.GetLength(0); i++)
            {
                io_BuildBoard.Append(string.Format("{0} ", sr_VerticalSaperatorLine));
                if (i_board[i_numberOfRow - 1, i].Color == Square.eSquareColor.White)
                {
                    charToPrint = sr_WhitePlayer;
                }
                else if (i_board[i_numberOfRow - 1, i].Color == Square.eSquareColor.Black)
                {
                    charToPrint = sr_BlackPlayer;
                }
                else
                {
                    charToPrint = sr_EmptySquare;
                }

                io_BuildBoard.Append(string.Format("{0} ", charToPrint));
            }

            io_BuildBoard.Append(string.Format("{0}\n", sr_VerticalSaperatorLine));
        }
    }
}
