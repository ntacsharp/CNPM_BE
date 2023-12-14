namespace CNPM_BE.Models
{
    public class FeePayment
    {
        public int Id { get; set; }
        public int FeeId { get; set; }
        public int Amount { get; set; }
        public int CreatorId {  get; set; }
        public DateTime CreatedTime { get; set; }
        public FeePaymentStatus Status { get; set; }
    }
    public enum FeePaymentStatus
    {
        Active,
        Deleted
    }
}
