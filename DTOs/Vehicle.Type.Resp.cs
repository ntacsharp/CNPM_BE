using CNPM_BE.Models;

namespace CNPM_BE.DTOs
{
    public class VehicleTypeResp
    {
        public int Id { get; set; }
        public string VehicleTypeCode { get; set; }
        public string Name { get; set; }
        public int ParkingFee { get; set; }
        public VehicleTypeResp(VehicleType vehicleType)
        {
            Id = vehicleType.Id;
            Name = vehicleType.Name;
            VehicleTypeCode = vehicleType.VehicleTypeCode;
            ParkingFee = vehicleType.ParkingFee;
        }
    }
}
