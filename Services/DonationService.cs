using CNPM_BE.Data;
using CNPM_BE.DTOs;
using CNPM_BE.Models;
using Microsoft.EntityFrameworkCore;

namespace CNPM_BE.Services
{
    public class DonationService
    {
        private readonly CNPMDbContext _context;
        public DonationService(CNPMDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResp> CreateDonationFund(AppUser user, DonationFundCreateReq req)
        {
            var resp = new ApiResp();
            var df = new DonationFund();
            df.Name = req.Name;
            df.CreatedTime = DateTime.Now;
            df.CreatorId = user.Id;
            df.ExpirationTime = req.ExpirationTime;
            df.Description = req.Description;
            df.SmallestAmount = req.SmallestAmount;
            df.ReceivedAmount = 0;
            try
            {
                await _context.DonationFund.AddAsync(df);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình tạo quỹ";
                return resp;
            }
            df = await _context.DonationFund.OrderBy(d => d.Id).LastOrDefaultAsync(d => d.Name == req.Name);
            var hh = await _context.Household.Where(h => (h.ManagerId == user.Id && h.IsActive)).ToListAsync();
            foreach(var h in hh)
            {
                var hd = new HouseholdDonation();
                hd.DonatorId = h.Id;
                hd.FundId = df.Id;
                hd.Amount = 0;
                try
                {
                    await _context.HouseholdDonation.AddAsync(hd);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    resp.code = -1;
                    resp.message = "Đã có lỗi xảy ra trong quá trình tạo quỹ";
                    return resp;
                }
            }
            resp.code = 1;
            resp.message = "Tạo quỹ thành công";
            return resp;
        }
        public async Task<ApiResp> CreateDonation(AppUser user, DonationCreateReq req)
        {
            var resp = new ApiResp();
            var df = await _context.DonationFund.FirstOrDefaultAsync(d => d.Id == req.FundId);
            var hd = await _context.HouseholdDonation.FirstOrDefaultAsync(d => (d.FundId == req.FundId && d.DonatorId == req.DonatorId));
            if (df == null) return null;
            var d = new Donation();
            d.CreatedTime = DateTime.Now;
            d.Amount = req.Amount;
            d.FundId = req.FundId;
            d.DonatorId = req.DonatorId;
            df.ReceivedAmount += req.Amount;
            hd.Amount += req.Amount;
            try
            {
                await _context.Donation.AddAsync(d);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình thêm bản thu";
                return resp;
            }
            resp.code = 1;
            resp.message = "Tạo bản thu thành công";
            return resp;
        }
        
    }
}
