using CNPM_BE.Data;
using CNPM_BE.DTOs;
using CNPM_BE.Models;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace CNPM_BE.Services
{
    public class UserService
    {
        private readonly CNPMDbContext _context;
        public UserService(CNPMDbContext context)
        {
            _context = context;
        }
        public async Task<Boolean> VerifyPassword(User user, LoginReq req)
        {
            return false;
        }
        public async Task<string> CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Role,"Admin")
            };
            return null;

        }
    }
}
