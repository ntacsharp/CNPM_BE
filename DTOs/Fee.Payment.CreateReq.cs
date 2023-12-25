namespace CNPM_BE.DTOs
{
    public class FeePaymentCreateReq
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
