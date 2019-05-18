namespace n_Square
{
    using n_Game;
    using n_UI;

    public struct Square
    {
        private char m_Column;
        private int m_Row;
        private eSquareColor m_Type;

        public enum eSquareColor
        {
            Empty = 0,
            White = 1,
            Black = 2
        }

        public Square(char i_Column, int i_Row)
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

        public char Column
        {
            get { return m_Column; }
            set { m_Column = value; }
        }

        public eSquareColor Color
        {
            get { return m_Type; }
            set { m_Type = value; }
        }

        public bool CheckIfSquareIsValidAndMakeMove(Game.eWhichPlayer i_WhichPlayer, bool i_ItIsPlayersMove, Square[,] i_Board)
        {
            bool validation = !true;
            if (i_Board[Row,Column - UI.sr_FirstLetter].Color == eSquareColor.Empty)
            {
                int checkerCounter = 0;
                int jumpColumn = 0, jumpRow = 0;
                Square.eSquareColor PlayersColor = i_WhichPlayer == Game.eWhichPlayer.First ? Square.eSquareColor.White : Square.eSquareColor.Black;

                while (checkerCounter < 8)
                {
                    switch (checkerCounter)
                    {
                        case 0:
                            jumpColumn = 1;
                            jumpRow = 0;
                            break;
                        case 1:
                            jumpColumn = -1;
                            jumpRow = 0;
                            break;
                        case 2:
                            jumpColumn = -1;
                            jumpRow = 1;
                            break;
                        case 3:
                            jumpColumn = 1;
                            jumpRow = -1;
                            break;
                        case 4:
                            jumpColumn = 0;
                            jumpRow = 1;
                            break;
                        case 5:
                            jumpColumn = 0;
                            jumpRow = -1;
                            break;
                        case 6:
                            jumpColumn = -1;
                            jumpRow = -1;
                            break;
                        case 7:
                            jumpColumn = 1;
                            jumpRow = 1;
                            break;
                    }

                    int i = Row + jumpRow;
                    int j = (Column - UI.sr_FirstLetter) + jumpColumn;

                    if (i < 0 || i >= Game.m_MatrixSize || j < 0 || j >= Game.m_MatrixSize || !isItOppositeColor(i_Board[i, j], i_WhichPlayer))
                    {
                        checkerCounter++;
                        continue;
                    }

                    i += jumpRow;
                    j += jumpColumn;

                    for (; (i < i_Board.GetLength(0) && i >= 0) && (j < i_Board.GetLength(1) && j >= 0); i += jumpRow, j += jumpColumn)
                    {
                        if (!isItOppositeColor(i_Board[i, j], i_WhichPlayer))
                        {
                            if (i_Board[i, j].Color == PlayersColor)
                            {
                                if (i_ItIsPlayersMove)
                                {
                                    Color = (Square.eSquareColor)i_WhichPlayer;
                                    Game.GetPlayer((int)(i_WhichPlayer - 1)).MakeMove(i_Board, this, i_Board[i, j], jumpColumn, jumpRow);
                                }

                                validation = true; 
                            }

                            break;
                        }
                    }

                    checkerCounter++;
                }

            }
            return validation;
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