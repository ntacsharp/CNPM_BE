﻿//using CNPM_BE.Data;
//using CNPM_BE.DTOs;
//using CNPM_BE.Services;
//using Microsoft.AspNetCore.Mvc;

//namespace CNPM_BE.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]/[action]/")]
//    public class DonationController : ControllerBase
//    {
//        private readonly DonationService _donationService;
//        private readonly UserService _userService;
//        public DonationController(DonationService donationService, UserService userService)
//        {
//            _donationService = donationService;
//            _userService = userService;
//        }
//        //[HttpPost]
//        //[ActionName("CreateDonationFund")]
//        //public async Task<ActionResult> CreateDonationFund(DonationFundCreateReq req)
//        //{
//        //    var user = await _userService.GetUser(User);
//        //    if (user == null)
//        //    {
//        //        return Unauthorized();
//        //    }
//        //    var resp = await _donationService.CreateDonationFund(user, req);
//        //    if (resp == null) return BadRequest();
//        //    return Ok(resp);
//        //}
//        [HttpPost]
//        [ActionName("AddDonation")]
//        public async Task<ActionResult> AddDonation(DonationCreateReq req)
//        {
//            var user = await _userService.GetUser(User);
//            if (user == null)
//            {
//                return Unauthorized();
//            }
//            var resp = await _donationService.AddDonation(user, req);
//            if (resp == null) return BadRequest();
//            return Ok(resp);
//        }
//        [HttpPost]
//        [ActionName("GetHouseholdDonation")]
//        public async Task<ActionResult> GetHouseholdDonation(Payload payload)
//        {
//            var user = await _userService.GetUser(User);
//            if (user == null)
//            {
//                return Unauthorized();
//            }
//            var resp = await _donationService.GetHouseholdDonation(user, payload);
//            if (resp == null) return BadRequest();
//            return Ok(resp);
//        }
//        [HttpPost]
//        [ActionName("GetDonationList")]
//        public async Task<ActionResult> GetDonationList(Payload payload)
//        {
//            var user = await _userService.GetUser(User);
//            if (user == null)
//            {
//                return Unauthorized();
//            }
//            var resp = await _donationService.GetDonationList(user, payload);
//            if (resp == null) return BadRequest();
//            return Ok(resp);
//        }
//        [HttpGet]
//        [ActionName("GetDonationFund")]
//        public async Task<ActionResult> GetDonationFund()
//        {
//            var user = await _userService.GetUser(User);
//            if (user == null)
//            {
//                return Unauthorized();
//            }
//            var resp = await _donationService.GetDonationFund(user);
//            if (resp == null) return BadRequest();
//            return Ok(resp);
//        }
//    }
//}
