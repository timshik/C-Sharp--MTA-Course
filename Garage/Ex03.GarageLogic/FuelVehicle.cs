namespace n_Vehicle
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using n_Wheel;
    using Garage;

    abstract public class FuelVehicle : BaseVehicle
    {
        eEnergyType m_Type;
        float m_FuelLevel;
        readonly float r_MaxFuelLevel;

        public FuelVehicle(int i_NumberOfWheels, float i_MaxFuelLevel, string i_ModelName,
            string i_PlateNumber, float i_MaxWheelPressure, string i_WheelManufacturer, eEnergyType i_Type)
            : base(i_NumberOfWheels, i_ModelName, i_PlateNumber, i_MaxWheelPressure, i_WheelManufacturer)
        {
            m_Type = i_Type;
            r_MaxFuelLevel = i_MaxFuelLevel;
            m_FuelLevel = i_MaxFuelLevel;
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

        public float FuelLevel
        {
            get { return m_FuelLevel; }
            set
            {
                if (m_FuelLevel + value > r_MaxFuelLevel)
                {
                    // trhow exception
                }
                m_FuelLevel += value;
                calculatePercentOfRemainingEnergy();
            }
        }

        public float MaxEnergyLevel // ONLY GET, cannot change max fuel level - readonly!
        {
            get { return r_MaxFuelLevel; }
        }
    }
}