using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class CreateNewVehicle
    {
        private Engine m_CreatedEngine;
        private Vehicle m_CreatedVehicle;
        private Vehicle m_CreatedVehicle1;


        public void AddNewCarCompleteInformation(Garage i_MyGarage, string i_LicenseNumberForNewVehicle, string i_VehicleModel, float i_CurrentEnergyLevel, Car.eColorOfCar i_CarColor, Car.eNumberOfDoors i_NumberOfDoors, char i_EnergyType, string i_OwnerName, string i_OwnerPhoneNumber, string i_WheelsModel, float i_CurrentWheelsPSI)
        {      
            if (i_EnergyType == Constants.k_Electric)
            {
                m_CreatedEngine = new ElectricEngine(Constants.k_CarBatteryMaxHours);
            }
            else
            {
                m_CreatedEngine = new FuelEngine(Constants.k_CarFuelTankCapacity, FuelEngine.eFuelType.Octan98);
            }

            m_CreatedEngine.CurrentEnergyStatus = i_CurrentEnergyLevel;
            m_CreatedVehicle = new Car(i_VehicleModel, i_LicenseNumberForNewVehicle, i_NumberOfDoors, i_CarColor, m_CreatedEngine, i_WheelsModel, i_CurrentWheelsPSI);
            i_MyGarage.AddNewVehicle(i_LicenseNumberForNewVehicle, i_OwnerName, i_OwnerPhoneNumber, m_CreatedVehicle);
        }

        public void AddNewMotorcycleCompleteInformation(Garage i_MyGarage, string i_LicenseNumberForNewVehicle, string i_VehicleModel, float i_CurrentEnergyLevel, Motorcycle.eLicenseType i_MotorcycleLicenseType, int i_EngineCapacitiyCC, char i_EnergyType, string i_OwnerName, string i_OwnerPhoneNumber, string i_WheelsModel, float i_CurrentWheelsPSI)
        {
            if (i_EnergyType == Constants.k_Electric)
            {
                m_CreatedEngine = new ElectricEngine(Constants.k_MotorcycleBatteryMaxHours);
            }
            else
            {
                m_CreatedEngine = new FuelEngine(Constants.k_MotorcycleFuelTackCapacity, FuelEngine.eFuelType.Octan96);
            }

            m_CreatedEngine.CurrentEnergyStatus = i_CurrentEnergyLevel;
            m_CreatedVehicle = new Motorcycle(i_VehicleModel, i_LicenseNumberForNewVehicle, i_MotorcycleLicenseType, i_EngineCapacitiyCC, m_CreatedEngine, i_WheelsModel, i_CurrentWheelsPSI);
            i_MyGarage.AddNewVehicle(i_LicenseNumberForNewVehicle, i_OwnerName, i_OwnerPhoneNumber, m_CreatedVehicle);
        }

        public void AddNewTruckCompleteInformation(Garage i_MyGarage, string i_LicenseNumberForNewVehicle, string i_VehicleModel, float i_CurrentEnergyLevel, bool i_CoolerTrunk, float i_TrunkCapacity, string i_OwnerName, string i_OwnerPhoneNumber, string i_WheelsModel, float i_CurrentWheelsPSI)
        {
            m_CreatedEngine = new FuelEngine(Constants.k_TruckFuelTankCapacity, FuelEngine.eFuelType.Soler);          
            m_CreatedEngine.CurrentEnergyStatus = i_CurrentEnergyLevel;
            m_CreatedVehicle = new Truck(i_VehicleModel, i_LicenseNumberForNewVehicle, i_CoolerTrunk, i_TrunkCapacity, m_CreatedEngine, i_WheelsModel, i_CurrentWheelsPSI);
            i_MyGarage.AddNewVehicle(i_LicenseNumberForNewVehicle, i_OwnerName, i_OwnerPhoneNumber, m_CreatedVehicle);
        }
    }
}
