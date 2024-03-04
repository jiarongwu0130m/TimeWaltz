using WebApplication1.Models.Enums;

namespace WebApplication1.Models
{
    public class EditVacationDto
    {
        public string? VacationType { get; set; }
        public GenderLimitEnum? Gender { get; set; }
        public CycleEnum Cycle { get; set; }
        public int NumberOfDays { get; set; }
        public int MinVacationHours { get; set; }

        
    }
}
