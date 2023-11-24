namespace CNPM_BE.Models
{
    public class FeePayment
    {
        public int Id { get; set; }
        public int HouseholdId { get; set; }
        public int Amount { get; set; }
        public FeeType Type { get; set; }
        public DateTime CreatedTime { get; set; }
    }
    public enum FeeType
    {
        Management,
        Parking,
        Service
    }
}
