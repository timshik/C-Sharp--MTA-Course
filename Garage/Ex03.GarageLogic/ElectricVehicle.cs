namespace Garage
{
    using System.Text;
    using n_Strings;

    public abstract class ElectricVehicle : BaseVehicle
    {
        private float m_RemainingBatteryTime = 0;
        private float m_MaxBatteryTime;

        public ElectricVehicle(int i_NumberOfWheels, string i_ModelName, string i_PlateNumber, float i_MaxWheelPressure, string i_WheelManufacturer, float i_MaxBatteryTime)
            : base(i_NumberOfWheels, i_ModelName, i_PlateNumber, i_MaxWheelPressure, i_WheelManufacturer)
        {
            m_MaxBatteryTime = i_MaxBatteryTime;
            m_RemainingBatteryTime = 100;
        }

        private void calculatePercentOfRemainingEnergy()
        {
            m_PercentOfRemainingEnergy = (m_MaxBatteryTime / m_RemainingBatteryTime) * 100;
        }

        public float RemainingBatteryTime
        {
            get { return m_MaxBatteryTime; }
            set { m_MaxBatteryTime = value; }
        }

        public float MaxBatteryTime
        {
            get { return m_RemainingBatteryTime; }
            set { m_RemainingBatteryTime = value; }
        }

        public void ChargeBattery(float i_AmmountofElectricToCharge)
        {
            if (m_RemainingBatteryTime + i_AmmountofElectricToCharge > m_MaxBatteryTime)
            {
                //// TODO : Throw exception
            }

            m_RemainingBatteryTime += i_AmmountofElectricToCharge;
            calculatePercentOfRemainingEnergy();
        }

        public override string ToString()
        {
            StringBuilder vehicleDetails = new StringBuilder();

            vehicleDetails.Append(base.ToString());
            vehicleDetails.AppendLine(Strings.electric_vehicle_battery);
            vehicleDetails.AppendFormat(Strings.battery_level, m_RemainingBatteryTime);

            return vehicleDetails.ToString();
        }
    }
}