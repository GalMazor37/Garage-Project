using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
       public enum eColorOfCar
        {
            Grey,
            Blue,
            White,
            Black
        }

        public enum  eNumberOfDoors
        {
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5
        }

        private readonly eColorOfCar r_ColorOfTheCar;
        private readonly eNumberOfDoors r_NumberOfDoors; //// Can be 2, 3, 4 or 5...
        
        public Car(string i_ModelName, string i_LicenseNumber, eNumberOfDoors i_NumberOfDoors, eColorOfCar i_ColorOfTheCar, Engine i_CarEngine, string i_WheelModel, float i_CurrentWheelsPSI) : 
            base(i_ModelName, i_LicenseNumber, Constants.k_CarNumberOfWheels, i_CarEngine, Constants.k_MaxCarAirPressure, i_WheelModel, i_CurrentWheelsPSI) 
        {
            r_NumberOfDoors = i_NumberOfDoors;
            r_ColorOfTheCar = i_ColorOfTheCar;
        }

        public eNumberOfDoors NumberOfDoors
        {
            get { return r_NumberOfDoors; }
        }

        public eColorOfCar ColorOfTheCar
        {
            get { return r_ColorOfTheCar; }
        }

        public override string UniqueVehicleInfo()
        {
            string carColorInfo = "Car Color: " + r_ColorOfTheCar.ToString();
            string carNumberOfDoorsInfo = "Car Number Of Doors: " + r_NumberOfDoors.ToString();
            return string.Format("{0}\n{1}", carColorInfo, carNumberOfDoorsInfo);
        }
    }
}
