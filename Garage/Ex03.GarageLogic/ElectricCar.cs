namespace Garage
{
    using System.Collections.Generic;
    using System.Text;
    using n_Strings;

    public class ElectricCar : ElectricVehicle
    {
        public static readonly int sr_NumberOfWheels = 4;
        public static readonly float sr_FullBatteryLevel = 1.8f, sr_MaxPressure = 33;
        private CarColor.eCarColor m_CarColor;
        private DoorNumber.eNumberOfDoors m_NumberOfDoors;

        public ElectricCar(Dictionary<string, object> i_Arguments)
            : base(i_Arguments)
        {
            m_NumberOfDoors = (DoorNumber.eNumberOfDoors)i_Arguments[VehicleManager.sr_KeyNumberOfDoors];
            m_CarColor = (CarColor.eCarColor)i_Arguments[VehicleManager.sr_KeyCarColor];
        }

        public CarColor.eCarColor CarColor
        {
            get { return m_CarColor; }
            set { m_CarColor = value; }
        }

        public DoorNumber.eNumberOfDoors NumberOfDoors
        {
            get { return m_NumberOfDoors; }
            set { m_NumberOfDoors = value; }
        }

        public override string ToString()
        {
            StringBuilder vehicleDetails = new StringBuilder();

            vehicleDetails.Append(base.ToString());
            vehicleDetails.AppendFormat(Strings.car_color, Garage.CarColor.sr_CarColorNames[(int)m_CarColor]);
            vehicleDetails.AppendFormat(Strings.door_number, m_NumberOfDoors);

            return vehicleDetails.ToString();
        }
    }
}