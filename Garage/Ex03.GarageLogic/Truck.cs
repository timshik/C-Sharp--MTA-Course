namespace n_Truck
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using n_Vehicle;
    using n_Wheel;
    using n_Strings;

    public class Truck : FuelVehicle
    {
        private static readonly int sr_NumberOfWheels = 12, sr_FullTunkLevel = 110, sr_MaxPressure = 26;
        private static readonly eEnergyType sr_EnergyType = eEnergyType.Soler;
        private readonly float r_TrunkLevel;
        private bool m_HazardousMaterials;

        public Truck(string i_ModelName, string i_PlateNumber, bool v_isDeliveryHazardousMaterials, float i_TrunkLevel, string i_WheelManufacturer)
            : base(sr_NumberOfWheels, sr_FullTunkLevel, i_ModelName, i_PlateNumber, sr_MaxPressure, i_WheelManufacturer, sr_EnergyType)
        {
            m_HazardousMaterials = v_isDeliveryHazardousMaterials;
            r_TrunkLevel = i_TrunkLevel;
        }

        public bool HazardousMaterials
        {
            get { return m_HazardousMaterials; }
            set { m_HazardousMaterials = value; }
        }

        public float TruckLevel
        {
            get { return r_TrunkLevel; }
        }

        public override string ToString()
        {
            StringBuilder vehicleDetails = new StringBuilder();

            vehicleDetails.Append(base.ToString());
            vehicleDetails.AppendFormat(Strings.hazardous_materials, m_HazardousMaterials ? Strings.yes : Strings.no);
            vehicleDetails.AppendFormat(Strings.trunk_capacity, r_TrunkLevel);

            return vehicleDetails.ToString();
        }
    }
}