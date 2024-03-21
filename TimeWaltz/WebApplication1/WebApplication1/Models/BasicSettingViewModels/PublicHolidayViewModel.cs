using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.BasicSettingViewModels
{
    public class PublicHolidayCreateViewModel
    {
        public string HolidayName { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime Date { get; set; }

    }

    public class PublicHolidayEditDto
    {
        public int Id { get; set; }
        public string HolidayName { get; set; }
        public DateTime Date { get; set; }
    }
    public class PublicHolidayCreateDto
    {
        public string HolidayName { get; set; }
        public DateTime Date { get; set; }
    }
    public class PublicHolidayDto
    {
        public int Id { get; set; }
        public string HolidayName { get; set; }
        public string Date { get; set; }
    }
    public class PublicHolidayViewModel
    {
        public int Id { get; set; }
        public string HolidayName { get; set; }
        public string? QueryHolidayName { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime Date { get; set; }
    }
    public class PublicHolidayEditViewModel
    {
        public int Id { get; set; }
        public string HolidayName { get; set; }
        public string? QueryHolidayName { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime Date { get; set; }
    }
}
