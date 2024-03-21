using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Helpers;
using WebApplication1.Models.BasicSettingViewModels;
using WebApplication1.Services;

namespace WebApplication1.Controllers.Api
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class SpecialVacationApiController : ControllerBase
    {
        private readonly SpecialVacationService _specialVacationService;

        public SpecialVacationApiController(SpecialVacationService specialVacationService)
        {
            this._specialVacationService = specialVacationService;
        }
        [HttpPost]
        public void Create(SpecialVacationCreateDto dto )
        {
            try
            {
                var entity = ViewModelHelper.ToDto(dto);
                _specialVacationService.Cerate(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("新增錯誤");
            }
            
        }
        [HttpGet]
        [Route("{id}")]
        public SpecialVacationEditDto GetEditData(int id)
        {
            try
            {
                var entity = _specialVacationService.GetEditDataOrNull(id);
                var model = EntityHelper.ToDto(entity);
                return model;
            }
            catch (Exception ex)
            {
                throw new Exception("程式錯誤");
            }
        }
        [HttpPost]
        [Route("{id}")]
        public void Edit(SpecialVacationEditDto model)
        {
            _specialVacationService.Edit(model);            
        }
        [HttpGet]
        public List<SpecialVacationDto> List()
        {
            try
            {
                var entities = _specialVacationService.GetListData();
                var models = EntityHelper.ToDto(entities);
                return models;
            }
            catch (Exception ex)
            {
                throw new Exception("程式錯誤");
            }
        }
        [HttpPost]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var entity = _specialVacationService.GetEditDataOrNull(id);
                _specialVacationService.Delete(entity);
                return Ok(new { status = true });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false });
            }
        }
    }
}
