using CNPM_BE.DTOs;
using CNPM_BE.Models;
using CNPM_BE.Services;
using Microsoft.AspNetCore.Mvc;

namespace CNPM_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class FeePaymentController : ControllerBase
    {
        private readonly FeeService _feeService;
        private readonly UserService _userService;
        public FeePaymentController(FeeService feeService, UserService userService)
        {
            _feeService = feeService;
            _userService = userService;
        }
        [HttpPost]
        public async Task<ActionResult> AddFeePayment(FeePaymentCreateReq req)
        {
            var userName = await _userService.GetUsernameFromToken(Request);
            var user = await _userService.GetUser(userName);
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _feeService.AddFeePayment(user, req);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
    }
}
