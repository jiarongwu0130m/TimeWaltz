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
        /// <summary>
        /// 給個人資料畫面用，取得個人資料
        /// </summary>
        /// <returns></returns>
        public List<PersonalDataDto> GetPersonalData()
        {
            var entities = _personalDataService.GetPersonalDataList();
            var models = EntityHelper.ToPersonalListDto(entities);
            return models;
        }

        /// <summary>
        /// 取得部門、班別下拉式選單資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<DepAndShiftDropDownDto> GetDepAndShiftDorpDownList(int id)
        {
            try
            {
                var sDropDownData = _personalDataService.GetShiftNameDropDownData();
                var dDropDownData = _personalDataService.GetDepartmentDropDownData();


                var model = new DepAndShiftDropDownDto
                {
                    DepartmentNameSelectItem = DropDownHelper.GetDepartmentNameDropDownList(dDropDownData),
                    ShiftNameSelectItems = DropDownHelper.GetShiftNameDropDownList(sDropDownData),
                };

                return model;
            }
            catch (Exception ex)
            {
                return Ok(new { status = false });
            }

        }
        /// <summary>
        /// 取得編輯畫面原始資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public ActionResult<PersonalDataEditDto> GetEditData(int id)
        {
            try
            {
                var entity = _personalDataService.GetPersonalDataOrNull(id);
                var model = EntityHelper.ToEditDto(entity);
                return model;
            }
            catch (Exception ex)
            {
                return Ok(new {status = false});
            }
           
        }
        /// <summary>
        /// 編輯畫面修改資料庫
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{id}")]
        public ActionResult Edit(PersonalDataEditDto model)
        {
            try
            {
                _personalDataService.EditPersonalData(model);
                return Ok(new {status = true});
            }
            catch (Exception ex)
            {
                return Ok(new { status = false });
            }

        }
        /// <summary>
        /// 新增一筆個人資料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(PersonalDataCreateDto model)
        {
            try
            {
                var entity = ViewModelHelper.ToEntity(model);
                _personalDataService.CreatePersonalData(entity);
                return Ok(new { status = true });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false });
            }

        }
        /// <summary>
        /// 刪除單筆個人資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var entity = _personalDataService.GetPersonalDataOrNull(id);
                _personalDataService.SoftDeletePersonalData(entity);
                return Ok(new { status = true });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false });
            }
        }
    }
}
