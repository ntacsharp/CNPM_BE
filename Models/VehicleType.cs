namespace CNPM_BE.Models
{
    public class VehicleType
    {
        public int Id { get; set; }
        public string VehicleTypeCode { get; set; }
        public string Name { get; set; }
        public int ParkingFee { get; set; }
        public VehicleTypeStatus Status { get; set; }
    }
    public enum VehicleTypeStatus
    {
        Active,
        Deleted
    }
}
