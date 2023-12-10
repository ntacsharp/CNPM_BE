using CNPM_BE.Models;

namespace CNPM_BE.DTOs
{
    public class HouseholdOption
    {
        public int Id { get; set; }
        public string ApartmentCode { get; set; }
        public string OwnerName { get; set; }
        public string Position { get; set; }
        public HouseholdOption(Apartment a, Resident r)
        {
            Id = a.Id;
            ApartmentCode = a.ApartmentCode;
            OwnerName = r.Name;
            Position = a.Position;
        }
    }
}
