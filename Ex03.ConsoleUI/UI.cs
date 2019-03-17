using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class UI
    {
        public enum eInputsToCheck
        {
            PhoneNumber,
            ChargingAmount,
            LicenseNumber
        }

        private readonly OutPutMessages r_Message = new OutPutMessages();
        private readonly CreateNewVehicle r_CreateNewVehicle = new CreateNewVehicle();
        private readonly Garage IOpenedNewGarage = new Garage();

        public Garage GetGarageFromUI
        {
            get { return IOpenedNewGarage; }
        }

        public void WorkingInTheGarage()
        {
            Console.Clear();
            char theDecisionToDoIs = chooseWhatToDo();
            while (theDecisionToDoIs != Constants.k_Exit)
            {
                if (theDecisionToDoIs == Constants.k_NewVehicle)
                {
                    createNewVehicleInTheGarage();
                }
                else if (theDecisionToDoIs == Constants.k_WantInformation)
                {
                    receiveInformation();
                }
                else if (theDecisionToDoIs == Constants.k_UpdateVehicleData)
                {
                    updateVehicleData();
                }

                theDecisionToDoIs = chooseWhatToDo();
            }

            Environment.Exit(0);
        }

        private char chooseWhatToDo()
        {        
            r_Message.ChooseWhatToDoDisplayMenu();
            char decision = Console.ReadKey().KeyChar;
            while (decision != Constants.k_NewVehicle && decision != Constants.k_WantInformation && decision != Constants.k_UpdateVehicleData && decision != Constants.k_Exit)
            {
                r_Message.PrintWrongMessage();
                decision = Console.ReadKey().KeyChar;
            }

            Console.Clear();
            return decision;
        }

        private void receiveInformation()
        {
            char decision;
            bool actionCompletedSuccessfullyOrPreviewMenu = false;
            while (!actionCompletedSuccessfullyOrPreviewMenu)
            {
                r_Message.ReceiveInformationDisplayMenu();
                decision = Console.ReadKey().KeyChar;

                while (decision != Constants.k_AllVehiclesLicenseNumbers && decision != Constants.k_SpecificVehicleFullData && decision != Constants.k_PreviewMenu)
                {
                    r_Message.PrintWrongMessage();
                    decision = Console.ReadKey().KeyChar;
                }

                Console.Clear();
                if (!backToPreviewScreen(decision))
                {
                    if (decision == Constants.k_AllVehiclesLicenseNumbers)
                    {
                        actionCompletedSuccessfullyOrPreviewMenu = printAllVehiclesLicenseNumber();
                    }
                    else
                    {
                        actionCompletedSuccessfullyOrPreviewMenu = printAllDataForSpecificVehicle();
                    }
                }
                else
                {
                    actionCompletedSuccessfullyOrPreviewMenu = true;
                }
            }
        } //// return to starting menu

        private bool printAllVehiclesLicenseNumber()
        {
            VehicleInTheGarage.eVehicleStatus VehicleStatusToPrint;
            bool printAllVehicles;
            bool printAllVehiclesLicenseNumberSuccessed = false;
            r_Message.PrintAllVehiclesLicenseNumberDisplayMenu();
            char decision = Console.ReadKey().KeyChar;
            while (decision != Constants.k_InProgress && decision != Constants.k_WaitingToGetPayment && decision != Constants.k_PaidAndReadyToGo && decision != Constants.k_AllTheVehicles && decision != Constants.k_PreviewMenu)
            {
                r_Message.PrintWrongMessage();
                decision = Console.ReadKey().KeyChar;
            }

            Console.Clear();
            if (!backToPreviewScreen(decision))
            {
                printAllVehicles = getVehicleStatusFromChar(decision, out VehicleStatusToPrint);
                if (printAllVehicles)
                {
                    Console.WriteLine("ALL VEHICLES LICENSE NUMBER : \n");
                }
                else
                {
                    Console.WriteLine("ALL VEHICLES LICENSE NUMBER IN STATUS {0} : \n", VehicleStatusToPrint);
                }

                foreach (KeyValuePair<string, VehicleInTheGarage> i_PrintLicenseNumbers in IOpenedNewGarage.AllVehiclesInTheGarage)
                {
                    if (printAllVehicles)
                    {
                        Console.WriteLine(i_PrintLicenseNumbers.Key + "\n");
                    }
                    else if (i_PrintLicenseNumbers.Value.StatusInTheGarage == VehicleStatusToPrint)
                    {
                        Console.WriteLine(i_PrintLicenseNumbers.Key + "\n");
                    }
                }

                Thread.Sleep(5000);
                Console.Clear();
                printAllVehiclesLicenseNumberSuccessed = true;
            }

            return printAllVehiclesLicenseNumberSuccessed;
        }
              
        private bool getVehicleStatusFromChar(char i_CharInputForVehicleStatus, out VehicleInTheGarage.eVehicleStatus i_CurrentStatus)
        {
            bool printAllVehicles = true;
            i_CurrentStatus = VehicleInTheGarage.eVehicleStatus.PaidAndReadyToGo; //// insert data just to be able to return in case of all vehicles
            if (i_CharInputForVehicleStatus == Constants.k_InProgress)
            {
                printAllVehicles = false;
                i_CurrentStatus = VehicleInTheGarage.eVehicleStatus.InProgress;
            }
            else if (i_CharInputForVehicleStatus == Constants.k_WaitingToGetPayment)
            {
                printAllVehicles = false;
                i_CurrentStatus = VehicleInTheGarage.eVehicleStatus.WaitingToGetPayment;
            }
            else if (i_CharInputForVehicleStatus == Constants.k_PaidAndReadyToGo)
            {
                printAllVehicles = false;
                i_CurrentStatus = VehicleInTheGarage.eVehicleStatus.PaidAndReadyToGo;
            }

            return printAllVehicles;
        }

        private bool printAllDataForSpecificVehicle()
        {
            VehicleInTheGarage vehicleToPrint = null;
            bool successfulyPrintedAllDataForSpecificVehicle = false;
            string licenseNumberToPrintData;
            r_Message.AskingForVehicleLicenseNumberDisplayMenu();
            licenseNumberToPrintData = Console.ReadLine();
            while (isTheInputCorrect(licenseNumberToPrintData, eInputsToCheck.LicenseNumber) == Constants.k_WrongInput)
            { //// wrong license number input
                r_Message.AskingForVehicleLicenseNumberDisplayMenu();
                licenseNumberToPrintData = Console.ReadLine();
            }

            while (!backToPreviewScreen(licenseNumberToPrintData) && !successfulyPrintedAllDataForSpecificVehicle)
            { //// return to preview menu                   
                if (!IOpenedNewGarage.LicenseNumberExist(licenseNumberToPrintData))
                {
                    r_Message.LicenseNumberNotExistMessage();
                    r_Message.AskingForVehicleLicenseNumberDisplayMenu();
                    licenseNumberToPrintData = Console.ReadLine();
                }
                else
                {
                    //// license number exist- lets print its information
                    IOpenedNewGarage.AllVehiclesInTheGarage.TryGetValue(licenseNumberToPrintData, out vehicleToPrint);
                    Console.Clear();
                    Console.Write(vehicleToPrint.VehicleInTheGarageInfo());
                    Thread.Sleep(10000);
                    Console.Clear();
                    successfulyPrintedAllDataForSpecificVehicle = true;
                }
            }
           
            return successfulyPrintedAllDataForSpecificVehicle;
        }
               
        private bool updateVehicleData()
        {
            bool actionCompletedSuccessfullyOrPreviewMenu = false;
            while (!actionCompletedSuccessfullyOrPreviewMenu)
            {
                r_Message.UpdateVehicleDataDisplayMenu();
                char decision = Console.ReadKey().KeyChar;
                while (decision != Constants.k_ChangeVehicleStatus && decision != Constants.k_InflateWheels && decision != Constants.k_FillEnergy && decision != Constants.k_PreviewMenu)
                {
                    r_Message.PrintWrongMessage();
                    decision = Console.ReadKey().KeyChar;
                }

                Console.Clear();
                if (!backToPreviewScreen(decision))
                {
                    if (decision == Constants.k_ChangeVehicleStatus)
                    {
                        actionCompletedSuccessfullyOrPreviewMenu = changeVehicleStatus();
                    }
                    else if (decision == Constants.k_InflateWheels)
                    {
                        actionCompletedSuccessfullyOrPreviewMenu = inflateVehicleWheels();
                    }
                    else
                    { //// decision == Constants.k_FillEnergy
                        actionCompletedSuccessfullyOrPreviewMenu = fillingEnergyInVehicle();
                    }
                }    
                else
                {
                    actionCompletedSuccessfullyOrPreviewMenu = true;
                }            
            }

            return actionCompletedSuccessfullyOrPreviewMenu;
        } //// return to starting menu

        private bool changeVehicleStatus()
        {
            string licenseNumberToChangeStatus;
            char decision;
            bool successfulyChangedVehicleStatus = false;
            r_Message.AskingForVehicleLicenseNumberDisplayMenu();
            licenseNumberToChangeStatus = Console.ReadLine();
            while (isTheInputCorrect(licenseNumberToChangeStatus, eInputsToCheck.LicenseNumber) == Constants.k_WrongInput)
            { //// wrong license number input
                r_Message.AskingForVehicleLicenseNumberDisplayMenu();
                licenseNumberToChangeStatus = Console.ReadLine();
            }

            while (!backToPreviewScreen(licenseNumberToChangeStatus) && !successfulyChangedVehicleStatus)
            {
                if (!IOpenedNewGarage.LicenseNumberExist(licenseNumberToChangeStatus))
                {
                    r_Message.LicenseNumberNotExistMessage();
                    r_Message.AskingForVehicleLicenseNumberDisplayMenu();
                    licenseNumberToChangeStatus = Console.ReadLine();
                }
                else
                {
                    r_Message.ChangeVehicleStatusDisplayMenu();
                    decision = Console.ReadKey().KeyChar;
                    while (decision != Constants.k_InProgress && decision != Constants.k_WaitingToGetPayment && decision != Constants.k_PaidAndReadyToGo && decision != Constants.k_PreviewMenu)
                    {
                        r_Message.PrintWrongMessage();
                        decision = Console.ReadKey().KeyChar;
                    }

                    if (!backToPreviewScreen(decision))
                    {
                        IOpenedNewGarage.UpdateVehicleStatus(licenseNumberToChangeStatus, decision);
                        r_Message.GarageUpdateStatusForExistVehicle();
                        successfulyChangedVehicleStatus = true;
                    }
                    else
                    {
                        r_Message.AskingForVehicleLicenseNumberDisplayMenu();
                        licenseNumberToChangeStatus = Console.ReadLine();
                    }
                }
            }

            return successfulyChangedVehicleStatus;
        }
        
        private bool inflateVehicleWheels()
        {
            string licenseNumberToInflateWheels;
            bool successfulyInflatedVehicleWheels = false;
            r_Message.AskingForVehicleLicenseNumberDisplayMenu();
            licenseNumberToInflateWheels = Console.ReadLine();
            while (isTheInputCorrect(licenseNumberToInflateWheels, eInputsToCheck.LicenseNumber) == Constants.k_WrongInput)
            { //// wrong license number input
                r_Message.AskingForVehicleLicenseNumberDisplayMenu();
                licenseNumberToInflateWheels = Console.ReadLine();
            }

            while (!backToPreviewScreen(licenseNumberToInflateWheels) && !successfulyInflatedVehicleWheels)
            {
                if (!IOpenedNewGarage.LicenseNumberExist(licenseNumberToInflateWheels))
                {
                    r_Message.LicenseNumberNotExistMessage();
                }
                else
                {
                    try
                    {
                        IOpenedNewGarage.InflateAirInWheels(licenseNumberToInflateWheels);
                        r_Message.SuccessMessageForInflateAction();
                        successfulyInflatedVehicleWheels = true;
                    }
                    catch (ValueOutOfRangeException InflateFailed)
                    {
                        Console.Clear();
                        Console.WriteLine("Catching ValueOutOfRangeException: ");
                        Console.WriteLine(InflateFailed.Message);
                        Thread.Sleep(4500);
                        Console.Clear();
                        successfulyInflatedVehicleWheels = false;
                    }
                }

                if (!successfulyInflatedVehicleWheels)
                {
                    r_Message.AskingForVehicleLicenseNumberDisplayMenu();
                    licenseNumberToInflateWheels = Console.ReadLine();
                }
            }

            return successfulyInflatedVehicleWheels;
        }

        private bool fillingEnergyInVehicle()
        {
            string licenseNumberToFillingEnergy;
            bool successfulyFilledgEnergyInVehicle = false;
            r_Message.AskingForVehicleLicenseNumberDisplayMenu();
            licenseNumberToFillingEnergy = Console.ReadLine();

            while (isTheInputCorrect(licenseNumberToFillingEnergy, eInputsToCheck.LicenseNumber) == Constants.k_WrongInput)
            { //// wrong license number input
                r_Message.AskingForVehicleLicenseNumberDisplayMenu();
                licenseNumberToFillingEnergy = Console.ReadLine();
            }

            while (!backToPreviewScreen(licenseNumberToFillingEnergy) && !successfulyFilledgEnergyInVehicle)
            {
                if (!IOpenedNewGarage.LicenseNumberExist(licenseNumberToFillingEnergy))
                {
                    r_Message.LicenseNumberNotExistMessage();
                }
                else
                { //// LICENSE NUMBER EXIST
                    try
                    {
                        successfulyFilledgEnergyInVehicle = fillinFuelOrElectricInVehicle(licenseNumberToFillingEnergy);
                    }
                    catch (Exception FillingEnergyFailed)
                    {
                        Console.Clear();
                        Console.WriteLine("Catching Exception : \n");
                        Console.WriteLine(FillingEnergyFailed.Message);
                        Thread.Sleep(4500);
                        Console.Clear();
                        successfulyFilledgEnergyInVehicle = false;
                    }
                }

                if (!successfulyFilledgEnergyInVehicle)
                {
                    r_Message.AskingForVehicleLicenseNumberDisplayMenu();
                    licenseNumberToFillingEnergy = Console.ReadLine();
                }
            }

            return successfulyFilledgEnergyInVehicle;
        }

        private bool fillinFuelOrElectricInVehicle(string i_LicenseNumberOfVehicleToFill)
        {
            char FuelTypeSign = Constants.k_CharIsEmpty;
            bool successfulyFilledgEnergyInVehicle = false;
            bool recievedFuelTypeToFill = false;
            bool backToPreviewMenu = false;
            string energyAmountToAddMessage;
            float energyAmountToAdd;
            string energyAmountToAddSTR;

            while (!backToPreviewMenu)
            { //// case the user doesn't want to go back to preview menu  
                if (IOpenedNewGarage.IsItAFuelEngine(i_LicenseNumberOfVehicleToFill, out energyAmountToAddMessage) && (!recievedFuelTypeToFill))
                { //// this is a fuel engine
                    r_Message.ChooseFuelInVehicleTypeDisplayMenu();
                    FuelTypeSign = Console.ReadKey().KeyChar;
                    while (FuelTypeSign != Constants.k_Octan95 && FuelTypeSign != Constants.k_Octan96 && FuelTypeSign != Constants.k_Octan98 && FuelTypeSign != Constants.k_Soler)
                    {
                        r_Message.ChooseFuelInVehicleTypeDisplayMenu();
                        FuelTypeSign = Console.ReadKey().KeyChar;
                    }

                    recievedFuelTypeToFill = true;
                }
                else
                { //// this is an electric engine
                    FuelTypeSign = Constants.k_Electricity;
                    recievedFuelTypeToFill = true;
                }

                if (!backToPreviewScreen(FuelTypeSign))
                {
                    r_Message.FillingEnergyInVehicleChooseAmountDisplayMenu(energyAmountToAddMessage);
                    energyAmountToAddSTR = Console.ReadLine();
                    while (!backToPreviewScreen(energyAmountToAddSTR) && !successfulyFilledgEnergyInVehicle)
                    {
                        if (float.TryParse(energyAmountToAddSTR, out energyAmountToAdd))
                        { //// case the string input could not convert to float
                            try
                            {
                                IOpenedNewGarage.FillingEnergyInTheVehicle(energyAmountToAdd, i_LicenseNumberOfVehicleToFill, FuelTypeSign);
                                r_Message.SuccessMessageForFillingEnergyAction();
                                successfulyFilledgEnergyInVehicle = true;
                            }
                            catch (Exception FillingEnergyFailed)
                            {
                                Console.Clear();
                                Console.WriteLine("Catching Exception : \n");
                                Console.WriteLine(FillingEnergyFailed.Message);
                                Thread.Sleep(4500);
                                Console.Clear();
                                successfulyFilledgEnergyInVehicle = false;
                            }
                        }
                        
                        if (!successfulyFilledgEnergyInVehicle)
                        {
                            r_Message.FillingEnergyInVehicleChooseAmountDisplayMenu(energyAmountToAddMessage);
                            energyAmountToAddSTR = Console.ReadLine();
                        }                       
                    }

                    if (FuelTypeSign == Constants.k_Electricity)
                    {
                        backToPreviewMenu = true;
                    }
                    else
                    {
                        recievedFuelTypeToFill = false;
                    }
                }
                else
                {
                    backToPreviewMenu = false;
                }          
            }

            return successfulyFilledgEnergyInVehicle;
        }

        private void createNewVehicleInTheGarage()
        {
            string licenseNumberToCreateVehicle;
            r_Message.AskingForVehicleLicenseNumberDisplayMenu();
            licenseNumberToCreateVehicle = Console.ReadLine();
            while (isTheInputCorrect(licenseNumberToCreateVehicle, eInputsToCheck.LicenseNumber) == Constants.k_WrongInput)
            { //// wrong license number input
                r_Message.AskingForVehicleLicenseNumberDisplayMenu();
                licenseNumberToCreateVehicle = Console.ReadLine();
            }

            if (!backToPreviewScreen(licenseNumberToCreateVehicle))
            {
                if (!IOpenedNewGarage.LicenseNumberExist(licenseNumberToCreateVehicle))
                { //// case the license number doesn't exist--> we can enter new vehicle
                    newVehicleInformation(licenseNumberToCreateVehicle);
                }
                else
                {
                    r_Message.ChangeStatusForExistVehicle();
                    IOpenedNewGarage.UpdateVehicleStatus(licenseNumberToCreateVehicle, Constants.k_InProgress);
                }
            }
        } //// return to starting menu

        private bool isTheInputCorrect(string i_StringToCheck, eInputsToCheck i_TypeOfInput, int i_LineToPrint = Constants.k_WrongInputMessageLineCommon)
        {
            bool correctInput = false;
            if (i_TypeOfInput == eInputsToCheck.PhoneNumber)
            {
                if (Regex.IsMatch(i_StringToCheck, "^[0-9]+$"))
                {
                    correctInput = true;
                }          
            }
            else
            { //// eInputsToCheck == eInputsToCheck.LicenseNumber
                if (Regex.IsMatch(i_StringToCheck, "^[a-z,A-Z,0-9]+"))
                {
                    correctInput = true;
                }
            }

            if (correctInput == Constants.k_WrongInput)
            {
                correctInput = false;
                Console.SetCursorPosition(Constants.k_StartPrintingMenuColumn, Constants.k_StartPrintingMenuLine + i_LineToPrint);
                Console.Write(Constants.k_WrongInputMessage);
                Thread.Sleep(2000);
            }

            return correctInput;
        }

        private bool backToPreviewScreen(char i_CharToCheck)
        {
            bool goBack;
            if(i_CharToCheck == Constants.k_PreviewMenu)
             {
                Console.Clear();
                goBack = true;
            }
            else
            {
                goBack = false;
            }

            return goBack;
        }

        private bool backToPreviewScreen(string i_StringToCheck)
        {
            bool goBack;
            if (i_StringToCheck.CompareTo(Constants.k_PreviewMenuSTR) == 0)
            {
                Console.Clear();
                goBack = true;
            }
            else
            { 
                goBack = false;
            }

            return goBack;
        }

        private void newVehicleInformation(string i_VehicleLicenseNumber)
        {
            string ownerName;
            string ownerPhoneNumber;
            r_Message.NewVehicleInformationDisplayMenu();
            ownerName = Console.ReadLine();
            while (ownerName.Length < 1)
            {
                r_Message.NewVehicleInformationDisplayMenu();
                ownerName = Console.ReadLine();
            }

            Console.SetCursorPosition(Constants.k_StartPrintingMenuColumn + 6, Constants.k_StartPrintingMenuLine + 5);
            ownerPhoneNumber = Console.ReadLine();
            if (backToPreviewScreen(ownerName) || backToPreviewScreen(ownerPhoneNumber))
            { //// case the user want to go back to preview menu
                if (GetGarageFromUI.LicenseNumberExist(i_VehicleLicenseNumber))
                { //// case we made a mistake and want to go back to main manu but already entered vehicle information
                    IOpenedNewGarage.RemoveVehicle(i_VehicleLicenseNumber);
                }

                createNewVehicleInTheGarage();
            }
            else
            {
                while (isTheInputCorrect(ownerPhoneNumber, eInputsToCheck.PhoneNumber, Constants.k_WrongInputPrintingLine) == Constants.k_WrongInput)
                {
                    Console.SetCursorPosition(Constants.k_StartPrintingMenuColumn, Constants.k_StartPrintingMenuLine + 5);
                    Console.Write("|  =>                                                    |");
                    Console.SetCursorPosition(Constants.k_StartPrintingMenuColumn + 6, Constants.k_StartPrintingMenuLine + 5);
                    ownerPhoneNumber = Console.ReadLine();
                }

                chooseNewVehicleType(i_VehicleLicenseNumber, ownerName, ownerPhoneNumber);
            }
        }

        private void chooseNewVehicleType(string i_VehicleLicenseNumber, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            char choosenVehicleType;
            r_Message.ChooseNewVehicleTypeDisplayMenu();
            choosenVehicleType = Console.ReadKey().KeyChar;
            while (choosenVehicleType != Constants.k_Car && choosenVehicleType != Constants.k_Motorcycle && choosenVehicleType != Constants.k_Truck)
            {
                r_Message.PrintWrongMessage();
                choosenVehicleType = Console.ReadKey().KeyChar;
            }

            Console.Clear();
            if (choosenVehicleType == Constants.k_Car)
            {
                enterInformation(i_VehicleLicenseNumber, Constants.k_Car, i_OwnerName, i_OwnerPhoneNumber);
            }
            else if (choosenVehicleType == Constants.k_Motorcycle)
            {
                enterInformation(i_VehicleLicenseNumber, Constants.k_Motorcycle, i_OwnerName, i_OwnerPhoneNumber);
            }
            else
            { //// if (choosenVehicleType == Constants.k_Truck)
                enterInformation(i_VehicleLicenseNumber, Constants.k_Truck, i_OwnerName, i_OwnerPhoneNumber);
            }
        }

        // $G$ DSN-007 (-5) This method is too long, should be separate to sub methods
        private void enterInformation(string i_VehicleLicenseNumber, char i_VehicleType, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            //// general members
            string vehicleModel;
            float currentEnergyLevel;
            string currentEnergyLevelSTR;
            float currentWheelsPSI;
            string currentWheelsPSISTR;
            char choosenVehicleEngineType = Constants.k_Fuel;
            string wheelModel;

            //// car members
            char carColorChar;
            Car.eColorOfCar carColor;
            int iNumberOfDoors;
            char numberOfDoorsChar;

            //// motorcycle members
            char motorcycleLicenseTypeChar;
            Motorcycle.eLicenseType i_MotorcycleLicenseType;
            int engineCapacitiyCC;
            string engineCapacitiyCCSTR;

            //// truck members
            bool trunkIsCool;
            char trunkIsCoolChar;
            float trunkCapacityCC;
            string trunkCapacitySTR;

            //// play with functions
            r_Message.VehicleModelDisplayMenu();
            vehicleModel = Console.ReadLine();
            if ((i_VehicleType == Constants.k_Car) || (i_VehicleType == Constants.k_Motorcycle))
            {
                r_Message.ChooseEngineTypeDisplayMenu();
                choosenVehicleEngineType = Console.ReadKey().KeyChar;
                while (choosenVehicleEngineType != Constants.k_Fuel && choosenVehicleEngineType != Constants.k_Electric)
                {
                    r_Message.PrintWrongMessage();
                    choosenVehicleEngineType = Console.ReadKey().KeyChar;
                }
            }

            r_Message.VehicleEnergyLevelDisplayMenu();
            currentEnergyLevelSTR = Console.ReadLine();
            while ((!float.TryParse(currentEnergyLevelSTR, out currentEnergyLevel)) || (currentEnergyLevel < Constants.k_MinimumEngineCapacity) || (currentEnergyLevel > IOpenedNewGarage.ReturnVehicleMaxEnergyCapacity(i_VehicleType, choosenVehicleEngineType)))
            { //// case the string input could not convert to float
                r_Message.VehicleEnergyLevelDisplayMenu();
                r_Message.PrintWrongInputMessage(Constants.k_MinimumEngineCapacity, IOpenedNewGarage.ReturnVehicleMaxEnergyCapacity(i_VehicleType, choosenVehicleEngineType));
                Console.SetCursorPosition(Constants.k_StartPrintingMenuColumn + 26, Constants.k_StartPrintingMenuLine + 3);
                currentEnergyLevelSTR = Console.ReadLine();
            }

            r_Message.WheelsModel();
            wheelModel = Console.ReadLine();
            Console.SetCursorPosition(Constants.k_StartPrintingMenuColumn + 24, Constants.k_StartPrintingMenuLine + 5);
            currentWheelsPSISTR = Console.ReadLine();
            while (!float.TryParse(currentWheelsPSISTR, out currentWheelsPSI) || (currentWheelsPSI < Constants.k_MinimumPSIPressure) || currentWheelsPSI > IOpenedNewGarage.ReturnVehicleMaxPSI(i_VehicleType))
            { //// case the string input could not convert to float
                Console.SetCursorPosition(Constants.k_StartPrintingMenuColumn + 24, Constants.k_StartPrintingMenuLine + 5);
                Console.Write("                        ");
                r_Message.PrintWrongInputMessage(Constants.k_MinimumPSIPressure, IOpenedNewGarage.ReturnVehicleMaxPSI(i_VehicleType));
                Console.SetCursorPosition(Constants.k_StartPrintingMenuColumn + 24, Constants.k_StartPrintingMenuLine + 5);
                currentWheelsPSISTR = Console.ReadLine();
            }

            float.TryParse(currentWheelsPSISTR, out currentWheelsPSI);

            if (i_VehicleType == Constants.k_Car)
            {
                r_Message.CarColorsDisplayMenu();
                carColorChar = Console.ReadKey().KeyChar;
                while (carColorChar != Constants.k_ColorGrey && carColorChar != Constants.k_ColorBlue && carColorChar != Constants.k_ColorWhite && carColorChar != Constants.k_ColorBlack)
                {
                    r_Message.CarColorsDisplayMenu();
                    r_Message.PrintWrongMessage();
                    carColorChar = Console.ReadKey().KeyChar;
                }

                carColor = getCarColorFromChar(carColorChar);
                r_Message.NumberOfDoorsDisplayMenu();
                numberOfDoorsChar = Console.ReadKey().KeyChar;
                while (numberOfDoorsChar != Constants.k_FiveDoors && numberOfDoorsChar != Constants.k_TwoDoors && numberOfDoorsChar != Constants.k_ThreeDoors && numberOfDoorsChar != Constants.k_FourDoors)
                {
                    r_Message.NumberOfDoorsDisplayMenu();
                    r_Message.PrintWrongMessage();
                    numberOfDoorsChar = Console.ReadKey().KeyChar;
                }

                iNumberOfDoors = numberOfDoorsChar - Constants.k_ValueToDecreaseFromCharToGetInt;
                r_CreateNewVehicle.AddNewCarCompleteInformation(IOpenedNewGarage, i_VehicleLicenseNumber, vehicleModel, currentEnergyLevel, carColor, (Car.eNumberOfDoors)iNumberOfDoors, choosenVehicleEngineType, i_OwnerName, i_OwnerPhoneNumber, wheelModel, currentWheelsPSI);
            }
            else if (i_VehicleType == Constants.k_Motorcycle)
            {
                r_Message.MotorcycleLicenseDisplayMenu();
                motorcycleLicenseTypeChar = Console.ReadKey().KeyChar;
                while (!GetGarageFromUI.GetLicenseTypeFromChar(motorcycleLicenseTypeChar, out i_MotorcycleLicenseType))
                {
                    r_Message.PrintWrongMessage();
                    r_Message.MotorcycleLicenseDisplayMenu();
                    motorcycleLicenseTypeChar = Console.ReadKey().KeyChar;
                }

                r_Message.EngineCapacityCCDisplayMenu();
                engineCapacitiyCCSTR = Console.ReadLine();
                while (!int.TryParse(engineCapacitiyCCSTR, out engineCapacitiyCC) || engineCapacitiyCC < Constants.k_MinimumCapacityInCC)
                {
                    r_Message.PrintWrongInputMessage(Constants.k_MinimumCapacityInCC, Constants.k_MaxEngineCapaityInCC);
                    r_Message.EngineCapacityCCDisplayMenu();
                    engineCapacitiyCCSTR = Console.ReadLine();
                }

                r_CreateNewVehicle.AddNewMotorcycleCompleteInformation(IOpenedNewGarage, i_VehicleLicenseNumber, vehicleModel, currentEnergyLevel, i_MotorcycleLicenseType, engineCapacitiyCC, choosenVehicleEngineType, i_OwnerName, i_OwnerPhoneNumber, wheelModel, currentWheelsPSI);
            }
            else
            { //// if (i_VehicleType == Constants.k_Truck)
                r_Message.TruckDisplayMenu();
                trunkCapacitySTR = Console.ReadLine();
                while ((!float.TryParse(trunkCapacitySTR, out trunkCapacityCC)) || (trunkCapacityCC < Constants.k_MinimumCapacityInCC) || (trunkCapacityCC > Constants.k_MaxTrunkeCapaityInCC))
                {
                    r_Message.PrintWrongInputMessage(Constants.k_MinimumCapacityInCC, Constants.k_MaxTrunkeCapaityInCC);
                    Thread.Sleep(3000);
                    r_Message.TruckDisplayMenu();
                    trunkCapacitySTR = Console.ReadLine();
                }

                Console.SetCursorPosition(Constants.k_StartPrintingMenuColumn + 1, Constants.k_StartPrintingMenuLine + 6);
                trunkIsCoolChar = Console.ReadKey().KeyChar;
                while (trunkIsCoolChar != Constants.k_Yes && trunkIsCoolChar != Constants.k_No)
                {
                    Console.SetCursorPosition(Constants.k_StartPrintingMenuColumn + 1, Constants.k_StartPrintingMenuLine + 6);
                    Console.Write("                        ");
                    r_Message.PrintWrongMessage();
                    Console.SetCursorPosition(Constants.k_StartPrintingMenuColumn + 1, Constants.k_StartPrintingMenuLine + 6);
                    trunkIsCoolChar = Console.ReadKey().KeyChar;
                }

                trunkIsCool = isTheTrunkIsColler(trunkIsCoolChar);
                r_CreateNewVehicle.AddNewTruckCompleteInformation(IOpenedNewGarage, i_VehicleLicenseNumber, vehicleModel, currentEnergyLevel, trunkIsCool, trunkCapacityCC, i_OwnerName, i_OwnerPhoneNumber, wheelModel, currentWheelsPSI);
            }

            Console.Clear();
            r_Message.SuccessMessageDisplayMenu();
            WorkingInTheGarage();
        }

        private bool isTheTrunkIsColler(char i_TrunkIsCoolerOrNot)
        {
            bool theTrunkIsCooler = true;
            if (i_TrunkIsCoolerOrNot == Constants.k_TheTrunkISCooler)
            {
                theTrunkIsCooler = true;
            }
            else
            {
                theTrunkIsCooler = false;
            }

            return theTrunkIsCooler;
        }

        private Car.eColorOfCar getCarColorFromChar(char i_CharInputForColor)
        {
            Car.eColorOfCar currentVehicleColor;
            if (i_CharInputForColor == Constants.k_ColorGrey)
            {
                currentVehicleColor = Car.eColorOfCar.Grey;
            }
            else if (i_CharInputForColor == Constants.k_ColorBlue)
            {
                currentVehicleColor = Car.eColorOfCar.Blue;
            }
            else if (i_CharInputForColor == Constants.k_ColorWhite)
            {
                currentVehicleColor = Car.eColorOfCar.White;
            }
            else
            { //// if (i_CharInputForColor == Constants.k_ColorBlack)
                currentVehicleColor = Car.eColorOfCar.Black;
            }

            return currentVehicleColor;
        }
    }
}