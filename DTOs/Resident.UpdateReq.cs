using CNPM_BE.Models;

namespace CNPM_BE.DTOs
{
    public class ResidentUpdateReq
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Career { get; set; }
        public int Gender { get; set; }
    }
}
