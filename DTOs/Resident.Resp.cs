using CNPM_BE.Models;

namespace CNPM_BE.DTOs
{
    public class ResidentResp
    {
        public int Id { get; set; }
        public string ResidentCode { get; set; }

        // id căn hộ
        public int ApartmentId { get; set; }
        public string PhoneNumber { get; set; }
        public string CCCD { get; set; }
        public string Position { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Career { get; set; }
        public ResidentGender Gender { get; set; }

        public ResidentResp(Resident resident, Apartment apartment)
        {
            Id = resident.Id;
            ResidentCode = resident.ResidentCode;
            ApartmentId = apartment.Id;
            Position = apartment.Position;
            Name = resident.Name;
            BirthDate = resident.BirthDate;
            Career = resident.Career;
            Gender = resident.Gender;
            PhoneNumber = resident.PhoneNumber;
            CCCD = resident.CCCD;
        }
    }
}
