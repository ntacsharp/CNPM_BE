using CNPM_BE.Data;
using CNPM_BE.DTOs;
using CNPM_BE.Services;
using Microsoft.AspNetCore.Mvc;

namespace CNPM_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]/")]
    public class DonationController : ControllerBase
    {
        private readonly DonationService _donationService;
        private readonly UserService _userService;
        public DonationController(DonationService donationService, UserService userService)
        {
            _donationService = donationService;
            _userService = userService;
        }
        [HttpPost]
        [ActionName("CreateDonationFund")]
        public async Task<ActionResult> CreateDonationFund(DonationFundCreateReq req)
        {
            var user = await _userService.GetUser(User); 
            if (user == null)
            {
                return Unauthorized();
            }
            var resp = await _donationService.CreateDonationFund(user, req);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
        [HttpPost]
        [ActionName("CreateDonation")]
        public async Task<ActionResult> CreateDonation(DonationCreateReq req)
        {
            var user = await _userService.GetUser(User);
            if (user == null)
            {
                return Unauthorized();
            }
            var resp = await _donationService.CreateDonation(user, req);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }

    }
}
