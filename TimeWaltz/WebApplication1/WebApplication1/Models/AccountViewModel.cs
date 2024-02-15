using WebApplication1.Models.Entity;

namespace WebApplication1.Models
{
    public class AccountViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string DepartmentName { get; set; } = null!;

        public string Account { get; set; } = null!;

        public string Password { get; set; } = null!;

        public bool Stop { get; set; }

    }
}
