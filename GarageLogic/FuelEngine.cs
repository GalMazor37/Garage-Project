using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class FuelEngine : Engine
    {
        public enum eFuelType
        {
            Octan95,
            Octan96,
            Octan98,
            Soler
        }
        //// members
        private readonly eFuelType r_FuelType;

        //// ctor
        public FuelEngine(float i_MaxEnergyCapacity, eFuelType i_FuelType) : base(i_MaxEnergyCapacity)
        {
            r_FuelType = i_FuelType;
        }

        public eFuelType FuelType
        {
            get { return r_FuelType; }
        }

        public static eFuelType GetFuelTypeFromChar(char i_CharInputForEnergyType)
        {
            eFuelType convertedFuelType;
            if (i_CharInputForEnergyType == Constants.k_Octan95)
            {
                convertedFuelType = eFuelType.Octan95;
            }
            else if (i_CharInputForEnergyType == Constants.k_Octan96)
            {
                convertedFuelType = eFuelType.Octan96;
            }
            else if (i_CharInputForEnergyType == Constants.k_Octan98)
            {
                convertedFuelType = eFuelType.Octan98;
            }
            else
            { 
                convertedFuelType = eFuelType.Soler;
            }

            return convertedFuelType;
        }

        public override string EngineInformation()
        {
            string currentEnergyLevelMessage;
            string maxEnergyLevelMessage;
            string typeEnergyMessage;
            currentEnergyLevelMessage = "Current Energy Level " + CurrentEnergyStatus.ToString() + " Liters";
            maxEnergyLevelMessage = "Max Energy Level " + MaxEnergyCapacity.ToString() + " Liters";
            typeEnergyMessage = "Fuel Type: " + r_FuelType.ToString();
            string fuelEngineInformation = string.Format("{0}\n{1}\n{2}", currentEnergyLevelMessage, maxEnergyLevelMessage, typeEnergyMessage);
            return fuelEngineInformation;
        }

        public override void FillEnergy(float i_EnergyToFill, char i_FuelType)
        {
            eFuelType i_CurrentFuelType = GetFuelTypeFromChar(i_FuelType);
            if (i_CurrentFuelType != r_FuelType)
            {
                throw new ArgumentException(Constants.k_WrongFuelMessage + r_FuelType);
            }

            if (AvailableEnergyStatus < i_EnergyToFill || i_EnergyToFill < 0)
            {
                throw new ValueOutOfRangeException(Constants.k_FillingFuelAction, i_EnergyToFill, AvailableEnergyStatus, Constants.k_ToMuchFuelMessage);
            }
            else
            { //// filling energy was a success!!
                CurrentEnergyStatus += i_EnergyToFill;
            }
        }
    }
}
