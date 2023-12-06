using CNPM_BE.Models;

namespace CNPM_BE.DTOs
{
    public class MemberResp
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public int Gender { get; set; }
        public string Nation { get; set; }
        public MemberResp(HouseholdMember hm)
        {
            Id = hm.Id;
            Name = hm.Name;
            BirthDate = hm.BirthDate.ToString("dd/MM/yyyy");
            Gender = hm.Gender;
            Nation = hm.Nation;
        }
    }
}
