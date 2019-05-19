using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using n_Strings;
using Garage;

namespace Ex03.ConsoleUI
{
    public class ConsoleUI
    {
        private static readonly int sr_MaxSelectableChoice = 8;
        GarageManager m_Garage;

        public void WelcomeToTheGarage(GarageManager i_Garage)
        {
            m_Garage = i_Garage;
            printMessage(Strings.welcome_massage);
            do
            {
                ShowOptions();
                DoAction(getUserChoice(1, sr_MaxSelectableChoice));
            }
            while (AnotherAction());

            printMessage(Strings.breakup_massage);
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

        public void ShowOptions()
        {
            ////List<VehicleProperties> m_Vehicles = new List<VehicleProperties>();
            ////public void AddNewVehicle(BaseVehicle i_Vehicle) { }
            ////private BaseVehicle getVehicleByPlateNumber(string i_PlateNumber) { }
            ////public void FuelVehicle(string i_PlateNumber, FuelVehicle.eEnergyType i_FuelType, int i_Amount) { }
            ////public void ChargeElectricVehicle(string i_PlateNumber, int i_Amount) { }
            ////public void ShowVehiclesByPlate() { }
            ////public void ChangeProperties(string i_PlateNumber, VehicleProperties.eStateOfService i_NewState) { }
            ////public void InflatingWheel(string i_PlateNumber) { }
            ////public void ShowPlatesOfAllVehicles() { }
        }
        public void DoAction(int i_Choice)
        {
            switch (i_Choice)
            {
                case 1:
                    m_Garage.AddNewVehicle(FillDetails(1));
                    break;
                case 2: m_Garage.
                    break;
                case 3:
                    break;
                case4:
                    break;
                case5:
                    break;
                case6:
                    break;
                case7:
                    break;
                case8:
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
    }
}