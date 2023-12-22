namespace CNPM_BE.Models
{
    public class Fee
    {
        public int Id { get; set; }
        public int CreatorId { get; set; }
        public int ApartmentId { get; set; }
        public string Note { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ExpiredDate { get; set; }
        public int ParkingFee { get; set; }
        public int ReceivedAmount { get; set; }
        public FeeStatus Status { get; set; }
    }
    public enum FeeStatus
    {
        OnGoing,
        Expired,
        Paid,
        Deleted
    }
}
