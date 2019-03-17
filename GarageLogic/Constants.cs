using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Constants
    {
        public const int k_MaxCharsLicenseNumber = 20;
        //// WHEELS NUMBER
        public const int k_MotorcycleNumberOfWheels = 2;
        public const int k_TruckNumberOfWheels = 12;
        public const int k_CarNumberOfWheels = 4;
        //// AIR PRUSSERE
        public const float k_MaxMotorcycleAirPressure = 30;
        public const float k_MaxCarAirPressure = 32;
        public const float k_MaxTruckAirPressure = 28;
        //// FUEL TANK
        public const float k_MotorcycleFuelTackCapacity = 6;
        public const float k_CarFuelTankCapacity = 45;
        public const float k_TruckFuelTankCapacity = 115;
        //// BATTERY HOURS
        public const float k_MotorcycleBatteryMaxHours = 1.8F;
        public const float k_CarBatteryMaxHours = 3.2F;

        //// ERROR MESSEGAS REASONS
        public const string k_ToMuchPsiMessage = "PSI in the wheel";
        public const string k_ToMuchFuelMessage = "Liters of fuel in the tank";
        public const string k_ToMuchHoursToChargeMessage = "Minutes in the battery";
        public const string k_WrongFuelMessage = "Wrong type of fuel, please choose ";

        //// ACTIONS
        public const string k_FillingFuelAction = "fill";
        public const string k_ChargingAction = "charge";
        public const string k_InflateAction = "inflate";

        //// CALCULATE VALUES
        public const float k_MinutesPerHour = 60;
        public const float k_PercentToMultiply = 100;

        //// VEHICLE STATUS
        public const char k_InProgress = '1';
        public const char k_WaitingToGetPayment = '2';
        public const char k_PaidAndReadyToGo = '3';

        //// ENGINE TYPES
        public const char k_Fuel = '1';
        public const char k_Electric = '2';

        //// FUEL TYPES
        public const char k_Octan95 = '1';
        public const char k_Octan96 = '2';
        public const char k_Octan98 = '3';
        public const char k_Soler = '4';
        public const char k_Electricity = '0';

        //// VEHICLE TYPES
        public const char k_Car = '1';
        public const char k_Motorcycle = '2';
        public const char k_Truck = '3';

        /// LICENSE TYPES 
        public const char k_LicenseTypeA = '1';
        public const char k_LicenseTypeA1 = '2';
        public const char k_LicenseTypeB1 = '3';
        public const char k_LicenseTypeB2 = '4';
        //// FILLING MESSAGES
        public const string k_EnterTimeToCharge = "|  ENTER TIME TO CHARGE (IN MINUTES) :                   |";
        public const string k_EnterLittersAmountToFill = "|  ENTER AMOUNT OF FUEL TO FILL :                        |";
    }
}
