namespace ReversiSharp
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using n_Board;
    using n_Player;
    using n_Square;
    using Reversi;

    public partial class Game : Form
    {
        private static readonly int m_StartPositionY = 132, m_StartPositionX = 83;
        private static int m_SquareSizeX, m_SquareSizeY;
        private static int m_SquareSize;
        private Player[] m_Players = new Player[2];
        private Board m_Board;
        private int m_CurrentPlayerIndex = 0;
        private bool m_KeepPlaying = true;

        public event PlayerListener OnCurrentTurn;

        public delegate LinkedList<Square> PlayerListener(Square i_PossibleSquare, bool i_ItIsPlayersMove, Board i_Board, out bool i_IsValid);

        public Game()
        {
            InitializeComponent();
        }

        public bool KeepPlaying
        {
            get { return m_KeepPlaying; }
        }

        private void backgroundPicture_MouseClick(object i_Sender, MouseEventArgs i_Mouse)
        {
            PictureBox sender = i_Sender as PictureBox;
            int coordinateX = sender.Location.X, coordinateY = sender.Location.Y,
                squareX = coordinateX - m_StartPositionX, squareY = coordinateY - m_StartPositionY;
            bool isValidSquare;

            if (squareX >= 0 && squareY >= 0)
            {
                squareX = squareX / m_SquareSizeX;
                squareY = squareY / m_SquareSizeY;
                if (squareX < WelcomeForm.m_BoardSize && squareY < WelcomeForm.m_BoardSize)
                {
                    OnCurrentTurn.Invoke(m_Board.SquareBoard[squareY, squareX], true, m_Board, out isValidSquare);
                    if (isValidSquare)
                    {
                        printChangedBoard(m_Board.SquareBoard);
                        changePlayers();
                        if (m_Players[m_CurrentPlayerIndex] is AiPlayer)
                        {
                            m_Board = ((AiPlayer)m_Players[1]).AlphaBetaPruning((Board)m_Board.Clone());
                            printChangedBoard(m_Board.SquareBoard);
                            changePlayers();
                        }

                        startPlaying(true);
                    }
                }
            }
        }

        private void changePlayers()
        {
            OnCurrentTurn -= m_Players[m_CurrentPlayerIndex].CheckIfSquareIsValidAndMakeMove;
            m_CurrentPlayerIndex = m_CurrentPlayerIndex == 0 ? 1 : 0;
            OnCurrentTurn += m_Players[m_CurrentPlayerIndex].CheckIfSquareIsValidAndMakeMove;
            setTitle(m_Players[m_CurrentPlayerIndex].NameOfUser);
        }

        private void printChangedBoard(Square[,] i_SquareBoard)
        {
            foreach (Square square in i_SquareBoard)
            {
                printSquare(getImageByColor(square.Color, !true), square.GetPoint);
            }
        }

        private void game_Load(object sender, EventArgs e)
        {
            Image backgroundImage = null;
            switch (WelcomeForm.m_BoardSize)
            {
                case 6:
                    backgroundImage = Properties.Resources.board6;
                    m_SquareSize = m_SquareSizeX = 70;
                    m_SquareSizeY = 77;
                    goto setBackgroundImage;
                case 8:
                    backgroundImage = Properties.Resources.board8;
                    m_SquareSize = 55;
                    m_SquareSizeX = 52;
                    m_SquareSizeY = 58;
                    goto setBackgroundImage;
                case 10:
                    backgroundImage = Properties.Resources.board10;
                    m_SquareSize = 44;
                    m_SquareSizeX = 42;
                    m_SquareSizeY = 46;
                    goto setBackgroundImage;
                case 12:
                    backgroundImage = Properties.Resources.board12;
                    m_SquareSize = m_SquareSizeX = 35;
                    m_SquareSizeY = 39;
                setBackgroundImage:
                    m_BackgroundPicture.Image = backgroundImage;
                    break;
            }

            resetGame();
        }

        private void startPlaying(bool i_IsFirstTime)
        {
            bool transparencyImage = true;
            LinkedList<Square> possibleMoves;

            possibleMoves = m_Players[m_CurrentPlayerIndex].ListOfPossibleMoves(m_Board);
            foreach (Square possibleSquare in possibleMoves)
            {
                printSquare(getImageByColor(m_Players[m_CurrentPlayerIndex].PlayerColor, transparencyImage), possibleSquare.GetPoint);
                m_Squares[possibleSquare.Row, possibleSquare.Column].MouseClick += backgroundPicture_MouseClick;
            }

            if (possibleMoves.Count == 0)
            {
                if (i_IsFirstTime)
                {
                    changePlayers();
                    startPlaying(!i_IsFirstTime);
                }
                else
                {
                    if (endGame())
                    {
                        OnCurrentTurn -= m_Players[m_CurrentPlayerIndex].CheckIfSquareIsValidAndMakeMove;
                        resetGame();
                    }
                    else
                    {
                        m_KeepPlaying = !true;
                    }
                }
            }
        }

        private void resetGame()
        {
            m_Players[0] = new Player(Strings.white_color, Square.eSquareColor.White);
            switch (WelcomeForm.m_GameMode)
            {
                case n_Game.Game.ePlayAgainst.PlayerVsPlayer:
                    m_Players[1] = new Player(Strings.black_color, Square.eSquareColor.Black);
                    break;
                case n_Game.Game.ePlayAgainst.PlayerVsComputer:
                    m_Players[1] = new AiPlayer(Strings.black_color, Square.eSquareColor.Black);
                    break;
            }

            setTitle(Strings.white_color);
            m_Squares = new PictureBox[WelcomeForm.m_BoardSize, WelcomeForm.m_BoardSize];
            m_Board = new Board(WelcomeForm.m_BoardSize);
            n_Game.Game.m_MatrixSize = WelcomeForm.m_BoardSize;
            setStartSquare();
            setPlayerColorMark();
            OnCurrentTurn += m_Players[m_CurrentPlayerIndex].CheckIfSquareIsValidAndMakeMove;
            printChangedBoard(m_Board.SquareBoard);
            startPlaying(true);
            m_KeepPlaying = true;
        }

        private void setPlayerColorMark()
        {
            int whitePlayerX = 84, blackPlayerX = whitePlayerX + 286;
            m_BlackPlayerMark.Image = Properties.Resources.blackPlayerMark;
            m_BlackPlayerMark.Size = new Size(131, 131);
            m_BlackPlayerMark.SizeMode = PictureBoxSizeMode.StretchImage;
            m_BlackPlayerMark.Location = new Point(blackPlayerX, 0);
            m_BlackPlayerMark.Visible = true;
            m_BlackPlayerMark.Parent = m_BackgroundPicture;
            m_BlackPlayerMark.BackColor = Color.Transparent;

            m_WhitePlayerMark.Image = Properties.Resources.whitePlayerMark;
            m_WhitePlayerMark.Size = new Size(131, 131);
            m_WhitePlayerMark.SizeMode = PictureBoxSizeMode.StretchImage;
            m_WhitePlayerMark.Location = new Point(whitePlayerX, 0);
            m_WhitePlayerMark.Visible = true;
            m_WhitePlayerMark.Parent = m_BackgroundPicture;
            m_WhitePlayerMark.BackColor = Color.Transparent;
        }

        private bool endGame()
        {
            StringBuilder message = new StringBuilder();
            int blackCounter, whiteCounter;
            bool anotherRound = !true;
            string winnerName = null;

            switch (checkWhoWon(ref m_Board.SquareBoard, out blackCounter, out whiteCounter))
            {
                case Square.eSquareColor.Empty:
                    message.Append(Strings.end_game_draw);
                    break;
                case Square.eSquareColor.White:
                    WelcomeForm.m_WhiteWins += 1;
                    winnerName = Strings.white_color;
                    goto winner;
                case Square.eSquareColor.Black:
                    WelcomeForm.m_BlackWins += 1;
                    winnerName = Strings.black_color;
                winner:
                    message.AppendFormat(
                        Strings.end_game_winner_string_1,
                        winnerName,
                        whiteCounter,
                        blackCounter,
                        WelcomeForm.m_WhiteWins,
                        WelcomeForm.m_BlackWins);
                    break;
            }

            if (WelcomeForm.m_RoundCounter < 4)
            {
                message.AppendFormat("{0}{1}", Environment.NewLine, Strings.end_game_winner_string_2);
                WelcomeForm.m_RoundCounter++;
            }

            DialogResult = MessageBox.Show(
                message.ToString(),
                Strings.game_basic_title,
                WelcomeForm.m_RoundCounter == 4 ? MessageBoxButtons.OK : MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (DialogResult == DialogResult.Yes)
            {
                anotherRound = true;
                foreach (PictureBox square in m_Squares)
                {
                    square.Dispose();
                }
            }

            return anotherRound;
        }

        private Square.eSquareColor checkWhoWon(ref Square[,] i_SquareBoard, out int o_BlackCounter, out int o_WhiteCounter)
        {
            o_WhiteCounter = o_BlackCounter = 0;
            Square.eSquareColor color;

            foreach (Square square in i_SquareBoard)
            {
                switch (square.Color)
                {
                    case Square.eSquareColor.White:
                        o_WhiteCounter++;
                        break;
                    case Square.eSquareColor.Black:
                        o_BlackCounter++;
                        break;
                }
            }

            if (o_BlackCounter == o_WhiteCounter)
            {
                color = Square.eSquareColor.Empty;
            }
            else
            {
                color = o_BlackCounter > o_WhiteCounter ? Square.eSquareColor.Black : Square.eSquareColor.White;
            }

            return color;
        }

        private Image getImageByColor(Square.eSquareColor i_PlayerColor, bool i_TransparencyImage)
        {
            Image image = null;
            if (i_TransparencyImage)
            {
                image = Properties.Resources.possibleMoveSquare;
            }
            else
            {
                switch (i_PlayerColor)
                {
                    case Square.eSquareColor.White:
                        image = Properties.Resources.whitePlayer;
                        break;
                    case Square.eSquareColor.Black:
                        image = Properties.Resources.blackPlayer;
                        break;
                    case Square.eSquareColor.Empty:
                        image = Properties.Resources.emptySquare;
                        break;
                }
            }

            return image;
        }

        private void game_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                m_KeepPlaying = !true;
            }
        }

        private void setStartSquare()
        {
            printSquare(Properties.Resources.whitePlayer, new Point(WelcomeForm.m_BoardSize / 2, WelcomeForm.m_BoardSize / 2));
            printSquare(Properties.Resources.blackPlayer, new Point((WelcomeForm.m_BoardSize / 2) - 1, WelcomeForm.m_BoardSize / 2));
            printSquare(Properties.Resources.blackPlayer, new Point(WelcomeForm.m_BoardSize / 2, (WelcomeForm.m_BoardSize / 2) - 1));
            printSquare(Properties.Resources.whitePlayer, new Point((WelcomeForm.m_BoardSize / 2) - 1, (WelcomeForm.m_BoardSize / 2) - 1));
        }

        private void setTitle(string m_PlayerColor)
        {
            Text = string.Format(Reversi.Strings.form_title_turn, m_PlayerColor);
        }

        private void printSquare(Image i_SquareImage, Point i_Position)
        {
            PictureBox square;
            square = m_Squares[i_Position.Y, i_Position.X];
            if (square == null)
            {
                m_Squares[i_Position.Y, i_Position.X] = new PictureBox();
                square = m_Squares[i_Position.Y, i_Position.X];
                square.Image = i_SquareImage;
                square.Size = new Size(m_SquareSize, m_SquareSize);
                square.SizeMode = PictureBoxSizeMode.StretchImage;
                square.Location = new Point(
                    m_StartPositionX + (i_Position.X * m_SquareSizeX),
                    m_StartPositionY + (i_Position.Y * m_SquareSizeY));
                this.Controls.Add(square);
                square.Visible = true;
                square.Parent = m_BackgroundPicture;
                square.BackColor = Color.Transparent;
            }
            else
            {
                square.Image = i_SquareImage;
                square.Refresh();
            }
        }
    }
}