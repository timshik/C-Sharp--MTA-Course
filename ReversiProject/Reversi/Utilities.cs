namespace n_Utilities
{
    using n_UI;

    public class Utilities
    {
        public static bool CheckIfColumnRight(char i_Column)
        {
            bool v_validation = true;
            if (i_Column < UI.sr_FirstLetter || i_Column > (char)(UI.sr_FirstLetter + n_Game.Game.m_MatrixSize))
            {
                v_validation = !true;
            }

            return v_validation;
        }

        public static bool CheckIfRowRight(int i_Row)
        {
            bool v_validation = true;
            if (i_Row < 0 || i_Row > n_Game.Game.m_MatrixSize)
            {
                v_validation = !true;
            }

            return v_validation;
        }
    }
}
