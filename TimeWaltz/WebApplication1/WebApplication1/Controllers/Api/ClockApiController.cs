﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.Entity;

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