namespace CNPM_BE.Services
{
    public class TimeConverterService
    {
        public TimeConverterService() { }
        public async Task<DateTime> ConvertToUTCTime(DateTime time)
        {
            TimeZoneInfo seAsiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var resp = TimeZoneInfo.ConvertTimeFromUtc(time.ToUniversalTime(), seAsiaTimeZone);
            return resp;
        }
    }
}
