namespace n_AI
{
    using System.Collections.Generic;
    using n_Square;
    using n_Player;
    using n_Game;

    public class AI
    {
        private static readonly int sr_MaxDepth = 8;    // Bigger value will make the computer stronger but slower (will think longer of every move)
                                                        // Lower value will make the computer weaker but faster
        private static readonly bool v_MakeMove = true;
        private static readonly int sr_CornerValue = 10, sr_SideValue = 8, sr_SimpleSquareValue = 5, sr_FullBoardValue = 100000;
        private Square[,] m_FinalBoard;
        private Player m_AI;
        private Square.eSquareColor m_Color;

        public AI()
        {
            this.m_AI = null;
        }

        public Square[,] AlphaBetaPruning(Square[,] i_CurrentBoard, Player i_Player)
        {
            this.m_AI = i_Player;
            this.m_Color = i_Player.PlayerColor;
            int alpha = int.MinValue + 1, beta = int.MaxValue, value;
            LinkedList<Square> possibleMoves = new LinkedList<Square>();

            possibleMoves = m_AI.ListOfPossibleMoves(i_CurrentBoard, m_Color);

            Square.eSquareColor color = (m_Color == Square.eSquareColor.Black) ? Square.eSquareColor.White : Square.eSquareColor.Black;
            foreach (Square square in possibleMoves)
            {
                value = MinValue(createNewBoard(i_CurrentBoard, square, m_Color), alpha, beta, 1, color);
                if (value > alpha)
                {
                    m_FinalBoard = createNewBoard(i_CurrentBoard, square, m_Color);
                    alpha = value;
                }
            }

            return m_FinalBoard;
        }

        private Square[,] createNewBoard(Square[,] i_CurrentBoard, Square i_PossibleMove, Square.eSquareColor i_Color)
        {
            Square[,] newBoard = (Square[,])i_CurrentBoard.Clone();
            i_PossibleMove.CheckIfSquareIsValidAndMakeMove((Game.eWhichPlayer)i_Color, v_MakeMove, newBoard);

            return newBoard;
        }

        public int MaxValue(Square[,] i_Board, int i_Alpha, int i_Beta, int i_Depth, Square.eSquareColor i_PrevColor)
        {
            LinkedList<Square> possibleMoves = m_AI.ListOfPossibleMoves(i_Board, i_PrevColor);
            int value;

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
                    value = max(value, MinValue(createNewBoard(i_Board, square, i_PrevColor), i_Alpha, i_Beta, i_Depth + 1, color));

                    if (value >= i_Beta)
                    {
                        break;
                    }

                    i_Alpha = max(i_Alpha, value);
                }
            }

            return value;
        }

        public int MinValue(Square[,] i_Board, int i_Alpha, int i_Beta, int i_Depth, Square.eSquareColor i_PrevColor)
        {
            LinkedList<Square> possibleMoves = m_AI.ListOfPossibleMoves(i_Board, i_PrevColor);
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

        private int evalBoard(Square[,] i_Board, int i_PossibleMovesCounter)
        {
            int value = 0;

            foreach (Square square in i_Board)
            {
                if (square.Color != Square.eSquareColor.Empty)
                {
                    if (inCorner(square))
                    {
                        value += getCurrentValueByColor(square.Color, sr_CornerValue);
                    }
                    else if (square.Row == 0 || square.Row == (n_Game.Game.m_MatrixSize - 1) || square.Column == 0 || square.Column == (n_Game.Game.m_MatrixSize - 1))
                    {
                        value += getCurrentValueByColor(square.Color, sr_SideValue);
                    }
                    else
                    {
                        value += getCurrentValueByColor(square.Color, sr_SimpleSquareValue);
                    }
                }
            }

            if (i_PossibleMovesCounter == 0)
            {
                value *= sr_FullBoardValue;
            }

            return value;
        }

        private bool inCorner(Square i_Square)
        {
            bool returnValue = !true;

            if (i_Square.Row == 0 && i_Square.Column == 0)
            {
                returnValue = true;
            }
            else if (i_Square.Row == 0 && i_Square.Column == n_Game.Game.m_MatrixSize - 1)
            {
                returnValue = true;
            }
            else if (i_Square.Row == n_Game.Game.m_MatrixSize - 1 && i_Square.Column == 0)
            {
                returnValue = true;
            }
            else if (i_Square.Row == n_Game.Game.m_MatrixSize - 1 && i_Square.Column == n_Game.Game.m_MatrixSize - 1)
            {
                returnValue = true;
            }

            return returnValue;
        }

        private int getCurrentValueByColor(Square.eSquareColor i_Color, int i_Value)
        {
            return (i_Color == m_Color) ? i_Value : -i_Value;
        }

        private bool cutOffTest(Square[,] i_Board, int i_Depth, LinkedList<Square> i_PossibleMoves)
        {
            return i_Depth == sr_MaxDepth || i_PossibleMoves.Count == 0;
        }
    }
}