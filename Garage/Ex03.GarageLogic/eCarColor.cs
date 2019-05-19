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
        public static readonly List<string> sr_CarColorNames = new List<string>();

        public enum eCarColor
        {
            Red,
            Blue,
            Black,
            Gray
        }

        public static void SetListOfCarColors()
        {
            sr_CarColorNames.Add(Strings.title_red);
            sr_CarColorNames.Add(Strings.title_blue);
            sr_CarColorNames.Add(Strings.title_black);
            sr_CarColorNames.Add(Strings.title_gray);
        }
    }
}