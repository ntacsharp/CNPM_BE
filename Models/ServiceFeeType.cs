namespace CNPM_BE.Models
{
    public class ServiceFeeType
    {
        public int Id { get; set; }
        public int CreatorId { get; set; }
        public string ServiceFeeTypeCode { get; set; }
        public string Name { get; set; }
        public int PricePerUnit { get; set; }
        public MeasuringUnit MeasuringUnit { get; set; }
        public ServiceFeeTypeStatus Status { get; set; }
    }
    public enum MeasuringUnit
    {
        Resident,
        Apartment,
        Number,
        M2
    }
    public enum ServiceFeeTypeStatus
    {
        Active,
        Deleted
    }
}
