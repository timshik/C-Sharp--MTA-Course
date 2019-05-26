namespace n_Car
{
    using System.Collections.Generic;
    using System.Text;
    using n_Vehicle;
    using Garage;
    using n_Strings;

    public class Car : FuelVehicle
    {
        public static readonly int sr_NumberOfWheels = 4;
        public static readonly float sr_MaxPressure = 31, sr_FullTunkLevel = 55;
        public static readonly eEnergyType sr_EnergyType = eEnergyType.Octan96;
        private CarColor.eCarColor m_CarColor;
        private DoorNumber.eNumberOfDoors m_NumberOfDoors;

        public Car(Dictionary<string, object> i_Arguments)
            : base(i_Arguments)
        {
            m_NumberOfDoors = (DoorNumber.eNumberOfDoors)i_Arguments[ArgumentsKeysets.sr_KeyNumberOfDoors];
            m_CarColor = (CarColor.eCarColor)i_Arguments[ArgumentsKeysets.sr_KeyCarColor];
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
            vehicleDetails.AppendLine(string.Format(Strings.car_color, Garage.CarColor.sr_CarColorNames[(int)m_CarColor]));
            vehicleDetails.AppendLine(string.Format(Strings.door_number, Garage.DoorNumber.sr_DoorsOptions[(int)m_NumberOfDoors - 2]));

            return vehicleDetails.ToString();
        }
    }
}