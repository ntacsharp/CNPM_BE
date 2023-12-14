namespace CNPM_BE.Models
{
    public class ServiceFee
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public int OldCount { get; set; }
        public int NewCount { get; set; }
        public int CreatorId { get; set; }
        public int FeeId { get; set; }
        public int TotalFee { get; set; }
        public ServiceFeeStatus Status { get; set; }
    }
    public enum ServiceFeeStatus
    {
        Active,
        Deleted
    }
}
