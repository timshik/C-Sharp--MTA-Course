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
        public static readonly string sr_KeyPlateNumber = "plate_number", sr_KeyWheelManufacturer = "wheel_manufacturer", sr_KeyModelName = "model_name",
            sr_KeyDeliveryMaterials = "delivery_materials", sr_KeyTruckCapacity = "truck_capacity", sr_KeyMaxWheelPressure = "max_wheel_pressure",
            sr_KeyNumberOfWheels = "num_of_wheels", sr_KeyTypeOfEnergy = "energy_type", sr_KeyNumberOfDoors = "num_of_door", sr_KeyCarColor = "car_color",
            sr_KeyMaxFuelLevel = "max_fuel_level", sr_KeyMaxBatteryTime = "max_battery_time", sr_KeyEngineCapacity = "engine_capacity",
            sr_KeyLicenseType = "license_type", sr_KeyNumOfWheels = "num_of_wheels";

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

        public static Car CreateNewCar(Dictionary<string, object> i_Arguments)
        {
            i_Arguments.Add(sr_KeyNumOfWheels, Car.sr_NumberOfWheels);
            i_Arguments.Add(sr_KeyMaxFuelLevel, Car.sr_FullTunkLevel);
            i_Arguments.Add(sr_KeyMaxWheelPressure, Car.sr_MaxPressure);
            i_Arguments.Add(sr_KeyTypeOfEnergy, Car.sr_EnergyType);
            return new Car(i_Arguments);
        }

        public static ElectricCar CreateNewElectricCar(Dictionary<string, object> i_Arguments)
        {
            i_Arguments.Add(sr_KeyNumOfWheels, ElectricCar.sr_NumberOfWheels);
            i_Arguments.Add(sr_KeyMaxBatteryTime, ElectricCar.sr_FullBatteryLevel);
            i_Arguments.Add(sr_KeyMaxWheelPressure, ElectricCar.sr_MaxPressure);
            return new ElectricCar(i_Arguments);
        }

        public static Motorcycle CreateNewMotorcycle(Dictionary<string, object> i_Arguments)
        {
            i_Arguments.Add(sr_KeyNumOfWheels, Motorcycle.sr_NumberOfWheels);
            i_Arguments.Add(sr_KeyMaxFuelLevel, Motorcycle.sr_FullTunkLevel);
            i_Arguments.Add(sr_KeyMaxWheelPressure, Motorcycle.sr_MaxPressure);
            i_Arguments.Add(sr_KeyTypeOfEnergy, Motorcycle.sr_EnergyType);
            return new Motorcycle(i_Arguments);
        }

        public static ElectricMotorcycle CreateNewElectricMotorcycle(Dictionary<string, object> i_Arguments)
        {
            i_Arguments.Add(sr_KeyNumOfWheels, ElectricMotorcycle.sr_NumberOfWheels);
            i_Arguments.Add(sr_KeyMaxBatteryTime, ElectricMotorcycle.sr_FullBatteryLevel);
            i_Arguments.Add(sr_KeyMaxWheelPressure, ElectricMotorcycle.sr_MaxPressure);
            return new ElectricMotorcycle(i_Arguments);
        }

        public static Truck CreateNewTruck(Dictionary<string, object> i_Arguments)
        {
            i_Arguments.Add(sr_KeyNumOfWheels, Truck.sr_NumberOfWheels);
            i_Arguments.Add(sr_KeyMaxFuelLevel, Truck.sr_FullTunkLevel);
            i_Arguments.Add(sr_KeyMaxWheelPressure, Truck.sr_MaxPressure);
            i_Arguments.Add(sr_KeyTypeOfEnergy, Truck.sr_EnergyType);
            return new Truck(i_Arguments);
        }
    }
}
