using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private readonly float r_TriedToFill;
        private readonly float r_MinValue = 0;
        private readonly float r_MaxAvailableCapacity;  //// equale to-> float MaxVaue as requested

        public ValueOutOfRangeException(
            string i_ActionToDo,
            float i_TriedToFill,
            float i_MaxAvailableCapacity,
            string i_WhatWeTriedToFill)
            : base(string.Format("An error occured while trying to {0} {1} {2}, please try to {0} 0 - {3} ", 
                      i_ActionToDo, i_TriedToFill, i_WhatWeTriedToFill, i_MaxAvailableCapacity))
        {
            r_TriedToFill = i_TriedToFill;
            r_MaxAvailableCapacity = i_MaxAvailableCapacity;
            if (i_MaxAvailableCapacity < r_MinValue)
            {
                r_MinValue = i_MaxAvailableCapacity;
            }
        }

        public float MinimumValue
        {
            get
            {
                return r_MinValue;
            }
        }

        public float MinimumValueToAdd 
        {
            get
            {
                if (r_MaxAvailableCapacity < r_MinValue)
                {
                    return r_MaxAvailableCapacity;
                }
                else
                {
                    return r_MinValue;
                }
            }
        }
    }
}
