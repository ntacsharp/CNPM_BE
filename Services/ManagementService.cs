//using CNPM_BE.Data;
//using CNPM_BE.DTOs;
//using CNPM_BE.Models;
//using Microsoft.EntityFrameworkCore;

//namespace CNPM_BE.Services
//{
//    public class ManagementService
//    {
//        private readonly CNPMDbContext _context;
//        private readonly TimeConverterService _timeConverterService;
//        public ManagementService(CNPMDbContext context, TimeConverterService timeConverterService)
//        {
//            _context = context;
//            _timeConverterService = timeConverterService;
//        }
//        public async Task<ApiResp> CreateHousehold(AppUser user, HouseholdCreateReq req)
//        {
//            var hh = new Household();
//            var resp = new ApiResp();
//            var ex = await _context.Household.OrderBy(h => h.Id).LastOrDefaultAsync(h => (h.ManagerId == user.Id && h.HouseholdCode == req.HouseholdCode && h.IsActive));
//            if (ex != null)
//            {
//                resp.code = -1;
//                resp.message = "Đã tồn tại hộ gia đình mang mã " + req.HouseholdCode + " trong hệ thống";
//                return resp;
//            }
//            hh.ManagerId = user.Id;
//            hh.ApartmentCode = req.ApartmentCode;
//            hh.OwnerName = req.OwnerName;
//            hh.HouseholdCode = req.HouseholdCode;
//            hh.Area = req.Area;
//            hh.CreatedTime = await _timeConverterService.ConvertToUTCTime(DateTime.Now);
//            hh.ServiceFeePerMeter = req.ServiceFeePerMeter;
//            hh.VehicleCount = req.VehicleCount;
//            hh.IsActive = true;
//            try
//            {
//                await _context.Household.AddAsync(hh);
//                await _context.SaveChangesAsync();
//            }
//            catch (Exception)
//            {
//                resp.code = -1;
//                resp.message = "Đã có lỗi xảy ra khi tạo hộ gia đình";
//                return resp;
//            }
//            hh = await _context.Household.OrderBy(h => h.Id).LastOrDefaultAsync(h => (h.ManagerId == user.Id && h.HouseholdCode == req.HouseholdCode && h.IsActive));
//            //var hf = new HouseholdFee();
//            //hf.
//            var chf = new CurrentHouseholdFee();
//            chf.HouseholdId = hh.Id;
//            chf.CreatorId = user.Id;
//            chf.IsActive = true;
//            chf.LeftoverManagementFee = 0;
//            chf.LeftoverParkingFee = 0;
//            chf.LeftoverServiceFee = 0;
//            chf.CurrentManagementFee = chf.TotalManagementFee = (int)(7000 * hh.Area);
//            chf.CurrentParkingFee = chf.TotalParkingFee = 70000 * hh.VehicleCount;
//            chf.CurrentServiceFee = chf.TotalServiceFee = (int)(hh.ServiceFeePerMeter * hh.Area);
//            chf.PaidManagementFee = 0;
//            chf.PaidParkingFee = 0;
//            chf.PaidManagementFee = 0;
//            try
//            {
//                await _context.CurrentHouseholdFee.AddAsync(chf);
//                await _context.SaveChangesAsync();
//            }
//            catch (Exception) 
//            {
//                resp.code = -1;
//                resp.message = "Đã có lỗi xảy ra khi tạo hộ gia đình";
//                return resp;
//            }
//            var fl = await _context.DonationFund.Where(f => (f.CreatorId == user.Id && f.ExpirationTime < DateTime.Now)).ToListAsync();
//            foreach(var f in fl)
//            {
//                var hd = new HouseholdDonation();
//                hd.Amount = 0;
//                hd.DonatorId = hh.Id;
//                hd.FundId = f.Id;
//                hd.CreatorId = user.Id;
//                try
//                {
//                    await _context.HouseholdDonation.AddAsync(hd);
//                    await _context.SaveChangesAsync();
//                }
//                catch (Exception)
//                {
//                    resp.code = -1;
//                    resp.message = "Đã có lỗi xảy ra khi tạo hộ gia đình";
//                    return resp;
//                }
//            }
//            resp.code = 1;
//            resp.message = "Thêm hộ gia đình thành công";
//            return resp;
//        }
//        public async Task<ApiResp> DeactivateHousehold(AppUser user, HouseholdDeactivateReq req)
//        {
//            var resp = new ApiResp();
//            var hh = await _context.Household.FirstOrDefaultAsync(h => (h.Id == req.HouseholdId && h.ManagerId == user.Id && h.IsActive));
//            var chf = await _context.CurrentHouseholdFee.FirstOrDefaultAsync(c => c.HouseholdId ==  req.HouseholdId);
//            if(hh == null)
//            {
//                resp.code = -1;
//                resp.message = "Bạn không có quyền quản lý hộ này";
//                return resp;
//            }
//            hh.IsActive = false;
//            chf.IsActive = false;
//            hh.DeactivateTime = DateTime.Now;
//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (Exception)
//            {
//                resp.code = -1;
//                resp.message = "Đã có lỗi xảy ra khi xóa hộ gia đình";
//                return resp;
//            }
//            resp.code = 1;
//            resp.message = "Xóa hộ gia đình thành công";
//            return resp;
//        }
//        public async Task<ApiResp> AddMember(AppUser user, AddMemberReq req)
//        {
//            var resp = new ApiResp();
//            var hh = await _context.Household.FirstOrDefaultAsync(h => (h.Id ==  req.HouseholdId
//                && h.ManagerId == user.Id
//                && h.IsActive
//            ));
//            if (hh == null)
//            {
//                resp.code = -1;
//                resp.message = "Bạn không có quyền quản lý hộ này";
//                return resp;
//            }
//            var hm = new HouseholdMember();
//            hm.Name = req.Name;
//            hm.HouseholdId = req.HouseholdId;
//            hm.Nation = req.Nation;
//            hm.BirthDate = req.BirthDate;
//            hm.Gender = req.Gender;
//            hm.IsActive = true;

//            try
//            {
//                await _context.HouseholdMember.AddAsync(hm);
//                await _context.SaveChangesAsync();
//            }
//            catch (Exception)
//            {
//                resp.code = -1;
//                resp.message = "Đã có lỗi xảy ra khi thêm thành viên";
//                return resp;
//            }
//            resp.code = 1;
//            resp.message = "Thêm thành viên thành công";
//            return resp;
//        }
//        public async Task<ApiResp> RemoveMember(AppUser user, RemoveMemberReq req)
//        {
//            var resp = new ApiResp();
//            var hm = await _context.HouseholdMember.FirstOrDefaultAsync(h => h.Id == req.Id);
//            var hh = await _context.Household.FirstOrDefaultAsync(h => h.Id == hm.HouseholdId);
//            if(hh.ManagerId != user.Id || !hh.IsActive)
//            {
//                resp.code = -1;
//                resp.message = "Không tìm được thành viên tương ứng";
//                return resp;
//            }
//            hm.IsActive = false;
//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (Exception)
//            {
//                resp.code = -1;
//                resp.message = "Đã có lỗi xảy ra khi xóa thành viên";
//                return resp;
//            }
//            resp.code = 1;
//            resp.message = "Xóa thành viên thành công";
//            return resp;
//        }
//        public async Task<List<HouseholdResp>> GetHouseholdList(AppUser user, Payload payload)
//        {
//            var list = await _context.Household.Where(h => h.ManagerId == user.Id).Skip(payload.Skip * payload.Take).Take(payload.Take).Select(h => new HouseholdResp(h)).ToListAsync();
//            return list;
//        }
//        public async Task<List<MemberResp>> GetMemberList(MemberReq req)
//        {
//            var list = await _context.HouseholdMember.Where(h => h.HouseholdId == req.Id && h.IsActive).Select(h => new MemberResp(h)).ToListAsync();
//            return list;
//        }
//        public async Task<List<HouseholdOption>> GetOption(AppUser user)
//        {
//            var x = await _context.Household.Where(h => h.ManagerId == user.Id && h.IsActive).ToListAsync();
//            var resp = new List<HouseholdOption>();
//            foreach(var hh in x)
//            {
//                var ho = new HouseholdOption();
//                ho.Id = hh.Id;
//                ho.OwnerName = hh.OwnerName;
//                ho.HouseholdCode = hh.HouseholdCode;
//                ho.ApartmentCode = hh.ApartmentCode;
//                resp.Add(ho);
//            }
//            return resp;
//        }
//    }
//}
