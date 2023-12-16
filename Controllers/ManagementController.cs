using CNPM_BE.Data;
using CNPM_BE.DTOs;
using CNPM_BE.Models;
using CNPM_BE.Services;
using Microsoft.AspNetCore.Mvc;

namespace CNPM_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
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
        public async Task<ActionResult> AddResident(Resident req)
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

        [HttpDelete("{req}")]
        public async Task<ActionResult> RemoveResident(int req)
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

        [HttpPut]
        public async Task<ActionResult> UpdateInformation(Resident req)
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

        [HttpGet("GetHouseholdList")]
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

        [HttpGet("GetOptionList")]
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
