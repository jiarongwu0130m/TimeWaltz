using WebApplication1.Models.Enums;

namespace WebApplication1.Models
{
    public class SpecialHolidayViewModel
    {
        public int Id { get; set; }
        public HowToGiveEnum? HowToGive { get; set; }
        public int? GiveDay { get; set; }
    }
    public class CreateSpecialHolidayViewModel
    {
        public HowToGiveEnum? HowToGive { get; set; }
        public int? GiveDay { get; set; }
    }
    public class EditSpecialHolidayViewModel
    {
        public int Id { get; set; }
        public HowToGiveEnum? HowToGive { get; set; }
        public int? GiveDay { get; set; }
    }
}
