namespace n_Player
{
    using System.Collections.Generic;
    using n_Square;
    using n_UI;
    using n_Game;
    using n_Board;
    using Reversi;

    public class Player
    {
        private static readonly bool sr_MakeMove = true;
        private string m_NameOfUser;
        protected Square.eSquareColor m_PlayerColor;

        public Square.eSquareColor PlayerColor
        {
            get { return m_PlayerColor; }
            set { m_PlayerColor = value; }
        }

        public Player(string i_NameOfUser, Square.eSquareColor i_PlayerColor)
        {
            m_NameOfUser = i_NameOfUser;
            m_PlayerColor = i_PlayerColor;
        }

        public string NameOfUser
        {
            get { return m_NameOfUser; }
            set { m_NameOfUser = value; }
        }

        public bool CanMakeMove(Board i_GameBoard)
        {
            bool validation = !true;
            Square[,] squareBoard = i_GameBoard.SquareBoard;
            foreach (Square square in squareBoard)
            {
                if (square.Color == Square.eSquareColor.Empty)
                {
                    if (square.CheckIfSquareIsValidAndMakeMove((Game.eWhichPlayer)m_PlayerColor, !sr_MakeMove, i_GameBoard.SquareBoard))
                    {
                        validation = true;
                        break;
                    }
                }
            }

            return validation;
        }

        public Square[,] MakeMove(Square[,] i_Board, Square i_ChosenSquare, Square i_DestinationSquare, int i_JumpColumn, int i_JumpRow)
        {
            for (int i = i_ChosenSquare.Row, j = i_ChosenSquare.Column - UI.sr_FirstLetter;
                !((i == i_DestinationSquare.Row) && (j == i_DestinationSquare.Column - UI.sr_FirstLetter)); i += i_JumpRow, j += i_JumpColumn)
            {
                i_Board[i, j].Color = i_ChosenSquare.Color;
            }

            return i_Board;
        }

        private void PlayerTurn(Game.eWhichPlayer i_WhichPlayer, Square[,] i_Board)
        {
            Square userChoice;
            bool v_ChoseValidSquare = true, v_keepPlaying = true;

            do
            {
                if (!v_ChoseValidSquare)
                {
                    UI.ShowError(Strings.not_valid_square);
                }

                userChoice = UI.GetPlayerChoice(Game.m_MatrixSize, m_NameOfUser, out v_keepPlaying);

                if (!v_keepPlaying)
                {
                    Game.QuitGame();
                }

                v_ChoseValidSquare = !true;
            }
            while (!userChoice.CheckIfSquareIsValidAndMakeMove(i_WhichPlayer, sr_MakeMove, i_Board));
        }

        public virtual Square[,] Play(Square[,] i_Board)
        {
            PlayerTurn((Game.eWhichPlayer)m_PlayerColor, i_Board);
            return i_Board;
        }

        public LinkedList<Square> ListOfPossibleMoves(Square[,] i_GameBoard, Square.eSquareColor i_Color)
        {
            LinkedList<Square> possiblesMoves = new LinkedList<Square>();
            foreach (Square square in i_GameBoard)
            {
                if (square.Color == Square.eSquareColor.Empty)
                {
                    if (square.CheckIfSquareIsValidAndMakeMove((Game.eWhichPlayer)i_Color, !sr_MakeMove, i_GameBoard))
                    {
                        possiblesMoves.AddLast(square);
                    }
                }
            }

            return possiblesMoves;
        }
    }
}