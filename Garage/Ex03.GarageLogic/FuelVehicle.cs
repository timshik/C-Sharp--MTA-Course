namespace n_Vehicle
{
    using System.Collections.Generic;
    using System.Text;
    using Garage;
    using n_Strings;

    public abstract class FuelVehicle : BaseVehicle
    {
        private readonly float r_MaxFuelLevel;
        public static readonly List<string> sr_EnergyTypeList = new List<string>();
        private eEnergyType m_Type;
        private float m_FuelLevel;

        public FuelVehicle(int i_NumberOfWheels, float i_MaxFuelLevel, string i_ModelName, string i_PlateNumber, float i_MaxWheelPressure, string i_WheelManufacturer, eEnergyType i_Type)
            : base(i_NumberOfWheels, i_ModelName, i_PlateNumber, i_MaxWheelPressure, i_WheelManufacturer)
        {
            m_Type = i_Type;
            r_MaxFuelLevel = i_MaxFuelLevel;
            m_FuelLevel = Garage.GarageManager.sr_BasicStartFloatLevel;
        }

        public static void SetEnergeyTypeList()
        {
            sr_EnergyTypeList.Add(Strings.soler);
            sr_EnergyTypeList.Add(Strings.octan_95);
            sr_EnergyTypeList.Add(Strings.octan_96);
            sr_EnergyTypeList.Add(Strings.octan_98);
        }

        public enum eEnergyType
        {
            Soler,
            Octan95,
            Octan96,
            Octan98,
        }

        private void calculatePercentOfRemainingEnergy()
        {
            m_PercentOfRemainingEnergy = (r_MaxFuelLevel / m_FuelLevel) * 100;
        }

        public eEnergyType EnergyType
        {
            get { return m_Type; }
            set { m_Type = value; }
        }

        public float Fuel
        {
            get { return m_FuelLevel; }
            set
            {
                if (m_FuelLevel + value > r_MaxFuelLevel || m_FuelLevel + value < 0)
                {
                    throw new ValueOutOfRangeException(r_MaxFuelLevel - m_FuelLevel, 0, Strings.out_of_range);
                }

                m_FuelLevel += value;
                calculatePercentOfRemainingEnergy();
            }
        }

        public float MaxEnergyLevel // ONLY GET, cannot change max fuel level - readonly!
        {
            get { return r_MaxFuelLevel; }
        }

        public override string ToString()
        {
            StringBuilder vehicleDetails = new StringBuilder();

            vehicleDetails.Append(base.ToString());
            vehicleDetails.AppendFormat(Strings.fuel_type, sr_EnergyTypeList[(int)m_Type]);
            vehicleDetails.AppendFormat(Strings.current_fuel_level, m_FuelLevel);

            return vehicleDetails.ToString();
        }
    }
}