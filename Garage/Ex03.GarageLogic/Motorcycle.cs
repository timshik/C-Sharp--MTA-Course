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
        private static readonly int sr_NumberOfWheels = 2, sr_FullTunkLevel = 8, sr_MaxPressure = 33;
        private static readonly eEnergyType sr_EnergyType = eEnergyType.Octan95;
        private LicenseType.eLicenseType m_LicenseType;
        private int m_EngineCapacity;

        public Motorcycle(string i_ModelName, string i_PlateNumber, int i_EngineCapacity, LicenseType.eLicenseType i_LicenseType, string i_WheelManufacturer)
            : base(sr_NumberOfWheels, sr_FullTunkLevel, i_ModelName, i_PlateNumber, sr_MaxPressure, i_WheelManufacturer, sr_EnergyType)
        {
            m_EngineCapacity = i_EngineCapacity;
            m_LicenseType = i_LicenseType;
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
            vehicleDetails.AppendFormat(Strings.engine_capacity, m_EngineCapacity);

            return vehicleDetails.ToString();
        }
    }
}