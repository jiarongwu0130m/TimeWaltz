namespace WebApplication1.Models.BasicSettingViewModels
{
    public class SpecialGradeViewModel
    {
        public int Id { get; set; }
        public int ServiceLength { get; set; }
        public int Days { get; set; }
    }
    public class SpecialGradeCreateViewModel
    {
        public int ServiceLength { get; set; }
        public int Days { get; set; }
    }
    public class SpecialGradeEditViewModel
    {
        public int Id { get; set; }
        public int ServiceLength { get; set; }
        public int Days { get; set; }
    }
}
