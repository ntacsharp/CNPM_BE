namespace CNPM_BE.Models
{
    public class Apartment
    {
        public int Id { get; set; }
        public string ApartmentCode { get; set; }
        public int CreatorId { get; set; }
        public int? OwnerId { get; set; }
        public string Position { get; set; }
        public double Area {  get; set; }
        public int RoomCount { get; set; }
        public long Price { get; set; }
        public ApartmentStatus Status { get; set; }
    }
    public enum ApartmentStatus
    {
        Occupied,
        Unoccupied,
        Deleted
    }
}
