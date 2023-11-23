namespace CNPM_BE.DTOs
{
    public class DonationCreateReq
    {
        public int DonatorId { get; set; }
        public int FundId { get; set; }
        public int Amount { get; set; }
    }
}
