namespace CNPM_BE.Models
{
    public class HouseholdFeeHistory
    {
        public int Id { get; set; }
        public int HouseholdId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
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
        public HouseholdFeeHistory(CurrentHouseholdFee chf, int month, int year)
        {
            Month = month;
            Year = year;
            CurrentManagementFee = chf.CurrentManagementFee;
            CurrentParkingFee = chf.CurrentParkingFee;
            CurrentServiceFee = chf.CurrentServiceFee;
            LeftoverManagementFee = chf.LeftoverManagementFee;
            LeftoverParkingFee = chf.LeftoverParkingFee;
            LeftoverServiceFee = chf.LeftoverServiceFee;
            TotalManagementFee = chf.TotalManagementFee;
            TotalParkingFee = chf.TotalParkingFee;
            TotalServiceFee = chf.TotalServiceFee;
            PaidManagementFee = chf.PaidManagementFee;
            PaidParkingFee = chf.PaidParkingFee;
            PaidServiceFee = chf.PaidServiceFee;
        }
    }
}
