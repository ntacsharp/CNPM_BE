namespace CNPM_BE.DTOs
{
    public class ResidentCreateReq
    {
        public int ApartmentId { get; set; }
        public string ResidentCode { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Career { get; set; }
        public int Gender { get; set; }
    }
}
