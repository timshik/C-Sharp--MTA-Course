using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n_Wheel
{
    public class Wheel
    {
        string m_Manufacturer;
        float m_CurrentPressure;
        readonly float r_MaxPressure;

        public Wheel(string i_ManufacturerName, float i_MaxPressure)
        {
            m_Manufacturer = i_ManufacturerName;
            m_CurrentPressure = i_MaxPressure;
            r_MaxPressure = i_MaxPressure;
        }

        public void FillTyre(float i_UnitToFill)
        {
            if(m_CurrentPressure + i_UnitToFill > r_MaxPressure)
            {
                m_CurrentPressure = r_MaxPressure;
            }
            else
            {
                m_CurrentPressure += r_MaxPressure;
            }
        }

        public float CurrentPressure
        {
            get { return m_CurrentPressure; }
            set { m_CurrentPressure = value; }
        }

        public float MaxPressure
        {
            get { return r_MaxPressure; }
        }

        public string ManufacturerName
        {
            get { return m_Manufacturer; }
            set { m_Manufacturer = value; }
        }
    }
}
