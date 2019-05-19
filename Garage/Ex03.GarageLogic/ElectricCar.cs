namespace Garage
{
    using System.Text;
    using n_Strings;

    public class ElectricCar : ElectricVehicle
    {
        private static readonly int sr_NumberOfWheels = 4, sr_MaxPressure = 33;
        private static readonly float sr_FullBatteryLevel = 1.8f;
        private CarColor.eCarColor m_CarColor;
        private DoorNumber.eNumberOfDoors m_NumberOfDoors;

        public ElectricCar(string i_ModelName, string i_PlateNumber, string i_WheelManufacturer, CarColor.eCarColor i_CarColor, DoorNumber.eNumberOfDoors i_NumberOfDoors)
            : base(sr_NumberOfWheels, i_ModelName, i_PlateNumber, sr_MaxPressure, i_WheelManufacturer, sr_FullBatteryLevel)
        {
            m_CarColor = i_CarColor;
            m_NumberOfDoors = i_NumberOfDoors;
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