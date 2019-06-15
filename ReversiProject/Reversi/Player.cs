namespace n_Player
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using n_Square;
    using n_Game;
    using n_Board;
    using Reversi;

    public class Player
    {
        private static readonly bool sr_MakeMove = true;
        private static Point[] m_Direction = new Point[]
        {
            new Point { X = 1, Y = 0 },
            new Point { X = -1, Y = 0 },
            new Point { X = -1, Y = 1 },
            new Point { X = 1, Y = -1 },
            new Point { X = 0, Y = 1 },
            new Point { X = 0, Y = -1 },
            new Point { X = -1, Y = -1 },
            new Point { X = 1, Y = 1 }
        };

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

        protected Square[,] MakeMove(Board i_Board, Square[] i_SquareRoute, Point i_JumpDirections, ref LinkedList<Square> io_ChangedSquares)
        {
            for (int i = i_SquareRoute[0].Row, j = i_SquareRoute[0].Column;
                !((i == i_SquareRoute[1].Row) && (j == i_SquareRoute[1].Column)); i += i_JumpDirections.Y, j += i_JumpDirections.X)
            {
                i_Board.SquareBoard[i, j].Color = i_SquareRoute[0].Color;
                io_ChangedSquares.AddLast(i_Board.SquareBoard[i, j]);
            }

            return i_Board.SquareBoard;
        }

        public LinkedList<Square> ListOfPossibleMoves(Board i_GameBoard)
        {
            LinkedList<Square> possiblesMoves = new LinkedList<Square>();

            bool validSquare;
            foreach (Square square in i_GameBoard.SquareBoard)
            {
                if (square.Color == Square.eSquareColor.Empty)
                {
                    CheckIfSquareIsValidAndMakeMove(square, !sr_MakeMove, i_GameBoard, out validSquare);
                    if (validSquare)
                    {
                        possiblesMoves.AddLast(square);
                    }
                }
            }

            return possiblesMoves;
        }

        public LinkedList<Square> CheckIfSquareIsValidAndMakeMove(Square i_PossibleSquare, bool i_ItIsPlayersMove, Board i_Board, out bool i_IsValid)
        {
            i_IsValid = !true;
            LinkedList<Square> changedSquares = new LinkedList<Square>();
            if (i_PossibleSquare.Color == Square.eSquareColor.Empty)
            {
                int checkerCounter = 0;
                Point jumpPoint;
                Square[] squareRoute = new Square[2];

                while (checkerCounter < 8)
                {
                    jumpPoint = m_Direction[checkerCounter];
                    int i = i_PossibleSquare.Row + jumpPoint.Y;
                    int j = i_PossibleSquare.Column + jumpPoint.X;

                    if (i < 0 || i >= Game.m_MatrixSize || j < 0 || j >= Game.m_MatrixSize || !isItOppositeColor(i_Board.SquareBoard[i, j], (Game.eWhichPlayer)PlayerColor))
                    {
                        checkerCounter++;
                        continue;
                    }

                    i += jumpPoint.Y;
                    j += jumpPoint.X;

                    for (; (i < i_Board.SquareBoard.GetLength(0) && i >= 0) && (j < i_Board.SquareBoard.GetLength(1) && j >= 0); i += jumpPoint.Y, j += jumpPoint.X)
                    {
                        if (!isItOppositeColor(i_Board.SquareBoard[i, j], (Game.eWhichPlayer)PlayerColor))
                        {
                            if (i_Board.SquareBoard[i, j].Color == PlayerColor)
                            {
                                if (i_ItIsPlayersMove)
                                {
                                    i_PossibleSquare.Color = PlayerColor;
                                    squareRoute[0] = i_PossibleSquare;
                                    squareRoute[1] = i_Board.SquareBoard[i, j];
                                    i_Board.SquareBoard = MakeMove(i_Board, squareRoute, jumpPoint, ref changedSquares);
                                }

                                i_IsValid = true;
                            }

                            break;
                        }
                    }

                    checkerCounter++;
                }
            }

            return changedSquares;
        }

        private bool isItOppositeColor(Square i_Square, Game.eWhichPlayer i_WhichPlayer)
        {
            bool returnValue = true;

            if (i_WhichPlayer == Game.eWhichPlayer.First && i_Square.Color != Square.eSquareColor.Black)
            {
                returnValue = !true;
            }
            else if (i_WhichPlayer == Game.eWhichPlayer.Second && i_Square.Color != Square.eSquareColor.White)
            {
                returnValue = !true;
            }

            return returnValue;
        }
    }
}