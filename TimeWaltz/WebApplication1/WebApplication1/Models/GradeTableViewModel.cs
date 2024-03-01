namespace WebApplication1.Models
{
    public class GradeTableViewModel
    {
        public int Id { get; set; }
        public int ServiceLength { get; set; }
        public int Days { get; set; }
    }
    public class CreateGradeTableViewModel
    {
        public int ServiceLength { get; set; }
        public int Days { get; set; }
    }
    public class EditGradeTableViewModel
    {
        public int Id { get; set; }
        public int ServiceLength { get; set; }
        public int Days { get; set; }
    }
}
