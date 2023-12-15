using CNPM_BE.Models;

namespace CNPM_BE.DTOs
{
    public class LoginResp
    {
        public string Token { get; set; }

        public int code { get; set; }

        public string message { get; set; }

        public AppUser? entity { get; set; }
    }
}
