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
    using n_Strings;

    public class VehicleManager
    {
        public static readonly List<string> VehicleList = new List<string>();

        public enum eVehicleTypes
        {
            Car,
            ElectricCar,
            Motorcycle,
            ElectricMotorcycle,
            Truck
        }
        
        public static void SetVehicleList()
        {
            VehicleList.Add(Strings.title_car);
            VehicleList.Add(Strings.title_electric_car);
            VehicleList.Add(Strings.title_motorcycle);
            VehicleList.Add(Strings.title_electric_motorcycle);
            VehicleList.Add(Strings.title_truck);
        }

        public static Car CreateNewCar(string i_PlateNumber, DoorNumber.eNumberOfDoors i_NumberOfDoors, CarColor.eCarColor i_CarColor, string i_ModelName, string i_WheelManufacturer)
        {
            return new Car(i_PlateNumber, i_NumberOfDoors, i_CarColor, i_ModelName, i_WheelManufacturer);
        }

        public static ElectricCar CreateNewElectricCar(string i_ModelName, string i_PlateNumber, string i_WheelManufacturer, CarColor.eCarColor i_CarColor, DoorNumber.eNumberOfDoors i_NumberOfDoors)
        {
            return new ElectricCar(i_ModelName, i_PlateNumber, i_WheelManufacturer, i_CarColor, i_NumberOfDoors);
        }

        public static Motorcycle CreateNewMotorcycle(string i_ModelName, string i_PlateNumber, int i_EngineCapacity, LicenseType.eLicenseType i_LicenseType, string i_WheelManufacturer)
        {
            return new Motorcycle(i_ModelName, i_PlateNumber, i_EngineCapacity, i_LicenseType, i_WheelManufacturer);
        }

        public static ElectricMotorcycle CreateNewElectricMotorcycle(string i_ModelName, string i_PlateNumber, string i_WheelManufacturer, LicenseType.eLicenseType i_LicenseType, int i_EngineCapacity)
        {
            return new ElectricMotorcycle(i_ModelName, i_PlateNumber, i_WheelManufacturer, i_LicenseType, i_EngineCapacity);
        }

        public static Truck CreateNewTruck(string i_ModelName, string i_PlateNumber, bool v_isDeliveryHazardousMaterials, float i_TrunkLevel, string i_WheelManufacturer)
        {
            return new Truck(i_ModelName, i_PlateNumber, v_isDeliveryHazardousMaterials, i_TrunkLevel, i_WheelManufacturer);
        }
    }
}
