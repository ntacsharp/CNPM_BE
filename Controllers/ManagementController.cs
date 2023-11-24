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
        [ActionName("CreateHousehold")]
        public async Task<ActionResult> CreateHousehold(HouseholdCreateReq req)
        {
            var user = await _userService.GetUser(User);
            if (user == null)
            {
                return Unauthorized();
            }
            var resp = await _managementService.CreateHousehold(user, req);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
        [HttpPost]
        [ActionName("DeactivateHousehold")]
        public async Task<ActionResult> DeactivateHousehold(int householdId)
        {
            var user = await _userService.GetUser(User);
            if (user == null)
            {
                return Unauthorized();
            }
            var resp = await _managementService.DeactivateHousehold(user, householdId);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
        [HttpPost]
        [ActionName("AddMember")]
        public async Task<ActionResult> AddMember(HouseholdAddMemberReq req)
        {
            var user = await _userService.GetUser(User);
            if (user == null)
            {
                return Unauthorized();
            }
            var resp = await _managementService.AddMember(user, req);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
        [HttpPost]
        [ActionName("RemoveMember")]
        public async Task<ActionResult> RemoveMember(HouseholdRemoveMemberReq req)
        {
            var user = await _userService.GetUser(User);
            if (user == null)
            {
                return Unauthorized();
            }
            var resp = await _managementService.RemoveMember(user, req);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
    }
}
