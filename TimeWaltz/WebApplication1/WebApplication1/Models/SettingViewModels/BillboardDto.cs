namespace WebApplication1.Models.SettingViewModels
{
    public class BillboardDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int EmployeeId { get; set; }
        public string? Content { get; set; }



    }
    public class BillboardEditDto
    {
        public int Id { get; set; }
        public int EmployeesID { get; set; }

        public string Title { get; set; }

        public string? Content { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string? PathRoute { get; set; }
    }

    public class BillboardCreat
    {
        public int EmployeesID { get; set; }

        public string Title { get; set; }

        public string? Content { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string? PathRoute { get; set; }
    }
}
