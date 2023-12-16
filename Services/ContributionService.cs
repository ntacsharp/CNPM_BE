using CNPM_BE.Data;
using CNPM_BE.DTOs;
using CNPM_BE.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace CNPM_BE.Services
{
    public class ContributionService
    {
        private readonly CNPMDbContext _context;
        private readonly TimeConverterService _timeConverterService;
        public ContributionService(CNPMDbContext context, TimeConverterService timeConverterService)
        {
            _context = context;
            _timeConverterService = timeConverterService;
        }

        public async Task<ApiResponseExpose<Contribution>> UpdateContributionInformation(AppUser user, ContributionUpdateReq req)
        {
            var resp = new ApiResponseExpose<Contribution>();

            var contribution = await _context.Contribution.FirstOrDefaultAsync(c => c.CreatorId == user.Id && c.Id == req.Id && c.Status == ContributionStatus.Active);
            if (contribution == null)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình tìm kiếm đóng góp";
                return resp;
            }

            contribution.ForThePoor = req.ForThePoor;
            contribution.ForVNSeasAndIslands = req.ForVNSeasAndIslands;
            contribution.DGFestival = req.DGFestival;
            contribution.ResidentialGroup = req.ResidentialGroup;
            contribution.ForChildren = req.ForChildren;
            contribution.Charity = req.Charity;
            contribution.Gratitude = req.Gratitude;
            contribution.StudyPromotion = req.StudyPromotion;
            contribution.ForTheElderly = req.ForTheElderly;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình cập nhật thông tin đóng góp";
                return resp;
            }

            resp.code = 1;
            resp.message = "Cập nhật thông tin đóng góp thành công";
            resp.entity = contribution;

            return resp;
        }

        public async Task<List<ContributionResp>> GetContributionList(AppUser user)
        {
            var list = await _context.Contribution.Where(c => c.CreatorId == user.Id && c.Status == ContributionStatus.Active).ToListAsync();
            var resp = new List<ContributionResp>();
            foreach (var contribution in list)
            {
                var apartment = await _context.Apartment.FirstOrDefaultAsync(a => a.Id == contribution.ApartmentId);
                var owner = await _context.Resident.FirstOrDefaultAsync(r => r.Id == apartment.OwnerId);
                var cr = new ContributionResp(contribution, owner, apartment);
                resp.Add(cr);
            }
            return resp;
        }
    }
}
