using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication1.Models.Dto
{
    public class PersonalDataDto
    {
        public int Id { get; set; }
        
    }

    public class GenderDropDownDto
    {
        public List<SelectListItem> GenderSelectItems { get; set; }
    }
}
