namespace CNPM_BE.DTOs
{
    public class FeeUpdateReq
    {
        public int Id { get; set; } 
        public string Note { get; set; }
        public List<ServiceFeeUpdateReq> ServiceFeeUpdateReqList { get; set; }
    }
}
