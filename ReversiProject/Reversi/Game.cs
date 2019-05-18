namespace n_Game
{
    using System;
    using n_Board;
    using n_Player;
    using n_Square;
    using n_UI;
    using Reversi;

    public class Game
    {
        private static Player[] m_Players = new Player[2];
        private ePlayAgainst WhoWillThePlayerPlayWith;
        private Board m_GameBoard;
        public static int m_MatrixSize;

        public static Player GetPlayer(int i_Player)
        {
            return m_Players[i_Player];
        }

        private enum ePlayAgainst
        {
            Player = 2,
            Computer = 1
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
            InitializeGame();
            if (WhoWillThePlayerPlayWith == ePlayAgainst.Computer)
            {
                m_Players[1] = new Player(Strings.computer_name, Square.eSquareColor.Black);
                m_Players[1].Computer = true;
                AskPlayerToChooseColor(m_Players);
            }
            else
            {
                m_Players[1] = new Player(UI.GetPlayerName(), Square.eSquareColor.Black);
            }

            StartPlaying();
            EndGame();
        }

        private void AskPlayerToChooseColor(Player[] m_Players)
        {
            if (UI.ChooseColor() == Square.eSquareColor.Black)
            {
                m_Players[0].Computer = true;
                m_Players[1].NameOfUser = m_Players[0].NameOfUser;
                m_Players[1].Computer = !true;
                m_Players[0].NameOfUser = Strings.computer_name;
            }
        }

        private void InitializeGame()
        {
            m_Players[0] = new Player(UI.GetPlayerName(), Square.eSquareColor.White);
            ////m_Players[0].Computer = true; // - make second player computer
            WhoWillThePlayerPlayWith = (ePlayAgainst)UI.GetChoicePlayAgainst();
        }

        private void SetBoard()
        {
            m_MatrixSize = UI.GetBoardSize();
            m_GameBoard = new Board(m_MatrixSize);
            m_GameBoard.PrintBoard();
        }

        public void StartPlaying()
        {
            SetBoard();
            bool v_isAvailableMoves;

            do
            {
                v_isAvailableMoves = !true;
                for (int i = 0; i < 2; i++)
                {
                    if (m_Players[i].CanMakeMove(m_GameBoard))
                    {
                        m_GameBoard.SquareBoard = m_Players[i].Play(m_GameBoard.SquareBoard);
                        v_isAvailableMoves = true;
                        m_GameBoard.PrintBoard();
                    }
                    else
                    {
                        UI.ShowError(string.Format(Strings.player_dont_have_available_moves, m_Players[i].NameOfUser));
                    }
                }
            }
            while (v_isAvailableMoves);

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
            if (counterBlack > counterWhite)
            {
                if(m_Players[0].PlayerColor == Square.eSquareColor.Black)
                {
                    winnerName = m_Players[0].NameOfUser;
                }
                else
                {
                    winnerName = m_Players[1].NameOfUser;
                }
            }
            else if (counterBlack < counterWhite)
            {
                if (m_Players[0].PlayerColor == Square.eSquareColor.Black)
                {
                    winnerName = m_Players[1].NameOfUser;
                }
                else
                {
                    winnerName = m_Players[0].NameOfUser;
                }
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
            if (UI.RestartGame())
            {
                StartPlaying();
            }
        }
    }
}