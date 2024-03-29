﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using WebApplication1.Helpers;
using WebApplication1.Models.BasicSettingViewModels;
using WebApplication1.Services;

namespace WebApplication1.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShiftScheduleApiController : ControllerBase
    {
        private readonly TimeWaltzContext _context;
        private readonly ShiftScheduleService _service;

        public ShiftScheduleApiController(TimeWaltzContext context,ShiftScheduleService service)
        {
            _context = context;
            _service = service;
        }
        public List<ShiftSchedulesViewModel> getShiftSchedule()
        {

            var entities = _service.GetShiftScheduleList();
            var models = EntityHelper.ToViewModel(entities);

            return models;
        }

        [HttpPost]
        public ActionResult Create(ShiftSchedulesCreateViewModel dto)
        {
            try
            {
                var entity = new ShiftSchedule
                {
                    ShiftsName = dto.ShiftsName,
                    StartTime = dto.StartTime,
                    EndTime = dto.EndTime,
                    MaxAdditionalClockIn = dto.MaxAdditionalClockIn,
                };
                _service.CreateShiftSchedule(entity);
                return Ok(new { status = true });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false });
            }
        }
        [HttpGet]
        [Route("{id}")]
        public ShiftScheduleEditDto GetEditData(int id)
        {
            var entity = _context.ShiftSchedules.FirstOrDefault(x => x.Id == id);
            var model = new ShiftScheduleEditDto
            {
                EndTime = entity.EndTime.ToString("HH:mm"),
                Id = entity.Id,
                MaxAdditionalClockIn = entity.MaxAdditionalClockIn,
                ShiftsName = entity.ShiftsName,
                StartTime = entity.StartTime.ToString("HH:mm"),
            };
            return model;
        }
        [HttpPost]
        [Route("{id}")]
        public ActionResult Edit(ShiftScheduleInputEditDto dto)
        {
            try
            {
                var entity = _context.ShiftSchedules.FirstOrDefault(x => x.Id == dto.Id);
                entity.ShiftsName = dto.ShiftsName;
                entity.StartTime = dto.StartTime;
                entity.EndTime = dto.EndTime;
                entity.MaxAdditionalClockIn = dto.MaxAdditionalClockIn;
                _context.SaveChanges();
                return Ok(new { status = true });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false });
            }
        }
        [HttpPost]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var entity = _context.ShiftSchedules.FirstOrDefault(x => x.Id == id);
                _context.ShiftSchedules.Remove(entity);
                _context.SaveChanges();
                return Ok(new {status = true});
            }
            catch (Exception ex) { 
                return Ok(new {status = false});
            }

        }
    }
}
