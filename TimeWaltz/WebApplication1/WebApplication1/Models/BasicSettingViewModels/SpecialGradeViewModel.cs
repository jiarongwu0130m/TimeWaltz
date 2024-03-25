using System.ComponentModel;

namespace WebApplication1.Models.BasicSettingViewModels
{
    public class SpecialGradeEditDto
    {
        public int Id { get; set; }
        public decimal ServiceLength { get; set; }
        public int Days { get; set; }
    }
    public class SpecialGradeCreateDto
    {
        public decimal ServiceLength { get; set; }
        public int Days { get; set; }
    }
    public class SpecialGradeDto
    {
        public int Id { get; set; }
        public decimal ServiceLength { get; set; }
        public int Days { get; set; }
    }
    public class SpecialGradeViewModel
    {
        public int Id { get; set; }
        public decimal ServiceLength { get; set; }
        public int Days { get; set; }
    }
    public class SpecialGradeCreateViewModel
    {
        [DisplayName("服務年資")]
        public int ServiceLength { get; set; }
        [DisplayName("特休天數")]
        public int Days { get; set; }
    }
    public class SpecialGradeEditViewModel
    {
        public int Id { get; set; }
        public decimal ServiceLength { get; set; }
        public int Days { get; set; }
    }
}
