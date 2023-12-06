namespace CNPM_BE.Models
{
    public class HouseholdDonation
    {
        public int Id { get; set; }
        public int DonatorId { get; set; }
        public int CreatorId { get; set; }
        public int FundId { get; set; }
        public int Amount { get; set; }
    }
}
