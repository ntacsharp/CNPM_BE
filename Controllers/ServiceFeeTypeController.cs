using CNPM_BE.DTOs;
using CNPM_BE.Models;
using CNPM_BE.Services;
using Microsoft.AspNetCore.Mvc;

namespace CNPM_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class ServiceFeeTypeController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly FeeService _feeService;
        public ServiceFeeTypeController(UserService userService, FeeService feeService)
        {
            _userService = userService;
            _feeService = feeService;
        }

        [HttpPost]
        public async Task<ActionResult> AddServiceFeeType(ServiceFeeTypeCreateReq req)
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _feeService.AddServiceFeeType(user, req);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
        [HttpGet]
        public async Task<ActionResult> GetServiceFeeTypeList()
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _feeService.GetServiceFeeTypeList(user);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateInformation(ServiceFeeType req)
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _feeService.UpdateServiceFeeTypeInformation(user, req);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }

        [HttpDelete("{req}")]
        public async Task<ActionResult> RemoveServiceFeeType([FromRoute] int req)
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _feeService.RemoveServiceFeeType(user, req);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
    }
}
