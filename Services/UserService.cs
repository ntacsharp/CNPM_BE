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
            newUser.ZaloLink = "";
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
            newUser = await _context.AppUser.OrderBy(u => u.Id).LastOrDefaultAsync();
            var t1 = new ServiceFeeType();
            t1.CreatorId = newUser.Id;
            t1.ServiceFeeTypeCode = "ST001";
            t1.Name = "Tiền điện";
            t1.PricePerUnit = 0;
            t1.MeasuringUnit = MeasuringUnit.Number;
            t1.Status = ServiceFeeTypeStatus.Active;
            t1.IsSystem = true;
            var t2 = new ServiceFeeType();
            t2.CreatorId = newUser.Id;
            t2.ServiceFeeTypeCode = "ST002";
            t2.Name = "Tiền nước";
            t2.PricePerUnit = 0;
            t2.MeasuringUnit = MeasuringUnit.M3;
            t2.Status = ServiceFeeTypeStatus.Active;
            t2.IsSystem = true;
            var t3 = new ServiceFeeType();
            t3.CreatorId = newUser.Id;
            t3.ServiceFeeTypeCode = "ST003";
            t3.Name = "Phí dịch vụ chung cư";
            t3.PricePerUnit = 0;
            t3.MeasuringUnit = MeasuringUnit.M2;
            t3.Status = ServiceFeeTypeStatus.Active;
            t3.IsSystem = true;
            var t4 = new ServiceFeeType();
            t4.CreatorId = newUser.Id;
            t4.ServiceFeeTypeCode = "ST004";
            t4.Name = "Phí quản lý chung cư";
            t4.PricePerUnit = 7000;
            t4.MeasuringUnit = MeasuringUnit.M2;
            t4.Status = ServiceFeeTypeStatus.Active;
            t4.IsSystem = true;
            try
            {
                await _context.ServiceFeeType.AddAsync(t1);
                await _context.ServiceFeeType.AddAsync(t2);
                await _context.ServiceFeeType.AddAsync(t3);
                await _context.ServiceFeeType.AddAsync(t4);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
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
                new Claim("username",user.Username),
                new Claim("email",user.Email),
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
        //public async Task<AppUser> GetUser(ClaimsPrincipal user)
        //{
        //    var tmp = user;
        //    var username = user.Claims.First(c => c.Type == "username").Value;
        //    if (username == null)
        //    {
                
        //    }
        //    Console.Write(tmp);
        //    var x = await GetUser(username);
        //    return x;
        //}
        public async Task<AppUser> GetUser(string username)
        {
            var user = await _context.AppUser.FirstOrDefaultAsync(a => a.Username == username);
            return user;
        }
        //public async Task<AppUser> GetUser()
        //{
        //    var user = await _context.AppUser.FirstOrDefaultAsync();
        //    return user;
        //}
        public async Task<ApiResponseExpose<AccountUpdateReq>> UpdateInformation(AppUser user, AccountUpdateReq req)
        {
            var resp = new ApiResponseExpose<AccountUpdateReq>();

            user.Name = req.Name;
            user.PhoneNumber = req.PhoneNumber;
            user.BankName = req.BankName;
            user.BankAccountNumber = req.BankAccountNumber;
            user.FacebookLink = req.FacebookLink;
            user.ZaloLink = req.ZaloLink;
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
        public async Task<string> GetUsernameFromToken(HttpRequest request)
        {
            try
            {
                var token = request.Headers["Authorization"];
                var jwtString = token.ToString().Substring(7);
                var decoded = new JwtSecurityToken(jwtEncodedString: jwtString);
                var username = decoded.Claims.First(c => c.Type == "username").Value;
                return username;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
