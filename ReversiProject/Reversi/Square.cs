namespace n_Square
{
    using System.Drawing;

    public struct Square
    {
        private int m_Column, m_Row;
        private eSquareColor m_Type;

        public enum eSquareColor
        {
            Empty = 0,
            White = 1,
            Black = 2
        }

        public Point GetPoint
        {
            get { return new Point(m_Column, m_Row); }
        }

        public Square(int i_Row, int i_Column)
        {
            m_Column = i_Column;
            m_Row = i_Row;
            m_Type = eSquareColor.Empty;
        }

        public int Row
        {
            get { return m_Row; }
            set { m_Row = value; }
        }

        public int Column
        {
            get { return m_Column; }
            set { m_Column = value; }
        }

        public eSquareColor Color
        {
            get { return m_Type; }
            set { m_Type = value; }
        }
    }
}