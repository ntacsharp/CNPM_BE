namespace CNPM_BE.Models
{
    public class CurrentHouseholdFee
    {
        public int Id { get; set; }
        public int HouseholdId { get; set; }
        public int CurrentManagementFee { get; set; }
        public int CurrentParkingFee { get; set; }
        public int CurrentServiceFee { get; set; }
        public int LeftoverManagementFee { get; set; }
        public int LeftoverParkingFee { get; set; }
        public int LeftoverServiceFee { get; set; }
        public int TotalManagementFee { get; set; }
        public int TotalParkingFee { get; set; }
        public int TotalServiceFee { get; set; }
        public int PaidManagementFee { get; set; }
        public int PaidParkingFee { get; set; }
        public int PaidServiceFee { get; set; }
    }
}
