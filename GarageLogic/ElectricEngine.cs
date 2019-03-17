using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        //// ctor
        public ElectricEngine(float i_MaxEnergyCapacity) : base(i_MaxEnergyCapacity)
        {
        }

        // $G$ DSN-999 (-5) This function sinature is not
        // as instructed in the exercise , the middle of page 2 
        //// methods
        public override void FillEnergy(float i_EnergyAmountToFill, char i_FuelType)
        { //// i_EnergyToFill is receiving in minutes
            if (HoursConvertToMinutes(AvailableEnergyStatus) < i_EnergyAmountToFill)
            {
                throw new ValueOutOfRangeException(Constants.k_ChargingAction, i_EnergyAmountToFill, HoursConvertToMinutes(AvailableEnergyStatus), Constants.k_ToMuchHoursToChargeMessage);
            }
            else
            { //// filling energy was a success!!
                CurrentEnergyStatus += minutesConvertToHours(i_EnergyAmountToFill);
            }
        }

        public float minutesConvertToHours(float i_Minutes)
        { //// this function convert minutes to hours in float values
            return i_Minutes / Constants.k_MinutesPerHour;
        }

        private float HoursConvertToMinutes(float i_Hours)
        { //// this function convert minutes to hours in float values
            return i_Hours * Constants.k_MinutesPerHour;
        }

        public override string EngineInformation()
        {
            string currentEnergyLevelMessage;
            string maxEnergyLevelMessage;
            string typeEnergyMessage;
            currentEnergyLevelMessage = "Current Energy Level " + CurrentEnergyStatus.ToString() + " Hours";
            maxEnergyLevelMessage = "Max Energy Level " + MaxEnergyCapacity.ToString() + " Hours";
            typeEnergyMessage = "Energy Engine ";
            string fuelEngineInformation = string.Format("{0}\n{1}\n{2}", currentEnergyLevelMessage, maxEnergyLevelMessage, typeEnergyMessage);
            return fuelEngineInformation;
        }
    }
}
