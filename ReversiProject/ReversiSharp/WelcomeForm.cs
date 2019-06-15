namespace ReversiSharp
{
    using System;
    using System.Windows.Forms;
    using Reversi;
    using static n_Game.Game;

    public partial class WelcomeForm : Form
    {
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

            buttonBoardSize.Text = string.Format(Strings.choose_board_size, m_BoardSize);
        }
    }
}