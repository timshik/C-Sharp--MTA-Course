namespace n_Vehicle
{
    using System.Collections.Generic;
    using System.Text;
    using Garage;
    using n_Strings;

    public abstract class FuelVehicle : BaseVehicle
    {
        private readonly float r_MaxFuelLevel;
        public static List<string> s_EnergyTypeList = new List<string>();
        private eEnergyType m_Type;
        private float m_FuelLevel;

        public FuelVehicle(Dictionary<string, object> i_Arguments)
            : base(i_Arguments)
        {
            m_Type = (eEnergyType)i_Arguments[ArgumentsKeysets.sr_KeyTypeOfEnergy];
            r_MaxFuelLevel = (float)i_Arguments[ArgumentsKeysets.sr_KeyMaxFuelLevel];
            m_FuelLevel = 0;
            Fuel = (float)i_Arguments[ArgumentsKeysets.sr_KeyCurrentEnergyLevel];
        }

        public static void SetEnergeyTypeList()
        {
            s_EnergyTypeList.Add(Strings.soler);
            s_EnergyTypeList.Add(Strings.octan_95);
            s_EnergyTypeList.Add(Strings.octan_96);
            s_EnergyTypeList.Add(Strings.octan_98);
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
            m_PercentOfRemainingEnergy = (m_FuelLevel / r_MaxFuelLevel) * 100;
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
            vehicleDetails.AppendLine(string.Format(Strings.fuel_type, s_EnergyTypeList[(int)m_Type]));
            vehicleDetails.AppendLine(string.Format(Strings.current_fuel_level, m_FuelLevel));
            vehicleDetails.AppendLine(string.Format(Strings.maximum_fuel_level, r_MaxFuelLevel));

            return vehicleDetails.ToString();
        }
    }
}