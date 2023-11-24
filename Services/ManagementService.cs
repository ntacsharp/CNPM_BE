using CNPM_BE.Data;
using CNPM_BE.DTOs;
using CNPM_BE.Models;
using Microsoft.EntityFrameworkCore;

namespace CNPM_BE.Services
{
    public class ManagementService
    {
        private readonly CNPMDbContext _context;
        public ManagementService(CNPMDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResp> CreateHousehold(AppUser user, HouseholdCreateReq req)
        {
            var hh = new Household();
            var resp = new ApiResp();
            hh.ManagerId = user.Id;
            hh.OwnerName = req.OwnerName;
            hh.HouseholdCode = req.HouseholdCode;
            hh.Area = req.Area;
            hh.CreatedTime = DateTime.Now;
            hh.ServiceFeePerMember = req.ServiceFeePerMember;
            hh.VehicleCount = req.VehicleCount;
            hh.IsActive = true;
            try
            {
                await _context.Household.AddAsync(hh);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra khi tạo hộ gia đình";
                return resp;
            }
            hh = await _context.Household.OrderBy(h => h.Id).LastOrDefaultAsync(h => (h.ManagerId == user.Id && h.HouseholdCode == req.HouseholdCode));
            //var hf = new HouseholdFee();
            //hf.
            var chf = new CurrentHouseholdFee();
            chf.HouseholdId = hh.Id;
            chf.IsActive = true;
            chf.LeftoverManagementFee = 0;
            chf.LeftoverParkingFee = 0;
            chf.LeftoverServiceFee = 0;
            chf.CurrentManagementFee = chf.TotalManagementFee = (int)(7000 * hh.Area);
            chf.CurrentParkingFee = chf.TotalParkingFee = 70000 * hh.VehicleCount;
            chf.CurrentServiceFee = chf.TotalServiceFee = (int)(hh.ServiceFeePerMember * hh.Area);
            chf.PaidManagementFee = 0;
            chf.PaidParkingFee = 0;
            chf.PaidManagementFee = 0;
            try
            {
                await _context.CurrentHouseholdFee.AddAsync(chf);
                await _context.SaveChangesAsync();
            }
            catch (Exception) 
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra khi tạo hộ gia đình";
                return resp;
            }
            var fl = await _context.DonationFund.Where(f => (f.CreatorId == user.Id && f.ExpirationTime < DateTime.Now)).ToListAsync();
            foreach(var f in fl)
            {
                var hd = new HouseholdDonation();
                hd.Amount = 0;
                hd.DonatorId = hh.Id;
                hd.FundId = f.Id;
                try
                {
                    await _context.HouseholdDonation.AddAsync(hd);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    resp.code = -1;
                    resp.message = "Đã có lỗi xảy ra khi tạo hộ gia đình";
                    return resp;
                }
            }
            resp.code = 1;
            resp.message = "Thêm hộ gia đình thành công";
            return resp;
        }
        public async Task<ApiResp> DeactivateHousehold(AppUser user, int householdId)
        {
            var resp = new ApiResp();
            var hh = await _context.Household.FirstOrDefaultAsync(h => (h.Id == householdId && h.ManagerId == user.Id));
            var chf = await _context.CurrentHouseholdFee.FirstOrDefaultAsync(c => c.HouseholdId ==  householdId);
            if(hh == null)
            {
                resp.code = -1;
                resp.message = "Bạn không có quyền quản lý hộ này";
                return resp;
            }
            hh.IsActive = false;
            chf.IsActive = false;
            hh.DeactivateTime = DateTime.Now;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra khi xóa hộ gia đình";
                return resp;
            }
            resp.code = 1;
            resp.message = "Xóa hộ gia đình thành công";
            return resp;
        }
        public async Task<ApiResp> AddMember(AppUser user, HouseholdAddMemberReq req)
        {
            var resp = new ApiResp();
            var hh = await _context.Household.FirstOrDefaultAsync(h => (h.Id ==  req.HouseholdId
                && h.ManagerId == user.Id
                && h.IsActive
            ));
            if (hh == null)
            {
                resp.code = -1;
                resp.message = "Bạn không có quyền quản lý hộ này";
                return resp;
            }
            var hm = new HouseholdMember();
            hm.Name = req.Name;
            hm.HouseholdId = req.HouseholdId;
            hm.IsActive = true;

            try
            {
                await _context.HouseholdMember.AddAsync(hm);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra khi thêm tạm trú";
                return resp;
            }
            resp.code = 1;
            resp.message = "Thêm tạm trú thành công";
            return resp;
        }
        public async Task<ApiResp> RemoveMember(AppUser user, HouseholdRemoveMemberReq req)
        {
            var resp = new ApiResp();
            var hm = await _context.HouseholdMember.FirstOrDefaultAsync(h => h.Id == req.Id);
            var hh = await _context.Household.FirstOrDefaultAsync(h => h.Id == hm.HouseholdId);
            if(hh.ManagerId != user.Id || !hh.IsActive)
            {
                resp.code = -1;
                resp.message = "Không tìm được thành viên tương ứng";
                return resp;
            }
            hm.IsActive = false;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra khi thêm tạm vắng";
                return resp;
            }
            resp.code = 1;
            resp.message = "Thêm tạm vắng thành công";
            return resp;
        }
    }
}
