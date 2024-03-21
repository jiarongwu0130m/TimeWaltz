using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Enums;
using Repository.Models;
using System.Security.Claims;
using WebApplication1.Areas.Employee.Models;
using WebApplication1.Filters;
using WebApplication1.Helpers;
using WebApplication1.Models.ApplicationFormViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "EmployeeAuthScheme")]
    public class ClockApiController : ControllerBase
    {
        private readonly TimeWaltzContext _timeWaltzDb;

        public ClockApiController(TimeWaltzContext timeWaltzDb)
        {
            _timeWaltzDb = timeWaltzDb;
        }
        /// <summary>
        /// 取得當日班表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public object ShiftSchedule()
        {
            var empId = User.GetId();
            var shift = _timeWaltzDb.Shifts.Include(shift => shift.ShiftSchedule).FirstOrDefault(x => x.EmployeesId == empId && EF.Functions.DateDiffDay(x.ShiftsDate, DateTime.Now) == 0);
            if (shift == null) return new { onTime = "", offTime = "" };
            return new
            {
                onTime = shift.ShiftSchedule.StartTime,
                offTime = shift.ShiftSchedule.EndTime,
            };

        }

        /// <summary>
        /// 打卡
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public object Info()
        {
            var empId = User.GetId();
            var allClock = _timeWaltzDb.Clocks.Where(x => x.EmployeesId == empId &&
            EF.Functions.DateDiffDay(x.Date, DateTime.Now) == 0).AsEnumerable();

            return new
            {
                On = allClock.Where(x => x.Status == ClockStatusEnum.上班打卡).MinBy(x => x.Date),
                Off = allClock.Where(x => x.Status == ClockStatusEnum.下班打卡).MaxBy(x => x.Date),
            };

        }
        /// <summary>
        /// 回傳上班打卡資訊
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public bool On(ClockOnDto dto)
        {
            return OnAndOff(dto, ClockStatusEnum.上班打卡);
        }

        /// <summary>
        /// 回傳下班打卡資訊
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public bool Off(ClockOnDto dto)
        {
            return OnAndOff(dto, ClockStatusEnum.下班打卡);
        }

        /// <summary>
        /// 處理上/下班打卡資訊
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [NonAction]
        public bool OnAndOff(ClockOnDto dto, ClockStatusEnum status)
        {
            var empId = User.GetId();

            try
            {
                _timeWaltzDb.Clocks.Add(new Clock
                {
                    Date = DateTime.Now,
                    EmployeesId = empId,
                    Latitude = dto.Latitude,
                    Longitude = dto.Longitude,
                    Status = status
                });

                _timeWaltzDb.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public CompRequestEmpIdNameGet GetEmployeeName()
        {

            var UserId = User.GetId();
            var userName = _timeWaltzDb.Employees.Where(x => x.Id == UserId).FirstOrDefault();
            
            string approvalEmployeeName = _timeWaltzDb.Employees.Where(
                x => x.Id == _timeWaltzDb.Employees.Where(x => x.Id == UserId).Select(x => x.Department.EmployeeId).FirstOrDefault()).Select(x=>x.Name).FirstOrDefault();
            var UserAndName = new CompRequestEmpIdNameGet
            {
                EmployeeId = UserId,
                EmployeeName = userName.Name,
                approvalEmployeeName= approvalEmployeeName,

            };

            return UserAndName;
        }
        [HttpGet("{id}")]
        public IActionResult GetEmpClocks(int id, [FromQuery] DateTime date)
        {
            var clockResult = _timeWaltzDb.Clocks
                .Where(x => x.EmployeesId == id && x.Date.Date == date);

            if (clockResult == null)
            {
                return NotFound();
            }

            return Ok(clockResult);
        }
    }
}

