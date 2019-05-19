namespace Garage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    abstract public class ElectricVehicle : BaseVehicle
    {
        float m_RemainingBatteryTime = 0;
        float m_MaxBatteryTime;

        public ElectricVehicle(int i_NumberOfWheels, string i_ModelName,
            string i_PlateNumber, float i_MaxWheelPressure, string i_WheelManufacturer, float i_MaxBatteryTime)
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
                //Throw exception
            }
            m_RemainingBatteryTime += i_AmmountofElectricToCharge;
            calculatePercentOfRemainingEnergy();
        }
    }
}