using CNPM_BE.Data;
using CNPM_BE.DTOs;
using CNPM_BE.Models;
using CNPM_BE.Services;
using Microsoft.AspNetCore.Mvc;

namespace CNPM_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class ContributionController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ContributionService _contributionService;
        public ContributionController( ContributionService contributionService, UserService userService)
        {
            _contributionService = contributionService;
            _userService = userService;
        }
        [HttpPost]
        public async Task<ActionResult> AddContribution(ContributionCreateReq req)
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _contributionService.AddContribution(user, req);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
        [HttpGet]
        public async Task<ActionResult> GetContributionList()
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _contributionService.GetContributionList(user);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateInformation(Contribution req)
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _contributionService.UpdateInformation(user, req);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }

        [HttpDelete("{req}")]
        public async Task<ActionResult> RemoveContribution([FromRoute] int req)
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _contributionService.RemoveContribution(user, req);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
    }
}
