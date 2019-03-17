using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.ConsoleUI
{
    public class Constants
    {
        //// PRINTING LOCATIONS
        public const int k_StartPrintingMenuColumn = 10;
        public const int k_StartPrintingMenuLine = 3;
        //// FIRST DECISIONS
        public const char k_NewVehicle = '1';
        public const char k_WantInformation = '2';
        public const char k_UpdateVehicleData = '3';
        public const char k_Exit = '4';

        //// WRONG MESSEGAS
        public const string k_WrongInputMessage = "|  => Wrong Input, Try Again                             |";
        //// Receive Information Display Menu
        public const char k_AllVehiclesLicenseNumbers = '1';
        public const char k_SpecificVehicleFullData = '2';

        //// Print All Vehicles License Number
        public const char k_InProgress = '1';
        public const char k_WaitingToGetPayment = '2';
        public const char k_PaidAndReadyToGo = '3';
        public const char k_AllTheVehicles = '4';

        //// Update Vehicle Data
        public const char k_ChangeVehicleStatus = '1';
        public const char k_InflateWheels = '2';
        public const char k_FillEnergy = '3';
        //// GENERAL
        public const char k_PreviewMenu = 'p';
        public const string k_PreviewMenuSTR = "p";
        public const bool k_WrongInput = false;
        public const int k_WrongInputPrintingLine = 5;
        public const int k_WrongInputMessageLineCommon = 3;
        public const char k_ValueToDecreaseFromCharToGetInt = '0';
        public const float k_MinimumPSIPressure = 0;
        public const float k_MinimumEngineCapacity = 0;
        public const float k_MinimumCapacityInCC = 0;
        public const char k_Yes = '1';
        public const char k_No = '2';
        public const float k_MaxEngineCapaityInCC = 10000;
        public const float k_MaxTrunkeCapaityInCC = 100000;
        public const string k_StringIsEmpty = "e";
        public const char k_CharIsEmpty = 'e';

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

        //// ENGINE TYPES
        public const char k_Fuel = '1';
        public const char k_Electric = '2';

        //// BATTERY HOURS
        public const float k_MotorcycleBatteryMaxHours = 1.8F;
        public const float k_CarBatteryMaxHours = 3.2F;

        //// FUEL TANK
        public const int k_MotorcycleFuelTankCapacity = 6;
        public const int k_CarFuelTankCapacity = 45;
        public const int k_TruckFuelTankCapacity = 115;
        //// COLORS 
        public const char k_ColorGrey = '1';
        public const char k_ColorBlue = '2';
        public const char k_ColorWhite = '3';
        public const char k_ColorBlack = '4';
        //// COOLER TRUNK
        public const char k_TheTrunkISCooler = '1';
        public const char k_TheTrunkISNOTCooler = '2';
        //// NUMBER OF DOORS
        public const char k_FiveDoors = '5';
        public const char k_TwoDoors = '2';
        public const char k_ThreeDoors = '3';
        public const char k_FourDoors = '4';
    }
}
