using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models.Enums;

namespace WebApplication1.Models.Dto
{
    public class VacationDto
    {
        public int Id { get; set; }
        public string? VacationType { get; set; }
        public string? Gender { get; set; }
        public string Cycle { get; set; }
        public int NumberOfDays { get; set; }
        public int MinVacationHours { get; set; }


    }
    public class VacationCreateDto
    {

        public string VacationType { get; set; }
        public GenderLimitEnum? Gender { get; set; }
        public CycleEnum Cycle { get; set; }
        public int NumberOfDays { get; set; }
        public int MinVacationHours { get; set; }


    }
    public class VacationEditDto
    {
        public int Id { get; set; }
        public string? VacationType { get; set; }
        public GenderLimitEnum? Gender { get; set; }
        public CycleEnum Cycle { get; set; }
        public int NumberOfDays { get; set; }
        public int MinVacationHours { get; set; }


    }
    public class VacationDropDownDto
    {
        public List<SelectListItem>? GenderSelectItems { get; set; }
        public List<SelectListItem>? CycleSelectItems { get; set; }
        

    }
}
