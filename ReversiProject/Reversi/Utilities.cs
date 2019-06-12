namespace n_Utilities
{
    public class Utilities
    {
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
