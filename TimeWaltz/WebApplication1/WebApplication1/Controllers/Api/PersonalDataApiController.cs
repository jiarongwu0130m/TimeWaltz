using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Helpers;
using WebApplication1.Models.BasicSettingViewModels;

namespace WebApplication1.Controllers.Api
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class PersonalDataApiController : ControllerBase
    {
        public PersonalDataCreateViewModel DropDownList()
        {
            var model = new PersonalDataCreateViewModel
            {
                GenderSelectItems = DropDownHelper.GetGenderDropDownList(),                
            };
            return model;
        }
    }
}
