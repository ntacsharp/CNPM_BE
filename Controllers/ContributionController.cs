using CNPM_BE.Data;
using CNPM_BE.DTOs;
using CNPM_BE.Services;
using Microsoft.AspNetCore.Mvc;

namespace CNPM_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]/")]
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
        [ActionName("UpdateContributionInformation")]
        public async Task<ActionResult> UpdateContributionInformation(ContributionUpdateReq req)
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                return NotFound();
            }
            var resp = await _contributionService.UpdateContributionInformation(user, req);
            if (resp == null) return BadRequest();
            return Ok(resp);
        }
        [HttpGet]
        [ActionName("GetContributionList")]
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
    }
}
