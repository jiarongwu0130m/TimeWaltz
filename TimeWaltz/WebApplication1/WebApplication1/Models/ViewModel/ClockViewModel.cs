using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.Enums;

namespace WebApplication1.Models.ViewModel
{
    public class ClockViewModel
    {
        public int EmployeesId { get; set; }

        [Display(Name = "打卡時間")]
        public DateTime Date { get; set; }

        public ClockStatusEnum Status { get; set; }

        [Display(Name = "緯度")]
        public decimal Latitude { get; set; }

        [Display(Name = "經度")]
        public decimal Longitude { get; set; }

        #region
        public DateTime? StartClockInDate { get; set; }
        public decimal? StartClockInLongitude { get; set; }
        public decimal? StartClockInLatitude { get; set; }

        public DateTime? EndClockInDate { get; set; }
        public decimal? EndClockInLongitude { get; set; }
        public decimal? EndClockInLatitude { get; set; }
        #endregion

        public DateTime ShiftsDate { get; set; }
        public TimeSpan ShiftScheduleStartTime { get; set; }
        public TimeSpan ShiftScheduleEndTime { get; set; }

    }
}
