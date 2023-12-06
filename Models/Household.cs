namespace CNPM_BE.Models
{
    public class Household
    {
        public int Id { get; set; }
        public int ManagerId { get; set; }
        public string OwnerName { get; set; }
        public string HouseholdCode { get; set; }
        public string ApartmentCode { get; set; }
        public int VehicleCount { get; set; }
        public double Area { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? DeactivateTime { get; set; }
        public int ServiceFeePerMeter { get; set; }
    }
}
