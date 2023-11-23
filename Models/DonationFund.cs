using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace CNPM_BE.Models
{
    public class DonationFund
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedTime { get; set; }
        public int CreatorId { get; set; }
        public DateTime ExpirationTime { get; set; }
        public int? SmallestAmount { get; set; }
        public string? Description { get; set; }
        public int ReceivedAmount { get; set; }
    }
}
