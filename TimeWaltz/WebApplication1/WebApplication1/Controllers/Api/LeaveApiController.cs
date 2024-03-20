using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using WebApplication1.Helpers;
using WebApplication1.Models.BasicSettingViewModels;
using WebApplication1.Models.PersonalRecordViewModels;
using WebApplication1.Services;

namespace WebApplication1.Controllers.Api
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class LeaveApiController : ControllerBase
    {
        private readonly TimeWaltzContext _db;
        private readonly LeaveService _leaveService;
        private readonly AgentEmployeeService _agentEmployeeService;
        private readonly RequestStatusService _requestStatusService;
        private readonly ApprovalService _approvalService;
        private readonly IWebHostEnvironment _env;


        public LeaveApiController(TimeWaltzContext timeWaltzContext , LeaveService leaveService, AgentEmployeeService agentEmployeeService, RequestStatusService requestStatusService, ApprovalService approvalService, IWebHostEnvironment webHostEnvironment)
        {
            this._db = timeWaltzContext;
            _leaveService = leaveService;
            _agentEmployeeService = agentEmployeeService;
            this._requestStatusService = requestStatusService;
            this._approvalService = approvalService;
            this._env = webHostEnvironment;
        }
        /// <summary>
        /// 請假申請單畫面，取下拉式選單資料
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public LeaveDropDownDto GetDropDownList()
        {
            var UserId = 1;
            var vacation = _leaveService.GetVacationDropDownData();
            var agent = _agentEmployeeService.GetAgentDropDownData(UserId);
            
            var model = new LeaveDropDownDto 
            {
                AgentDropDownList = DropDownHelper.GetAgentDropDownList(agent),
                VacationDropDownList = DropDownHelper.GetVacationTypeDropDownList(vacation),
            };
            return model;
        }
        /// <summary>
        /// 請假申請單頁面，取得請假當事人的員工ID和姓名
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public EmpIdNameGet GetEmployeeName()
        {
            var UserId = 1;
            var userNameAndIdPare = _leaveService.GetNameOrNull(UserId);
            var employee = EntityHelper.GetNameAndIdPare(userNameAndIdPare);
            

            return employee;
        }
        /// <summary>
        /// 新增一筆請假，修改資料庫
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create([FromForm]LeaveCreateDto model)
        {
            try
            {
                //在處理(多塞)從使用者那裡接收不到的資料時要再建一個新的更大的更多欄位的model才比較安全(正確)
                
                //var relativePath = "";
                //if (model.FileRoute != null)
                //{
                //    var dir = $"{_env.WebRootPath}";
                //    relativePath = $"/pic/{Guid.NewGuid()}_{model.FileRoute.FileName}";


                //    using (var fs = new FileStream(dir + relativePath, FileMode.Create))
                //    {
                //        model.FileRoute.CopyTo(fs);
                //    }

                //    model.RelativeFileRoute = relativePath;

                    
                //}

                //var approvalModel = _leaveService.GetApprovalEmp(fileModel);
                //var leaveModel = _leaveService.AddLeaveTime2(approvalModel);
                //var entity = ViewModelHelper.ToEntity(leaveModel);
                //_leaveService.CreateLeaveRequest(entity);
                ////TODO: 新增狀態表
                //_requestStatusService.NewRequestStatus(entity);
                ////TODO: 新增簽核表
                //_approvalService.NewApproval(entity);
                return Ok(new { status = true });

            }
            catch (Exception ex)
            {
                return Ok(new { status = false });
            }
        }
        [HttpGet]
        public ActionResult<LeaveDto> List() 
        {
            try
            {
                var entities = _leaveService.GetLeaveListData();
                var models = EntityHelper.ToDto(entities);
                return Ok(models);
            }
            catch (Exception ex)
            {
                return Ok(new { status = false });
            }
            
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult<LeaveEditDto> Edit(int Id)
        {
            try
            {
                var entity = _leaveService.GetEditDataOrNull(Id);
                var model = EntityHelper.ToDto(entity);
                return Ok(model);
            }
            catch
            {
                return Ok(new { status = false });
            }
        }
    }
}
