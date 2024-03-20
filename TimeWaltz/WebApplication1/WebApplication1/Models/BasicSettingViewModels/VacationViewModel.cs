using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Repository.Enums;

namespace WebApplication1.Models.BasicSettingViewModels
{


    public class VacationViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "請填假別名稱")]
        public string VacationType { get; set; }
        public string? QueryVacationType { get; set; }
        public GenderLimitEnum? Gender { get; set; }
        public List<SelectListItem>? GenderSelectItems { get; set; }

        public int NumberOfDays { get; set; }

        public List<SelectListItem>? CycleSelectItems { get; set; }
        public CycleEnum Cycle { get; set; }


        public int MinVacationHours { get; set; }
    }

    public class VacationCreateViewModel
    {
        [Required(ErrorMessage = "請填假別名稱")]
        public string VacationType { get; set; }
        public string? QueryVacationType { get; set; }
        public GenderLimitEnum? Gender { get; set; }
        public List<SelectListItem>? GenderSelectItems { get; set; }

        public int NumberOfDays { get; set; }

        public List<SelectListItem>? CycleSelectItems { get; set; }
        public CycleEnum Cycle { get; set; }


        public int MinVacationHours { get; set; }
    }
    public class VacationEditViewModel
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "請填假別名稱")]
        public string VacationType { get; set; }
        public string? QueryVacationType { get; set; }
        public GenderLimitEnum? Gender { get; set; }
        public List<SelectListItem>? GenderSelectItems { get; set; }

        public int NumberOfDays { get; set; }

        public List<SelectListItem>? CycleSelectItems { get; set; }
        public CycleEnum Cycle { get; set; }


        public int MinVacationHours { get; set; }
    }
}
