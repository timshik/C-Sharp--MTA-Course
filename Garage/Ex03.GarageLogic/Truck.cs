namespace n_Truck
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using n_Vehicle;
    using n_Wheel;

    public class Truck : FuelVehicle
    {
        private static readonly int sr_NumberOfWheels = 12, sr_FullTunkLevel = 110, sr_MaxPressure = 26;
        private static readonly eEnergyType sr_EnergyType = eEnergyType.Soler;
        bool m_HazardousMaterials;
        readonly float r_TrunkLevel;

        public Truck(string i_ModelName, string i_PlateNumber, bool v_isDeliveryHazardousMaterials, float i_TrunkLevel, string i_WheelManufacturer) 
            : base(sr_NumberOfWheels, sr_FullTunkLevel, i_ModelName,i_PlateNumber,sr_MaxPressure,i_WheelManufacturer, sr_EnergyType)
        {
            m_HazardousMaterials = v_isDeliveryHazardousMaterials;
            r_TrunkLevel = i_TrunkLevel;
        }

        public bool HazardousMaterials
        {
            set { m_HazardousMaterials  = value; }
            get { return m_HazardousMaterials; }
        }

        public float TruckLevel
        {
            get {return r_TrunkLevel; }
        }
    }
}