﻿using CNPM_BE.Data;
using CNPM_BE.DTOs;
using CNPM_BE.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

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
        public async Task<ActionResult> Login(LoginReq req)
        {
            var user = await _context.AppUser.FirstOrDefaultAsync(u => u.Username == req.UsernameOrEmail || u.Email == req.UsernameOrEmail);
            if (user == null)
            {
                return Ok(new LoginResp { code = -1, message = "Người dùng không tồn tại!" });
            }
            var isCorrect = await _userService.VerifyPassword(user, req.Password);
            if (!isCorrect)
            {
                return Ok(new LoginResp { code = -1, message = "Sai mật khẩu!" });
            }
            string token = await _userService.CreateToken(user);
            return Ok(new LoginResp { Token = token, entity = user });
        }
        [HttpPost]
        [ActionName("Register")]
        public async Task<ActionResult> Register(RegisterReq req)
        {
            var resp = await _userService.CreateNewUser(req);
            if(resp == null) return BadRequest();
            return Ok(resp);
        }
        [HttpPost]
        [ActionName("ChangePassword")]
        public async Task<ActionResult> ChangePassword(ChangePasswordReq req)
        {
            var userName = await _userService.GetUsernameFromToken(Request);
            var user = await _userService.GetUser(userName);
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _userService.ChangePassword(user, req);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
        [HttpPost]
        [ActionName("UpdateInformation")]
        public async Task<ActionResult> UpdateInformation(AccountUpdateReq req)
        {
            var userName = await _userService.GetUsernameFromToken(Request);
            var user = await _userService.GetUser(userName);
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _userService.UpdateInformation(user, req);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
    }
}
