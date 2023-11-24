namespace CNPM_BE.DTOs
{
    public class FeePaymentCreateReq
    {
        public int HouseholdId { get; set; }
        public int Amount { get; set; }
        public int Type { get; set; }
    }
}
