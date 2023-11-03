using CNPM_BE.Data;
using CNPM_BE.DTOs;
using CNPM_BE.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;


namespace CNPM_BE.Services
{
    public class UserService
    {
        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        private readonly CNPMDbContext _context;
        private readonly IConfiguration _config;
        public UserService(CNPMDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public async Task<bool> VerifyPassword(User user, LoginReq req)
        {
            string hashedPassword = await HashPassword(req.Password, user.PasswordSalt);
            return hashedPassword == user.PasswordHash;
        }
        public async Task<ApiResp> CreateNewUser(RegisterReq req)
        {
            var newUser = new User();
            newUser.Username = req.Username;
            newUser.Role = req.Role;
            newUser.Email = req.Email;
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            newUser.PasswordSalt = salt;
            newUser.PasswordHash = await HashPassword(req.Password, salt);
            try
            {
                await _context.User.AddAsync(newUser);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
            var resp = new ApiResp();
            resp.code = 1;
            resp.message = "Đăng ký thành công";
            return resp;
        }
        public async Task<string> HashPassword(string password, byte[] salt)
        {
            salt = new byte[keySize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var hash = new Rfc2898DeriveBytes(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm).GetBytes(keySize);

            return Convert.ToHexString(hash);
        }
        public async Task<string> CreateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
