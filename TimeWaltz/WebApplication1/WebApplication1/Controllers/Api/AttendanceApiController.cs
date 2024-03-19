using Microsoft.AspNetCore.Mvc;
using WebApplication1.Helpers;
using WebApplication1.Models.Entity;
using WebApplication1.Services;


namespace WebApplication1.Controllers.Api
{
    public class AttendanceApiController
    {
        private readonly AttendanceService _attendanceService;

        public AttendanceApiController(AttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }
        //[HttpGet]
        //public ActionResult GetAttendanceData()
        //{
        //    var userId = 1;
        //    var entities = _attendanceService.GetAttendanceData(userId);

        //}
    }
}
