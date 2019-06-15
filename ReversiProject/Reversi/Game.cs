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
        public static int m_MatrixSize;

        public enum ePlayAgainst
        {
            PlayerVsPlayer = 2,
            PlayerVsComputer = 1
        }

        public enum eWhichPlayer
        {
            First = 1,
            Second = 2
        }
    }
}