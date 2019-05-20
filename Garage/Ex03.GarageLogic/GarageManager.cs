using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using n_Vehicle;
using n_Strings;

namespace Garage
{
    public class GarageManager
    {
        private List<VehicleProperties> m_Vehicles = new List<VehicleProperties>();

        public void AddNewVehicle(BaseVehicle i_Vehicle, string i_OwnerName, string i_PhoneNumber, VehicleProperties.eStateOfService i_Status)
        {
            m_Vehicles.Add(new VehicleProperties(i_Vehicle, i_OwnerName, i_PhoneNumber, i_Status));
        }

        public VehicleProperties GetVehicleByPlateNumber(string i_PlateNumber)
        {
            foreach (VehicleProperties vehicle in m_Vehicles)
            {
                if (vehicle.Vehicle.PlateNumber.Equals(i_PlateNumber))
                {
                    return vehicle;
                    //// vehicle.Status = VehicleProperties.eStateOfService.InRepair;//לבדוק האם הרכב כבר בתיקון או שולם 
                }
            }

            throw new ArgumentException(string.Format(Strings.plate_didnt_found, i_PlateNumber));
        }

        public List<VehicleProperties> Vehicles
        {
            get { return m_Vehicles; }
        }

        public void FuelVehicle(BaseVehicle i_Vehicle, FuelVehicle.eEnergyType i_FuelType, float i_Amount)
        {
        }

        public void ChargeElectricVehicle(BaseVehicle i_Vehicle, float i_Amount)
        {
        }

        public void ShowVehiclesByPlate()
        {
        }

        public void ChangeProperties(string i_PlateNumber, VehicleProperties.eStateOfService i_NewState)
        {
        }

        public void InflatingWheel(BaseVehicle i_Vehicle, float i_Amount)
        {
        }

        public void ShowPlatesOfAllVehicles()
        {
        }
    }
}