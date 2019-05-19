using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using n_Strings;
using Garage;
using n_Vehicle;
using n_Wheel;

namespace Ex03.ConsoleUI
{
    public class ConsoleUI
    {
        private static readonly int sr_MaxSelectableChoice = 8;
        GarageManager m_Garage;
        private static readonly List<string> sr_FirstMenuStringArray;
        private static readonly List<string> sr_BooleanOptions;

        public void WelcomeToTheGarage(GarageManager i_Garage)
        {
            initializationEnums();
            m_Garage = i_Garage;
            printMessage(Strings.welcome_massage);
            do
            {
                ShowOptions(sr_FirstMenuStringArray);
                DoAction(getUserChoice(1, sr_MaxSelectableChoice));
            }
            while (AnotherAction());

            printMessage(Strings.breakup_massage);
        }

        private void initializationEnums()
        {
            CarColor.SetListOfCarColors();
            LicenseType.SetListOfLicenseType();
            VehicleManager.SetVehicleList();
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

        private void printMessage(string i_Message)
        {
            Console.WriteLine(i_Message);
        }

        private void showError(string i_Message)
        {
            Console.WriteLine(i_Message);
        }

        private int getUserChoice(int i_MinValue, int i_MaxValue)
        {
            int userChoice;

            while (!int.TryParse(Console.ReadLine(), out userChoice))
            {
                showError(Strings.invalid_integer);
            }

            if (userChoice < i_MinValue || userChoice > i_MaxValue)
            {
                showError(Strings.invalid_menu_choice);
                userChoice = getUserChoice(i_MinValue, i_MaxValue);
            }

            return userChoice;
        }

        public void ShowOptions(List<string> i_OptionsArray)
        {
            for (int i = 0; i < i_OptionsArray.Count; i++)
            {
                printMessage(string.Format(Strings.menu_format, i + 1, i_OptionsArray[i]));
            }
        }

        public void DoAction(int i_Choice)
        {
            switch (i_Choice)
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
                    ExitProgram();
                    break;
                default:
                    break;
            }
        }

        public bool AnotherAction()
        {
            int choice;
            bool userChoice = !true;
            printMessage(Strings.continue_massage);
            choice = getUserChoice(1, 2);

            if (choice == 1)
            {
                userChoice = true;
            }

            return userChoice;
        }

        private string getStringFromUser()
        {
            return Console.ReadLine();
        }

        private void addNewVehicleUI()
        {
            string plateNumber, modelName, wheelManufacturer;

            printMessage(Strings.enter_plate_number);
            plateNumber = Console.ReadLine();
            try
            {
                m_Garage.GetVehicleByPlateNumber(plateNumber);
                printMessage(Strings.choose_type_of_vehicle);
                ShowOptions(VehicleManager.VehicleList);
                VehicleManager.eVehicleTypes type = (VehicleManager.eVehicleTypes)(getUserChoice(1, VehicleManager.VehicleList.Count) - 1);
                modelName = getStringFromUser();
                wheelManufacturer = getStringFromUser();
                switch (type)
                {
                    case VehicleManager.eVehicleTypes.Truck:
                        m_Garage.AddNewVehicle(VehicleManager.CreateNewTruck(modelName, plateNumber, TruckDeliveryMaterials(), TruckCapacityLevel(), wheelManufacturer));
                        break;
                    case VehicleManager.eVehicleTypes.Motorcycle:
                        m_Garage.AddNewVehicle(VehicleManager.CreateNewMotorcycle(modelName, plateNumber, getEngineCapacity(), getLicenseType(), wheelManufacturer));
                        break;
                    case VehicleManager.eVehicleTypes.Car:
                        m_Garage.AddNewVehicle(VehicleManager.CreateNewCar(plateNumber, getNumberOfDoors(), getCarColor(), modelName, wheelManufacturer));
                        break;
                    case VehicleManager.eVehicleTypes.ElectricCar:
                        m_Garage.AddNewVehicle(VehicleManager.CreateNewElectricCar(modelName, plateNumber, wheelManufacturer, getCarColor(), getNumberOfDoors()));
                        break;
                    case VehicleManager.eVehicleTypes.ElectricMotorcycle:
                        m_Garage.AddNewVehicle(VehicleManager.CreateNewElectricMotorcycle(modelName, plateNumber, wheelManufacturer, getLicenseType(), getEngineCapacity()));
                        break;
                    default:
                        break;
                }
            }
            catch (ArgumentException i_PlateError)
            {
                showError(i_PlateError.Message);
            }
        }

        private CarColor.eCarColor getCarColor()
        {
            CarColor.eCarColor carColor;
            printMessage(Strings.choose_car_color);
            ShowOptions(CarColor.sr_CarColorNames);
            carColor = (CarColor.eCarColor)getUserChoice(1, DoorNumber.sr_DoorsOptions.Count);
            return carColor;
        }

        private DoorNumber.eNumberOfDoors getNumberOfDoors()
        {
            DoorNumber.eNumberOfDoors doorNumbers;
            printMessage(Strings.choose_door_number);
            ShowOptions(DoorNumber.sr_DoorsOptions);
            doorNumbers = (DoorNumber.eNumberOfDoors)getUserChoice(1, DoorNumber.sr_DoorsOptions.Count) + 1;
            return doorNumbers;
        }

        private float TruckCapacityLevel()
        {
            return getFloatFromUser();
        }

        private float getFloatFromUser()
        {
            float userChoice;

            while (!float.TryParse(Console.ReadLine(), out userChoice))
            {
                showError(Strings.invalid_integer);
            }

            return userChoice;
        }

        private int getIntegerFromUser()
        {
            int userChoice;

            while (!int.TryParse(Console.ReadLine(), out userChoice))
            {
                showError(Strings.invalid_integer);
            }

            return userChoice;
        }

        private bool TruckDeliveryMaterials()
        {
            ShowOptions(sr_BooleanOptions);
            return getUserChoice(1, 2) == 1 ? true : !true;
        }
        public LicenseType.eLicenseType getLicenseType()
        {
            int usersChoice;
            LicenseType.eLicenseType licenseType;
            Console.WriteLine(Strings.get_license_massage);
            ShowOptions(LicenseType.sr_LicenseType);
            usersChoice = getUserChoice(1, 4);
            licenseType = (LicenseType.eLicenseType)(usersChoice - 1);
            return licenseType;
        }
        public int getEngineCapacity()
        {
            printMessage(Strings.engine_capacity_massage);
            return getIntegerFromUser();
        }
        public void FuelVehicleUI()
        {
            string plateNumber;
            FuelVehicle.eEnergyType energyType;
            float amountOfFuelToAdd;
            BaseVehicle VehicleToFuel ;

            printMessage(Strings.enter_plate_number);
            plateNumber = getStringFromUser();
            try
            {
                VehicleToFuel = m_Garage.GetVehicleByPlateNumber(plateNumber).Vehicle;
                ShowOptions(FuelVehicle.sr_EnergyTypeList);
                energyType = (FuelVehicle.eEnergyType)(getUserChoice(1, FuelVehicle.sr_EnergyTypeList.Count) -1);
                Console.WriteLine(Strings.ammount_fuel_massage);
                amountOfFuelToAdd = getFloatFromUser();
                m_Garage.FuelVehicle(VehicleToFuel, energyType, amountOfFuelToAdd);

            }
            catch (ArgumentException i_PlateError)
            {
                showError(i_PlateError.Message);
            }
        }
        public void ChargeElectricVehicleUI()
        {
            string plateNumber;
            
            float amountOfElectricInMinutesToAdd;
            BaseVehicle VehicleToCharge;

            printMessage(Strings.enter_plate_number);
            plateNumber = getStringFromUser();
            try
            {
                VehicleToCharge = m_Garage.GetVehicleByPlateNumber(plateNumber).Vehicle;
                Console.WriteLine(Strings.amount_fuel_massage);
                amountOfElectricInMinutesToAdd = getFloatFromUser();
                m_Garage.ChargeElectricVehicle(VehicleToCharge, amountOfElectricInMinutesToAdd);

            }
            catch (ArgumentException i_PlateError)
            {
                showError(i_PlateError.Message);
            }

        }
        public void ShowVehiclesByPlateUI() { }

        public void ChangePropertiesUI()
        {
            string plateNumber;
            VehicleProperties.eStateOfService newType;
            VehicleProperties vehicle;
            printMessage(Strings.enter_plate_number);
            plateNumber = getStringFromUser();
            try
            {
                vehicle = m_Garage.GetVehicleByPlateNumber(plateNumber);
                printMessage(string.Format(Strings.change_status_options,VehicleProperties.sr_StateListOptions[(int)vehicle.Status]));
                newType = (VehicleProperties.eStateOfService) getUserChoice(1, VehicleProperties.sr_StateListOptions.Count) - 1;
                vehicle.Status = newType;
            }
            catch (ArgumentException i_PlateError)
            {
                showError(i_PlateError.Message);
            }
        }

        public void InflatingWheelUI()
        {
            string plateNumber;
            BaseVehicle vehicle;
            float amount;
            printMessage(Strings.enter_plate_number);
            plateNumber = getStringFromUser();
            try
            {
                vehicle = m_Garage.GetVehicleByPlateNumber(plateNumber).Vehicle;
                printMessage(Strings.amount_to_fill_tire);
                amount = getFloatFromUser();
                m_Garage.InflatingWheel(vehicle, amount);
            }
            catch (ArgumentException i_PlateError)
            {
                showError(i_PlateError.Message);
            }
        }
        public void ShowPlatesOfAllVehiclesUI() { }
    }
}