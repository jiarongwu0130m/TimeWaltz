namespace WebApplication1.Models.PersonalRecordViewModels
{
    public class AttendanceDto
    {
        public DateTime ClockInTime { get; set; }
        public DateTime ClockOutTime { get; set; }
        public DateTime ShiftDate { get; set; }
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }
        public string AttendanceStatusEnum { get; set; }
    }
    public class AttendanceViewModel
    {
        
        /// <summary>
        /// 打卡時間-Start
        /// </summary>
        public DateTime ClockInTime { get; set; }
        /// <summary>
        /// 打卡時間-end
        /// </summary>
        public DateTime ClockOutTime { get; set; }

        /// <summary>
        /// 班別日期
        /// </summary>
        public DateTime ShiftDate { get; set; }
        /// <summary>
        /// 班別時間-Start
        /// </summary>
        public DateTime ShiftStart { get; set; }
        /// <summary>
        /// 班別日期時間-end
        /// </summary>
        public DateTime ShiftEnd { get; set; }


        /// <summary>
        /// 出缺勤狀態狀態
        /// </summary>
        public string AttendanceStatus { get; set; }
        /// <summary>
        /// 出勤日期
        /// </summary>
        public string AttendanceDate { get; set; }


    }
}
