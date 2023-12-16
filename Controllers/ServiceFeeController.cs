using CNPM_BE.Models;
using CNPM_BE.Services;
using Microsoft.AspNetCore.Mvc;

namespace CNPM_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class ServiceFeeController : ControllerBase
    {
        private readonly FeeService _feeService;
        private readonly UserService _userService;
        public ServiceFeeController(FeeService feeService, UserService userService)
        {
            _feeService = feeService;
            _userService = userService;
        }
        [HttpPut]
        public async Task<ActionResult> UpdateInformation(ServiceFee req)
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _feeService.UpdateServiceFeeInformation(user, req);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
    }
}
