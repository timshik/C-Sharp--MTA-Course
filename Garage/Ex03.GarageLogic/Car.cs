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
        CarColor.eCarColor m_CarColor;
        DoorNumber.eNumberOfDoors m_NumberOfDoors;

        public Car(string i_PlateNumber, DoorNumber.eNumberOfDoors i_NumberOfDoors, CarColor.eCarColor i_CarColor, string i_ModelName, string i_WheelManufacturer) 
            : base(sr_NumberOfWheels, sr_FullTunkLevel, i_ModelName, i_PlateNumber, sr_MaxPressure, i_WheelManufacturer, eEnergyType.Octan95)
        {
            m_NumberOfDoors = i_NumberOfDoors;
            m_CarColor = i_CarColor;
        }

        public DoorNumber.eNumberOfDoors NumberOfDoors
        {
            get { return m_NumberOfDoors; }
            set { m_NumberOfDoors = value; }
        }
        public CarColor.eCarColor CarColor
        {
            get { return m_CarColor; }
            set { m_CarColor =value; }
        }
    }
}