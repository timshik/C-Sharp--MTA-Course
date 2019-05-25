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

    /// <summary>
    /// If adding new vehicle type you need to create:
    ///     1. Create and fill VehicleTypesOption class and add it to 
    ///         s_OptionsToAskUserByTypes dictionary (for getting more information from user).
    ///     2. Adding name of type to sr_VehicleList (and of course to Enum 'eVehicleTypes').
    /// </summary>
    public class VehicleManager
    {
        public class VehicleTypesOptions
        {
            private List<List<string>> m_OptionList = new List<List<string>>();
            private List<int> m_OptionListOffsets = new List<int>();
            private List<string> m_OptionListMessages = new List<string>(), m_OptionKeys = new List<string>();

            public ref List<List<string>> OptionList
            {
                get { return ref m_OptionList; }
            }

            public ref List<string> OptionKeys
            {
                get { return ref m_OptionKeys; }
            }

            public ref List<int> OptionListOffsets
            {
                get { return ref m_OptionListOffsets; }
            }

            public ref List<string> OptionListMessages
            {
                get { return ref m_OptionListMessages; }
            }
        }

        public static readonly string sr_KeyPlateNumber = "plate_number", sr_KeyWheelManufacturer = "wheel_manufacturer", sr_KeyModelName = "model_name",
            sr_KeyDeliveryMaterials = "delivery_materials", sr_KeyTruckCapacity = "truck_capacity", sr_KeyMaxWheelPressure = "max_wheel_pressure",
            sr_KeyNumberOfWheels = "num_of_wheels", sr_KeyTypeOfEnergy = "energy_type", sr_KeyNumberOfDoors = "num_of_door", sr_KeyCarColor = "car_color",
            sr_KeyMaxFuelLevel = "max_fuel_level", sr_KeyMaxBatteryTime = "max_battery_time", sr_KeyEngineCapacity = "engine_capacity",
            sr_KeyLicenseType = "license_type", sr_KeyNumOfWheels = "num_of_wheels", sr_KeyPhoneNumber = "phone_number", sr_KeyOwnerName = "owner_name",
            sr_KeyRepairStatus = "vehicle_status", sr_KeyTypeOfVehicle = "type_of_vehicle",
            sr_KeyCurrentEnergyLevel = "current_energy", sr_KeyCurrentWheelPressure = "current_wheel_pressure";

        public static Dictionary<eVehicleTypes, VehicleTypesOptions> s_OptionsToAskUserByTypes = new Dictionary<eVehicleTypes, VehicleTypesOptions>();
        private static readonly List<string> sr_VehicleList = new List<string>();
        private static readonly List<string> sr_BooleanOptions = new List<string>();

        public enum eVehicleTypes
        {
            Car,
            ElectricCar,
            Motorcycle,
            ElectricMotorcycle,
            Truck
        }

        public static BaseVehicle CreateNewVehicle(ref Dictionary<string, object> i_ArgumentList)
        {
            eVehicleTypes type = (eVehicleTypes)i_ArgumentList[sr_KeyTypeOfVehicle];
            BaseVehicle vehicle;
            switch (type)
            {
                case eVehicleTypes.Car:
                    vehicle = CreateNewCar(i_ArgumentList);
                    break;
                case eVehicleTypes.ElectricCar:
                    vehicle = CreateNewElectricCar(i_ArgumentList);
                    break;
                case eVehicleTypes.Motorcycle:
                    vehicle = CreateNewMotorcycle(i_ArgumentList);
                    break;
                case eVehicleTypes.ElectricMotorcycle:
                    vehicle = CreateNewElectricMotorcycle(i_ArgumentList);
                    break;
                case eVehicleTypes.Truck:
                    vehicle = CreateNewTruck(i_ArgumentList);
                    break;
                default:
                    throw new Exception(Strings.unknown_error);
            }

            return vehicle;
        }

        public static void ArrangementExtandedOptionList()
        {
            VehicleTypesOptions carOptions = new VehicleTypesOptions();
            SetCarAdditionalOptionList(ref carOptions.OptionList, ref carOptions.OptionListOffsets, ref carOptions.OptionListMessages, ref carOptions.OptionKeys);

            VehicleTypesOptions electricCarOptions = new VehicleTypesOptions();
            SetElectricCarAdditionalOptionList(ref electricCarOptions.OptionList, ref electricCarOptions.OptionListOffsets, ref electricCarOptions.OptionListMessages, ref electricCarOptions.OptionKeys);

            VehicleTypesOptions motorcycleOptions = new VehicleTypesOptions();
            SetMotorcycleOptionList(ref motorcycleOptions.OptionList, ref motorcycleOptions.OptionListOffsets, ref motorcycleOptions.OptionListMessages, ref motorcycleOptions.OptionKeys);

            VehicleTypesOptions electricMotorOptions = new VehicleTypesOptions();
            SetElectricMotorcycleOptionList(ref electricMotorOptions.OptionList, ref electricMotorOptions.OptionListOffsets, ref electricMotorOptions.OptionListMessages, ref electricMotorOptions.OptionKeys);

            VehicleTypesOptions truckOptions = new VehicleTypesOptions();
            SetTruckOptionList(ref truckOptions.OptionList, ref truckOptions.OptionListOffsets, ref truckOptions.OptionListMessages, ref truckOptions.OptionKeys);

            s_OptionsToAskUserByTypes.Add(eVehicleTypes.Car, carOptions);
            s_OptionsToAskUserByTypes.Add(eVehicleTypes.ElectricCar, electricCarOptions);
            s_OptionsToAskUserByTypes.Add(eVehicleTypes.Motorcycle, motorcycleOptions);
            s_OptionsToAskUserByTypes.Add(eVehicleTypes.ElectricMotorcycle, electricMotorOptions);
            s_OptionsToAskUserByTypes.Add(eVehicleTypes.Truck, truckOptions);
        }

        public static List<string> VehicleList
        {
            get { return sr_VehicleList; }
        }

        public static List<string> BooleanList
        {
            get { return sr_BooleanOptions; }
        }

        public static void SetVehicleList()
        {
            sr_VehicleList.Add(Strings.title_car);
            sr_VehicleList.Add(Strings.title_electric_car);
            sr_VehicleList.Add(Strings.title_motorcycle);
            sr_VehicleList.Add(Strings.title_electric_motorcycle);
            sr_VehicleList.Add(Strings.title_truck);
        }

        public static void SetBooleanList()
        {
            sr_BooleanOptions.Add(Strings.true_option);
            sr_BooleanOptions.Add(Strings.false_option);
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

        private static void SetCarAdditionalOptionList(ref List<List<string>> i_OptionList, ref List<int> i_OptionOffsets, ref List<string> i_OptionMessages, ref List<string> i_OptionKeys)
        {
            i_OptionList.Add(DoorNumber.sr_DoorsOptions);
            i_OptionList.Add(CarColor.sr_CarColorNames);
            i_OptionMessages.Add(Strings.choose_door_number);
            i_OptionMessages.Add(Strings.choose_car_color);
            i_OptionOffsets.Add(1);
            i_OptionOffsets.Add(-1);
            i_OptionKeys.Add(sr_KeyNumberOfDoors);
            i_OptionKeys.Add(sr_KeyCarColor);
        }

        private static void SetElectricCarAdditionalOptionList(ref List<List<string>> i_OptionList, ref List<int> i_OptionOffsets, ref List<string> i_OptionMessages, ref List<string> i_OptionKeys)
        {
            SetCarAdditionalOptionList(ref i_OptionList, ref i_OptionOffsets, ref i_OptionMessages, ref i_OptionKeys);
        }

        private static void SetMotorcycleOptionList(ref List<List<string>> i_OptionList, ref List<int> i_OptionOffsets, ref List<string> i_OptionMessages, ref List<string> i_OptionKeys)
        {
            i_OptionList.Add(LicenseType.sr_LicenseType);
            i_OptionList.Add(null);
            i_OptionMessages.Add(Strings.get_license_massage);
            i_OptionMessages.Add(Strings.set_engine_capacity);
            i_OptionOffsets.Add(-1);
            i_OptionOffsets.Add(0);
            i_OptionKeys.Add(sr_KeyLicenseType);
            i_OptionKeys.Add(sr_KeyEngineCapacity);
        }

        private static void SetElectricMotorcycleOptionList(ref List<List<string>> i_OptionList, ref List<int> i_OptionOffsets, ref List<string> i_OptionMessages, ref List<string> i_OptionKeys)
        {
            SetMotorcycleOptionList(ref i_OptionList, ref i_OptionOffsets, ref i_OptionMessages, ref i_OptionKeys);
        }

        private static void SetTruckOptionList(ref List<List<string>> i_OptionList, ref List<int> i_OptionOffsets, ref List<string> i_OptionMessages, ref List<string> i_OptionKeys)
        {
            i_OptionList.Add(sr_BooleanOptions);
            i_OptionList.Add(null);
            i_OptionMessages.Add(Strings.will_delivery_materials);
            i_OptionMessages.Add(Strings.set_truck_capacity_level);
            i_OptionOffsets.Add(-1);
            i_OptionOffsets.Add(0);
            i_OptionKeys.Add(sr_KeyDeliveryMaterials);
            i_OptionKeys.Add(sr_KeyTruckCapacity);
        }
    }
}
