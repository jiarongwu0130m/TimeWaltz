namespace WebApplication1.Models.SettingViewModels
{
    public class BillboardDto
    {
    }

    public class BillboardCreat
    {
        public string Title { get; set; }

        public string? Content { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string? PathRoute { get; set; }
    }
}
