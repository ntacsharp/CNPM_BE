namespace CNPM_BE.DTOs
{
    public class FeeResp
    {
        public string HouseholdOwner { get; set; }
        public string HouseholdCode { get; set; }
        public string ApartmentCode { get; set; }
        public int CurrentManagementFee { get; set; }
        public int CurrentParkingFee { get; set; }
        public int CurrentServiceFee { get; set; }
        public int LeftoverManagementFee { get; set; }
        public int LeftoverParkingFee { get; set; }
        public int LeftoverServiceFee { get; set; }
        public int TotalManagementFee { get; set; }
        public int TotalParkingFee { get; set; }
        public int TotalServiceFee { get; set; }
        public int PaidManagementFee { get; set; }
        public int PaidParkingFee { get; set; }
        public int PaidServiceFee { get; set; }
    }
}
