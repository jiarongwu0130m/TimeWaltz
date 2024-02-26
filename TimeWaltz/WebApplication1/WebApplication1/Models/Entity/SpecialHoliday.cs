using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models.Enums;

namespace WebApplication1.Models.Entity
{
    public class SpecialHoliday
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public HowToGiveEnum? HowToGive {  get; set; }
        public int? GiveDay { get; set; }
    }
}
