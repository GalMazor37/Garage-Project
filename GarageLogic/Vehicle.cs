using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        //// members
        private readonly List<Wheel> m_VehicleWheels = new List<Wheel>();
        private readonly Engine m_EngineOfTheVehicle;
        private readonly string r_ModelName;
        private readonly string r_LicenseNumber;                                   //// max 20 digits
        private float m_EnergyPercent;

        //// methods
        public Vehicle(string i_ModelName, string i_LicenseNumber, int i_NumberOfWheels, Engine i_Engine, float i_MaxAirPressure, string i_WheelModel, float i_CurrentWheelsPSI)
        {
            r_ModelName = i_ModelName;
            r_LicenseNumber = i_LicenseNumber;
            m_EngineOfTheVehicle = i_Engine;
            energyLevel = i_Engine.CurrentEnergyStatus;
            createWheels(i_MaxAirPressure, i_WheelModel, i_CurrentWheelsPSI, i_NumberOfWheels);
        }

        private void createWheels(float i_MaxAirPressure, string i_WheelModel, float i_CurrentWheelsPSI, int i_NumberOfWheels)
        {         
           for(int i = 0; i < i_NumberOfWheels; i++)
            {
                m_VehicleWheels.Add(new Wheel(i_WheelModel, i_MaxAirPressure, i_CurrentWheelsPSI));
            }
        }

        public List<Wheel> VehicleWheelsList
        {
            get { return m_VehicleWheels; }    
        }

        public void InflateAllWheels()
        {
            foreach (Wheel wheel in m_VehicleWheels)
            {
                float MissingPSIToInflate = wheel.AvailableAirPressure;
                wheel.InflateWheel(MissingPSIToInflate);
            }
        }

        public string ModelName
        {
            get { return r_ModelName; }
        }

        public string LicenseNumber
        {
            get { return r_LicenseNumber; }
        }

        private float energyLevel
        {
            get { return m_EnergyPercent; }
            set
            {
                m_EnergyPercent = value * Constants.k_PercentToMultiply / m_EngineOfTheVehicle.MaxEnergyCapacity;
            }
        }

        public Engine VehicleEngine
        {
            get { return m_EngineOfTheVehicle; }
        }

        public string VehicleInfo()
        {
            string modelNameMessage = "Model Name: " + r_ModelName.ToString();
            string licenseNumberMessage = "License Number: " + r_LicenseNumber.ToString();
            string energyPercentMessage = "Current Energy Percent: " + m_EnergyPercent.ToString() + "%";
            StringBuilder wheelsListSubjectMessage = new StringBuilder("Wheels List: \n" + stringOfAllWheelsInformationByVehicle());
            string information = string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n", modelNameMessage, licenseNumberMessage, energyPercentMessage,
                m_EngineOfTheVehicle.EngineInformation(), wheelsListSubjectMessage, UniqueVehicleInfo());
            return information;
        }

        public abstract string UniqueVehicleInfo();

        private StringBuilder stringOfAllWheelsInformationByVehicle()
        {
            StringBuilder wheelsList = new StringBuilder();
            int counter = 1;
            foreach (Wheel wheel in m_VehicleWheels)
            {
                wheelsList.AppendLine(counter + "." + " Wheel Model " + wheel.ModelName + ", Current PSI- " + wheel.CurrentAirPressure + ", Max PSI- " + wheel.MaxAirPressure);
                counter++;
            }

            return wheelsList;
        }
    }
}
