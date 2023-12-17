using CNPM_BE.Data;
using CNPM_BE.DTOs;
using CNPM_BE.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace CNPM_BE.Services
{
    public class UserService
    {
        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        private readonly CNPMDbContext _context;
        private readonly IConfiguration _config;
        private readonly FeeService _feeService;
        public UserService(CNPMDbContext context, IConfiguration config, FeeService feeService)
        {
            _context = context;
            _config = config;
            _feeService = feeService;
        }
        public async Task<bool> VerifyPassword(AppUser user, string password)
        {
            string hashedPassword = await HashPassword(password, user.PasswordSalt);
            return hashedPassword == user.PasswordHash;
        }
        public async Task<ApiResponseExpose<AppUser>> CreateNewUser(RegisterReq req)
        {
            var resp = new ApiResponseExpose<AppUser>();

            var count = await _context.AppUser.CountAsync();
            var exUser = await _context.AppUser.FirstOrDefaultAsync(u => (u.Username == req.Username));
            if(exUser != null)
            {
                resp.code = -1;
                resp.message = "Tài khoản đã tồn tại";
                return resp;
            }

            var newUser = new AppUser();
            newUser.Username = req.Username;
            newUser.Email = req.Email;
            newUser.Name = "";
            newUser.PhoneNumber = "";
            newUser.BankName = "";
            newUser.BankAccountNumber = "";
            newUser.FacebookLink = "";
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            newUser.PasswordSalt = salt;
            newUser.PasswordHash = await HashPassword(req.Password, salt);
            try
            {
                await _context.AppUser.AddAsync(newUser);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return null;
            }

            resp.code = 1;
            resp.message = "Đăng ký thành công";
            resp.entity = newUser;

            return resp;
        }
        public async Task<string> HashPassword(string password, byte[] salt)
        {
            var hash = new Rfc2898DeriveBytes(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm).GetBytes(keySize);

            return Convert.ToHexString(hash);
        }
        public async Task<string> CreateToken(AppUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Username),
                new Claim(ClaimTypes.Email,user.Email),
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<ApiResp> ChangePassword(AppUser user, ChangePasswordReq req)
        {
            var resp = new ApiResp();
            if((await VerifyPassword(user, req.OldPassword)) == false)
            {
                resp.code = -1;
                resp.message = "Sai mật khẩu";
                return resp;
            }
            user.PasswordHash = await HashPassword(req.NewPassword, user.PasswordSalt);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra!";
                return resp;
            }
            resp.code = 1;
            resp.message = "Đổi mật khẩu thành công";
            return resp;
        }
        public async Task<AppUser> GetUser(ClaimsPrincipal user)
        {
            var email = user.Claims.First(c => c.Type == "email").Value;
            var x = await GetUser(email);
            return x;
        }
        public async Task<AppUser> GetUser(string email)
        {
            var user = await _context.AppUser.FirstOrDefaultAsync(a => a.Email == email);
            return user;
        }
        public async Task<AppUser> GetUser()
        {
            var user = await _context.AppUser.FirstOrDefaultAsync();
            return user;
        }
        public async Task<ApiResponseExpose<AccountUpdateReq>> UpdateInformation(AppUser user, AccountUpdateReq req)
        {
            var resp = new ApiResponseExpose<AccountUpdateReq>();

            user.Name = req.Name;
            user.PhoneNumber = req.PhoneNumber;
            user.BankName = req.BankName;
            user.BankAccountNumber = req.BankAccountNumber;
            user.FacebookLink = req.FacebookLink;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình cập nhật thông tin";
                return resp;
            }

            resp.code = 1;
            resp.message = "Cập nhật thông tin thành công";
            resp.entity = req;

            return resp;
        }
    }
}
