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
        public async Task<ApiResponseExpose<ContributionResp>> AddContribution(AppUser user, ContributionCreateReq req)
        {
            var resp = new ApiResponseExpose<ContributionResp>();
            var contribution = new Contribution();
            contribution.ResidentId = req.ResidentId;
            contribution.ForThePoor = req.ForThePoor;
            contribution.ForVNSeasAndIslands = req.ForVNSeasAndIslands;
            contribution.DGFestival = req.DGFestival;
            contribution.ResidentialGroup = req.ResidentialGroup;
            contribution.ForChildren = req.ForChildren;
            contribution.Charity = req.Charity;
            contribution.Gratitude = req.Gratitude;
            contribution.StudyPromotion = req.StudyPromotion;
            contribution.ForTheElderly = req.ForTheElderly;
            contribution.CreatedTime = await _timeConverterService.ConvertToUTCTime(req.CreatedTime);
            contribution.CreatorId = user.Id;
            try
            {
                await _context.Contribution.AddAsync(contribution);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình thêm bản đóng góp";
                return resp;
            }
            resp.code = 1;
            resp.message = "Thêm bản đóng góp thành công";
            var resident = await _context.Resident.FirstOrDefaultAsync(r => r.Id == contribution.ResidentId);
            var apartment = await _context.Apartment.FirstOrDefaultAsync(a => a.Id == resident.ApartmentId);
            var cResp = new ContributionResp(contribution, resident, apartment);
            resp.entity = cResp;
            return resp;
    }
        public async Task<ApiResponseExpose<ContributionResp>> UpdateInformation(AppUser user, Contribution req)
        {
            var resp = new ApiResponseExpose<ContributionResp>();
            var contribution = await _context.Contribution.FirstOrDefaultAsync(c => c.Id == req.Id && c.Status == ContributionStatus.Active);
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
            contribution.CreatedTime = await _timeConverterService.ConvertToUTCTime(req.CreatedTime);
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
            var resident = await _context.Resident.FirstOrDefaultAsync(r => r.Id == contribution.ResidentId);
            var apartment = await _context.Apartment.FirstOrDefaultAsync(a => a.Id == resident.ApartmentId);
            var cResp = new ContributionResp(contribution, resident, apartment);
            resp.entity = cResp;
            return resp;
        }
        public async Task<ApiResponseExpose<ContributionResp>> RemoveContribution(AppUser user, int req)
        {
            var resp = new ApiResponseExpose<ContributionResp>();
            var contribution = await _context.Contribution.FirstOrDefaultAsync(c => c.Id == req && c.Status == ContributionStatus.Active);
            if(contribution == null)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình tìm kiếm đóng góp";
                return resp;
            }
            contribution.Status = ContributionStatus.Deleted;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình xóa bản đóng góp";
                return resp;
            }
            resp.code = 1;
            resp.message = "Xóa bản đóng góp thành công";
            var resident = await _context.Resident.FirstOrDefaultAsync(r => r.Id == contribution.ResidentId);
            var apartment = await _context.Apartment.FirstOrDefaultAsync(a => a.Id == resident.ApartmentId);
            var cResp = new ContributionResp(contribution, resident, apartment);
            resp.entity = cResp;
            return resp;
        }

        public async Task<List<ContributionResp>> GetContributionList(AppUser user)
        {
            var list = await _context.Contribution.Where(c => c.Status == ContributionStatus.Active).ToListAsync();
            var resp = new List<ContributionResp>();
            foreach (var contribution in list)
            {
                var resident = await _context.Resident.FirstOrDefaultAsync(r => r.Id == contribution.ResidentId);
                var apartment = await _context.Apartment.FirstOrDefaultAsync(a => a.Id == resident.ApartmentId);
                var cr = new ContributionResp(contribution, resident, apartment);
                resp.Add(cr);
            }
            return resp;
        }
    }
}
