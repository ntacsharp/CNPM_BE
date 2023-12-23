using CNPM_BE.Models;

namespace CNPM_BE.DTOs
{
    public class ServiceFeeResp
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PricePerUnit { get; set; }
        public MeasuringUnit MeasuringUnit { get; set; }
        public int OldCount { get; set; }
        public int NewCount { get; set; }
        public int TotalFee { get; set; }
    }
}
