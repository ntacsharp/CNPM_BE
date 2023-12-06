namespace CNPM_BE.DTOs
{
    public class HouseholdCreateReq
    {
        public string OwnerName { get; set; }
        public string HouseholdCode { get; set; }
        public string ApartmentCode { get; set; }
        public int VehicleCount { get; set; }
        public double Area { get; set; }
        public int ServiceFeePerMeter { get; set; }
    }
}
