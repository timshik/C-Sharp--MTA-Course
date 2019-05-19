namespace Garage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using n_Car;
    using n_Motorcycle;
    using n_Truck;

    class CreateNewVehicle
    {
        public static readonly int sr_NumberOfVehicleTypes = 5;

        public enum eVehicleTypes
        {
            Car,
            ElectricCar,
            Motorcycle,
            ElectricMotorcycle,
            Truck
        }

        public static Car CreateNewCar(string i_PlateNumber, float i_MaxEnergeyLevel, float i_FuelLevel, Car.eEnergyType i_Type,
            eNumberOfDoors i_NumberOfDoors, eCarColor i_CarColor, string i_ModelName, string i_WheelManufacturer)
        {
            return new Car(i_PlateNumber, i_MaxEnergeyLevel, i_FuelLevel, i_Type, i_NumberOfDoors, i_CarColor, i_ModelName, i_WheelManufacturer);
        }

        public static ElectricCar CreateNewElectricCar(string i_ModelName, string i_PlateNumber, string i_WheelManufacturer, eCarColor i_CarColor, eNumberOfDoors i_NumberOfDoors)
        {
            return new ElectricCar(i_ModelName, i_PlateNumber, i_WheelManufacturer, i_CarColor, i_NumberOfDoors);
        }

        public static Motorcycle CreateNewMotorcycle(string i_ModelName, string i_PlateNumber, int i_EngineCapacity, eLicenseType i_LicenseType, string i_WheelManufacturer)
        {
            return new Motorcycle(i_ModelName, i_PlateNumber, i_EngineCapacity, i_LicenseType, i_WheelManufacturer);
        }

        public static ElectricMotorcycle CreateNewElectricMotorcycle(string i_ModelName, string i_PlateNumber, string i_WheelManufacturer, eLicenseType i_LicenseType, int i_EngineCapacity)
        {
            return new ElectricMotorcycle(i_ModelName, i_PlateNumber, i_WheelManufacturer, i_LicenseType, i_EngineCapacity);
        }

        public static Truck CreateNewTruck(string i_ModelName, string i_PlateNumber, bool v_isDeliveryHazardousMaterials, float i_TrunkLevel, string i_WheelManufacturer)
        {
            return new Truck(i_ModelName, i_PlateNumber, v_isDeliveryHazardousMaterials, i_TrunkLevel, i_WheelManufacturer);
        }
    }
}
