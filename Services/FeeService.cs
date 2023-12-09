//using CNPM_BE.Data;
//using CNPM_BE.DTOs;
//using CNPM_BE.Models;
//using Microsoft.EntityFrameworkCore;

//namespace CNPM_BE.Services
//{
//    public class FeeService
//    {
//        private readonly CNPMDbContext _context;
//        public FeeService(CNPMDbContext context)
//        {
//            _context = context;
//        }
//        public async Task<ApiResp> CreateFeePayment(AppUser user, FeePaymentCreateReq req)
//        {
//            var hh = await _context.Household.FirstOrDefaultAsync(h => (h.ManagerId == user.Id && h.Id == req.HouseholdId));
//            var resp = new ApiResp();
//            if (hh == null)
//            {
//                resp.code = -1;
//                resp.message = "Bạn không có quyền thêm lịch sủ trả phí cho hộ này";
//                return resp;
//            }
//            var chf = await _context.CurrentHouseholdFee.FirstOrDefaultAsync(c => c.HouseholdId == hh.Id);
//            var fp = new FeePayment();
//            fp.HouseholdId = hh.Id;
//            fp.Amount = req.Amount;
//            fp.CreatedTime = DateTime.Now;
//            if (req.Type == 0)
//            {
//                chf.PaidManagementFee += req.Amount;
//                fp.Type = FeeType.Management;
//            }
//            else if (req.Type == 1)
//            {
//                chf.PaidParkingFee += req.Amount;
//                fp.Type = FeeType.Parking;
//            }
//            else if (req.Type == 2)
//            {
//                chf.PaidServiceFee += req.Amount;
//                fp.Type = FeeType.Service;
//            }
//            else
//            {
//                return null;
//            }
//            try
//            {
//                await _context.CurrentHouseholdFee.AddAsync(chf);
//                await _context.SaveChangesAsync();
//            }
//            catch (Exception)
//            {
//                resp.code = -1;
//                resp.message = "Đã có lỗi xảy ra khi thêm lịch sử trả phí";
//                return resp;
//            }
//            resp.code = 1;
//            resp.message = "Thêm lịch sử trả phí thành công";
//            return resp;
//        }
//        public async Task<List<FeeResp>> GetHouseholdFee(AppUser user, Payload payload)
//        {
//            var x = await _context.CurrentHouseholdFee.Where(c => c.CreatorId == user.Id).Skip(payload.Skip * payload.Take).Take(payload.Take).ToListAsync();
//            var resp = new List<FeeResp>();
//            foreach(var chf in x)
//            {
//                var fr = new FeeResp();
//                var hh = await _context.Household.FirstOrDefaultAsync(h => h.Id == chf.HouseholdId);
//                fr.ApartmentCode = hh.ApartmentCode;
//                fr.HouseholdCode = hh.HouseholdCode;
//                fr.HouseholdOwner = hh.OwnerName;
//                fr.CurrentManagementFee = chf.CurrentManagementFee;
//                fr.CurrentParkingFee = chf.CurrentParkingFee;
//                fr.CurrentServiceFee = chf.CurrentServiceFee;
//                fr.LeftoverManagementFee = chf.LeftoverManagementFee;
//                fr.LeftoverParkingFee = chf.LeftoverParkingFee;
//                fr.LeftoverServiceFee = chf.LeftoverServiceFee;
//                fr.TotalManagementFee = chf.TotalManagementFee;
//                fr.TotalParkingFee = chf.TotalParkingFee;
//                fr.TotalServiceFee = chf.TotalServiceFee;
//                fr.PaidManagementFee = chf.PaidManagementFee;
//                fr.PaidParkingFee = chf.PaidParkingFee;
//                fr.PaidServiceFee = chf.PaidServiceFee;
//                resp.Add(fr);
//            }
//            return resp;
//        }
//    }
//}
