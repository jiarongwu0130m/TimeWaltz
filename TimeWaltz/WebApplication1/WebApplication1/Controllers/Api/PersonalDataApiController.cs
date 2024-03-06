using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Helpers;
using WebApplication1.Models.BasicSettingViewModels;
using WebApplication1.Models.Entity;
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
        /// <summary>
        /// 拿到性別下拉選單資料
        /// </summary>
        /// <returns></returns>
        public GenderDropDownDto DropDownList()
        {
            var model = new GenderDropDownDto
            {
                GenderSelectItems = DropDownHelper.GetGenderDropDownList(),
            };
            return model;
        }

        public List<Employee> GetPersonalData()
        {
            var entity = _personalDataService.GetPersonalDataList();
            return entity;
        }

        /// <summary>
        /// 給個人資料設定畫面使用，取得員工的資料和員工班別資料
        /// </summary>
        /// <returns></returns>
        public List<PersonalDataDto> EmployeeAndShiftData()
        {

            var entity = _personalDataService.GetEmployeeAndShiftData();
            var model = EntityHelper.ToViewModel(entity);
            return model;
        }
        /// <summary>
        /// 給個人資料設定畫面使用，取得員工部門
        /// </summary>
        /// <returns></returns>
        public List<string> GetDepName()
        {
            return  _personalDataService.GetDepartmentDropDownData().Select(d => d.DepartmentName).ToList();
        }
        
    }
}
