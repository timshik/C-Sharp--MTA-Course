﻿namespace Garage
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
        private static readonly int sr_NumberOfWheels = 2, sr_MaxPressure = 33;
        private static readonly float sr_FullBatteryLevel = 1.4f;
        private LicenseType.eLicenseType m_LicenseType;
        private int m_EngineCapacity;

        public ElectricMotorcycle(string i_ModelName, string i_PlateNumber, string i_WheelManufacturer, LicenseType.eLicenseType i_LicenseType, int i_EngineCapacity)
            : base(sr_NumberOfWheels, i_ModelName, i_PlateNumber, sr_MaxPressure, i_WheelManufacturer, sr_FullBatteryLevel)
        {
            m_EngineCapacity = i_EngineCapacity;
            m_LicenseType = i_LicenseType;
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
            vehicleDetails.AppendFormat(Strings.license_type, Garage.LicenseType.sr_LicenseType[(int)m_LicenseType]);
            vehicleDetails.AppendFormat(Strings.show_engine_capacity, m_EngineCapacity);

            return vehicleDetails.ToString();
        }
    }
}