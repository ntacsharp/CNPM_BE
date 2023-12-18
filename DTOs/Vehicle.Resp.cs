using CNPM_BE.Models;

namespace CNPM_BE.DTOs
{
    public class VehicleResp
    {
        public int Id { get; set; }
        //public string VehicleCode { get; set; }
        //public string Name { get; set; }
        public string Plate { get; set; }
        public string OwnerCode { get; set; }
        public string OwnerName { get; set; }
        public string VehicleType { get; set; }
        public int ParkingFee { get; set; }
        public VehicleResp(Vehicle vehicle, Resident resident, VehicleType vehicleType)
        {
            Id = vehicle.Id;
            //VehicleCode = vehicle.VehicleCode;
            //Name = vehicle.Name;
            Plate = vehicle.Plate;
            OwnerCode = resident.ResidentCode;
            OwnerName = resident.Name;
            VehicleType = vehicleType.Name;
            ParkingFee = vehicleType.ParkingFee;
        }
    }
}
