using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Helpers;
using WebApplication1.Models;

namespace WebApplication1.Controllers.Api
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class PersonalDataApiController : ControllerBase
    {
        public CreatePersonalDataViewModel DropDownList()
        {
            var model = new CreatePersonalDataViewModel
            {
                GenderSelectItems = DropDownHelper.GetGenderDropDownList(),                
            };
            return model;
        }
    }
}
