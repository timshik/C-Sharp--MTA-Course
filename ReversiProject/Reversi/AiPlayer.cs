using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using n_Player;
using n_Square;
using n_Game;
using n_UI;

namespace Reversi
{
    class AiPlayer : Player
    {
        private int m_MaxDepth;  // Bigger value will make the computer stronger but slower (will think longer of every move)
                                 // Lower value will make the computer weaker but faster
        private static readonly bool v_MakeMove = true;
        private int m_CornerValue, m_SideValue, m_SimpleSquareValue, m_FullBoardValue = 100000;
        private Square[,] m_FinalBoard;
        private bool evalFlag = false;

        public bool EvalFunction
        {
            set { evalFlag = value; }
        }

        public AiPlayer(string i_NameOfUser, Square.eSquareColor i_PlayerColor, int i_CornerValue, int i_SideValue, int i_RestValue, int i_MaxDepth)
            : base(i_NameOfUser, i_PlayerColor)
        {
            m_CornerValue = i_CornerValue;
            m_SideValue = i_SideValue;
            m_SimpleSquareValue = i_RestValue;
            m_MaxDepth = i_MaxDepth;
        }

        public override Square[,] Play(Square[,] i_Board)
        {
            return AlphaBetaPruning(i_Board);
        }

        public Square[,] AlphaBetaPruning(Square[,] i_CurrentBoard)
        {
            int alpha = int.MinValue + 1, beta = int.MaxValue, value;
            LinkedList<Square> possibleMoves = new LinkedList<Square>();

            possibleMoves = ListOfPossibleMoves(i_CurrentBoard, m_PlayerColor);

            Square.eSquareColor color = (m_PlayerColor == Square.eSquareColor.Black) ? Square.eSquareColor.White : Square.eSquareColor.Black;
            foreach (Square square in possibleMoves)
            {
                value = MinValue(createNewBoard(i_CurrentBoard, square, m_PlayerColor), alpha, beta, 1, color);
                if (value > alpha)
                {
                    m_FinalBoard = createNewBoard(i_CurrentBoard, square, m_PlayerColor);
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
            LinkedList<Square> possibleMoves = ListOfPossibleMoves(i_Board, i_PrevColor);
            int value;
            Square[,] board;

            if (cutOffTest(i_Board, i_Depth, possibleMoves))
            {
                if (evalFlag)
                    value = evalBoard(ref i_Board, possibleMoves.Count);
                else
                    value = newEvalBoard(ref i_Board);
            }
            else
            {
                value = int.MinValue + 1;
                Square.eSquareColor color = (i_PrevColor == Square.eSquareColor.Black) ? Square.eSquareColor.White : Square.eSquareColor.Black;
                foreach (Square square in possibleMoves)
                {
                    board = createNewBoard(i_Board, square, i_PrevColor);
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

        public int MinValue(Square[,] i_Board, int i_Alpha, int i_Beta, int i_Depth, Square.eSquareColor i_PrevColor)
        {
            LinkedList<Square> possibleMoves = ListOfPossibleMoves(i_Board, i_PrevColor);
            int value;

            if (cutOffTest(i_Board, i_Depth, possibleMoves))
            {
                if (evalFlag)
                    value = evalBoard(ref i_Board, possibleMoves.Count);
                else
                    value = newEvalBoard(ref i_Board);
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

        private int evalBoard(ref Square[,] i_Board, int i_PossibleMovesCounter)
        {
            int value = 0;
            foreach (Square square in i_Board)
            {
                if (square.Color != Square.eSquareColor.Empty)
                {
                    if (inCorner(square))
                    {
                        value += getCurrentValueByColor(square.Color, m_CornerValue);
                    }
                    else if (square.Row == 0 || square.Row == (n_Game.Game.m_MatrixSize - 1) || (square.Column - UI.sr_FirstLetter) == 0 || (square.Column - UI.sr_FirstLetter) == (n_Game.Game.m_MatrixSize - 1))
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
                countCoins(ref counterBlack, ref counterWhite, ref cornerBlack, ref cornerWhite, ref i_Board);
                if (counterBlack > counterWhite)
                {
                    if (PlayerColor == Square.eSquareColor.Black)
                        value = m_FullBoardValue;
                    else
                        value = -m_FullBoardValue;
                }
                else if (counterBlack < counterWhite)
                {
                    if (PlayerColor == Square.eSquareColor.Black)
                        value = -m_FullBoardValue;
                    else
                        value = m_FullBoardValue;
                }
                else // draw
                {
                    value = 0;
                }
            }

            return value;
        }

        private int newEvalBoard(ref Square[,] i_Board)
        {
            int blackCoins = 0, whiteCoins = 0, myPossibleMoves = 0, opponentPossibleMoves = 0
                    , blackCornerCoins = 0, whiteCornerCoins = 0;
            int coinParityHeuristicValue, mobilityHeuristicValue, cornerHeuristicValue;
            countCoins(ref blackCoins, ref whiteCoins, ref blackCornerCoins, ref whiteCornerCoins, ref i_Board);
            if (PlayerColor == Square.eSquareColor.Black)
            {
                coinParityHeuristicValue = 100 * (blackCoins - whiteCoins) / (blackCoins + whiteCoins);
            }
            else
            {
                coinParityHeuristicValue = 100 * (whiteCoins - blackCoins) / (blackCoins + whiteCoins);
            }

            countMoves(ref myPossibleMoves, ref opponentPossibleMoves, ref i_Board);

            if (myPossibleMoves + opponentPossibleMoves != 0)
                mobilityHeuristicValue = 100 * (myPossibleMoves - opponentPossibleMoves) / (myPossibleMoves + opponentPossibleMoves);
            else
                mobilityHeuristicValue = 0;


            if (blackCornerCoins + whiteCornerCoins != 0)
            {
                if (PlayerColor == Square.eSquareColor.Black)
                {
                    cornerHeuristicValue = 100 * (blackCornerCoins - whiteCornerCoins) / (blackCornerCoins + whiteCornerCoins);
                }
                else
                {
                    cornerHeuristicValue = 100 * (whiteCornerCoins - blackCornerCoins) / (blackCornerCoins + whiteCornerCoins);
                }
            }
            else
                cornerHeuristicValue = 0;

            return cornerHeuristicValue + mobilityHeuristicValue + coinParityHeuristicValue;

        }

        private void countMoves(ref int myPossibleMoves, ref int opponentPossibleMoves, ref Square[,] i_Board)
        {
            Player one = Game.GetPlayer(0);
            Player two = Game.GetPlayer(1);

            if (one == this)
            {
                myPossibleMoves = one.ListOfPossibleMoves(i_Board, PlayerColor).Count;
                opponentPossibleMoves = two.ListOfPossibleMoves(i_Board, two.PlayerColor).Count;
            }
            else
            {
                opponentPossibleMoves = one.ListOfPossibleMoves(i_Board, PlayerColor).Count;
                myPossibleMoves = two.ListOfPossibleMoves(i_Board, two.PlayerColor).Count;
            }
        }

        private void countCoins(ref int i_BlackCoins, ref int i_WhiteCoins, ref int blackCornerCoins, ref int whiteCornerCoins, ref Square[,] i_Board)
        {
            foreach (Square square in i_Board)
            {
                switch (square.Color)
                {
                    case Square.eSquareColor.White:
                        i_WhiteCoins++;
                        if (inCorner(square))
                            whiteCornerCoins++;
                        break;
                    case Square.eSquareColor.Black:
                        i_BlackCoins++;
                        if (inCorner(square))
                            blackCornerCoins++;
                        break;
                    default:
                        break;
                }
            }
        }

        private bool inCorner(Square i_Square)
        {
            bool returnValue = !true;

            if (i_Square.Row == 0 && (i_Square.Column - UI.sr_FirstLetter) == 0) // left top corner
            {
                returnValue = true;
            }
            else if (i_Square.Row == 0 && (i_Square.Column - UI.sr_FirstLetter) == n_Game.Game.m_MatrixSize - 1) //right top corner
            {
                returnValue = true;
            }
            else if (i_Square.Row == n_Game.Game.m_MatrixSize - 1 && (i_Square.Column - UI.sr_FirstLetter) == 0) // left bottom corner
            {
                returnValue = true;
            }
            else if (i_Square.Row == n_Game.Game.m_MatrixSize - 1 && (i_Square.Column - UI.sr_FirstLetter) == n_Game.Game.m_MatrixSize - 1) // right bottom corner
            {
                returnValue = true;
            }

            return returnValue;
        }

        private int getCurrentValueByColor(Square.eSquareColor i_Color, int i_Value)
        {
            return (i_Color == m_PlayerColor) ? i_Value : -i_Value;
        }

        private bool cutOffTest(Square[,] i_Board, int i_Depth, LinkedList<Square> i_PossibleMoves)
        {
            return i_Depth == m_MaxDepth || i_PossibleMoves.Count == 0;
        }
    }
}