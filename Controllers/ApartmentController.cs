using CNPM_BE.Data;
using CNPM_BE.DTOs;
using CNPM_BE.Services;
using Microsoft.AspNetCore.Mvc;

namespace CNPM_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]/")]
    public class ApartmentController : ControllerBase
    {
        private readonly CNPMDbContext _context;
        private readonly UserService _userService;
        private readonly ApartmentService _apartmentService;
        public ApartmentController(CNPMDbContext context, UserService userService, ApartmentService apartmentService)
        {
            _context = context;
            _userService = userService;
            _apartmentService = apartmentService;
        }
        [HttpPost]
        [ActionName("AddApartment")]
        public async Task<ActionResult> AddApartment(ApartmentCreateReq req)
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _apartmentService.AddApartment(user, req);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
        [HttpGet]
        [ActionName("GetApartmentList")]
        public async Task<ActionResult> GetApartmentList()
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _apartmentService.GetApartmentList(user);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
        [HttpPost]
        [ActionName("UpdateInformation")]
        public async Task<ActionResult> UpdateInformation(ApartmentUpdateReq req)
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _apartmentService.UpdateInformation(user, req);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
    }
}
