namespace CNPM_BE.Models
{
    public class HouseholdMember
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HouseholdId { get; set; }
        public bool IsActive { get; set; }
    }
}
