﻿using CNPM_BE.Data;
using CNPM_BE.DTOs;
using CNPM_BE.Models;
using CNPM_BE.Services;
using Microsoft.AspNetCore.Mvc;

namespace CNPM_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class ApartmentController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ApartmentService _apartmentService;
        public ApartmentController(UserService userService, ApartmentService apartmentService)
        {
            _userService = userService;
            _apartmentService = apartmentService;
        }

        [HttpPost]
        public async Task<ActionResult> AddApartment(ApartmentCreateReq req)
        {
            var userName = await _userService.GetUsernameFromToken(Request);
            var user = await _userService.GetUser(userName);
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _apartmentService.AddApartment(user, req);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
        [HttpGet]
        public async Task<ActionResult> GetApartmentList()
        {
            var userName = await _userService.GetUsernameFromToken(Request);
            var user = await _userService.GetUser(userName);
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _apartmentService.GetApartmentList(user);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateInformation(Apartment req)
        {
            var userName = await _userService.GetUsernameFromToken(Request);
            var user = await _userService.GetUser(userName);
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _apartmentService.UpdateInformation(user, req);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }

        [HttpDelete("{req}")]
        public async Task<ActionResult> RemoveApartment([FromRoute] int req)
        {
            var userName = await _userService.GetUsernameFromToken(Request);
            var user = await _userService.GetUser(userName);
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _apartmentService.RemoveApartment(user, req);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
    }
}
