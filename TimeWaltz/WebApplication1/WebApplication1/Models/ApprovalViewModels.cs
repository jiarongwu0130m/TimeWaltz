using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Repository.Enum;

namespace WebApplication1.Models
{
    public class ApprovaViewModel
    {
        public int Id { get; set; }

        public int TableType { get; set; }

        [Column("TableID")]
        public int TableId { get; set; }

    }
    public class ApprovaCreateDto
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string? Remark { get; set; }

        public RequestStatusEnum? Status { get; set; }
    }
    public class ApprovaEditDto
    {
        public int Id { get; set; }
        public int Status { get; set; }
        
        [StringLength(50)]
        public string? Remark { get; set; }

    }


}
