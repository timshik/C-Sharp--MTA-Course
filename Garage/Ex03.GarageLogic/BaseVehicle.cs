using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using n_Wheel;
using n_Strings;

namespace Garage
{
    public abstract class BaseVehicle
    {
        private string m_ModelName, m_PlateNumber;
        protected float m_PercentOfRemainingEnergy;
        private Wheel[] m_Wheels;

        public BaseVehicle(Dictionary<string, object> i_Arguments) 
        {
            int numberOfWheels = (int)i_Arguments[VehicleManager.sr_KeyNumberOfWheels];
            m_Wheels = new Wheel[numberOfWheels];
            m_ModelName = (string)i_Arguments[VehicleManager.sr_KeyModelName];
            m_PlateNumber = (string)i_Arguments[VehicleManager.sr_KeyPlateNumber];

            string wheelManufacturer = (string)i_Arguments[VehicleManager.sr_KeyWheelManufacturer];
            float wheelMaxPressure = (float)i_Arguments[VehicleManager.sr_KeyMaxWheelPressure];

            for (int i = 0; i < numberOfWheels; i++)
            {
                m_Wheels[i] = new Wheel(wheelManufacturer, wheelMaxPressure);
            }
        }

        public float PercentOfRemainingEnergy
        {
            get { return m_PercentOfRemainingEnergy; }
            set { m_PercentOfRemainingEnergy = value; }
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

        public void FillTires()
        {
            foreach (Wheel wheel in m_Wheels)
            {
                wheel.FillTire(wheel.MaxPressure - wheel.CurrentPressure);
            }
        }

        public override string ToString()
        {
            StringBuilder vehicleDetails = new StringBuilder();
            vehicleDetails.AppendFormat(Strings.show_type_of_vehicle, GetType().Name);
            vehicleDetails.AppendFormat(Strings.plate_number, m_PlateNumber);
            vehicleDetails.AppendFormat(Strings.model_name, m_ModelName);
            vehicleDetails.AppendFormat(Strings.remaning_energy, m_PercentOfRemainingEnergy);
            vehicleDetails.AppendFormat(m_Wheels[0].ToString());

            return vehicleDetails.ToString();
        }
    }
}