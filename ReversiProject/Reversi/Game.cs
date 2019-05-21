namespace n_Game
{
    using System;
    using System.Diagnostics;
    using n_Board;
    using n_Player;
    using n_Square;
    using n_UI;
    using Reversi;

    public class Game
    {
        private static Player[] m_Players = new Player[2];
        private static readonly int sr_FirstAiPlayerCornerValue = 200, sr_FirstAiPlayerSideValue = 20, sr_FirstAiPlayerRestValue = 5, sr_FirstDepth = 6;
        private static readonly int sr_SecondAiPlayerCornerValue = 100, sr_SecondAiPlayerSideValue = 20, sr_SecondAiPlayerRestValue = 5, sr_SecondDepth = 6;
        private ePlayAgainst WhoWillThePlayerPlayWith;
        private Board m_GameBoard;
        public static int m_MatrixSize;

        public static Player GetPlayer(int i_Player)
        {
            return m_Players[i_Player];
        }

        private enum ePlayAgainst
        {
            PlayerVsPlayer = 2,
            PlayerVsComputer = 1,
            ComputerVsComputer = 0
        }

        public enum eWhichPlayer
        {
            First = 1,
            Second = 2
        }

        public static void QuitGame()
        {
            UI.QuitGameMessage();
            System.Environment.Exit(1);
        }

        public void PlayGame()
        {
            WhoWillThePlayerPlayWith = (ePlayAgainst)UI.GetChoicePlayAgainst();
            Square.eSquareColor firstColor, secondColor;
            AskPlayerToChooseColor(out firstColor, out secondColor);
            switch (WhoWillThePlayerPlayWith)
            {
                case ePlayAgainst.PlayerVsPlayer:
                    m_Players[0] = new Player(UI.GetPlayerName(), firstColor);
                    m_Players[1] = new Player(UI.GetPlayerName(), secondColor);
                    break;
                case ePlayAgainst.PlayerVsComputer:
                    if (firstColor == Square.eSquareColor.Black)
                    {
                        m_Players[0] = new Player(UI.GetPlayerName(), firstColor);
                        m_Players[1] = new AiPlayer(Strings.computer_name, secondColor,
                                                sr_FirstAiPlayerCornerValue, sr_FirstAiPlayerSideValue, sr_FirstAiPlayerRestValue, sr_FirstDepth);
                    }
                    else
                    {
                        m_Players[1] = new Player(UI.GetPlayerName(), firstColor);
                        m_Players[0] = new AiPlayer(Strings.computer_name, secondColor,
                                                sr_FirstAiPlayerCornerValue, sr_FirstAiPlayerSideValue, sr_FirstAiPlayerRestValue, sr_FirstDepth);
                    }
                    break;
                case ePlayAgainst.ComputerVsComputer:
                    m_Players[0] = new AiPlayer(Strings.first_computer_name, firstColor,
                        sr_FirstAiPlayerCornerValue, sr_FirstAiPlayerSideValue, sr_FirstAiPlayerRestValue, sr_FirstDepth);
                    m_Players[1] = new AiPlayer(Strings.second_computer_name, secondColor,
                        sr_SecondAiPlayerCornerValue, sr_SecondAiPlayerSideValue, sr_SecondAiPlayerRestValue, sr_SecondDepth);
                    break;
                default:
                    break;
            }
            SetBoard();
            Stopwatch sw;
            sw = Stopwatch.StartNew();
            StartPlaying();
            Console.WriteLine(sw.ElapsedMilliseconds);
            EndGame();
        }

        private void AskPlayerToChooseColor(out Square.eSquareColor i_First, out Square.eSquareColor i_Second)
        {
            if (UI.ChooseColor() == Square.eSquareColor.Black)
            {
                i_First = Square.eSquareColor.Black;
                i_Second = Square.eSquareColor.White;
            }
            else
            {
                i_First = Square.eSquareColor.White;
                i_Second = Square.eSquareColor.Black;
            }
        }

        private void SetBoard()
        {
            m_MatrixSize = UI.GetBoardSize();
            m_GameBoard = new Board(m_MatrixSize);
            m_GameBoard.PrintBoard();
        }

        public void StartPlaying()
        {
            bool isAvailableMoves, disableNoMoveErrorTwice = !true;

            do
            {
                isAvailableMoves = !true;
                for (int i = 0; i < 2; i++)
                {
                    if (m_Players[i].CanMakeMove(m_GameBoard))
                    {
                        m_GameBoard.SquareBoard = m_Players[i].Play(m_GameBoard.SquareBoard);
                        isAvailableMoves = true;
                        m_GameBoard.PrintBoard();
                        disableNoMoveErrorTwice = !true;
                    }
                    else
                    {
                        if (!disableNoMoveErrorTwice)
                        {
                            UI.ShowError(string.Format(Strings.player_dont_have_available_moves, m_Players[i].NameOfUser));
                            disableNoMoveErrorTwice = true;
                        }
                    }
                }
            }
            while (isAvailableMoves);

            DeclareWinner(CheckWhoWon());
        }

        private string CheckWhoWon()
        {
            int counterWhite = 0, counterBlack = 0;

            foreach (Square square in m_GameBoard.SquareBoard)
            {
                switch (square.Color)
                {
                    case Square.eSquareColor.White:
                        counterWhite++;
                        break;
                    case Square.eSquareColor.Black:
                        counterBlack++;
                        break;
                    default:
                        break;
                }
            }

            string winnerName;
            int playerNumber = 0;

            if (counterBlack > counterWhite)
            {
                playerNumber = m_Players[0].PlayerColor == Square.eSquareColor.Black ? 0 : 1;
            }
            else if (counterBlack < counterWhite)
            {
                playerNumber = m_Players[0].PlayerColor == Square.eSquareColor.Black ? 1 : 0;
            }
            else
            {
                playerNumber = -1;
            }

            if (playerNumber != -1)
            {
                winnerName = m_Players[playerNumber].NameOfUser;
            }
            else
            {
                winnerName = Strings.draw; // there is chance for draw?
            }

            UI.PrintPointStatus(counterBlack, counterWhite);

            return winnerName;
        }

        private void DeclareWinner(string i_PlayerName)
        {
            Console.WriteLine(string.Format(Strings.winner_declaration, i_PlayerName));
        }

        public void EndGame()
        {
            while (UI.RestartGame())
            {
                StartPlaying();
                m_GameBoard = new Board(m_MatrixSize);
                m_GameBoard.PrintBoard();
            }
        }
    }
}