using CNPM_BE.Data;
using CNPM_BE.DTOs;
using CNPM_BE.Models;
using CNPM_BE.Services;
using Microsoft.AspNetCore.Mvc;

namespace CNPM_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]/")]
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
        [ActionName("AddServiceFeeType")]
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
        [HttpPost]
        [ActionName("RemoveServiceFeeType")]
        public async Task<ActionResult> RemoveServiceFeeType(ServiceFeeTypeDeleteReq req)
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
        [HttpPost]
        [ActionName("UpdateServiceFeeTypeInformation")]
        public async Task<ActionResult> UpdateServiceFeeTypeInformation(ServiceFeeTypeUpdateReq req)
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
        [HttpGet]
        [ActionName("GetServiceTypeList")]
        public async Task<ActionResult> GetServiceTypeList()
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _feeService.GetServiceTypeList(user);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
        [HttpPost]
        [ActionName("AddFee")]
        public async Task<ActionResult> AddFee()
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _feeService.AddFee(user);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
        [HttpPost]
        [ActionName("RemoveFee")]
        public async Task<ActionResult> RemoveFee(FeeDeleteReq req)
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _feeService.RemoveFee(user, req);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
        [HttpPost]
        [ActionName("UpdateFeeInformation")]
        public async Task<ActionResult> UpdateFeeInformation(FeeUpdateReq req)
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _feeService.UpdateFeeInformation(user, req);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
        [HttpGet]
        [ActionName("GetFeeResp")]
        public async Task<ActionResult> GetFeeResp()
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _feeService.GetFeeResp(user);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
        [HttpPost]
        [ActionName("AddFeePayment")]
        public async Task<ActionResult> AddFeePayment(FeePaymentCreateReq req)
        {
            var user = await _userService.GetUser();
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
