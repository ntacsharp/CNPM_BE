namespace CNPM_BE.DTOs
{
    public class DonationResp
    {
        public int Id { get; set; }
        public string Donator { get; set; }
        public string CreatedTime { get; set; }
        public string Fund { get; set; }
        public int Amount { get; set; }
    }
}
