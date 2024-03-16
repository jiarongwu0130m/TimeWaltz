using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.Entity;

namespace WebApplication1.Controllers.Api
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class SpecialHolidayApi : ControllerBase
    {
        private readonly TimeWaltzContext _timeWaltzContext;

        public SpecialHolidayApi(TimeWaltzContext timeWaltzContext)
        {
            _timeWaltzContext = timeWaltzContext;
        }
        public void CountSpecialHolidayDaysAnniversary()
        {
            var userId = 1;
            var emp = _timeWaltzContext.Employees.FirstOrDefault(x => x.Id == userId);
            if(emp != null)
            {
                DateTime Today = DateTime.Now;
                var serviceDays = Today - emp.HireDate;
                _timeWaltzContext.SpecialGrade.Where(s => s.ServiceLength == serviceDays.Days).Select(x => new
                {

                });
            }
            
        }
        public void CountSpecialHolidayDaysYear()
        {

        }
    }
}
