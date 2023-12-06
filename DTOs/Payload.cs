namespace CNPM_BE.DTOs
{
    public class Payload
    {
        public int Take { get; set; }
        public int Skip { get; set; }
        public string Sort { get; set; }
        public List<string> Columns { get; set; }
    }
}
