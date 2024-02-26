using WebApplication1.Models.Enums;

namespace WebApplication1.Models
{
    public class FlextimeViewModel
    {
        public int Id { get; set; }

        public int? FlexibleTime { get; set; }
                
        public bool MoveUp { get; set; }
    }
}