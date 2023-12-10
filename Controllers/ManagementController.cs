using CNPM_BE.Data;
using CNPM_BE.DTOs;
using CNPM_BE.Models;
using CNPM_BE.Services;
using Microsoft.AspNetCore.Mvc;

namespace CNPM_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]/")]
    public class ManagementController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ManagementService _managementService;
        public ManagementController(UserService userService, ManagementService managementService)
        {
            _userService = userService;
            _managementService = managementService;
        }
        [HttpPost]
        [ActionName("AddResident")]
        public async Task<ActionResult> AddResident(ResidentCreateReq req)
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _managementService.AddResident(user, req);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
        [HttpPost]
        [ActionName("RemoveResident")]
        public async Task<ActionResult> RemoveResident(ResidentDeleteReq req)
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _managementService.RemoveResident(user, req);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
        [HttpPost]
        [ActionName("UpdateInformation")]
        public async Task<ActionResult> UpdateInformation(ResidentUpdateReq req)
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _managementService.UpdateInformation(user, req);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
        [HttpGet]
        [ActionName("GetResidentList")]
        public async Task<ActionResult> GetResidentList()
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _managementService.GetResidentList(user);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
        [HttpGet]
        [ActionName("GetHouseholdList")]
        public async Task<ActionResult> GetHouseholdList()
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _managementService.GetHouseholdList(user);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
        [HttpGet]
        [ActionName("GetOptionList")]
        public async Task<ActionResult> GetOptionList()
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _managementService.GetOptionList(user);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
    }
}
