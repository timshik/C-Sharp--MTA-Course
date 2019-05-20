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
        
        public BaseVehicle(int i_NumberOfWheels, string i_ModelName, string i_PlateNumber, float i_MaxWheelPressure, string i_WheelManufacturer)
        {
            m_Wheels = new Wheel[i_NumberOfWheels];
            m_ModelName = i_ModelName;
            m_PlateNumber = i_PlateNumber;

            for (int i = 0; i < i_NumberOfWheels; i++)
            {
                m_Wheels[i] = new Wheel(i_WheelManufacturer, i_MaxWheelPressure);
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

        public override string ToString()
        {
            StringBuilder vehicleDetails = new StringBuilder();
            vehicleDetails.AppendFormat(Strings.plate_number, m_PlateNumber);
            vehicleDetails.AppendFormat(Strings.model_name, m_ModelName);
            vehicleDetails.AppendFormat(Strings.remaning_energy, m_PercentOfRemainingEnergy);
            vehicleDetails.AppendFormat(m_Wheels[0].ToString());

            return vehicleDetails.ToString();
        }
    }
}