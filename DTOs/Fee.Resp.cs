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
        public string CreatedTime { get; set; }
        public string ExpiredDate { get; set; }
        public int TotalFee { get; set; }
        public string Status { get; set; }
        public int ManagementFee { get; set; }
        public int ParkingFee { get; set; }
        public int ServiceFee { get; set; }
        public int ReceivedAmount { get; set; }
        public List<ServiceFeeResp> ServiceFeeList { get; set; }
        public List<FeePaymentResp> FeePaymentList { get; set; }
    }
}
