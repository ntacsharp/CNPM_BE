using CNPM_BE.Models;

namespace CNPM_BE.DTOs
{
    public class ResidentResp
    {
        public int Id { get; set; }
        public string ResidentCode { get; set; }
        public string ApartmentCode { get; set; }
        public string Position { get; set; }
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public string Career { get; set; }
        public ResidentGender Gender { get; set; }
        public ResidentResp(Resident resident, Apartment apartment)
        {
            Id = resident.Id;
            ResidentCode = resident.ResidentCode;
            ApartmentCode = apartment.ApartmentCode;
            Position = apartment.Position;
            Name = resident.Name;
            BirthDate = resident.BirthDate.ToString("dd/MM/yyyy");
            Career = resident.Career;
            Gender = resident.Gender;
        }
    }
}
