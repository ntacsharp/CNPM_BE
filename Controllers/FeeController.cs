using CNPM_BE.Data;
using CNPM_BE.DTOs;
using CNPM_BE.Models;
using CNPM_BE.Services;
using Microsoft.AspNetCore.Mvc;

namespace CNPM_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class FeeController : ControllerBase
    {
        private readonly FeeService _feeService;
        private readonly UserService _userService;
        public FeeController(FeeService feeService, UserService userService)
        {
            _feeService = feeService;
            _userService = userService;
        }
        [HttpPost]
        public async Task<ActionResult> AddFee()
        {
            var userName = await _userService.GetUsernameFromToken(Request);
            var user = await _userService.GetUser(userName);
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _feeService.AddFee(user);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
        [HttpGet]
        public async Task<ActionResult> GetFeeList()
        {
            var userName = await _userService.GetUsernameFromToken(Request);
            var user = await _userService.GetUser(userName);
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _feeService.GetFeeList(user);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateInformation(Fee req)
        {
            var userName = await _userService.GetUsernameFromToken(Request);
            var user = await _userService.GetUser(userName);
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _feeService.UpdateFeeInformation(user, req);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }

        [HttpDelete("{req}")]
        public async Task<ActionResult> RemoveFee([FromRoute] int req)
        {
            var userName = await _userService.GetUsernameFromToken(Request);
            var user = await _userService.GetUser(userName);
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _feeService.RemoveFee(user, req);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
    }
}
