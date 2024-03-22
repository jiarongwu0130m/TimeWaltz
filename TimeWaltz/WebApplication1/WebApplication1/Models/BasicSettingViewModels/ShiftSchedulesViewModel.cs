
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.BasicSettingViewModels
{
    public class ShiftScheduleEditDto
    {
        public int Id { get; set; }
        public string ShiftsName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int MaxAdditionalClockIn { get; set; }

    }
    public class ShiftScheduleCreateDto
    {
        public string ShiftsName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int MaxAdditionalClockIn { get; set; }

    }
    public class ShiftSchedulesViewModel
    {
        public int Id { get; set; }
        [Required]
        public string ShiftName { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime Date { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double BreakTimeHours { get; set; }
        public int MaxAdditionalClockIn { get; set; }

    }
    public class ShiftSchedulesCreateViewModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("班別名稱")]
        public string ShiftName { get; set; }
        [DisplayName("開始時間")]
        public DateTime StartTime { get; set; }
        [DisplayName("結束時間")]
        public DateTime EndTime { get; set; }
        [DisplayName("休息時數")]
        public double BreakTimeHours { get; set; }
        [DisplayName("最大打卡次數")]
        public int MaxAdditionalClockIn { get; set; }

    }
    public class ShiftSchedulesEditViewModel
    {
        public int Id { get; set; }
        [Required]
        public string ShiftName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double BreakTimeHours { get; set; }
        public int MaxAdditionalClockIn { get; set; }

    }

}
