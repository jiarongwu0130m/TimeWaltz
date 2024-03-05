using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.ViewModel
{
    public class PublicHolidayCreateViewModel
    {
        public string HolidayName { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime Date { get; set; }

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
