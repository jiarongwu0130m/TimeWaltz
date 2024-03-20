namespace WebApplication1.Models.BasicSettingViewModels
{
    public class SpecialVacationDto
    {
        public int Id { get; set; }

        public string SpecialVacationName { get; set; }

        public string Date { get; set; }
    }
    public class SpecialVacationCreateDto
    {
        public string SpecialVacationName { get; set; }

        public DateTime Date { get; set; }
    }
    public class SpecialVacationEditDto
    {
        public int Id { get; set; }
        public string SpecialVacationName { get; set; }

        public DateTime Date { get; set; }
    }
}
