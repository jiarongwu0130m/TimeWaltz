﻿using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class QueryController : Controller
    {
        public IActionResult Overtime()
        {
            return View();
        }

        public IActionResult Complement()
        {
            return View();
        }

        public IActionResult ShiftSchedule()
        {
            return View();
        }

        public IActionResult Leave()
        {
            return View();
        }
        public IActionResult LeaveDetail()
        {
            return View();
        }
        public IActionResult Attendance()
        {
            return View();
        }
    }
}
