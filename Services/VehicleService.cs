using CNPM_BE.Data;
using CNPM_BE.DTOs;
using CNPM_BE.Models;
using Microsoft.EntityFrameworkCore;

namespace CNPM_BE.Services
{
    public class VehicleService
    {
        private readonly CNPMDbContext _context;
        private readonly TimeConverterService _timeConverterService;
        public VehicleService(CNPMDbContext context, TimeConverterService timeConverterService)
        {
            _context = context;
            _timeConverterService = timeConverterService;
        }
        public async Task<ApiResp> AddVehicle(AppUser user, VehicleCreateReq req)
        {
            var resp = new ApiResp();
            var newVehicle = new Vehicle();
            var owner = await _context.Resident.FirstOrDefaultAsync(r => r.Id == req.OwnerId && r.CreatorId == user.Id && r.Status == ResidentStatus.Active);
            var ex = await _context.Vehicle.FirstOrDefaultAsync(v => v.CreatorId == user.Id && v.VehicleCode == req.VehicleCode && v.Status == VehicleStatus.Active);
            if (ex != null)
            {
                resp.code = -1;
                resp.message = "Đã tồn tại phương tiện với mã " + req.VehicleCode;
                return resp;
            }
            var resident = await _context.Resident.FirstOrDefaultAsync(r => r.Id == req.OwnerId && r.Status != ResidentStatus.Deleted && r.CreatorId == user.Id);
            if (resident == null)
            {
                resp.code = -1;
                resp.message = "Cư dân không hợp lệ";
                return resp;
            }
            
            newVehicle.CreatorId = user.Id;
            newVehicle.Status = VehicleStatus.Active;
            newVehicle.Plate = req.Plate;
            newVehicle.CreatedTime = await _timeConverterService.ConvertToUTCTime(DateTime.Now);
            newVehicle.Name = req.Name;
            newVehicle.OwnerId = req.OwnerId;
            newVehicle.VehicleCode = req.VehicleCode;
            newVehicle.VehicleTypeId = req.VehicleTypeId;
            newVehicle.ApartmentId = owner.ApartmentId;
            try
            {
                await _context.Vehicle.AddAsync(newVehicle);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình thêm phương tiện mã " + req.VehicleCode;
                return resp;
            }
            resp.code = 1;
            resp.message = "Thêm phương tiện mã " + req.VehicleCode + " thành công";
            return resp;
        }
        public async Task<ApiResp> RemoveVehicle(AppUser user, VehicleDeleteReq req)
        {
            var resp = new ApiResp();
            var vehicle = await _context.Vehicle.FirstOrDefaultAsync(v => v.CreatorId == user.Id && v.Id == req.Id && v.Status == VehicleStatus.Active);
            if (vehicle == null)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình tìm kiếm phương tiện";
                return resp;
            }
            vehicle.Status = VehicleStatus.Deleted;
            vehicle.DeletedTime = await _timeConverterService.ConvertToUTCTime(DateTime.Now);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình xóa phương tiện";
                return resp;
            }
            resp.code = 1;
            resp.message = "Xóa phương tiện thành công";
            return resp;
        }
        public async Task<ApiResp> UpdateVehicleInformation(AppUser user, VehicleUpdateReq req)
        {
            var resp = new ApiResp();
            var vehicle = await _context.Vehicle.FirstOrDefaultAsync(v => v.Id == req.Id && v.CreatorId == user.Id && v.Status == VehicleStatus.Active);
            if (vehicle == null)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình tìm kiếm phương tiện";
                return resp;
            }
            vehicle.Name = req.Name;
            vehicle.Plate = req.Plate;
            vehicle.VehicleTypeId = req.VehicleTypeId;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình cập nhật thông tin phương tiện";
                return resp;
            }
            resp.code = 1;
            resp.message = "Cập nhật thông tin phương tiện thành công";
            return resp;
        }
        public async Task<List<VehicleResp>> GetVehicleList(AppUser user)
        {
            var list = await _context.Vehicle.Where(v => v.CreatorId == user.Id && v.Status == VehicleStatus.Active).ToListAsync();
            var resp = new List<VehicleResp>();
            foreach (var v in list)
            {
                var a = await _context.Resident.FirstOrDefaultAsync(r => r.Id == v.OwnerId);
                var vht = await _context.VehicleType.FirstOrDefaultAsync(vh => vh.Id == v.VehicleTypeId);
                var vr = new VehicleResp(v, a, vht);
                resp.Add(vr);
            }
            return resp;
        }
        public async Task<ApiResp> AddVehicleType(AppUser user, VehicleTypeCreateReq req)
        {
            var resp = new ApiResp();
            var newVehicleType = new VehicleType();
            var ex = await _context.VehicleType.FirstOrDefaultAsync(v => v.VehicleTypeCode == req.VehicleTypeCode && v.CreatorId == user.Id && v.Status == VehicleTypeStatus.Active);
            if (ex != null)
            {
                resp.code = -1;
                resp.message = "Đã tồn tại loại phương tiện với mã " + req.VehicleTypeCode;
                return resp;
            }
            newVehicleType.VehicleTypeCode = req.VehicleTypeCode;
            newVehicleType.Name = req.Name;
            newVehicleType.ParkingFee = req.ParkingFee;
            newVehicleType.Status = VehicleTypeStatus.Active;
            newVehicleType.CreatorId = user.Id;
            try
            {
                await _context.VehicleType.AddAsync(newVehicleType);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình thêm loại phương tiện mã " + req.VehicleTypeCode;
                return resp;
            }
            resp.code = 1;
            resp.message = "Thêm loại phương tiện mã " + req.VehicleTypeCode + " thành công";
            return resp;
        }
        public async Task<ApiResp> RemoveVehicleType(AppUser user, VehicleTypeDeleteReq req)
        {
            var resp = new ApiResp();
            var vehicleType = await _context.VehicleType.FirstOrDefaultAsync(v => v.CreatorId == user.Id && v.Id == req.Id && v.Status == VehicleTypeStatus.Active);
            if (vehicleType == null)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình tìm kiếm loại phương tiện";
                return resp;
            }
            vehicleType.Status = VehicleTypeStatus.Deleted;
            var vehicleList = await _context.Vehicle.Where(v => v.VehicleTypeId == vehicleType.Id && v.CreatorId == user.Id && v.Status == VehicleStatus.Active).ToListAsync();
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
                resp.message = "Đã có lỗi xảy ra trong quá trình xóa loại phương tiện";
                return resp;
            }
            resp.code = 1;
            resp.message = "Xóa loại phương tiện thành công";
            return resp;
        }
        public async Task<ApiResp> UpdateVehicleTypeInformation(AppUser user, VehicleTypeUpdateReq req)
        {
            var resp = new ApiResp();
            var vehicleType = await _context.VehicleType.FirstOrDefaultAsync(v => v.Id == req.Id && v.CreatorId == user.Id && v.Status == VehicleTypeStatus.Active);
            if (vehicleType == null)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình tìm kiếm loại phương tiện";
                return resp;
            }
            vehicleType.Name = req.Name;
            vehicleType.ParkingFee = req.ParkingFee;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình cập nhật thông tin loại phương tiện";
                return resp;
            }
            resp.code = 1;
            resp.message = "Cập nhật thông tin loại phương tiện thành công";
            return resp;
        }
        public async Task<List<VehicleTypeResp>> GetVehicleTypeList(AppUser user)
        {
            var resp = await _context.VehicleType.Where(v => v.CreatorId == user.Id && v.Status == VehicleTypeStatus.Active).Select(v => new VehicleTypeResp(v)).ToListAsync();
            return resp;
        }
        public async Task<int> GetParkingFee(AppUser user, Vehicle v)
        {
            var type = await _context.VehicleType.FirstOrDefaultAsync(vt => vt.Id == v.VehicleTypeId && vt.CreatorId == user.Id);
            return type.ParkingFee;
        }
    }
}
