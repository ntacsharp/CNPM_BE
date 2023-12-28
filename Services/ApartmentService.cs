using CNPM_BE.Data;
using CNPM_BE.DTOs;
using CNPM_BE.Models;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace CNPM_BE.Services
{
    public class ApartmentService
    {
        private readonly CNPMDbContext _context;
        private readonly TimeConverterService _timeConverterService;
        public ApartmentService(CNPMDbContext context, TimeConverterService timeConverterService)
        {
            _context = context;
            _timeConverterService = timeConverterService;
        }
        public async Task<ApiResponseExpose<Apartment>> AddApartment(AppUser user, ApartmentCreateReq req)
        {
            var resp = new ApiResponseExpose<Apartment>();

            var ex = await _context.Apartment.FirstOrDefaultAsync(a => a.ApartmentCode == req.ApartmentCode && a.Status != ApartmentStatus.Deleted);
            if(ex != null)
            {
                resp.code = -1;
                resp.message = "Đã tồn tại căn hộ với mã " + req.ApartmentCode;
                return resp;
            }

            var newApartment = new Apartment();
            newApartment.CreatorId = user.Id;
            newApartment.ApartmentCode = req.ApartmentCode;
            newApartment.Position = req.Position;
            newApartment.Area = req.Area;
            newApartment.RoomCount = req.RoomCount;
            newApartment.Price = req.Price;
            newApartment.Status = ApartmentStatus.Unoccupied;
            try
            {
                await _context.Apartment.AddAsync(newApartment);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình thêm căn hộ mã " + req.ApartmentCode;
                return resp;
            }

            resp.code = 1;
            resp.message = "Thêm căn hộ mã " + req.ApartmentCode + " thành công";
            resp.entity = newApartment;

            return resp;
        }
        public async Task<ApiResponseExpose<Apartment>> RemoveApartment(AppUser user, int req)
        {
            var resp = new ApiResponseExpose<Apartment>();

            var apartment = await _context.Apartment.FirstOrDefaultAsync(a => a.Id == req && a.Status != ApartmentStatus.Deleted);
            if (apartment == null)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình tìm kiếm căn hộ";
                return resp;
            }

            apartment.Status = ApartmentStatus.Deleted;
            var residentList = await _context.Resident.Where(r => r.ApartmentId == apartment.Id && r.Status == ResidentStatus.Active).ToListAsync();
            foreach (var res in residentList)
            {
                res.Status = ResidentStatus.Deleted;
                res.DeletedTime = await _timeConverterService.ConvertToUTCTime(DateTime.Now);
                var vehicleList = await _context.Vehicle.Where(v => v.OwnerId == res.Id && v.Status == VehicleStatus.Active).ToListAsync();
                foreach (var vehicle in vehicleList)
                {
                    vehicle.Status = VehicleStatus.Deleted;
                    vehicle.DeletedTime = await _timeConverterService.ConvertToUTCTime(DateTime.Now);
                }
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình xóa căn hộ mã " + apartment.ApartmentCode;
                return resp;
            }

            resp.code = 1;
            resp.message = "Xóa căn hộ mã " + apartment.ApartmentCode + " thành công";
            resp.entity = apartment;

            return resp;
        }
        public async Task<ApiResponseExpose<Apartment>> UpdateInformation(AppUser user, Apartment req)
        {
            var resp = new ApiResponseExpose<Apartment>();

            var apartment = await _context.Apartment.FirstOrDefaultAsync(a => a.Id == req.Id && a.Status != ApartmentStatus.Deleted);
            if (apartment == null)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình tìm kiếm căn hộ";
                return resp;
            }

            apartment.RoomCount = req.RoomCount;
            apartment.Price = req.Price;
            apartment.Area = req.Area;
            apartment.Position = req.Position;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình cập nhật thông tin căn hộ mã " + apartment.ApartmentCode;
                return resp;
            }
            resp.code = 1;
            resp.message = "Cập nhật thông tin căn hộ mã " + apartment.ApartmentCode + " thành công";
            resp.entity = apartment;

            return resp;
        }
        public async Task<List<ApartmentResp>> GetApartmentList(AppUser user)
        {
            var resp = await _context.Apartment.Where(a => a.Status != ApartmentStatus.Deleted).Select(a => new ApartmentResp(a)).ToListAsync();
            return resp;
        }
    }
}
