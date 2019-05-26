using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using n_Strings;

namespace Garage
{
    public class DoorNumber
    {
        public static List<string> s_DoorsOptions = new List<string>();

        public enum eNumberOfDoors
        {
            TwoDoors = 2,
            ThreeDoors,
            FourDoors,
            FiveDoors
        }

        public static void SetListOfOptions()
        {
            s_DoorsOptions.Add(Strings.two_doors);
            s_DoorsOptions.Add(Strings.three_doors);
            s_DoorsOptions.Add(Strings.four_doors);
            s_DoorsOptions.Add(Strings.five_doors);
        }
    }
}