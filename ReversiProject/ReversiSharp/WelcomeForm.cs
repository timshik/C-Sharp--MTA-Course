using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static n_Game.Game;

namespace ReversiSharp
{
    public partial class WelcomeForm : Form
    {
        private static readonly string m_BoardSizeText = "Board Size: {0}x{0} (Click to increase)";
        public static ePlayAgainst m_GameMode;
        public static int m_RoundCounter = 0, m_BlackWins = 0, m_WhiteWins = 0;

        public WelcomeForm()
        {
            InitializeComponent();
        }

        private void ButtonAgainstComputer_Click(object sender, EventArgs e)
        {
            m_GameMode = ePlayAgainst.PlayerVsComputer;
            PlayGame();
        }

        private void ButtonAgainstPlayer_Click(object sender, EventArgs e)
        {
            m_GameMode = ePlayAgainst.PlayerVsPlayer;
            PlayGame();
        }

        private void PlayGame()
        {
            this.Hide();
            Game gameForm = new Game();
            while (gameForm.KeepPlaying)
            {
                gameForm = new Game();
                gameForm.ShowDialog();
            }
        }

        private void ButtonBoardSize_Click(object sender, EventArgs e)
        {
            if (m_BoardSize == 12)
            {
                m_BoardSize = 6;
            }
            else
            {
                m_BoardSize += 2;
            }

            buttonBoardSize.Text = string.Format(m_BoardSizeText, m_BoardSize);
        }
    }
}
