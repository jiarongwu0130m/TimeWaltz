using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using WebApplication1.Helpers;
using WebApplication1.Models.ApplicationFormViewModels;
using WebApplication1.Models.BasicSettingViewModels;
using WebApplication1.Services;

namespace WebApplication1.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OvertimeApplicationsApiController : ControllerBase
    {
        private readonly TimeWaltzContext _context;
        private readonly OvertimeRequestService _overtimeRequestService;

        public OvertimeApplicationsApiController(TimeWaltzContext context, OvertimeRequestService overtimeRequestService)
        {
            _context = context;
            _overtimeRequestService = overtimeRequestService;
        }

        // GET: api/OvertimeApplicationsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OvertimeApplication>>> GetOvertimeApplications()
        {
            if (_context.OvertimeApplications == null)
            {
                return NotFound();
            }
            return await _context.OvertimeApplications.ToListAsync();
        }



        [HttpPost]
        public bool Create(OvertimeRequestDto dto)
        {
            try
            {
                var entity = ViewModelHelper.ToEntity(dto);
                _overtimeRequestService.CreateOvertimeRequest(entity);              
                return true;
            }
            catch (Exception) {
                return false;
            }
        }            


        [HttpGet]
        [Route("{id}")]
        public OvertimeViewModel GetOvertimeRequestData(int id)
        {
            try
            {
                var entity = _overtimeRequestService.GetOvertimeRequestTypeOrNull(id);
                var model = EntityHelper.ToViewModel(entity);
                return model;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet]
        public EmpIdNameGet GetEmoloyeeName()
        {
            var UserId = 1;
            var userNameAndIdPare = _overtimeRequestService.GetNameOrNull(UserId);
            var employee = EntityHelper.GetNameAndIdPare(userNameAndIdPare);


            return employee;
        }

        // GET: api/OvertimeApplicationsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OvertimeApplication>> GetOvertimeApplication(int id)
        {
          if (_context.OvertimeApplications == null)
          {
              return NotFound();
          }
            var overtimeApplication = await _context.OvertimeApplications.FindAsync(id);

            if (overtimeApplication == null)
            {
                return NotFound();
            }

            return overtimeApplication;
        }

        [HttpGet]
        public List<OvertimeViewModel> GetOvertimeList()
        {
            var entities = _overtimeRequestService.GetOvertimeList();
            var models = EntityHelper.ToViewModel(entities);
            return models;
        }

        // PUT: api/OvertimeApplicationsApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOvertimeApplication(int id, OvertimeApplication overtimeApplication)
        {
            if (id != overtimeApplication.Id)
            {
                return BadRequest();
            }

            _context.Entry(overtimeApplication).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OvertimeApplicationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/OvertimeApplicationsApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OvertimeApplication>> PostOvertimeApplication(OvertimeApplication overtimeApplication)
        {
          if (_context.OvertimeApplications == null)
          {
              return Problem("Entity set 'TimeWaltzContext.OvertimeApplications'  is null.");
          }
            _context.OvertimeApplications.Add(overtimeApplication);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOvertimeApplication", new { id = overtimeApplication.Id }, overtimeApplication);
        }

        // DELETE: api/OvertimeApplicationsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOvertimeApplication(int id)
        {
            if (_context.OvertimeApplications == null)
            {
                return NotFound();
            }
            var overtimeApplication = await _context.OvertimeApplications.FindAsync(id);
            if (overtimeApplication == null)
            {
                return NotFound();
            }

            _context.OvertimeApplications.Remove(overtimeApplication);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OvertimeApplicationExists(int id)
        {
            return (_context.OvertimeApplications?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
