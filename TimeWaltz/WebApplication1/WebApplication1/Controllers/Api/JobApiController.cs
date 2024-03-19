using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.Entity;
using WebApplication1.Services;

namespace WebApplication1.Controllers.Api
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class JobApiController : ControllerBase
    {
        private readonly TimeWaltzContext _timeWaltzContext;
        private readonly SpecialHolidayDaysService _specialHolidayDaysService;
        private readonly ShiftService _shiftService;

        public JobApiController(TimeWaltzContext timeWaltzContext, SpecialHolidayDaysService specialHolidayDaysService, ShiftService shiftService)
        {
            _timeWaltzContext = timeWaltzContext;
            _specialHolidayDaysService = specialHolidayDaysService;
            _shiftService = shiftService;
        }

        public void KeepUpdateSpecialHolidayDays()
        {
            RecurringJob.AddOrUpdate(
               "SpecialHolidayDaysJob",
               () => _specialHolidayDaysService.CountSpecialHolidayDaysAnniversary()               
               ,
               Cron.Daily);
        }
        public void KeepUpdateShift()
        {
            RecurringJob.AddOrUpdate(
               "SpecialHolidayDaysJob",
               () => _shiftService.GetAllShiftData()
               ,
               Cron.Monthly(10));
        }




    }
}
