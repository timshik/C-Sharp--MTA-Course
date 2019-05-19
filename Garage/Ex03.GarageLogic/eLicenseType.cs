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
        public static readonly List<string> sr_LicenseType = new List<string>();
        public enum eLicenseType
        {
            A,
            A1,
            A2,
            B
        }

        public static void SetListOfLicenseType()
        {
            sr_LicenseType.Add(Strings.license_a);
            sr_LicenseType.Add(Strings.license_a1);
            sr_LicenseType.Add(Strings.license_a2);
            sr_LicenseType.Add(Strings.license_b);
        }
    }
}