using CNPM_BE.Models;

namespace CNPM_BE.DTOs
{
    public class ApartmentResp
    {
        public int Id { get; set; }
        public string ApartmentCode { get; set; }
        public string Position { get; set; }
        public double Area { get; set; }
        public int RoomCount { get; set; }
        public int Price { get; set; }
        public ApartmentStatus Status { get; set; }
        public ApartmentResp(Apartment a) 
        {
            Id = a.Id;
            ApartmentCode = a.ApartmentCode;
            Position = a.Position;
            Area = a.Area;
            RoomCount = a.RoomCount;
            Price = a.Price;
            Status = a.Status;
        }
    }
}
