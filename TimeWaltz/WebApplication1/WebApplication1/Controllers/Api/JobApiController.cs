using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers.Api
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class JobApiController : ControllerBase
    {
        private readonly TimeWaltzContext _timeWaltzContext;
        private readonly SpecialHolidayDaysService _specialHolidayDaysService;

        public JobApiController(TimeWaltzContext timeWaltzContext, SpecialHolidayDaysService specialHolidayDaysService)
        {
            _timeWaltzContext = timeWaltzContext;
            this._specialHolidayDaysService = specialHolidayDaysService;
        }

        public void KeepUpdateSpecialHolidayDays()
        {
            RecurringJob.AddOrUpdate(
               "SpecialHolidayDaysJob",
               () => _specialHolidayDaysService.CountSpecialHolidayDaysAnniversary()               
               ,
               Cron.Daily);
        }


       
        
    }
}
