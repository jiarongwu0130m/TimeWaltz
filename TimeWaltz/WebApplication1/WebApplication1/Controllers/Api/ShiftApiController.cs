using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.Entity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShiftApiController : ControllerBase
    {
        private readonly TimeWaltzContext _timeWaltzDb;

        public ShiftApiController(TimeWaltzContext timeWaltzDb)
        {
            _timeWaltzDb = timeWaltzDb;
        }

        [HttpGet("{id}")]
        public IActionResult GetEmpShifts(int id, [FromQuery] DateTime date)
        {
            var shiftsResult = _timeWaltzDb.Shifts
                .Where(x => x.EmployeesId == id && x.ShiftsDate == date)
                .Include(x => x.ShiftSchedule)
                .ToList();

            if (shiftsResult.Count == 0)
            {
                return NotFound();
            }

            foreach (var shift in shiftsResult)
            {
                shift.ShiftScheduleStartTime = shift.ShiftSchedule.StartTime;
                shift.ShiftScheduleEndTime = shift.ShiftSchedule.EndTime;
                shift.ShiftsDate = shift.ShiftsDate.Date;
            }
            return Ok(shiftsResult);
        }

        // GET: api/<ShiftApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ShiftApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ShiftApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ShiftApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ShiftApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
