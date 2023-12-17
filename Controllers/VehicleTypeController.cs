using CNPM_BE.DTOs;
using CNPM_BE.Models;
using CNPM_BE.Services;
using Microsoft.AspNetCore.Mvc;

namespace CNPM_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class VehicleTypeController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly VehicleService _vehicleService;
        public VehicleTypeController(UserService userService, VehicleService vehicleService)
        {
            _userService = userService;
            _vehicleService = vehicleService;
        }
        [HttpPost]
        public async Task<ActionResult> AddVehicletype(VehicleTypeCreateReq req)
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _vehicleService.AddVehicleType(user, req);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
        [HttpGet]
        public async Task<ActionResult> GetVehicleTypeList()
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _vehicleService.GetVehicleTypeList(user);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateInformation(VehicleType req)
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _vehicleService.UpdateVehicleTypeInformation(user, req);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }

        [HttpDelete("{req}")]
        public async Task<ActionResult> RemoveVehicleType([FromRoute] int req)
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _vehicleService.RemoveVehicleType(user, req);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
    }
}
