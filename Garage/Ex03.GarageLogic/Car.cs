namespace n_Car
{
    using System.Text;
    using n_Vehicle;
    using Garage;
    using n_Strings;

    public class Car : FuelVehicle
    {
        public static readonly int sr_NumberOfWheels = 4, sr_FullTunkLevel = 55, sr_MaxPressure = 31;
        private CarColor.eCarColor m_CarColor;
        private DoorNumber.eNumberOfDoors m_NumberOfDoors;

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
            set { m_CarColor = value; }
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