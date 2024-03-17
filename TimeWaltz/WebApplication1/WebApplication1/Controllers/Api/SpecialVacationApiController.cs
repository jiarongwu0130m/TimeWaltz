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
    }
}
