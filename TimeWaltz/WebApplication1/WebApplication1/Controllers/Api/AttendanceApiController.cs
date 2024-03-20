using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.PersonalRecordViewModels;
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


        [HttpGet]
        public AttendanceViewModel GetAttendanceData()
        {
            var userId = 1;
            return  _attendanceService.GetAttendanceData(userId);

        }
    }
}
