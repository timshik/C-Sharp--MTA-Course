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
        private Dictionary<string, VehicleProperties> m_Vehicles = new Dictionary<string, VehicleProperties>();

        public void AddNewVehicle(BaseVehicle i_Vehicle, ref Dictionary<string, object> i_ArgumentsList)
        {
            m_Vehicles.Add(
                i_Vehicle.PlateNumber,
                new VehicleProperties(
                    i_Vehicle,
                    (string)i_ArgumentsList[VehicleManager.sr_KeyOwnerName],
                    (string)i_ArgumentsList[VehicleManager.sr_KeyPhoneNumber],
                    (VehicleProperties.eStateOfService)i_ArgumentsList[VehicleManager.sr_KeyRepairStatus]));
        }

        public VehicleProperties GetVehicleByPlateNumber(string i_PlateNumber)
        {
            return m_Vehicles[i_PlateNumber];
        }

        public Dictionary<string, VehicleProperties> Vehicles
        {
            get { return m_Vehicles; }
        }

        public void FuelVehicle(BaseVehicle i_Vehicle, FuelVehicle.eEnergyType i_FuelType, float i_Amount)
        {
            FuelVehicle vehicle = i_Vehicle as FuelVehicle;

            if (vehicle != null)
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

        public void InflatingWheel(BaseVehicle i_Vehicle)
        {
            i_Vehicle.FillTires();
        }
    }
}