namespace n_Car
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using n_Wheel;
    using n_Vehicle;
    using Garage;

    public class Car : FuelVehicle
    {
        public static readonly int sr_NumberOfWheels = 4, sr_FullTunkLevel = 55, sr_MaxPressure = 31;
        eCarColor m_CarColor;
        eNumberOfDoors m_NumberOfDoors;

        public Car(string i_PlateNumber, float i_MaxEnergeyLevel, float i_FuelLevel, eEnergyType i_Type,
        eNumberOfDoors i_NumberOfDoors, eCarColor i_CarColor, string i_ModelName, string i_WheelManufacturer) 
            : base(sr_NumberOfWheels, i_MaxEnergeyLevel, i_ModelName, i_PlateNumber, sr_MaxPressure, i_WheelManufacturer, i_Type)
        {
            m_NumberOfDoors = i_NumberOfDoors;
            m_CarColor = i_CarColor;
        }

        public eNumberOfDoors NumberOfDoors
        {
            get { return m_NumberOfDoors; }
            set { m_NumberOfDoors = value; }
        }
        public eCarColor CarColor
        {
            get { return m_CarColor; }
            set { m_CarColor =value; }
        }
    }
}