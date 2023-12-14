namespace CNPM_BE.DTOs
{
    public class ServiceFeeTypeCreateReq
    {
        public string ServiceFeeTypeCode { get; set; }
        public string Name { get; set; }
        public int PricePerUnit { get; set; }
        public int MeasuringUnit { get; set; }
    }
}
