namespace CNPM_BE.DTOs
{
    public class ApartmentCreateReq
    {
        public string ApartmentCode { get; set; }
        public string Position { get; set; }
        public double Area { get; set; }
        public int RoomCount { get; set; }
        public long Price { get; set; }
    }
}
