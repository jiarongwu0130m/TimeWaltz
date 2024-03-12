using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Helpers;
using WebApplication1.Models.BasicSettingViewModels;

using WebApplication1.Services;
using static WebApplication1.Controllers.Api.VacationDetailsApiController;

namespace WebApplication1.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VacationDetailsApiController : ControllerBase
    {
        private readonly VacationTypeService _vacationTypeService;

        public VacationDetailsApiController(VacationTypeService vacationTypeService)
        {
            _vacationTypeService = vacationTypeService;
        }
        /// <summary>
        /// 可提供性別限制以及循環的下拉選單資料
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public VacationDropDownDto DropDownList()
        {
            var model = new VacationDropDownDto
            {
                GenderSelectItems = DropDownHelper.GetGenderDropDownList(),
                CycleSelectItems = DropDownHelper.GetCycleDropDownList()
            };
            return model;
        }
        /// <summary>
        /// 新增假別的Post方法， 接到使用者輸入完成的資料並存入資料庫回傳OK
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult VacationCreate(VacationCreateDto model)
        {
            try
            {
                var entity = ViewModelHelper.ToEntity(model);
                _vacationTypeService.CreateVacationType(entity);
                return Ok(new { status = true });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false });
            }

        }

        /// <summary>
        /// GET假別設定編輯頁面的原資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public VacationEditDto GetVacationEditData(int id)
        {
            try
            {
                var entity = _vacationTypeService.GetVacationTypeOrNull(id);
                var model = EntityHelper.ToEditDto(entity);
                return model;
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }
        /// <summary>
        /// 編輯畫面送出後觸發的Post方法，修改資料庫後導回List
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult VacationEdit(VacationEditDto dto)
        {
            try
            {
                _vacationTypeService.EditVacationType(dto);
                return Ok(new {status = true });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false });
            }
        }

        [HttpGet]
        public List<VacationDto> GetVacationList()
        {
            var entities = _vacationTypeService.GetVacationDetailsList();
            var models = EntityHelper.ToDto(entities);
            return models;
        }

        [HttpPost]
        public ActionResult VacationDelete([FromForm] int Id)
        {
            try
            {
                var entity = _vacationTypeService.GetVacationTypeOrNull(Id);
                _vacationTypeService.DeleteVacationType(entity);
                return Ok(new { status = true });
            }catch (Exception ex)
            {
                return Ok(new { status = false });
            }
        }
    }
}
