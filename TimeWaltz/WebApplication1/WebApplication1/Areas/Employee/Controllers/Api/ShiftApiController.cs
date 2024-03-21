using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using WebApplication1.Helpers;
using WebApplication1.Models.BasicSettingViewModels;
using WebApplication1.Services;

namespace WebApplication1.Areas.Employee.Controllers.Api
{
    [Route("mobile/api/Shift/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "EmployeeAuthScheme")]
    public class ShiftApiController : ControllerBase
    {
        private readonly TimeWaltzContext _timeWaltzDb;

        public ShiftApiController(TimeWaltzContext timeWaltzDb)
        {
            _timeWaltzDb = timeWaltzDb;
        }

        [HttpGet]
        public IActionResult Schedule()
        {
            var shiftsResult = _timeWaltzDb.Shifts.AsNoTracking()
                .Include(x => x.ShiftSchedule)
                .Where(x => x.EmployeesId == User.GetId())
                .Select(x=> new
                {
                    Id = x.Id,
                    Start = x.ShiftsDate.Date + x.ShiftSchedule.StartTime.TimeOfDay,
                    End = x.ShiftsDate.Date + x.ShiftSchedule.EndTime.TimeOfDay,
                    AllDay = false,
                    Title = x.ShiftSchedule.ShiftsName
                });

            return Ok(shiftsResult);

        }
    }
}
