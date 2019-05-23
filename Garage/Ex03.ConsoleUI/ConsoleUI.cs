namespace Ex03.ConsoleUI
{
    using System;
    using System.Collections.Generic;
    using Garage;
    using n_Strings;
    using n_Vehicle;

    public class ConsoleUI
    {
        private static readonly List<string> sr_FirstMenuStringArray = new List<string>();
        private static readonly List<string> sr_BooleanOptions = new List<string>();
        private GarageManager m_Garage = new GarageManager();

        public static void Main()
        {
            ConsoleUI UI = new ConsoleUI();
            UI.WelcomeToTheGarage();
        }

        public void WelcomeToTheGarage()
        {
            initializationEnums();
            printMessage(Strings.welcome_massage);
            do
            {
                DoAction();
            }
            while (askForAnotherAction());
        }

        private void initializationEnums()
        {
            CarColor.SetListOfCarColors();
            LicenseType.SetListOfLicenseType();
            VehicleManager.SetVehicleList();
            VehicleProperties.SetListOfOptions();
            DoorNumber.SetListOfOptions();
            FuelVehicle.SetEnergeyTypeList();
            sr_FirstMenuStringArray.Add(Strings.menu_options_1);
            sr_FirstMenuStringArray.Add(Strings.menu_options_2);
            sr_FirstMenuStringArray.Add(Strings.menu_options_3);
            sr_FirstMenuStringArray.Add(Strings.menu_options_4);
            sr_FirstMenuStringArray.Add(Strings.menu_options_5);
            sr_FirstMenuStringArray.Add(Strings.menu_options_6);
            sr_FirstMenuStringArray.Add(Strings.menu_options_7);
            sr_FirstMenuStringArray.Add(Strings.menu_options_8);
            sr_BooleanOptions.Add(Strings.true_option);
            sr_BooleanOptions.Add(Strings.false_option);
        }

        private static void printMessage(string i_Message)
        {
            Console.WriteLine(i_Message);
        }

        private void showError(string i_Message)
        {
            Console.WriteLine(i_Message);
        }

        private static int getUserChoice(int i_MinValue, int i_MaxValue)
        {
            int userChoice = getIntegerFromUser();

            if (userChoice < i_MinValue || userChoice > i_MaxValue)
            {
                throw new ValueOutOfRangeException(i_MaxValue, i_MinValue, Strings.invalid_menu_choice);
            }

            return userChoice;
        }

        private static void showOptions(List<string> i_OptionsArray)
        {
            for (int i = 0; i < i_OptionsArray.Count; i++)
            {
                printMessage(string.Format(Strings.menu_format, i + 1, i_OptionsArray[i]));
            }
        }

        public void DoAction()
        {
            try
            {
                int choice = getOptionFromUser<int>(string.Empty, sr_FirstMenuStringArray, 0);
                switch (choice)
                {
                    case 1:
                        addNewVehicleUI();
                        break;
                    case 2:
                        ShowPlatesOfAllVehiclesUI();
                        break;
                    case 3:
                        ChangePropertiesUI();
                        break;
                    case 4:
                        InflatingWheelUI();
                        break;
                    case 5:
                        FuelVehicleUI();
                        break;
                    case 6:
                        ChargeElectricVehicleUI();
                        break;
                    case 7:
                        ShowVehiclesByPlateUI();
                        break;
                    case 8:
                        exitProgram();
                        break;
                    default:
                        break;
                }
            }
            catch (ValueOutOfRangeException i_ValueOutOfRange)
            {
                showError(i_ValueOutOfRange.Message);
            }
            catch (FormatException i_FormatException)
            {
                showError(i_FormatException.Message);
            }
            catch (Exception i_Exception)
            {
                showError(i_Exception.Message);
            }
        }

        private void exitProgram()
        {
            printMessage(Strings.exit_program_message);
            Environment.Exit(1);
        }

        private bool askForAnotherAction()
        {
            printMessage(Strings.continue_massage);
            return getUserChoice(1, 2) == 1;
        }

        private string getStringFromUser()
        {
            return Console.ReadLine();
        }

        private string getStringFromUser(string i_Message)
        {
            printMessage(i_Message);
            return getStringFromUser();
        }

        private void addNewVehicleUI()
        {
            VehicleProperties.eStateOfService statusOfNewVehicle;
            Dictionary<string, object> basicArgumentsMap = new Dictionary<string, object>();
            string plateNumber = getStringFromUser(Strings.enter_plate_number);

            try
            {
                VehicleProperties vehicle = m_Garage.GetVehicleByPlateNumber(plateNumber);
                printMessage(string.Format(Strings.change_status_options, VehicleProperties.sr_StateListOptions[(int)vehicle.Status]));
                showOptions(VehicleProperties.sr_StateListOptions);
                statusOfNewVehicle = (VehicleProperties.eStateOfService)getUserChoice(1, VehicleProperties.sr_StateListOptions.Count) - 1;
                vehicle.Status = statusOfNewVehicle;
            }
            catch (Exception i_PlateError)
            {
                showError(i_PlateError.Message);
                printMessage(Strings.create_new_vehicle);
                basicArgumentsMap.Add(VehicleManager.sr_KeyTypeOfVehicle, getOptionFromUser<VehicleManager.eVehicleTypes>(Strings.choose_type_of_vehicle, VehicleManager.VehicleList, -1));
                string ownerName = getStringFromUser(Strings.enter_owner_name);
                string phoneNumber = getStringFromUser(Strings.enter_phone_number);
                basicArgumentsMap.Add(VehicleManager.sr_KeyModelName, getStringFromUser(Strings.enter_model_name));
                basicArgumentsMap.Add(VehicleManager.sr_KeyWheelManufacturer, getStringFromUser(Strings.enter_wheel_manufacturer));
                basicArgumentsMap.Add(VehicleManager.sr_KeyPlateNumber, plateNumber);
                VehicleProperties.eStateOfService repairStatus = getOptionFromUser<VehicleProperties.eStateOfService>(Strings.choose_status_of_vehicle, VehicleProperties.sr_StateListOptions, -1);
                switch ((VehicleManager.eVehicleTypes)basicArgumentsMap[VehicleManager.sr_KeyTypeOfVehicle])
                {
                    case VehicleManager.eVehicleTypes.Truck:

                        basicArgumentsMap.Add(VehicleManager.sr_KeyDeliveryMaterials, getOptionFromUser<int>(Strings.will_delivery_materials,sr_BooleanOptions,-1) == 0);
                        basicArgumentsMap.Add(VehicleManager.sr_KeyTruckCapacity, getFloatFromUser(Strings.set_truck_capacity_level));
                        m_Garage.AddNewVehicle(VehicleManager.CreateNewTruck(basicArgumentsMap), ownerName, phoneNumber, repairStatus);
                        break;
                    case VehicleManager.eVehicleTypes.Car:
                    case VehicleManager.eVehicleTypes.ElectricCar:
                        basicArgumentsMap.Add(VehicleManager.sr_KeyCarColor, getOptionFromUser<CarColor.eCarColor>(Strings.choose_car_color, CarColor.sr_CarColorNames, -1));
                        basicArgumentsMap.Add(VehicleManager.sr_KeyNumberOfDoors, getOptionFromUser<DoorNumber.eNumberOfDoors>(Strings.choose_door_number, DoorNumber.sr_DoorsOptions, 1));
                        if ((VehicleManager.eVehicleTypes)basicArgumentsMap[VehicleManager.sr_KeyRepairStatus] == VehicleManager.eVehicleTypes.Car)
                        {
                            m_Garage.AddNewVehicle(VehicleManager.CreateNewCar(basicArgumentsMap), ownerName, phoneNumber, repairStatus);
                            break;
                        }

                        m_Garage.AddNewVehicle(VehicleManager.CreateNewElectricCar(basicArgumentsMap), ownerName, phoneNumber, repairStatus);
                        break;
                    case VehicleManager.eVehicleTypes.Motorcycle:
                    case VehicleManager.eVehicleTypes.ElectricMotorcycle:
                        basicArgumentsMap.Add(VehicleManager.sr_KeyEngineCapacity, getIntegerFromUser(Strings.set_engine_capacity));
                        basicArgumentsMap.Add(VehicleManager.sr_KeyLicenseType, getOptionFromUser<LicenseType.eLicenseType>(Strings.get_license_massage, LicenseType.sr_LicenseType, -1));
                        if ((VehicleManager.eVehicleTypes)basicArgumentsMap[VehicleManager.sr_KeyRepairStatus] == VehicleManager.eVehicleTypes.Motorcycle)
                        {
                            m_Garage.AddNewVehicle(VehicleManager.CreateNewMotorcycle(basicArgumentsMap), ownerName, phoneNumber, repairStatus);
                            break;
                        }

                        m_Garage.AddNewVehicle(VehicleManager.CreateNewElectricMotorcycle(basicArgumentsMap), ownerName, phoneNumber, repairStatus);
                        break;
                    default:
                        break;
                }
            }
        }

        public static string AskForBasicDetail(string i_MessageToPrint)
        {
            printMessage(i_MessageToPrint);
            return Console.ReadLine();
        }

        public static int AskForArgumentWithOptions(string i_MessageToPrint, List<string> i_OptionList, int i_OffsetFromChoices)
        {
            return getOptionFromUser<int>(i_MessageToPrint, i_OptionList, i_OffsetFromChoices);
        }

        public static T getOptionFromUser<T>(string i_Message, List<string> i_OptionList, int i_OffsetFromChoices)
        {
            T parameterToReturn;
            printMessage(i_Message);
            showOptions(i_OptionList);
            parameterToReturn = (T)(object)(getUserChoice(1, i_OptionList.Count) + i_OffsetFromChoices);
            return parameterToReturn;
        }

        private float getFloatFromUser()
        {
            float userChoice;

            if (!float.TryParse(Console.ReadLine(), out userChoice))
            {
                throw new FormatException(Strings.invalid_integer);
            }

            return userChoice;
        }

        private float getFloatFromUser(string i_Message)
        {
            printMessage(i_Message);
            return getFloatFromUser();
        }

        private float getIntegerFromUser(string i_Message)
        {
            printMessage(i_Message);
            return getIntegerFromUser();
        }

        private static int getIntegerFromUser()
        {
            int userChoice;

            if (!int.TryParse(Console.ReadLine(), out userChoice))
            {
                throw new FormatException(Strings.invalid_integer);
            }

            return userChoice;
        }

        public void FuelVehicleUI()
        {
            string plateNumber;
            FuelVehicle.eEnergyType energyType;
            float amountOfFuelToAdd;
            BaseVehicle VehicleToFuel;

            printMessage(Strings.enter_plate_number);
            plateNumber = getStringFromUser();
            energyType = getOptionFromUser<FuelVehicle.eEnergyType>(Strings.choose_type_of_fuel, FuelVehicle.sr_EnergyTypeList, -1);
            amountOfFuelToAdd = getFloatFromUser(Strings.amount_fuel_massage);
            try
            {
                VehicleToFuel = m_Garage.GetVehicleByPlateNumber(plateNumber).Vehicle;
                m_Garage.FuelVehicle(VehicleToFuel, energyType, amountOfFuelToAdd);
            }
            catch (ValueOutOfRangeException i_ValueOutOfRange)
            {
                showError(i_ValueOutOfRange.Message);
                if (amountOfFuelToAdd > i_ValueOutOfRange.MaxValue)
                {
                    showError(string.Format(Strings.out_of_range_max, amountOfFuelToAdd));
                }
                else
                {
                    showError(string.Format(Strings.out_of_range_min, amountOfFuelToAdd));
                }
            }
            catch (Exception i_Exception)
            {
                showError(i_Exception.Message);
            }
        }

        public void ChargeElectricVehicleUI()
        {
            string plateNumber = getStringFromUser(Strings.enter_plate_number);
            float amountOfElectricInMinutesToAdd = getFloatFromUser(Strings.amount_to_charge);

            try
            {
                BaseVehicle VehicleToCharge = m_Garage.GetVehicleByPlateNumber(plateNumber).Vehicle;
                m_Garage.ChargeElectricVehicle(VehicleToCharge, amountOfElectricInMinutesToAdd);
            }
            catch (ValueOutOfRangeException i_ValueOutOfRange)
            {
                showError(i_ValueOutOfRange.Message);
                if (amountOfElectricInMinutesToAdd > i_ValueOutOfRange.MaxValue)
                {
                    showError(string.Format(Strings.out_of_range_max, amountOfElectricInMinutesToAdd));
                }
                else
                {
                    showError(string.Format(Strings.out_of_range_min, amountOfElectricInMinutesToAdd));
                }
            }
            catch (Exception i_PlateError)
            {
                showError(i_PlateError.Message);
            }
        }

        public void ShowVehiclesByPlateUI()
        {
            if (m_Garage.Vehicles.Count != 0)
            {
                foreach (KeyValuePair<string,VehicleProperties> vehicle in m_Garage.Vehicles)
                {
                    printMessage(string.Format("{0}\n{1}\n", vehicle.ToString(), Strings.line_brake));
                }
            }
            else
            {
                showError(Strings.no_available_vehicle_in_garage);
            }
        }

        public void ChangePropertiesUI()
        {
            string plateNumber = getStringFromUser(Strings.enter_plate_number);
            try
            {
                VehicleProperties vehicle = m_Garage.GetVehicleByPlateNumber(plateNumber);
                VehicleProperties.eStateOfService newType = getOptionFromUser<VehicleProperties.eStateOfService>(string.Format(Strings.change_status_options, VehicleProperties.sr_StateListOptions[(int)vehicle.Status]), VehicleProperties.sr_StateListOptions, -1);
                vehicle.Status = newType;
            }
            catch (ArgumentException i_PlateError)
            {
                showError(i_PlateError.Message);
            }
        }

        public void InflatingWheelUI()
        {
            string plateNumber = getStringFromUser(Strings.enter_plate_number);

            try
            {
                BaseVehicle vehicle = m_Garage.GetVehicleByPlateNumber(plateNumber).Vehicle;
                m_Garage.InflatingWheel(vehicle);
            }
            catch (ArgumentException i_PlateError)
            {
                showError(i_PlateError.Message);
            }
        }

        public void ShowPlatesOfAllVehiclesUI()
        {
            int vehicleInGarage = m_Garage.Vehicles.Count, vehicleCounter = 1;
            if (vehicleInGarage != 0)
            {
                foreach (KeyValuePair<string, VehicleProperties> vehicle in m_Garage.Vehicles)
                {
                    printMessage(string.Format("{0}: {1}", vehicleCounter, vehicle.Value.Vehicle.PlateNumber));
                }
            }
            else
            {
                showError(Strings.no_available_vehicle_in_garage);
            }
        }
    }
}