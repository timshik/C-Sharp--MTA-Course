using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using n_Strings;

namespace Garage
{
    public class LicenseType
    {
        public static List<string> s_LicenseType = new List<string>();

        public enum eLicenseType
        {
            A,
            A1,
            A2,
            B
        }

        public static void SetListOfLicenseType()
        {
            s_LicenseType.Add(Strings.license_a);
            s_LicenseType.Add(Strings.license_a1);
            s_LicenseType.Add(Strings.license_a2);
            s_LicenseType.Add(Strings.license_b);
        }
    }
}