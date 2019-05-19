using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using n_Strings;

namespace Garage
{
    public class VehicleProperties
    {
        BaseVehicle m_Vehicle;
        string m_OwnerName, m_PhoneNumber;
        eStateOfService m_Status;
        public static readonly List<string> sr_StateListOptions = new List<string>();

        public static void SetListOfOptions()
        {
            sr_StateListOptions.Add(Strings.inrepair_title);
            sr_StateListOptions.Add(Strings.fixed_title);
            sr_StateListOptions.Add(Strings.paid_title);
        }

        public enum eStateOfService
        {
            InRepair,
            Fixed,
            Paid
        }

        public BaseVehicle Vehicle
        {
            get { return m_Vehicle; }
            set { m_Vehicle = value; }
        }

        public string OwnerName
        {
            get { return m_OwnerName; }
            set { m_OwnerName = value; }
        }

        public string PhoneNumber
        {
            get { return m_PhoneNumber; }
            set { m_PhoneNumber = value; }
        }

        public eStateOfService Status
        {
            get { return m_Status; }
            set { m_Status = value; }
        }
    }
}
