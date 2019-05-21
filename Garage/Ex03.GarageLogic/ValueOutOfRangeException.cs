using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using n_Strings;

namespace Garage
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_MaxValue, m_MinValue;

        public ValueOutOfRangeException(float i_MaxValue, float i_MinValue, string i_Message) 
            : base(i_Message)
        {
            m_MaxValue = i_MaxValue;
            m_MinValue = i_MinValue;
        }

        public float MaxValue
        {
            get { return m_MaxValue; }
        }

        public float MinValue
        {
            get { return m_MinValue; }
        }
    }
}
