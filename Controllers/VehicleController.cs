using CNPM_BE.Data;
using CNPM_BE.DTOs;
using CNPM_BE.Models;
using CNPM_BE.Services;
using Microsoft.AspNetCore.Mvc;

namespace CNPM_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class VehicleController : ControllerBase
    {
        private readonly UserService _userService;
        private VehicleService _vehicleService;
        public VehicleController(UserService userService, VehicleService vehicleService)
        {
            _userService = userService;
            _vehicleService = vehicleService;
        }
        [HttpPost]
        public async Task<ActionResult> AddVehicle(VehicleCreateReq req)
        {
            var userName = await _userService.GetUsernameFromToken(Request);
            var user = await _userService.GetUser(userName);
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _vehicleService.AddVehicle(user, req);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
        [HttpGet]
        public async Task<ActionResult> GetVehicleList()
        {
            var userName = await _userService.GetUsernameFromToken(Request);
            var user = await _userService.GetUser(userName);
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _vehicleService.GetVehicleList(user);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateInformation(Vehicle req)
        {
            var userName = await _userService.GetUsernameFromToken(Request);
            var user = await _userService.GetUser(userName);
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _vehicleService.UpdateVehicleInformation(user, req);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }

        [HttpDelete("{req}")]
        public async Task<ActionResult> RemoveVehicle([FromRoute] int req)
        {
            var userName = await _userService.GetUsernameFromToken(Request);
            var user = await _userService.GetUser(userName);
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _vehicleService.RemoveVehicle(user, req);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
    }
}
