namespace CNPM_BE.DTOs
{
    public class HouseholdResp
    {
        public int Id {  get; set; }
        public string ApartmentCode { get; set; }
        public string Position {  get; set; }
        public string OwnerCode { get; set; }
        public string OwnerName { get; set; }
        public List<ResidentResp> ResidentList { get; set; }
    }
}
