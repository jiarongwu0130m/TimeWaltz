using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Helpers;
using WebApplication1.Models.Dto;
using WebApplication1.Models.ViewModel;
using WebApplication1.Services;

namespace WebApplication1.Controllers.Api
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class PersonalDataApiController : ControllerBase
    {
        private readonly PersonalDataService _personalDataService;

        public PersonalDataApiController(PersonalDataService personalDataService)
        {
            _personalDataService = personalDataService;
        }
        public GenderDropDownDto DropDownList()
        {
            var model = new GenderDropDownDto
            {
                GenderSelectItems = DropDownHelper.GetGenderDropDownList(),                
            };
            return model;
        }

        public PersonalDataDto EmployeeAndShiftData()
        {
            var entity = _personalDataService.GetEmployeeAndShiftData();
        }
    }
}
