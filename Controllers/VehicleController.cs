using CNPM_BE.Data;
using CNPM_BE.DTOs;
using CNPM_BE.Models;
using CNPM_BE.Services;
using Microsoft.AspNetCore.Mvc;

namespace CNPM_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]/")]
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
        [ActionName("AddVehicle")]
        public async Task<ActionResult> AddVehicle(VehicleCreateReq req)
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _vehicleService.AddVehicle(user, req);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
        [HttpPost]
        [ActionName("RemoveVehicle")]
        public async Task<ActionResult> RemoveVehicle(VehicleDeleteReq req)
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _vehicleService.RemoveVehicle(user, req);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
        [HttpPost]
        [ActionName("UpdateVehicleInformation")]
        public async Task<ActionResult> UpdateVehicleInformation(VehicleUpdateReq req)
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _vehicleService.UpdateVehicleInformation(user, req);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
        [HttpGet]
        [ActionName("GetVehicleList")]
        public async Task<ActionResult> GetVehicleList()
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _vehicleService.GetVehicleList(user);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
        [HttpPost]
        [ActionName("AddVehicleType")]
        public async Task<ActionResult> AddVehicleType(VehicleTypeCreateReq req)
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
        [HttpPost]
        [ActionName("RemoveVehicleType")]
        public async Task<ActionResult> RemoveVehicleType(VehicleTypeDeleteReq req)
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
        [HttpPost]
        [ActionName("UpdateVehicleTypeInformation")]
        public async Task<ActionResult> UpdateVehicleTypeInformation(VehicleTypeUpdateReq req)
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
        [HttpGet]
        [ActionName("GetVehicleTypeList")]
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
    }
}
