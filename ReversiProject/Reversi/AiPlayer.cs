using System;
using System.Collections.Generic;
using n_Player;
using n_Square;
using n_Game;
using n_Board;

namespace Reversi
{
    public class AiPlayer : Player
    {
        private static readonly bool v_MakeMove = true;
        private int m_MaxDepth = 15;     // Bigger value will make the computer stronger but slower (will think longer of every move)
                                        // Lower value will make the computer weaker but faster
        private int m_CornerValue = 200, m_SideValue = 20, m_SimpleSquareValue = 5, m_FullBoardValue = 100000;
        private Board m_FinalBoard;

        public AiPlayer(string i_NameOfUser, Square.eSquareColor i_PlayerColor)
            : base(i_NameOfUser, i_PlayerColor)
        {
        }

        public Board AlphaBetaPruning(Board i_CurrentBoard)
        {
            int alpha = int.MinValue + 1, beta = int.MaxValue, value;
            LinkedList<Square> possibleMoves = new LinkedList<Square>();

            possibleMoves = listOfPossibleMoves(i_CurrentBoard, m_PlayerColor);

            Square.eSquareColor color = (m_PlayerColor == Square.eSquareColor.Black) ? Square.eSquareColor.White : Square.eSquareColor.Black;
            foreach (Square square in possibleMoves)
            {
                value = MinValue(createNewBoard((Board)i_CurrentBoard.Clone(), square, m_PlayerColor), alpha, beta, 1, color);
                if (value > alpha)
                {
                    m_FinalBoard = createNewBoard((Board)i_CurrentBoard.Clone(), square, m_PlayerColor);
                    alpha = value;
                }
            }

            return m_FinalBoard;
        }

        private Board createNewBoard(Board i_CurrentBoard, Square i_PossibleMove, Square.eSquareColor i_Color)
        {
            bool isValid;
            CheckIfSquareIsValidAndMakeMove(i_PossibleMove, v_MakeMove, i_CurrentBoard, out isValid);

            return i_CurrentBoard;
        }

        public int MaxValue(Board i_Board, int i_Alpha, int i_Beta, int i_Depth, Square.eSquareColor i_PrevColor)
        {
            LinkedList<Square> possibleMoves = listOfPossibleMoves(i_Board, i_PrevColor);
            int value;
            Board board;

            if (cutOffTest(i_Board, i_Depth, possibleMoves))
            {
                value = evalBoard(i_Board, possibleMoves.Count);
            }
            else
            {
                value = int.MinValue + 1;
                Square.eSquareColor color = (i_PrevColor == Square.eSquareColor.Black) ? Square.eSquareColor.White : Square.eSquareColor.Black;
                foreach (Square square in possibleMoves)
                {
                    board = createNewBoard((Board)i_Board.Clone(), square, i_PrevColor);
                    value = max(value, MinValue(board, i_Alpha, i_Beta, i_Depth + 1, color));
                    if (value >= i_Beta)
                    {
                        break;
                    }

                    i_Alpha = max(i_Alpha, value);
                }
            }

            return value;
        }

        public int MinValue(Board i_Board, int i_Alpha, int i_Beta, int i_Depth, Square.eSquareColor i_PrevColor)
        {
            LinkedList<Square> possibleMoves = listOfPossibleMoves(i_Board, i_PrevColor);
            int value;

            if (cutOffTest(i_Board, i_Depth, possibleMoves))
            {
                value = evalBoard(i_Board, possibleMoves.Count);
            }
            else
            {
                value = int.MaxValue;
                Square.eSquareColor color = (i_PrevColor == Square.eSquareColor.Black) ? Square.eSquareColor.White : Square.eSquareColor.Black;
                foreach (Square square in possibleMoves)
                {
                    value = min(value, MaxValue(createNewBoard(i_Board, square, i_PrevColor), i_Alpha, i_Beta, i_Depth + 1, color));
                    if (value <= i_Alpha)
                    {
                        break;
                    }

                    i_Beta = min(i_Beta, value);
                }
            }

            return value;
        }

        private int min(int i_ValueA, int i_ValueB)
        {
            return i_ValueA >= i_ValueB ? i_ValueB : i_ValueA;
        }

        private int max(int i_ValueA, int i_ValueB)
        {
            return i_ValueA >= i_ValueB ? i_ValueA : i_ValueB;
        }

        private int evalBoard(Board i_Board, int i_PossibleMovesCounter)
        {
            int value = 0;
            foreach (Square square in i_Board.SquareBoard)
            {
                if (square.Color != Square.eSquareColor.Empty)
                {
                    if (inCorner(square))
                    {
                        value += getCurrentValueByColor(square.Color, m_CornerValue);
                    }
                    else if (square.Row == 0 || square.Row == (n_Game.Game.m_MatrixSize - 1) || square.Column == 0 || square.Column == (n_Game.Game.m_MatrixSize - 1))
                    {
                        value += getCurrentValueByColor(square.Color, m_SideValue);
                    }
                    else
                    {
                        value += getCurrentValueByColor(square.Color, m_SimpleSquareValue);
                    }
                }
            }

            if (i_PossibleMovesCounter == 0)
            {
                int counterWhite = 0, counterBlack = 0;
                int cornerWhite = 0, cornerBlack = 0;
                countCoins(ref counterBlack, ref counterWhite, ref cornerBlack, ref cornerWhite, i_Board);
                if (counterBlack > counterWhite)
                {
                    value = PlayerColor == Square.eSquareColor.Black ? m_FullBoardValue : -m_FullBoardValue;
                }
                else if (counterBlack < counterWhite)
                {
                    value = PlayerColor == Square.eSquareColor.Black ? -m_FullBoardValue : m_FullBoardValue;
                }
                else
                { // draw
                    value = 0;
                }
            }

            return value;
        }

        private void countCoins(ref int i_BlackCoins, ref int i_WhiteCoins, ref int blackCornerCoins, ref int whiteCornerCoins, Board i_Board)
        {
            foreach (Square square in i_Board.SquareBoard)
            {
                switch (square.Color)
                {
                    case Square.eSquareColor.White:
                        i_WhiteCoins++;
                        if (inCorner(square))
                        {
                            whiteCornerCoins++;
                        }

                        break;
                    case Square.eSquareColor.Black:
                        i_BlackCoins++;
                        if (inCorner(square))
                        {
                            blackCornerCoins++;
                        }

                        break;
                    default:
                        break;
                }
            }
        }

        private bool inCorner(Square i_Square)
        {
            bool returnValue = !true;

            if (i_Square.Row == 0 && i_Square.Column == 0)
            { // left top corner
                returnValue = true;
            }
            else if (i_Square.Row == 0 && i_Square.Column == n_Game.Game.m_MatrixSize - 1)
            { // right top corner
                returnValue = true;
            }
            else if (i_Square.Row == n_Game.Game.m_MatrixSize - 1 && i_Square.Column == 0)
            { // left bottom corner
                returnValue = true;
            }
            else if (i_Square.Row == n_Game.Game.m_MatrixSize - 1 && i_Square.Column == n_Game.Game.m_MatrixSize - 1)
            { // right bottom corner
                returnValue = true;
            }

            return returnValue;
        }

        private int getCurrentValueByColor(Square.eSquareColor i_Color, int i_Value)
        {
            return (i_Color == m_PlayerColor) ? i_Value : -1 * i_Value;
        }

        private bool cutOffTest(Board i_Board, int i_Depth, LinkedList<Square> i_PossibleMoves)
        {
            return i_Depth == m_MaxDepth || i_PossibleMoves.Count == 0;
        }

        private LinkedList<Square> listOfPossibleMoves(Board i_GameBoard, Square.eSquareColor i_CurrentMoveColor)
        {
            return new Player("temp", i_CurrentMoveColor).ListOfPossibleMoves(i_GameBoard);
        }
    }
}