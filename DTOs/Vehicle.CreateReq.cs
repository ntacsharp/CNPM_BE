using CNPM_BE.Models;

namespace CNPM_BE.DTOs
{
    public class VehicleCreateReq
    {
        public string VehicleCode { get; set; }
        public string Name { get; set; }
        public string Plate { get; set; }
        public int OwnerId { get; set; }
        public int VehicleTypeId { get; set; }
    }
}
