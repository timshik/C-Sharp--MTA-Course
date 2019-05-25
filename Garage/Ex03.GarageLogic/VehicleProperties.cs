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
        public static readonly List<string> sr_StateListOptions = new List<string>();
        private BaseVehicle m_Vehicle;
        private string m_OwnerName, m_PhoneNumber;
        private eStateOfService m_Status;

        public VehicleProperties(BaseVehicle i_Vehicle, string i_OwnerName, string i_PhoneNumber, eStateOfService i_Status)
        {
            this.m_Vehicle = i_Vehicle;
            this.m_OwnerName = i_OwnerName;
            this.m_PhoneNumber = i_PhoneNumber;
            this.m_Status = i_Status;
        }

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

        public override string ToString()
        {
            StringBuilder vehicleDetails = new StringBuilder();
            vehicleDetails.AppendLine(m_Vehicle.ToString());
            vehicleDetails.AppendLine(string.Format(Strings.owner_name, m_OwnerName));
            vehicleDetails.AppendLine(string.Format(Strings.owner_phone_number, m_PhoneNumber));
            vehicleDetails.AppendLine(string.Format(Strings.vehicle_status, sr_StateListOptions[(int)m_Status]));

            return vehicleDetails.ToString();
        }
    }
}
