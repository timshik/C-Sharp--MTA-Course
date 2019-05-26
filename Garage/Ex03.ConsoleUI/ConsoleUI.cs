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
        private GarageManager m_Garage = new GarageManager();

        private static void printMessage(string i_Message)
        {
            Console.WriteLine(i_Message);
        }

        public static void Main()
        {
            ConsoleUI UI = new ConsoleUI();
            UI.WelcomeToTheGarage();
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

        public static string AskForBasicDetail(string i_MessageToPrint)
        {
            printMessage(i_MessageToPrint);
            return Console.ReadLine();
        }

        public static int AskForArgumentWithOptions(string i_MessageToPrint, List<string> i_OptionList, int i_OffsetFromChoices)
        {
            return getOptionFromUser<int>(i_MessageToPrint, i_OptionList, i_OffsetFromChoices);
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

        public static T getOptionFromUser<T>(string i_Message, List<string> i_OptionList, int i_OffsetFromChoices)
        {
            T parameterToReturn;
            printMessage(i_Message);
            showOptions(i_OptionList);
            parameterToReturn = (T)(object)(getUserChoice(1, i_OptionList.Count) + i_OffsetFromChoices);
            return parameterToReturn;
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
            VehicleManager.SetBooleanList();
            VehicleManager.ArrangementExtandedOptionList();
            sr_FirstMenuStringArray.Add(Strings.menu_options_1);
            sr_FirstMenuStringArray.Add(Strings.menu_options_2);
            sr_FirstMenuStringArray.Add(Strings.menu_options_3);
            sr_FirstMenuStringArray.Add(Strings.menu_options_4);
            sr_FirstMenuStringArray.Add(Strings.menu_options_5);
            sr_FirstMenuStringArray.Add(Strings.menu_options_6);
            sr_FirstMenuStringArray.Add(Strings.menu_options_7);
            sr_FirstMenuStringArray.Add(Strings.menu_options_8);
        }

        private void showError(string i_Message)
        {
            Console.WriteLine(i_Message);
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
                if (i_PlateError is KeyNotFoundException)
                {
                    showError(string.Format(Strings.plate_didnt_found, plateNumber));
                }
                else
                {
                    showError(Strings.unknown_error);
                }

                printMessage(Strings.create_new_vehicle);
                VehicleManager.eVehicleTypes vehicleType = getOptionFromUser<VehicleManager.eVehicleTypes>(Strings.choose_type_of_vehicle, VehicleManager.VehicleList, -1);
                basicArgumentsMap.Add(ArgumentsKeysets.sr_KeyTypeOfVehicle, vehicleType);
                basicArgumentsMap.Add(ArgumentsKeysets.sr_KeyOwnerName, getStringFromUser(Strings.enter_owner_name));
                basicArgumentsMap.Add(ArgumentsKeysets.sr_KeyPhoneNumber, getStringFromUser(Strings.enter_phone_number));
                basicArgumentsMap.Add(ArgumentsKeysets.sr_KeyModelName, getStringFromUser(Strings.enter_model_name));
                basicArgumentsMap.Add(ArgumentsKeysets.sr_KeyWheelManufacturer, getStringFromUser(Strings.enter_wheel_manufacturer));
                basicArgumentsMap.Add(ArgumentsKeysets.sr_KeyPlateNumber, plateNumber);
                basicArgumentsMap.Add(ArgumentsKeysets.sr_KeyRepairStatus, getOptionFromUser<VehicleProperties.eStateOfService>(Strings.choose_status_of_vehicle, VehicleProperties.sr_StateListOptions, -1));
                basicArgumentsMap.Add(ArgumentsKeysets.sr_KeyCurrentEnergyLevel, getFloatFromUser(Strings.enter_current_energy_level));
                basicArgumentsMap.Add(ArgumentsKeysets.sr_KeyCurrentWheelPressure, getFloatFromUser(Strings.enter_current_wheel_pressure_level));
                getMoreInformationBasedOnType(VehicleManager.s_OptionsToAskUserByTypes[vehicleType], ref basicArgumentsMap);
                m_Garage.AddNewVehicle(VehicleManager.CreateNewVehicle(ref basicArgumentsMap), ref basicArgumentsMap);
            }
        }

        private void getMoreInformationBasedOnType(VehicleManager.VehicleTypesOptions i_VehicleTypesOptions, ref Dictionary<string, object> i_BasicArgumentsMap)
        {
            for (int i = 0; i < i_VehicleTypesOptions.OptionList.Count; i++)
            {
                if (i_VehicleTypesOptions.OptionList[i] == null)
                {
                    i_BasicArgumentsMap.Add(
                        i_VehicleTypesOptions.OptionKeys[i],
                        getStringFromUser(i_VehicleTypesOptions.OptionListMessages[i]));
                }
                else
                {
                    i_BasicArgumentsMap.Add(
                        i_VehicleTypesOptions.OptionKeys[i],
                        getOptionFromUser<object>(i_VehicleTypesOptions.OptionListMessages[i], i_VehicleTypesOptions.OptionList[i], i_VehicleTypesOptions.OptionListOffsets[i]));
                }
            }
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
                foreach (KeyValuePair<string, VehicleProperties> vehicle in m_Garage.Vehicles)
                {
                    printMessage(string.Format("{0}{2}{1}{2}", vehicle.ToString(), Strings.line_brake, Environment.NewLine));
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