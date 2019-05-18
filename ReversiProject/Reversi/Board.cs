using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using n_UI;
using n_Square;
using n_Game;

namespace n_Board
{
    public class Board
    {
        public Square[,] m_Board;
        private List<Square> m_EmptySquares = new List<Square>(), m_ValidSquares = new List<Square>();

        public Board(int i_SizeOfMatrix)
        {
            m_Board = new Square[i_SizeOfMatrix, i_SizeOfMatrix];
            for (int i = 0; i < i_SizeOfMatrix; i++)
            {
                for (int j = 0; j < i_SizeOfMatrix; j++)
                {
                    m_Board[i, j] = new Square((char)(UI.sr_FirstLetter + j), i);
                }
            }

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

        public Square[,] SquareBoard
        {
            get { return m_Board; }
            set { m_Board = value; }
        }

        public Board()
        {
            m_Board = null;
        }

        public void PrintBoard()
        {
            UI.PrintMatrix(m_Board);
        }
    }
}