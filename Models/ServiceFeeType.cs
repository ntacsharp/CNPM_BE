namespace CNPM_BE.Models
{
    public class ServiceFeeType
    {
        public int Id { get; set; }
        public string ServiceFeeTypeCode { get; set; }
        public string Name { get; set; }
        public int PricePerUnit { get; set; }
        public MeasuringUnit MeasuringUnit { get; set; }
        public ServiceFeeTypeStatus Status { get; set; }
        public bool IsSystem {  get; set; }
    }
    public enum MeasuringUnit
    {
        Resident, // người/tháng
        Apartment, // hộ/tháng
        M2, // m vuông
        Number, // số
        M3 // m khối
    }
    public enum ServiceFeeTypeStatus
    {
        Active,
        Deleted
    }
}
