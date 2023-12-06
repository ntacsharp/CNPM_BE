using CNPM_BE.Models;

namespace CNPM_BE.DTOs
{
    public class HouseholdResp
    {
        public int Id {  get; set; }
        public string OwnerName { get; set; }
        public string HouseholdCode { get; set; }
        public string ApartmentCode { get; set; }
        public int VehicleCount { get; set; }
        public double Area { get; set; }
        public int ServiceFeePerMeter { get; set; }
        public string CreatedTime { get; set; }
        public string? DeactivateTime { get; set; }
        public bool IsActive { get; set; }
        public HouseholdResp(Household hh)
        { 
            Id = hh.Id;
            OwnerName = hh.OwnerName;
            HouseholdCode = hh.HouseholdCode;
            ApartmentCode = hh.ApartmentCode;
            VehicleCount = hh.VehicleCount;
            Area = hh.Area;
            IsActive = hh.IsActive;
            ServiceFeePerMeter = hh.ServiceFeePerMeter;
            CreatedTime = hh.CreatedTime.ToString();
            if(hh.DeactivateTime != null) DeactivateTime = hh.DeactivateTime.ToString();
        }
    }
}
