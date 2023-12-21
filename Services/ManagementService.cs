using CNPM_BE.Data;
using CNPM_BE.DTOs;
using CNPM_BE.Models;
using Microsoft.EntityFrameworkCore;

namespace CNPM_BE.Services
{
    public class ManagementService
    {
        private readonly CNPMDbContext _context;
        private readonly TimeConverterService _timeConverterService;
        public ManagementService(CNPMDbContext context, TimeConverterService timeConverterService)
        {
            _context = context;
            _timeConverterService = timeConverterService;
        }

        public async Task<ApiResponseExpose<ResidentResp>> AddResident(AppUser user, ResidentCreateReq req)
        {
            var resp = new ApiResponseExpose<ResidentResp>();

            var newResident = new Resident();
            var ex = await _context.Resident.FirstOrDefaultAsync(r => r.ResidentCode == req.ResidentCode && r.CreatorId == user.Id && r.Status != ResidentStatus.Deleted);
            if (ex != null)
            {
                resp.code = -1;
                resp.message = "Đã tồn tại cư dân với mã " + req.ResidentCode;
                return resp;
            }

            var apartment = await _context.Apartment.FirstOrDefaultAsync(a => a.Id == req.ApartmentId && a.Status != ApartmentStatus.Deleted && a.CreatorId == user.Id);
            if (apartment == null)
            {
                resp.code = -1;
                resp.message = "Căn hộ không hợp lệ";
                return resp;
            }

            newResident.CreatorId = user.Id;
            newResident.ResidentCode = req.ResidentCode;
            newResident.ApartmentId = req.ApartmentId;
            newResident.Name = req.Name;
            newResident.BirthDate = await _timeConverterService.ConvertToUTCTime(req.BirthDate);
            newResident.Career = req.Career;
            newResident.Gender = (ResidentGender)req.Gender;
            newResident.Status = ResidentStatus.Active;
            newResident.CreatedTime = await _timeConverterService.ConvertToUTCTime(DateTime.Now);
            newResident.PhoneNumber = req.PhoneNumber;
            newResident.CCCD = req.CCCD;
            newResident.IsOwner = false;
            try
            {
                await _context.Resident.AddAsync(newResident);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình thêm cư dân mã " + req.ResidentCode;
                return resp;
            }
            newResident = await _context.Resident.OrderBy(r => r.Id).LastOrDefaultAsync(r => r.CreatorId == user.Id && r.Status != ResidentStatus.Deleted);
            if (apartment.Status == ApartmentStatus.Unoccupied)
            {
                apartment.Status = ApartmentStatus.Occupied;
                apartment.OwnerId = newResident.Id;
                newResident.IsOwner = true;
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình thêm cư dân mã " + req.ResidentCode;
                return resp;
            }

            resp.code = 1;
            resp.message = "Thêm cư dân mã " + req.ResidentCode + " thành công";
            resp.entity = new ResidentResp(newResident, apartment);
            return resp;
        }

        public async Task<ApiResponseExpose<ResidentResp>> RemoveResident(AppUser user, int req)
        {
            var resp = new ApiResponseExpose<ResidentResp>();
            var resident = await _context.Resident.FirstOrDefaultAsync(r => r.CreatorId == user.Id && r.Id == req && r.Status != ResidentStatus.Deleted);
            if (resident == null)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình tìm kiếm cư dân";
                return resp;
            }
            var apartment = await _context.Apartment.FirstOrDefaultAsync(a => a.Id == resident.ApartmentId);
            var count = await _context.Resident.Where(r => r.ApartmentId == resident.ApartmentId && r.Status != ResidentStatus.Deleted).CountAsync();
            if (count <= 1)
            {
                apartment.Status = ApartmentStatus.Unoccupied;
                apartment.OwnerId = null;
            }
            else
            {
                var newOwner = await _context.Resident.FirstOrDefaultAsync(r => r.ApartmentId == apartment.Id && r.CreatorId == user.Id && r.Status != ResidentStatus.Deleted);
                apartment.OwnerId = newOwner.Id;
                newOwner.IsOwner = true;
            }
            resident.Status = ResidentStatus.Deleted;
            resident.DeletedTime = await _timeConverterService.ConvertToUTCTime(DateTime.Now);
            var vehicleList = await _context.Vehicle.Where(v => v.OwnerId == resident.Id && v.CreatorId == user.Id && v.Status == VehicleStatus.Active).ToListAsync();
            foreach (var vehicle in vehicleList)
            {
                vehicle.Status = VehicleStatus.Deleted;
                vehicle.DeletedTime = await _timeConverterService.ConvertToUTCTime(DateTime.Now);
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình xóa cư dân";
                return resp;
            }

            resp.code = 1;
            resp.message = "Xóa cư dân thành công";
            resp.entity = new ResidentResp(resident, apartment);
            return resp;
        }

        public async Task<ApiResponseExpose<ResidentResp>> UpdateInformation(AppUser user, Resident req)
        {
            var resp = new ApiResponseExpose<ResidentResp>();
            var resident = await _context.Resident.FirstOrDefaultAsync(r => r.Id ==  req.Id && r.CreatorId == user.Id && r.Status != ResidentStatus.Deleted);
            if (resident == null)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình tìm kiếm cư dân";
                return resp;
            }
            resident.Name = req.Name;
            resident.Career = req.Career;
            resident.Gender = (ResidentGender)req.Gender;
            resident.BirthDate = await _timeConverterService.ConvertToUTCTime(req.BirthDate);
            resident.PhoneNumber = req.PhoneNumber;
            resident.CCCD = req.CCCD;
            resident.Status = (ResidentStatus)req.Status;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình cập nhật thông tin cư dân";
                return resp;
            }

            resp.code = 1;
            resp.message = "Cập nhật thông tin cư dân thành công";
            var apartment = await _context.Apartment.FirstOrDefaultAsync(a => a.Id == resident.ApartmentId);
            resp.entity = new ResidentResp(resident, apartment);

            return resp;
        }

        public async Task<List<ResidentResp>> GetResidentList(AppUser user)
        {
            var list = await _context.Resident.Where(r => r.CreatorId == user.Id && r.Status != ResidentStatus.Deleted).ToListAsync();
            var resp = new List<ResidentResp>();
            foreach(var r in list)
            {
                var a = await _context.Apartment.FirstOrDefaultAsync(a => a.Id == r.ApartmentId);
                var rr = new ResidentResp(r, a);
                resp.Add(rr);
            }
            return resp;
        }

        public async Task<List<HouseholdResp>> GetHouseholdList(AppUser user)
        {
            var apartmentList = await _context.Apartment.Where(a => a.CreatorId == user.Id && a.Status == ApartmentStatus.Occupied).ToListAsync();
            var resp = new List<HouseholdResp>();
            foreach (var a in apartmentList)
            {
                var hr = new HouseholdResp();
                hr.Id = a.Id;
                hr.Position = a.Position;
                hr.ApartmentCode = a.ApartmentCode;
                //var owner = await _context.Resident.FirstOrDefaultAsync(r => r.Id == a.OwnerId);
                //hr.OwnerCode = owner.ResidentCode;
                //hr.OwnerName = owner.Name;
                var rlist = new List<ResidentResp>();
                var residentList = await _context.Resident.Where(r => r.ApartmentId == a.Id && r.Status != ResidentStatus.Deleted).ToListAsync();
                foreach(var res in residentList)
                {
                    var rr = new ResidentResp(res, a);
                    rlist.Add(rr);
                }
                hr.ResidentList = rlist;
                var vlist = new List<VehicleResp>();
                var vehicleList = await _context.Vehicle.Where(v => v.ApartmentId == a.Id && v.Status == VehicleStatus.Active).ToListAsync();
                foreach (var veh in vehicleList)
                {
                    var owner = await _context.Resident.FirstOrDefaultAsync(r => r.Id == veh.OwnerId);
                    var type = await _context.VehicleType.FirstOrDefaultAsync(v => v.Id == veh.VehicleTypeId);
                    var vr = new VehicleResp(veh, owner, type);
                    vlist.Add(vr);
                }
                hr.VehicleList = vlist;
                resp.Add(hr);
            }
            return resp;
        }
    }
}
