namespace n_Vehicle
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using n_Wheel;

    public class Vehicle
    {
        string m_ModelName, m_PlateNumber;
        float m_FuelLevel;
        readonly float r_MaxFuelLevel;
        Wheel[] m_Wheels;
        eEnergyType m_Type;

        public Vehicle(int i_NumberOfWheels, float i_MaxFuelLevel, string i_ModelName, string i_PlateNumber, float i_MaxWheelPressure)
        {
            r_MaxFuelLevel = i_MaxFuelLevel;
            m_Wheels = new Wheel[i_NumberOfWheels];
            m_ModelName = i_ModelName;
            m_PlateNumber = i_PlateNumber;
            m_FuelLevel = i_MaxFuelLevel/4; // all vehicles get 1/4 tank - FUNNY?

            for (int i = 0; i < i_NumberOfWheels; i++)
            {

            }
        }

        public Wheel[] Wheels
        {
            get { return m_Wheels; }
            set { m_Wheels = value; }
        }

        public string ModelName
        {
            get { return m_ModelName; }
            set { m_ModelName = value; }
        }

        public string PlateNumber
        {
            get { return m_PlateNumber; }
            set { m_PlateNumber = value; }
        }

        public enum eEnergyType
        {
            Electric,
            Soler,
            Octan95,
            Octan96
        }

        public eEnergyType EnergyType
        {
            get { return m_Type; }
            set { m_Type = value; }
        }

        public float FuelLevel
        {
            get { return m_FuelLevel; }
            set { m_FuelLevel = value; } // TODO: check if adding fuel pass the limit
        }

        public float MaxFuelLevel // ONLY GET, cannot change max fuel level - readonly!
        { 
            get { return r_MaxFuelLevel; } 
        }
    }
}