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
    using Garage;

    public class Truck : FuelVehicle
    {
        public static readonly int sr_NumberOfWheels = 12;
        public static readonly float sr_FullTunkLevel = 110, sr_MaxPressure = 26;
        public static readonly eEnergyType sr_EnergyType = eEnergyType.Soler;
        private readonly float r_TrunkLevel;
        private bool m_HazardousMaterials;

        public Truck(Dictionary<string, object> i_Arguments)
            : base(i_Arguments)
        {
            m_HazardousMaterials = (bool)i_Arguments[VehicleManager.sr_KeyDeliveryMaterials];
            r_TrunkLevel = (float)i_Arguments[VehicleManager.sr_KeyTruckCapacity];
            if(r_TrunkLevel < 0)
            {
                throw new ValueOutOfRangeException(float.MaxValue, 0, Strings.trunk_capacity_less_than_zero);
            }
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