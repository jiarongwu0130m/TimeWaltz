using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Enums;
using Repository.Models;
using WebApplication1.Areas.Employee.Models;
using WebApplication1.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClockApiController : ControllerBase
    {
        private readonly TimeWaltzContext _timeWaltzDb;

        public ClockApiController(TimeWaltzContext timeWaltzDb)
        {
            _timeWaltzDb = timeWaltzDb;
        }
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
        [HttpPost]
        public bool On(ClockOnDto dto)
        {
            return OnAndOff(dto, ClockStatusEnum.上班打卡);
        }

        [HttpPost]
        public bool Off(ClockOnDto dto)
        {
            return OnAndOff(dto, ClockStatusEnum.下班打卡);
        }


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

        // GET: api/<ClockApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ClockApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ClockApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ClockApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ClockApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

