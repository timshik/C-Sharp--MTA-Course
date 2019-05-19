using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage
{
    class ElectricCar : ElectricVehicle
    {
        private static readonly int sr_NumberOfWheels = 4, sr_MaxPressure = 33;
        private static readonly float sr_FullBatteryLevel = 1.8f;
        eCarColor m_CarColor;
        eNumberOfDoors m_NumberOfDoors;

        public ElectricCar(string i_ModelName, string i_PlateNumber, string i_WheelManufacturer, eCarColor i_CarColor, eNumberOfDoors i_NumberOfDoors)
            : base(sr_NumberOfWheels, i_ModelName, i_PlateNumber, sr_MaxPressure, i_WheelManufacturer , sr_FullBatteryLevel)
        {
            m_CarColor = i_CarColor;
            m_NumberOfDoors = i_NumberOfDoors;
        }

        public eCarColor CarColor
        {
            get{ return m_CarColor; }
            set { m_CarColor = value; }
        }
        public eNumberOfDoors NumberOfDoors
        {
            get { return m_NumberOfDoors; }
            set { m_NumberOfDoors = value; }
        }
    }
}