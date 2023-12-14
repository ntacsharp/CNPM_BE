namespace CNPM_BE.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string VehicleCode { get; set; }
        public string Name { get; set; }
        public string Plate { get; set; }
        public int OwnerId { get; set; }
        public int CreatorId { get; set; }
        public int ApartmentId { get; set; }
        public VehicleStatus Status { get; set; }
        public int VehicleTypeId { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
    }
    public enum VehicleStatus
    {
        Active,
        Deleted
    }
}
