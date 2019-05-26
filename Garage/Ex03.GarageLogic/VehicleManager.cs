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
    /// when adding new vehicle type you need to create:
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

        public static Dictionary<eVehicleTypes, VehicleTypesOptions> s_OptionsToAskUserByTypes = new Dictionary<eVehicleTypes, VehicleTypesOptions>();
        private static List<string> s_VehicleList = new List<string>();
        private static List<string> s_BooleanOptions = new List<string>();

        public enum eVehicleTypes
        {
            Car,
            ElectricCar,
            Motorcycle,
            ElectricMotorcycle,
            Truck
        }

        public static BaseVehicle CreateNewVehicle(ref Dictionary<string, object> io_ArgumentList)
        {
            eVehicleTypes type = (eVehicleTypes)io_ArgumentList[ArgumentsKeysets.sr_KeyTypeOfVehicle];
            BaseVehicle vehicle;
            switch (type)
            {
                case eVehicleTypes.Car:
                    vehicle = CreateNewCar(io_ArgumentList);
                    break;
                case eVehicleTypes.ElectricCar:
                    vehicle = CreateNewElectricCar(io_ArgumentList);
                    break;
                case eVehicleTypes.Motorcycle:
                    vehicle = CreateNewMotorcycle(io_ArgumentList);
                    break;
                case eVehicleTypes.ElectricMotorcycle:
                    vehicle = CreateNewElectricMotorcycle(io_ArgumentList);
                    break;
                case eVehicleTypes.Truck:
                    vehicle = CreateNewTruck(io_ArgumentList);
                    break;
                default:
                    throw new Exception(Strings.unknown_error);
            }

            return vehicle;
        }

        public static void ArrangementExtandedOptionList()
        {
            VehicleTypesOptions carOptions = new VehicleTypesOptions();
            setCarAdditionalOptionList(ref carOptions.OptionList, ref carOptions.OptionListOffsets, ref carOptions.OptionListMessages, ref carOptions.OptionKeys);

            VehicleTypesOptions electricCarOptions = new VehicleTypesOptions();
            setElectricCarAdditionalOptionList(ref electricCarOptions.OptionList, ref electricCarOptions.OptionListOffsets, ref electricCarOptions.OptionListMessages, ref electricCarOptions.OptionKeys);

            VehicleTypesOptions motorcycleOptions = new VehicleTypesOptions();
            setMotorcycleOptionList(ref motorcycleOptions.OptionList, ref motorcycleOptions.OptionListOffsets, ref motorcycleOptions.OptionListMessages, ref motorcycleOptions.OptionKeys);

            VehicleTypesOptions electricMotorOptions = new VehicleTypesOptions();
            setElectricMotorcycleOptionList(ref electricMotorOptions.OptionList, ref electricMotorOptions.OptionListOffsets, ref electricMotorOptions.OptionListMessages, ref electricMotorOptions.OptionKeys);

            VehicleTypesOptions truckOptions = new VehicleTypesOptions();
            setTruckOptionList(ref truckOptions.OptionList, ref truckOptions.OptionListOffsets, ref truckOptions.OptionListMessages, ref truckOptions.OptionKeys);

            s_OptionsToAskUserByTypes.Add(eVehicleTypes.Car, carOptions);
            s_OptionsToAskUserByTypes.Add(eVehicleTypes.ElectricCar, electricCarOptions);
            s_OptionsToAskUserByTypes.Add(eVehicleTypes.Motorcycle, motorcycleOptions);
            s_OptionsToAskUserByTypes.Add(eVehicleTypes.ElectricMotorcycle, electricMotorOptions);
            s_OptionsToAskUserByTypes.Add(eVehicleTypes.Truck, truckOptions);
        }

        public static List<string> VehicleList
        {
            get { return s_VehicleList; }
        }

        public static List<string> BooleanList
        {
            get { return s_BooleanOptions; }
        }

        public static void SetVehicleList()
        {
            s_VehicleList.Add(Strings.title_car);
            s_VehicleList.Add(Strings.title_electric_car);
            s_VehicleList.Add(Strings.title_motorcycle);
            s_VehicleList.Add(Strings.title_electric_motorcycle);
            s_VehicleList.Add(Strings.title_truck);
        }

        public static void SetBooleanList()
        {
            s_BooleanOptions.Add(Strings.true_option);
            s_BooleanOptions.Add(Strings.false_option);
        }

        public static Car CreateNewCar(Dictionary<string, object> i_Arguments)
        {
            i_Arguments.Add(ArgumentsKeysets.sr_KeyNumOfWheels, Car.sr_NumberOfWheels);
            i_Arguments.Add(ArgumentsKeysets.sr_KeyMaxFuelLevel, Car.sr_FullTunkLevel);
            i_Arguments.Add(ArgumentsKeysets.sr_KeyMaxWheelPressure, Car.sr_MaxPressure);
            i_Arguments.Add(ArgumentsKeysets.sr_KeyTypeOfEnergy, Car.sr_EnergyType);
            return new Car(i_Arguments);
        }

        public static ElectricCar CreateNewElectricCar(Dictionary<string, object> i_Arguments)
        {
            i_Arguments.Add(ArgumentsKeysets.sr_KeyNumOfWheels, ElectricCar.sr_NumberOfWheels);
            i_Arguments.Add(ArgumentsKeysets.sr_KeyMaxBatteryTime, ElectricCar.sr_FullBatteryLevel);
            i_Arguments.Add(ArgumentsKeysets.sr_KeyMaxWheelPressure, ElectricCar.sr_MaxPressure);
            return new ElectricCar(i_Arguments);
        }

        public static Motorcycle CreateNewMotorcycle(Dictionary<string, object> i_Arguments)
        {
            i_Arguments.Add(ArgumentsKeysets.sr_KeyNumOfWheels, Motorcycle.sr_NumberOfWheels);
            i_Arguments.Add(ArgumentsKeysets.sr_KeyMaxFuelLevel, Motorcycle.sr_FullTunkLevel);
            i_Arguments.Add(ArgumentsKeysets.sr_KeyMaxWheelPressure, Motorcycle.sr_MaxPressure);
            i_Arguments.Add(ArgumentsKeysets.sr_KeyTypeOfEnergy, Motorcycle.sr_EnergyType);
            return new Motorcycle(i_Arguments);
        }

        public static ElectricMotorcycle CreateNewElectricMotorcycle(Dictionary<string, object> i_Arguments)
        {
            i_Arguments.Add(ArgumentsKeysets.sr_KeyNumOfWheels, ElectricMotorcycle.sr_NumberOfWheels);
            i_Arguments.Add(ArgumentsKeysets.sr_KeyMaxBatteryTime, ElectricMotorcycle.sr_FullBatteryLevel);
            i_Arguments.Add(ArgumentsKeysets.sr_KeyMaxWheelPressure, ElectricMotorcycle.sr_MaxPressure);
            return new ElectricMotorcycle(i_Arguments);
        }

        public static Truck CreateNewTruck(Dictionary<string, object> i_Arguments)
        {
            i_Arguments.Add(ArgumentsKeysets.sr_KeyNumOfWheels, Truck.sr_NumberOfWheels);
            i_Arguments.Add(ArgumentsKeysets.sr_KeyMaxFuelLevel, Truck.sr_FullTunkLevel);
            i_Arguments.Add(ArgumentsKeysets.sr_KeyMaxWheelPressure, Truck.sr_MaxPressure);
            i_Arguments.Add(ArgumentsKeysets.sr_KeyTypeOfEnergy, Truck.sr_EnergyType);
            return new Truck(i_Arguments);
        }

        private static void setCarAdditionalOptionList(ref List<List<string>> io_OptionList, ref List<int> io_OptionOffsets, ref List<string> io_OptionMessages, ref List<string> io_OptionKeys)
        {
            io_OptionList.Add(DoorNumber.s_DoorsOptions);
            io_OptionList.Add(CarColor.s_CarColorNames);
            io_OptionMessages.Add(Strings.choose_door_number);
            io_OptionMessages.Add(Strings.choose_car_color);
            io_OptionOffsets.Add(1);
            io_OptionOffsets.Add(-1);
            io_OptionKeys.Add(ArgumentsKeysets.sr_KeyNumberOfDoors);
            io_OptionKeys.Add(ArgumentsKeysets.sr_KeyCarColor);
        }

        private static void setElectricCarAdditionalOptionList(ref List<List<string>> io_OptionList, ref List<int> io_OptionOffsets, ref List<string> io_OptionMessages, ref List<string> io_OptionKeys)
        {
            setCarAdditionalOptionList(ref io_OptionList, ref io_OptionOffsets, ref io_OptionMessages, ref io_OptionKeys);
        }

        private static void setMotorcycleOptionList(ref List<List<string>> io_OptionList, ref List<int> io_OptionOffsets, ref List<string> io_OptionMessages, ref List<string> io_OptionKeys)
        {
            io_OptionList.Add(LicenseType.s_LicenseType);
            io_OptionList.Add(null);
            io_OptionMessages.Add(Strings.get_license_massage);
            io_OptionMessages.Add(Strings.set_engine_capacity);
            io_OptionOffsets.Add(-1);
            io_OptionOffsets.Add(0);
            io_OptionKeys.Add(ArgumentsKeysets.sr_KeyLicenseType);
            io_OptionKeys.Add(ArgumentsKeysets.sr_KeyEngineCapacity);
        }

        private static void setElectricMotorcycleOptionList(ref List<List<string>> io_OptionList, ref List<int> io_OptionOffsets, ref List<string> io_OptionMessages, ref List<string> io_OptionKeys)
        {
            setMotorcycleOptionList(ref io_OptionList, ref io_OptionOffsets, ref io_OptionMessages, ref io_OptionKeys);
        }

        private static void setTruckOptionList(ref List<List<string>> io_OptionList, ref List<int> io_OptionOffsets, ref List<string> io_OptionMessages, ref List<string> io_OptionKeys)
        {
            io_OptionList.Add(s_BooleanOptions);
            io_OptionList.Add(null);
            io_OptionMessages.Add(Strings.will_delivery_materials);
            io_OptionMessages.Add(Strings.set_truck_capacity_level);
            io_OptionOffsets.Add(-1);
            io_OptionOffsets.Add(0);
            io_OptionKeys.Add(ArgumentsKeysets.sr_KeyDeliveryMaterials);
            io_OptionKeys.Add(ArgumentsKeysets.sr_KeyTruckCapacity);
        }
    }
}
