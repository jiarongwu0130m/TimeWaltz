using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Helpers;
using WebApplication1.Models;

namespace WebApplication1.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VacationDetailsApiController : ControllerBase
    {
        public CreateVacationTypeViewModel DropDownList()
        {
            var model = new CreateVacationTypeViewModel
            {
                GenderSelectItems = DropDownHelper.GetGenderDropDownList(),
                CycleSelectItems = DropDownHelper.GetCycleDropDownList()
            };
            return model;
        }
    }
}
