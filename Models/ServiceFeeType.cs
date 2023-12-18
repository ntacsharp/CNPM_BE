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
        public bool IsSystem {  get; set; }
    }
    public enum MeasuringUnit
    {
        Resident,
        Apartment,
        M2,
        Number,
        M3
    }
    public enum ServiceFeeTypeStatus
    {
        Active,
        Deleted
    }
}
