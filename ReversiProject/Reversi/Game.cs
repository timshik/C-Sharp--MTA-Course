namespace n_Game
{
    using System;
    using System.Diagnostics;
    using n_Board;
    using n_Player;
    using n_Square;
    using Reversi;

    public class Game
    {
        private static Player[] m_Players = new Player[2];
        public static int m_MatrixSize;

        public static Player GetPlayer(int i_Player)
        {
            return m_Players[i_Player];
        }

        public enum ePlayAgainst
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
    }
}