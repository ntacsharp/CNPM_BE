using CNPM_BE.Data;
using CNPM_BE.DTOs;
using CNPM_BE.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace CNPM_BE.Services
{
    public class FeeService
    {
        private readonly CNPMDbContext _context;
        private readonly TimeConverterService _timeConverterService;
        private readonly VehicleService _vehicleService;
        public FeeService(CNPMDbContext context, TimeConverterService timeConverterService, VehicleService vehicleService)
        {
            _context = context;
            _timeConverterService = timeConverterService;
            _vehicleService = vehicleService;
        }
        public async Task<ApiResponseExpose<ServiceFeeTypeResp>> AddServiceFeeType(AppUser user, ServiceFeeTypeCreateReq req)
        {
            var resp = new ApiResponseExpose<ServiceFeeTypeResp>();
            var newServiceFeeType = new ServiceFeeType();
            var ex = await _context.ServiceFeeType.FirstOrDefaultAsync(s => s.ServiceFeeTypeCode == req.ServiceFeeTypeCode && s.Status == ServiceFeeTypeStatus.Active);
            if (ex != null)
            {
                resp.code = -1;
                resp.message = "Đã tồn tại phí dịch vụ với mã " + req.ServiceFeeTypeCode;
                return resp;
            }
            newServiceFeeType.ServiceFeeTypeCode = req.ServiceFeeTypeCode;
            newServiceFeeType.Name = req.Name;
            newServiceFeeType.PricePerUnit = req.PricePerUnit;
            newServiceFeeType.MeasuringUnit = (MeasuringUnit)req.MeasuringUnit;
            newServiceFeeType.Status = ServiceFeeTypeStatus.Active;
            newServiceFeeType.IsSystem = false;
            try
            {
                await _context.ServiceFeeType.AddAsync(newServiceFeeType);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình thêm phí dịch vụ mã " + req.ServiceFeeTypeCode;
                return resp;
            }
            resp.code = 1;
            resp.message = "Thêm phí dịch vụ mã " + req.ServiceFeeTypeCode + " thành công";
            resp.entity = new ServiceFeeTypeResp(newServiceFeeType);
            return resp;
        }
        public async Task<ApiResponseExpose<ServiceFeeTypeResp>> RemoveServiceFeeType(AppUser user, int req)
        {
            var resp = new ApiResponseExpose<ServiceFeeTypeResp>();
            var serviceFeeType = await _context.ServiceFeeType.FirstOrDefaultAsync(s => s.Id == req && s.Status == ServiceFeeTypeStatus.Active);
            if (serviceFeeType == null)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình tìm kiếm phí dịch vụ";
                return resp;
            }
            if (serviceFeeType.IsSystem)
            {
                resp.code = -1;
                resp.message = "Không thể xóa loại phí mặc định";
                return resp;
            }
            serviceFeeType.Status = ServiceFeeTypeStatus.Deleted;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình xóa phí dịch vụ";
                return resp;
            }
            resp.code = 1;
            resp.message = "Xóa phí dịch vụ thành công";
            resp.entity = new ServiceFeeTypeResp(serviceFeeType);
            return resp;
        }
        public async Task<ApiResponseExpose<ServiceFeeTypeResp>> UpdateServiceFeeTypeInformation(AppUser user, ServiceFeeType req)
        {
            var resp = new ApiResponseExpose<ServiceFeeTypeResp>();
            var serviceFeeType = await _context.ServiceFeeType.FirstOrDefaultAsync(v => v.Id == req.Id && v.Status == ServiceFeeTypeStatus.Active);
            if (serviceFeeType == null)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình tìm kiếm phí dịch vụ";
                return resp;
            }
            serviceFeeType.Name = req.Name;
            serviceFeeType.PricePerUnit = req.PricePerUnit;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình cập nhật thông tin phí dịch vụ";
                return resp;
            }
            resp.code = 1;
            resp.message = "Cập nhật thông tin phí dịch vụ thành công";
            resp.entity = new ServiceFeeTypeResp(serviceFeeType);
            return resp;
        }
        public async Task<List<ServiceFeeTypeResp>> GetServiceFeeTypeList(AppUser user)
        {
            var resp = await _context.ServiceFeeType.Where(s => s.Status == ServiceFeeTypeStatus.Active).Select(s => new ServiceFeeTypeResp(s)).ToListAsync();
            return resp;
        }
        public async Task<ApiResponseExpose<List<FeeResp>>> AddFee(AppUser user)
        {
            var resp = new ApiResponseExpose<List<FeeResp>>();
            var lfr = new List<FeeResp>();
            var current = await _timeConverterService.ConvertToUTCTime(DateTime.Now);
            var daysInMonth = DateTime.DaysInMonth(current.Year, current.Month);
            DateTime lastDayOfMonth = new DateTime(current.Year, current.Month, daysInMonth);
            var apartmentList = await _context.Apartment.Where(a => a.Status == ApartmentStatus.Occupied).ToListAsync();
            foreach(var apartment in apartmentList)
            {
                var fee = await _context.Fee.FirstOrDefaultAsync(f => f.ApartmentId == apartment.Id && f.Status != FeeStatus.Expired && f.Status != FeeStatus.Deleted);
                var serviceTypeList = await _context.ServiceFeeType.Where(s => s.Status == ServiceFeeTypeStatus.Active).ToListAsync();
                var vehicleList = await _context.Vehicle.Where(v => v.ApartmentId == apartment.Id && v.Status == VehicleStatus.Active).ToListAsync();
                if (fee == null)
                {
                    var newFee = new Fee();
                    var residentCount = await _context.Resident.Where(r => r.ApartmentId == apartment.Id && r.Status == ResidentStatus.Active).CountAsync();
                    newFee.CreatorId = user.Id;
                    newFee.ApartmentId = apartment.Id;
                    newFee.Note = "";
                    newFee.CreatedTime = current;
                    newFee.ExpiredDate = lastDayOfMonth;
                    newFee.ParkingFee = 0;
                    newFee.ReceivedAmount = 0;
                    foreach (var vehicle in vehicleList)
                    {
                        newFee.ParkingFee += (await _vehicleService.GetParkingFee(user, vehicle));
                    }
                    newFee.ReceivedAmount = 0;
                    newFee.Status = FeeStatus.OnGoing;
                    try
                    {
                        await _context.Fee.AddAsync(newFee);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception)
                    {
                        resp.code = -1;
                        resp.message = "Đã có lỗi xảy ra trong quá trình thêm bản thu phí";
                        return resp;
                    }
                    newFee = await _context.Fee.OrderBy(f => f.Id).LastOrDefaultAsync(f => f.Status != FeeStatus.Deleted);
                    
                    foreach (var serviceType in serviceTypeList)
                    {
                        var newServiceFee = new ServiceFee();
                        newServiceFee.CreatorId = user.Id;
                        newServiceFee.FeeId = newFee.Id;
                        newServiceFee.TypeId = serviceType.Id;
                        newServiceFee.OldCount = 0;
                        newServiceFee.NewCount = 0;
                        newServiceFee.TotalFee = 0;
                        if (serviceType.MeasuringUnit == MeasuringUnit.Apartment) newServiceFee.TotalFee = serviceType.PricePerUnit;
                        else if (serviceType.MeasuringUnit == MeasuringUnit.Resident) newServiceFee.TotalFee = serviceType.PricePerUnit * residentCount;
                        else if (serviceType.MeasuringUnit == MeasuringUnit.M2) newServiceFee.TotalFee = (int)(serviceType.PricePerUnit * apartment.Area);
                        newServiceFee.Status = ServiceFeeStatus.Active;
                        try
                        {
                            await _context.ServiceFee.AddAsync(newServiceFee);
                            await _context.SaveChangesAsync();
                        }
                        catch (Exception)
                        {
                            resp.code = -1;
                            resp.message = "Đã có lỗi xảy ra trong quá trình thêm bản thu phí";
                            return resp;
                        }
                    }
                    lfr.Add(await CreateFeeResp(newFee));
                }
                else
                {
                    fee.ParkingFee = 0;
                    foreach (var vehicle in vehicleList)
                    {
                        var owner = await _context.Resident.FirstOrDefaultAsync(r => r.Id == vehicle.OwnerId);
                        if(owner.Status == ResidentStatus.Active) fee.ParkingFee += (await _vehicleService.GetParkingFee(user, vehicle));
                    }
                    var serviceFeeList = await _context.ServiceFee.Where(s => s.FeeId == fee.Id && s.Status == ServiceFeeStatus.Active).ToListAsync();
                    foreach (var serviceFee in serviceFeeList)
                    {
                        var serviceType = await _context.ServiceFeeType.FirstOrDefaultAsync(s => s.Id == serviceFee.TypeId);
                        if (serviceType.Status == ServiceFeeTypeStatus.Deleted)
                        {
                            serviceFee.Status = ServiceFeeStatus.Deleted;
                        }
                        else if (serviceType.MeasuringUnit == MeasuringUnit.Resident)
                        {
                            var residentCount = await _context.Resident.Where(r => r.ApartmentId == apartment.Id && r.Status == ResidentStatus.Active).CountAsync();
                            serviceFee.TotalFee = residentCount * serviceType.PricePerUnit;
                        }
                    }
                    foreach (var serviceType in serviceTypeList)
                    {
                        var serviceFee = await _context.ServiceFee.FirstOrDefaultAsync(s => s.FeeId == fee.Id && s.TypeId == serviceType.Id && s.Status == ServiceFeeStatus.Active);
                        if (serviceFee == null)
                        {
                            serviceFee = new ServiceFee();
                            serviceFee.CreatorId = user.Id;
                            serviceFee.FeeId = fee.Id;
                            serviceFee.TypeId = serviceType.Id;
                            serviceFee.OldCount = 0;
                            serviceFee.NewCount = 0;
                            serviceFee.TotalFee = 0;
                            serviceFee.Status = ServiceFeeStatus.Active;
                            try
                            {
                                await _context.ServiceFee.AddAsync(serviceFee);
                                await _context.SaveChangesAsync();
                            }
                            catch (Exception)
                            {
                                resp.code = -1;
                                resp.message = "Đã có lỗi xảy ra trong quá trình thêm bản thu phí";
                                return resp;
                            }
                        }
                    }
                    
                    lfr.Add(await CreateFeeResp(fee));
                }
            }
            resp.code = 1;
            resp.message = "Thêm bản thu phí thành công";
            resp.entity = lfr;
            return resp;
        }
        public async Task<ApiResponseExpose<FeeResp>> RemoveFee(AppUser user, int req)
        {
            var resp = new ApiResponseExpose<FeeResp>();
            var fee = await _context.Fee.FirstOrDefaultAsync(f => f.Id == req && f.Status != FeeStatus.Deleted);
            if (fee == null)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình tìm kiếm bản thu phí";
                return resp;
            }
            var serviceFeeList = await _context.ServiceFee.Where(s => s.FeeId == fee.Id).ToListAsync();
            fee.Status = FeeStatus.Deleted;
            foreach(var serviceFee in serviceFeeList)
            {
                serviceFee.Status = ServiceFeeStatus.Deleted;
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình xóa bản thu phí";
                return resp;
            }
            resp.code = 1;
            resp.message = "Xóa bản thu phí thành công";
            resp.entity = await CreateFeeResp(fee);
            return resp;
        }
        public async Task<ApiResponseExpose<FeeResp>> UpdateFeeInformation(AppUser user, Fee req)
        {
            var resp = new ApiResponseExpose<FeeResp>();
            var fee = await _context.Fee.FirstOrDefaultAsync(f => f.Id == req.Id && f.Status != FeeStatus.Deleted);
            if (fee == null)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình tìm kiếm bản thu phí";
                return resp;
            }
            fee.Note = req.Note;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình cập nhật thông tin bản thu phí";
                return resp;
            }
            resp.code = 1;
            resp.message = "Cập nhật thông tin bản thu phí thành công";
            resp.entity = await CreateFeeResp(fee);
            return resp;
        }
        public async Task<List<FeeResp>> GetFeeList(AppUser user)
        {
            var feeList = await _context.Fee.Where(f => f.Status != FeeStatus.Deleted).ToListAsync();
            var resp = new List<FeeResp>();
            foreach (var fee in feeList)
            {
                var feeResp = await CreateFeeResp(fee);
                resp.Add(feeResp);
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
            return resp;
        }
        private async Task<FeeResp> CreateFeeResp(Fee fee)
        {
            var feeResp = new FeeResp();
            var apartment = await _context.Apartment.FirstOrDefaultAsync(a => a.Id == fee.ApartmentId);
            var owner = await _context.Resident.FirstOrDefaultAsync(r => r.Id == apartment.OwnerId);
            var serviceFeeList = await _context.ServiceFee.Where(f => f.FeeId == fee.Id && f.Status == ServiceFeeStatus.Active).ToListAsync();
            var paymentList = await _context.FeePayment.Where(f => f.FeeId == fee.Id && f.Status == FeePaymentStatus.Active).OrderBy(f => f.CreatedTime).ToListAsync();
            var vehicleList = await _context.Vehicle.Where(v => v.ApartmentId == fee.ApartmentId && v.Status == VehicleStatus.Active).ToListAsync();
            var list = new List<ServiceFeeResp>();
            var flist = new List<FeePaymentResp>();
            var vlist = new List<VehicleResp>();
            feeResp.Id = fee.Id;
            feeResp.ApartmentCode = apartment.ApartmentCode;
            feeResp.Area = apartment.Area;
            feeResp.OwnerName = owner.Name;
            feeResp.OwnerCode = owner.ResidentCode;
            feeResp.Note = fee.Note;
            feeResp.StartDate = await _timeConverterService.ConvertToUTCTime(new DateTime(feeResp.CreatedTime.Year, feeResp.CreatedTime.Month, 1));
            feeResp.CreatedTime = fee.CreatedTime;
            feeResp.ExpiredDate = fee.ExpiredDate;
            feeResp.ReceivedAmount = fee.ReceivedAmount;
            feeResp.ParkingFee = fee.ParkingFee;
            feeResp.ServiceFee = 0;
            foreach (var serviceFee in serviceFeeList)
            {
                var serviceFeeResp = new ServiceFeeResp();
                var type = await _context.ServiceFeeType.FirstOrDefaultAsync(s => s.Id == serviceFee.TypeId);
                serviceFeeResp.Id = serviceFee.Id;
                serviceFeeResp.Name = type.Name;
                serviceFeeResp.PricePerUnit = type.PricePerUnit;
                serviceFeeResp.MeasuringUnit = type.MeasuringUnit;
                serviceFeeResp.OldCount = serviceFee.OldCount;
                serviceFeeResp.NewCount = serviceFee.NewCount;
                serviceFeeResp.TotalFee = serviceFee.TotalFee;
                feeResp.ServiceFee += serviceFee.TotalFee;
                if (type.ServiceFeeTypeCode == "ST001") feeResp.ElectricityFee = serviceFeeResp.TotalFee;
                else if (type.ServiceFeeTypeCode == "ST002") feeResp.WaterFee = serviceFeeResp.TotalFee;
                list.Add(serviceFeeResp);
            }
            feeResp.ServiceFeeList = list;
            foreach (var payment in paymentList)
            {
                var fpr = new FeePaymentResp();
                fpr.Id = payment.Id;
                fpr.Amount = payment.Amount;
                fpr.CreatedTime = payment.CreatedTime;
                flist.Add(fpr);
            }
            feeResp.FeePaymentList = flist;
            foreach(var vehicle in  vehicleList)
            {
                var r = await _context.Resident.FirstOrDefaultAsync(r => r.Id == vehicle.OwnerId);
                if (r.Status == ResidentStatus.Deleted) continue;
                var t = await _context.VehicleType.FirstOrDefaultAsync(v => v.Id == vehicle.VehicleTypeId);
                var vr = new VehicleResp(vehicle, r, t);
                vlist.Add(vr);
            }
            feeResp.VehicleList = vlist;
            feeResp.TotalFee = feeResp.ParkingFee + feeResp.ServiceFee;
            if (feeResp.ReceivedAmount >= feeResp.TotalFee) fee.Status = FeeStatus.Paid;
            else if (fee.ExpiredDate <= await _timeConverterService.ConvertToUTCTime(DateTime.Now)) fee.Status = FeeStatus.Expired;
            feeResp.Status = fee.Status;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
            return feeResp;
        }
        public async Task<ApiResponseExpose<FeePayment>> AddFeePayment(AppUser user, FeePaymentCreateReq req)
        {
            var resp = new ApiResponseExpose<FeePayment>();
            var fee = await _context.Fee.FirstOrDefaultAsync(f => f.Id == req.Id);
            var feePayment = new FeePayment();
            feePayment.FeeId = req.Id;
            feePayment.Amount = req.Amount;
            feePayment.CreatorId = user.Id;
            feePayment.CreatedTime = await _timeConverterService.ConvertToUTCTime(req.CreatedTime);
            feePayment.Status = FeePaymentStatus.Active;
            fee.ReceivedAmount += req.Amount;
            try
            {
                await _context.FeePayment.AddAsync(feePayment);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra";
                return resp;
            }
            resp.code = 1;
            resp.message = "Thu phí thành công";
            resp.entity = feePayment;
            return resp;
        }
        public async Task<ApiResponseExpose<List<ServiceFeeResp>>> UpdateServiceFeeInformation(AppUser user, List<ServiceFee> req)
        {
            var resp = new ApiResponseExpose<List<ServiceFeeResp>>();
            var list = new List<ServiceFeeResp>();
            foreach (var fee in req)
            {
                var serviceFee = await _context.ServiceFee.FirstOrDefaultAsync(f => f.Id == fee.Id && f.Status != ServiceFeeStatus.Deleted);
                if (serviceFee == null)
                {
                    resp.code = -1;
                    resp.message = "Đã có lỗi xảy ra trong quá trình tìm kiếm bản thu phí";
                    return resp;
                }
                var type = await _context.ServiceFeeType.FirstOrDefaultAsync(s => s.Id == fee.TypeId);
                serviceFee.OldCount = fee.OldCount;
                serviceFee.NewCount = fee.NewCount;
                serviceFee.TotalFee = type.PricePerUnit * (fee.NewCount - fee.OldCount);
                var serviceFeeResp = new ServiceFeeResp();
                serviceFeeResp.Id = serviceFee.Id;
                serviceFeeResp.Name = type.Name;
                serviceFeeResp.PricePerUnit = type.PricePerUnit;
                serviceFeeResp.MeasuringUnit = type.MeasuringUnit;
                serviceFeeResp.OldCount = serviceFee.OldCount;
                serviceFeeResp.NewCount = serviceFee.NewCount;
                serviceFeeResp.TotalFee = serviceFee.TotalFee;
                list.Add(serviceFeeResp);
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                resp.code = -1;
                resp.message = "Đã có lỗi xảy ra trong quá trình cập nhật thông tin bản thu phí";
                return resp;
            }
            resp.code = 1;
            resp.message = "Cập nhật thông tin bản thu phí thành công";
            resp.entity = list;
            return resp;
        }
    }
}
