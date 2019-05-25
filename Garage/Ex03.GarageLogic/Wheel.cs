namespace n_Wheel
{
    using System.Text;
    using n_Strings;
    using Garage;

    public class Wheel
    {
        private readonly float r_MaxPressure;
        private string m_Manufacturer;
        private float m_CurrentPressure;

        public Wheel(string i_ManufacturerName, float i_MaxPressure, float i_CurrentPressure)
        {
            r_MaxPressure = i_MaxPressure;
            m_Manufacturer = i_ManufacturerName;
            m_CurrentPressure = 0;
            FillTire(i_CurrentPressure);
        }

        public void FillTire(float i_UnitToFill)
        {
            if (m_CurrentPressure + i_UnitToFill > r_MaxPressure || m_CurrentPressure + i_UnitToFill < 0)
            {
                throw new ValueOutOfRangeException(r_MaxPressure - m_CurrentPressure, 0, Strings.out_of_range);
            }

            m_CurrentPressure += i_UnitToFill;
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

        public override string ToString()
        {
            StringBuilder vehicleDetails = new StringBuilder();

            vehicleDetails.AppendLine(string.Format(Strings.wheel_manufacturer, ManufacturerName));
            vehicleDetails.AppendLine(string.Format(Strings.wheel_current_pressure, CurrentPressure));
            return vehicleDetails.ToString();
        }
    }
}