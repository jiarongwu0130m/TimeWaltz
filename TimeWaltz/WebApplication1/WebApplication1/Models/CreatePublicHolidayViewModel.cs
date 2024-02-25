using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class CreatePublicHolidayViewModel
    {
        public string HolidayName { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime Date { get; set; }

    }
}
