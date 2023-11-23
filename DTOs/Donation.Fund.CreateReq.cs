namespace CNPM_BE.DTOs
{
    public class DonationFundCreateReq
    {
        public string Name { get; set; }
        public int? SmallestAmount { get; set; }
        public string? Description { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
