using CNPM_BE.Models;

namespace CNPM_BE.DTOs
{
    public class VehicleUpdateReq
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Plate { get; set; }
        public int VehicleTypeId { get; set; }
    }
}
