namespace CNPM_BE.DTOs
{
    public class AddMemberReq
    {
        public string Name { get; set; }
        public int HouseholdId { get; set; }
        public DateTime BirthDate { get; set; }
        public int Gender { get; set; }
        public string Nation { get; set; }
    }
}
