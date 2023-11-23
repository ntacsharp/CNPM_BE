namespace CNPM_BE.Models
{
    public class HouseholdFee
    {
        public int Id { get; set; }
        public int HouseholdId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int ServiceFee { get; set; }
        public int ManagementFee { get; set; }
        public int ParkingFee { get; set; }
        public int ReceivedServiceFee { get; set; }
        public int ReceivedManagementFee { get; set; }
        public int ReceivedParkingFee { get; set; }
    }
}
