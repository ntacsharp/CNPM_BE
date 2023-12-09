using CNPM_BE.Data;
using CNPM_BE.DTOs;
using CNPM_BE.Models;
using Microsoft.EntityFrameworkCore;

namespace CNPM_BE.Services
{
    public class DonationService
    {
        private readonly CNPMDbContext _context;
        private readonly TimeConverterService _timeConverterService;
        public DonationService(CNPMDbContext context, TimeConverterService timeConverterService)
        {
            _context = context;
            _timeConverterService = timeConverterService;
        }
        public async Task<List<DonationResp>> GetDonationList(AppUser user, Payload payload)
        {
            var list = new List<DonationResp>();
            var x = await _context.Donation.Where(d => d.CreatorId == user.Id).Skip(payload.Skip * payload.Take).Take(payload.Take).ToListAsync();
            foreach (var d in x)
            {
                var dr = new DonationResp();
                var fund = await _context.DonationFund.FirstOrDefaultAsync(df => df.Id == d.FundId);
                var donator = await _context.Household.FirstOrDefaultAsync(h => h.Id == d.DonatorId);
                dr.Id = d.Id;
                dr.Fund = fund.Name;
                dr.Donator = donator.ApartmentCode;
                dr.Amount = d.Amount;
                dr.CreatedTime = d.CreatedTime.ToString();
                list.Add(dr);
            }
            return list;
        }

        //public async Task<ApiResp> CreateDonationFund(AppUser user, DonationFundCreateReq req)
        //{
        //    var resp = new ApiResp();
        //    var df = new DonationFund();
        //    df.Name = req.Name;
        //    df.CreatedTime = DateTime.Now;
        //    df.CreatorId = user.Id;
        //    df.ExpirationTime = req.ExpirationTime;
        //    df.Description = req.Description;
        //    df.SmallestAmount = req.SmallestAmount;
        //    df.ReceivedAmount = 0;
        //    try
        //    {
        //        await _context.DonationFund.AddAsync(df);
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (Exception)
        //    {
        //        resp.code = -1;
        //        resp.message = "Đã có lỗi xảy ra trong quá trình tạo quỹ";
        //        return resp;
        //    }
        //    df = await _context.DonationFund.OrderBy(d => d.Id).LastOrDefaultAsync(d => d.Name == req.Name);
        //    var hh = await _context.Household.Where(h => (h.ManagerId == user.Id && h.IsActive)).ToListAsync();
        //    foreach (var h in hh)
        //    {
        //        var hd = new HouseholdDonation();
        //        hd.DonatorId = h.Id;
        //        hd.FundId = df.Id;
        //        hd.Amount = 0;
        //        try
        //        {
        //            await _context.HouseholdDonation.AddAsync(hd);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (Exception)
        //        {
        //            resp.code = -1;
        //            resp.message = "Đã có lỗi xảy ra trong quá trình tạo quỹ";
        //            return resp;
        //        }
        //    }
        //    resp.code = 1;
        //    resp.message = "Tạo quỹ thành công";
        //    return resp;
        //}
        public async Task<ApiResp> AddDonation(AppUser user, DonationCreateReq req)
        {
            var resp = new ApiResp();
            var df = await _context.DonationFund.FirstOrDefaultAsync(d => d.Id == req.FundId);
            var hd = await _context.HouseholdDonation.FirstOrDefaultAsync(d => (d.FundId == req.FundId && d.DonatorId == req.DonatorId));
            if (df == null) return null;
            var d = new Donation();
            d.CreatedTime = await _timeConverterService.ConvertToUTCTime(DateTime.Now);
            d.Amount = req.Amount;
            d.CreatorId = user.Id;
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
        public async Task<List<HouseholdDonation>> GetHouseholdDonation(AppUser user, Payload payload)
        {
            var list = await _context.HouseholdDonation.Where(h => h.CreatorId == user.Id).Skip(payload.Skip * payload.Take).Take(payload.Take).ToListAsync();
            return list;
        }
        public async Task<List<DonationFund>> GetDonationFund(AppUser user)
        {
            var list = await _context.DonationFund.Where(d => d.CreatorId == user.Id).ToListAsync();
            return list;
        }
    }
}
