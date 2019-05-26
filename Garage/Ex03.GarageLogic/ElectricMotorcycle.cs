namespace Garage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using n_Wheel;
    using n_Strings;

    public class ElectricMotorcycle : ElectricVehicle
    {
        public static readonly int sr_NumberOfWheels = 2;
        public static readonly float sr_FullBatteryLevel = 1.4f, sr_MaxPressure = 33;
        private LicenseType.eLicenseType m_LicenseType;
        private int m_EngineCapacity;

        public ElectricMotorcycle(Dictionary<string, object> i_Arguments)
            : base(i_Arguments)
        {
            m_EngineCapacity = int.Parse((string)i_Arguments[ArgumentsKeysets.sr_KeyEngineCapacity]);
            m_LicenseType = (LicenseType.eLicenseType)i_Arguments[ArgumentsKeysets.sr_KeyLicenseType];
            if (m_EngineCapacity < 0)
            {
                throw new ValueOutOfRangeException(float.MaxValue, 0, Strings.engine_capacity_less_than_zero);
            }
        }

        public int EngineCapacity
        {
            get { return m_EngineCapacity; }
            set { m_EngineCapacity = value; }
        }

        public LicenseType.eLicenseType LicenseType
        {
            get { return m_LicenseType; }
            set { m_LicenseType = value; }
        }

        public override string ToString()
        {
            StringBuilder vehicleDetails = new StringBuilder();

            vehicleDetails.Append(base.ToString());
            vehicleDetails.AppendLine(string.Format(Strings.license_type, Garage.LicenseType.s_LicenseType[(int)m_LicenseType]));
            vehicleDetails.AppendLine(string.Format(Strings.show_engine_capacity, m_EngineCapacity));

            return vehicleDetails.ToString();
        }
    }
}