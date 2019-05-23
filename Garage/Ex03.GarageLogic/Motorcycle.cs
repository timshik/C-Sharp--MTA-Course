namespace n_Motorcycle
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using n_Vehicle;
    using n_Wheel;
    using Garage;
    using n_Strings;

    public class Motorcycle : FuelVehicle
    {
        public static readonly int sr_NumberOfWheels = 2;
        public static readonly float sr_FullTunkLevel = 8, sr_MaxPressure = 33;
        public static readonly eEnergyType sr_EnergyType = eEnergyType.Octan95;
        private LicenseType.eLicenseType m_LicenseType;
        private int m_EngineCapacity;

        public Motorcycle(Dictionary<string, object> i_Arguments)
            : base(i_Arguments)
        {
            m_EngineCapacity = int.Parse((string)i_Arguments[VehicleManager.sr_KeyEngineCapacity]);
            m_LicenseType = (LicenseType.eLicenseType)i_Arguments[VehicleManager.sr_KeyLicenseType];
            if (m_EngineCapacity < 0)
            {
                throw new ValueOutOfRangeException(float.MaxValue, 0, Strings.engine_capacity_less_than_zero);
            }
        }

        public LicenseType.eLicenseType LicenseType
        {
            get { return m_LicenseType; }
            set { m_LicenseType = value; }
        }

        public int EngineCapacity
        {
            get { return m_EngineCapacity; }
            set { m_EngineCapacity = value; }
        }

        public override string ToString()
        {
            StringBuilder vehicleDetails = new StringBuilder();

            vehicleDetails.Append(base.ToString());
            vehicleDetails.AppendFormat(Strings.license_type, Garage.LicenseType.sr_LicenseType[(int)m_LicenseType]);
            vehicleDetails.AppendFormat(Strings.show_engine_capacity, m_EngineCapacity);

            return vehicleDetails.ToString();
        }
    }
}