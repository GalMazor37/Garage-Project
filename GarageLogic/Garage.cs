using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<string, VehicleInTheGarage> m_MyGarage = new Dictionary<string, VehicleInTheGarage>();

        public void AddNewVehicle(string i_LicenseNumberForNewVehicle, string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_VehicleToReadyToInsert)
        {
            VehicleInTheGarage createdVehicle;
            createdVehicle = new VehicleInTheGarage(i_OwnerName, i_OwnerPhoneNumber, i_VehicleToReadyToInsert);
            m_MyGarage.Add(i_LicenseNumberForNewVehicle, createdVehicle);
        }

        public Dictionary<string, VehicleInTheGarage> AllVehiclesInTheGarage
        {
            get { return m_MyGarage; }
        }

        public void RemoveVehicle(string i_LicenseNumberForNewVehicle)
        {
            m_MyGarage.Remove(i_LicenseNumberForNewVehicle);
        }

        public bool LicenseNumberExist(string i_LicenseNumberToSearch)
        {
            return m_MyGarage.ContainsKey(i_LicenseNumberToSearch);
        }

        public bool IsItAFuelEngine(string i_LicenseNumberToSearch, out string energyAmountToAddMessage)
        {
            Engine createdEngine;
            bool itIsAFuelEngine = true;
            VehicleInTheGarage m_GetEngineType;
            m_MyGarage.TryGetValue(i_LicenseNumberToSearch, out m_GetEngineType);
            object FuelEngineTypeIs = typeof(Ex03.GarageLogic.FuelEngine);
            createdEngine = m_GetEngineType.Vehicle.VehicleEngine;
            object MyEngineTypeToCompare = createdEngine.GetType();
            if (FuelEngineTypeIs == MyEngineTypeToCompare)
            {
                itIsAFuelEngine = true;
                energyAmountToAddMessage = Constants.k_EnterLittersAmountToFill;
            }
            else
            {
                itIsAFuelEngine = false;
                energyAmountToAddMessage = Constants.k_EnterTimeToCharge;
            }

            return itIsAFuelEngine;
        }

        private bool isItAFuelEngine(char i_FuelOrElectric)
        {
            const bool v_ItsAFuelEngine = true;
            if (i_FuelOrElectric == Constants.k_Fuel)
            {
                return v_ItsAFuelEngine;
            }
            else
            {
                return !v_ItsAFuelEngine;
            }
        }

        public void FillingEnergyInTheVehicle(float i_EnergyAmountToFill, string i_LicenseNumberOfTheVehicle, char i_EnergyTypeToFill)
        {
            VehicleInTheGarage vehicleToFill;
            m_MyGarage.TryGetValue(i_LicenseNumberOfTheVehicle, out vehicleToFill);
            if (vehicleToFill.Vehicle.VehicleEngine is FuelEngine)
            {
                vehicleToFill.Vehicle.VehicleEngine.FillEnergy(i_EnergyAmountToFill, i_EnergyTypeToFill);
            }
            else
            {
                vehicleToFill.Vehicle.VehicleEngine.FillEnergy(i_EnergyAmountToFill, i_EnergyTypeToFill);
            }          
        }

        public void UpdateVehicleStatus(string i_LicenseNumberForNewVehicle, char i_VehicleNewStatusAsChar)
        {
            VehicleInTheGarage createdVehicle;
            VehicleInTheGarage.eVehicleStatus getStatusFromChar;
            m_MyGarage.TryGetValue(i_LicenseNumberForNewVehicle, out createdVehicle);
            if (i_VehicleNewStatusAsChar == Constants.k_InProgress)
            {
                getStatusFromChar = VehicleInTheGarage.eVehicleStatus.InProgress;
            }
            else if (i_VehicleNewStatusAsChar == Constants.k_WaitingToGetPayment)
            {
                getStatusFromChar = VehicleInTheGarage.eVehicleStatus.WaitingToGetPayment;
            }
            else
            { 
                getStatusFromChar = VehicleInTheGarage.eVehicleStatus.PaidAndReadyToGo;
            }

            createdVehicle.StatusInTheGarage = getStatusFromChar;
        }

        public void InflateAirInWheels(string i_LicenseNumberOfTheVehicle)
        {
            VehicleInTheGarage vehicleToFill;
            m_MyGarage.TryGetValue(i_LicenseNumberOfTheVehicle, out vehicleToFill);
            vehicleToFill.Vehicle.InflateAllWheels();
        }  

        public float ReturnVehicleMaxPSI(char i_VehicleType)
        {
            float vehicleMaxCarAirPressure;
            if (i_VehicleType == Constants.k_Car)
            {
                vehicleMaxCarAirPressure = Constants.k_MaxCarAirPressure;
            }
            else if (i_VehicleType == Constants.k_Motorcycle)
            {
                vehicleMaxCarAirPressure = Constants.k_MaxMotorcycleAirPressure;
            }
            else
            { 
                vehicleMaxCarAirPressure = Constants.k_MaxTruckAirPressure;
            }

            return vehicleMaxCarAirPressure;
        }

        public float ReturnVehicleMaxEnergyCapacity(char i_VehicleType, char i_EnergyType)
        {
            float vehiceEnergyFullCapacity;
            if (i_EnergyType == Constants.k_Fuel)
            {
                if (i_VehicleType == Constants.k_Car)
                {
                    vehiceEnergyFullCapacity = Constants.k_CarFuelTankCapacity;
                }
                else if (i_VehicleType == Constants.k_Motorcycle)
                {
                    vehiceEnergyFullCapacity = Constants.k_MotorcycleFuelTackCapacity;
                }
                else
                { 
                    vehiceEnergyFullCapacity = Constants.k_TruckFuelTankCapacity;
                }
            }
            else
            { //// it is an electric engine
                if (i_VehicleType == Constants.k_Car)
                {
                    vehiceEnergyFullCapacity = Constants.k_CarBatteryMaxHours;
                }
                else
                { //// if (i_VehicleType == Constants.k_Motorcycle) ---> there are no trucks with electric engine
                    vehiceEnergyFullCapacity = Constants.k_MotorcycleBatteryMaxHours;
                }
            }

            return vehiceEnergyFullCapacity;
        }

        public bool GetLicenseTypeFromChar(char i_CharInputForLicenseType, out Motorcycle.eLicenseType i_LicenseType)
        {
            bool thisIsAVaildLicenseType;
            if (i_CharInputForLicenseType == Constants.k_LicenseTypeA)
            {
                i_LicenseType = Motorcycle.eLicenseType.A;
                thisIsAVaildLicenseType = true;
            }
            else if (i_CharInputForLicenseType == Constants.k_LicenseTypeA1)
            {
                i_LicenseType = Motorcycle.eLicenseType.A1;
                thisIsAVaildLicenseType = true;
            }
            else if (i_CharInputForLicenseType == Constants.k_LicenseTypeB1)
            {
                i_LicenseType = Motorcycle.eLicenseType.B1;
                thisIsAVaildLicenseType = true;
            }
            else if (i_CharInputForLicenseType == Constants.k_LicenseTypeB2)
            { //// if (i_CharInputForColor == Constants.k_LicenseTypeB2)
                i_LicenseType = Motorcycle.eLicenseType.B2;
                thisIsAVaildLicenseType = true;
            }
            else
            {
                i_LicenseType = Motorcycle.eLicenseType.B2;
                thisIsAVaildLicenseType = false;
            }

            return thisIsAVaildLicenseType;
        }
    }
}
