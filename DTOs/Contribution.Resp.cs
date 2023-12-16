using CNPM_BE.Models;

namespace CNPM_BE.DTOs
{
    public class ContributionResp
    {
        public int Id { get; set; }
        public string ApartmentCode { get; set; }
        public string ResidentName { get; set; }
        public string ResidentCode { get; set; }
        public string Position { get; set; }
        public int ForThePoor { get; set; }
        public int ForVNSeasAndIslands { get; set; }
        public int DGFestival { get; set; }
        public int ResidentialGroup { get; set; }
        public int ForChildren { get; set; }
        public int Charity { get; set; }
        public int Gratitude { get; set; }
        public int StudyPromotion { get; set; }
        public int ForTheElderly { get; set; }
        public string CreatedTime { get; set; }
        public ContributionResp(Contribution contribution, Resident owner, Apartment apartment)
        {
            Id = contribution.Id;
            ForThePoor = contribution.ForThePoor;
            ForVNSeasAndIslands = contribution.ForVNSeasAndIslands;
            DGFestival = contribution.DGFestival;
            ResidentialGroup  = contribution.ResidentialGroup;
            ForChildren = contribution.ForChildren;
            Charity = contribution.Charity;
            Gratitude = contribution.Gratitude;
            StudyPromotion = contribution.StudyPromotion;
            ForTheElderly = contribution.ForTheElderly;
            ResidentCode = owner.ResidentCode;
            ResidentName = owner.Name;
            Position = apartment.Position;
            ApartmentCode = apartment.ApartmentCode;
            CreatedTime = contribution.CreatedTime.ToString();
        }
    }
}
