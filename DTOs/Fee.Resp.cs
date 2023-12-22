using CNPM_BE.Models;

namespace CNPM_BE.DTOs
{
    public class FeeResp
    {
        public int Id { get; set; }
        public string ApartmentCode { get; set; }
        public double Area { get; set; }
        public string OwnerCode { get; set; }
        public string OwnerName { get; set; }
        public string Note { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpiredDate { get; set; }
        public int TotalFee { get; set; }
        public FeeStatus Status { get; set; }
        public int ParkingFee { get; set; }
        public int ServiceFee { get; set; }
        public int ElectricityFee { get; set; }
        public int WaterFee { get; set; }
        public int ReceivedAmount { get; set; }
        public List<ServiceFeeResp> ServiceFeeList { get; set; }
        public List<FeePaymentResp> FeePaymentList { get; set; }
        public List<VehicleResp> VehicleList { get; set; }
    }
}
