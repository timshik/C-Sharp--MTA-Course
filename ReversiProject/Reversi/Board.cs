using System;
using System.Collections.Generic;
using n_Square;
using n_Game;

namespace n_Board
{
    public class Board : ICloneable
    {
        private Square[,] m_Board;
        private List<Square> m_EmptySquares = new List<Square>(), m_ValidSquares = new List<Square>();

        public Board(int i_SizeOfMatrix)
        {
            m_Board = new Square[i_SizeOfMatrix, i_SizeOfMatrix];
            for (int i = 0; i < i_SizeOfMatrix; i++)
            {
                for (int j = 0; j < i_SizeOfMatrix; j++)
                {
                    m_Board[i, j] = new Square(i, j);
                }
            }

            m_Board[(i_SizeOfMatrix / 2) - 1, (i_SizeOfMatrix / 2) - 1].Color = Square.eSquareColor.White;
            m_Board[(i_SizeOfMatrix / 2), (i_SizeOfMatrix / 2) - 1].Color = Square.eSquareColor.Black;
            m_Board[(i_SizeOfMatrix / 2) - 1, (i_SizeOfMatrix / 2)].Color = Square.eSquareColor.Black;
            m_Board[(i_SizeOfMatrix / 2), (i_SizeOfMatrix / 2)].Color = Square.eSquareColor.White;

            BoardSize = i_SizeOfMatrix;
        }

        public int BoardSize
        {
            set
            {
                for (int i = 0; i < Game.m_MatrixSize; i++)
                {
                    for (int j = 0; j < Game.m_MatrixSize; j++)
                    {
                        m_Board[i, j].Color = Square.eSquareColor.Empty;
                    }
                }

                m_Board[(value / 2) - 1, (value / 2) - 1].Color = Square.eSquareColor.White;
                m_Board[(value / 2), (value / 2) - 1].Color = Square.eSquareColor.Black;
                m_Board[(value / 2) - 1, (value / 2)].Color = Square.eSquareColor.Black;
                m_Board[(value / 2), (value / 2)].Color = Square.eSquareColor.White;
            }
        }

        public ref Square[,] SquareBoard
        {
            get { return ref m_Board; }
        }

        public Board()
        {
            m_Board = null;
        }

        public object Clone()
        {
            Board board = new Board(m_Board.Length);
            board.m_Board = (Square[,])m_Board.Clone();

            return board;
        }
    }
}