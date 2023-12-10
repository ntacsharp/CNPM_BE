namespace CNPM_BE.Models
{
    public class Resident
    {
        public int Id { get; set; }
        public string ResidentCode { get; set; }
        public int ApartmentId { get; set; }
        public int CreatorId { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Career {  get; set; }
        public ResidentGender Gender { get; set; }
        public ResidentStatus Status { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
    }
    public enum ResidentGender
    {
        Male,
        Female,
        Other
    }
    public enum ResidentStatus
    {
        Active,
        Deleted
    }
}
