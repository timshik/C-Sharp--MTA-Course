using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using n_Strings;

namespace Garage
{
    public class CarColor
    {
        public static List<string> s_CarColorNames = new List<string>();

        public enum eCarColor
        {
            Red,
            Blue,
            Black,
            Gray
        }

        public static void SetListOfCarColors()
        {
            s_CarColorNames.Add(Strings.title_red);
            s_CarColorNames.Add(Strings.title_blue);
            s_CarColorNames.Add(Strings.title_black);
            s_CarColorNames.Add(Strings.title_gray);
        }
    }
}