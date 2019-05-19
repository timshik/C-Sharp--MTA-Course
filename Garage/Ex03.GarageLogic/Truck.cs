namespace n_Truck
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using n_Vehicle;
    using n_Wheel;

    public class Truck : Vehicle
    {
        public static readonly int sr_NumberOfWheels = 12, sr_FullTunkLevel = 110, sr_MaxPressure = 26;
        public static readonly eEnergyType sr_EnergyType = eEnergyType.Soler;
        bool m_HazardousMaterials;
        readonly float r_TrunkLevel;

        public Truck(string i_ModelName, string i_PlateNumber, bool v_isDeliveryHazardousMaterials, float i_TrunkLevel) 
            : base(sr_NumberOfWheels, sr_FullTunkLevel, i_ModelName,i_PlateNumber)
        {
            m_HazardousMaterials = v_isDeliveryHazardousMaterials;
            r_TrunkLevel = i_TrunkLevel;
        }
    }
}
