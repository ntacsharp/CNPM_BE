//using CNPM_BE.Data;
//using CNPM_BE.DTOs;
//using CNPM_BE.Models;
//using CNPM_BE.Services;
//using Microsoft.AspNetCore.Mvc;

//namespace CNPM_BE.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]/[action]/")]
//    public class FeeController : ControllerBase
//    {
//        private readonly FeeService _feeService;
//        private readonly UserService _userService;
//        public FeeController(FeeService feeService, UserService userService)
//        {
//            _feeService = feeService;
//            _userService = userService;
//        }
//        [HttpPost]
//        [ActionName("CreateFeePayment")]
//        public async Task<ActionResult> CreateFeePayment(FeePaymentCreateReq req)
//        {
//            var user = await _userService.GetUser(User);
//            if (user == null)
//            {
//                return Unauthorized();
//            }
//            var resp = await _feeService.CreateFeePayment(user, req);
//            if (resp == null) return BadRequest();
//            return Ok(resp);
//        }
//        [HttpPost]
//        [ActionName("GetHouseholdFee")]
//        public async Task<ActionResult> GetHouseholdFee(Payload payload)
//        {
//            var user = await _userService.GetUser(User);
//            if (user == null)
//            {
//                return Unauthorized();
//            }
//            var resp = await _feeService.GetHouseholdFee(user, payload);
//            if (resp == null) return BadRequest();
//            return Ok(resp);
//        }
//    }
//}
