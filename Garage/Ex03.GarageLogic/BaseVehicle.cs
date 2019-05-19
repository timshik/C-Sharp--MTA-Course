using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using n_Wheel;

namespace Garage
{
    abstract public class BaseVehicle
    {
        string m_ModelName, m_PlateNumber;
        protected float m_PercentOfRemainingEnergy;
        Wheel[] m_Wheels;
        
        public BaseVehicle(int i_NumberOfWheels, string i_ModelName,
            string i_PlateNumber, float i_MaxWheelPressure, string i_WheelManufacturer)
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
            set { m_PercentOfRemainingEnergy = value; }
            get { return m_PercentOfRemainingEnergy; }
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
    }
}