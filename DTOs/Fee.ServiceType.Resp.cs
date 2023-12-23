using CNPM_BE.Models;

namespace CNPM_BE.DTOs
{
    public class ServiceFeeTypeResp
    {
        public int Id { get; set; }
        public string ServiceFeeTypeCode { get; set; }
        public string Name { get; set; }
        public int PricePerUnit { get; set; }
        public MeasuringUnit MeasuringUnit { get; set; }
        public bool IsSystem {  get; set; }
        public ServiceFeeTypeResp(ServiceFeeType sft)
        {
            Id = sft.Id;
            ServiceFeeTypeCode = sft.ServiceFeeTypeCode;
            Name = sft.Name;
            PricePerUnit = sft.PricePerUnit;
            MeasuringUnit = sft.MeasuringUnit;
            IsSystem = sft.IsSystem;
        }
    }
}
