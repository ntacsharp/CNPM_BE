using CNPM_BE.Data;
using CNPM_BE.DTOs;
using CNPM_BE.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CNPM_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]/")]
    public class UserController : ControllerBase
    {
        private readonly CNPMDbContext _context;
        private readonly UserService _userService;
        public UserController(CNPMDbContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
        }
        [HttpPost]
        [ActionName("Login")]
        public async Task<IActionResult> Login(LoginReq req)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Username == req.UsernameOrEmail || u.Email == req.UsernameOrEmail);
            if (user == null)
            {
                return NotFound("User not found!");
            }
            var isCorrect = await _userService.VerifyPassword(user, req);
            if (!isCorrect)
            {
                return BadRequest("Wrong password!");
            }
            string token = await _userService.CreateToken(user);
            return Ok(new LoginResp { Token = token });
        }
        [HttpPost]
        [ActionName("Register")]
        public async Task<IActionResult> Register(RegisterReq req)
        {
            var resp = await _userService.CreateNewUser(req);
            if(resp == null) return BadRequest();
            return Ok(resp);
        }
    }
}
