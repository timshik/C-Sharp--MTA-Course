using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using n_Vehicle;
using n_Strings;
using n_Wheel;

namespace Garage
{
    public class GarageManager
    {
        public static readonly int sr_BasicStartIntLevel = 0, sr_BasicStartFloatLevel = 0;
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
                }
            }

            throw new Exception(string.Format(Strings.plate_didnt_found, i_PlateNumber));
        }

        public List<VehicleProperties> Vehicles
        {
            get { return m_Vehicles; }
        }

        public void FuelVehicle(BaseVehicle i_Vehicle, FuelVehicle.eEnergyType i_FuelType, float i_Amount)
        {
            FuelVehicle vehicle = i_Vehicle as FuelVehicle;

            if(vehicle != null)
            {
                if (vehicle.EnergyType == i_FuelType)
                {
                    vehicle.Fuel = i_Amount;
                }
                else
                {
                    throw new ArgumentException(Strings.try_to_fuel_other_type_of_fuel);
                }
            }
            else
            {
                throw new ArgumentException(Strings.try_to_fuel_electric_vehicle);
            }
        }

        public void ChargeElectricVehicle(BaseVehicle i_Vehicle, float i_Amount)
        {
            ElectricVehicle vehicle = i_Vehicle as ElectricVehicle;

            if (vehicle != null)
            {
                vehicle.ChargeBattery(i_Amount);
            }
            else
            {
                throw new ArgumentException(Strings.try_to_charge_fuel_vehicle);
            }
        }

        public void InflatingWheel(BaseVehicle i_Vehicle, float i_Amount)
        {
            i_Vehicle.FillTires(i_Amount);
        }
    }
}