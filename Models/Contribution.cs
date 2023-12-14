namespace CNPM_BE.Models
{
    public class Contribution
    {
        public int Id { get; set; }
        public int CreatorId { get; set; }
        public int ApartmentId { get; set; }
        public int ForThePoor {  get; set; }
        public int ForVNSeasAndIslands { get; set; }
        public int DGFestival { get; set; }
        public int ResidentialGroup { get; set; }
        public int ForChildren { get; set; }
        public int Charity { get; set; }
        public int Gratitude { get; set; }
        public int StudyPromotion { get; set; }
        public int ForTheElderly { get; set; }
    }
}
