using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using n_Vehicle;

namespace Garage
{
    public class GarageManager
    {
        List<VehicleProperties> m_Vehicles = new List<VehicleProperties>();
        public void AddNewVehicle(BaseVehicle i_Vehicle) { }
        private BaseVehicle getVehicleByPlateNumber(string i_PlateNumber) { }
        public void FuelVehicle(string i_PlateNumber, FuelVehicle.eEnergyType i_FuelType, int i_Amount) { }
        public void ChargeElectricVehicle(string i_PlateNumber, int i_Amount) { }
        public void ShowVehiclesByPlate() { }
        public void ChangeProperties(string i_PlateNumber, VehicleProperties.eStateOfService i_NewState) { }
        public void InflatingWheel(string i_PlateNumber) { }
        public void ShowPlatesOfAllVehicles() { }
    }
}