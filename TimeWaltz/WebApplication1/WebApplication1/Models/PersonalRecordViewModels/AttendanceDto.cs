namespace WebApplication1.Models.PersonalRecordViewModels
{
    public class AttendanceDto
    {
        public DateTime ClockInTime { get; set; }
        public DateTime ClockOutTime { get; set; }
        public DateTime ShiftDate { get; set; }
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }
        public string AttendanceStatusEnum { get; set;}
    }
}
