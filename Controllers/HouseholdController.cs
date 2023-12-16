using CNPM_BE.DTOs;
using CNPM_BE.Models;
using CNPM_BE.Services;
using Microsoft.AspNetCore.Mvc;

namespace CNPM_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class HouseholdController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ManagementService _managementService;
        public HouseholdController(UserService userService, ManagementService managementService)
        {
            _userService = userService;
            _managementService = managementService;
        }

        [HttpGet]
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
    }
}
