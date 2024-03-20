﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Enums;
using Repository.Models;
using System.Drawing;
using WebApplication1.Helpers;
using WebApplication1.Models.BasicSettingViewModels;
using WebApplication1.Models.PersonalRecordViewModels;
using WebApplication1.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApplication1.Controllers.Api
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class LeaveApiController : ControllerBase
    {
        private readonly VacationTypeService _vacationTypeService;
        private readonly TimeWaltzContext _db;
        private readonly LeaveService _leaveService;
        private readonly AgentEmployeeService _agentEmployeeService;
        private readonly RequestStatusService _requestStatusService;
        private readonly ApprovalService _approvalService;
        private readonly IWebHostEnvironment _env;


        public LeaveApiController(VacationTypeService vacationTypeService, TimeWaltzContext timeWaltzContext, LeaveService leaveService, AgentEmployeeService agentEmployeeService, RequestStatusService requestStatusService, ApprovalService approvalService, IWebHostEnvironment webHostEnvironment)
        {
            this._vacationTypeService = vacationTypeService;
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
            var UserId = User.GetEmployeeId();

            var vacation = _vacationTypeService.GetVacationDetailsList();
            var agent = _db.Employees.FirstOrDefault(x => x.Id == UserId).Department.Employees.ToList();

            var dto = new LeaveDropDownDto
            {
                AgentDropDownList = agent.Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Name,
                }).ToList(),
                VacationDropDownList = vacation.Select(v => new SelectListItem
                {
                    Value = v.Id.ToString(),
                    Text = v.VacationType
                }).ToList(),
            };
            return dto;
        }
        /// <summary>
        /// 請假申請單頁面，取得請假當事人的員工ID和姓名
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public EmpIdNameGet GetEmployeeName()
        {
            var UserId = User.GetEmployeeId();
            var userName = _db.Employees.FirstOrDefault(x => x.Id == UserId).Name;
            var UserAndName = new EmpIdNameGet
            {
                EmployeeId = UserId,
                EmployeeName = userName,
            };

            return UserAndName;
        }
        /// <summary>
        /// 新增一筆請假，修改資料庫
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create([FromForm] LeaveCreateDto dto)
        {
            try
            {
                //在處理(多塞)從使用者那裡接收不到的資料時要再建一個新的更大的更多欄位的model才比較安全(正確)
                var model = new LeaveCreateModel
                {
                    EmployeesId = dto.EmployeesId,
                    StartTime = dto.StartTime,
                    EndTime = dto.EndTime,
                    VacationDetailsId = dto.VacationDetailsId,
                    Reason = dto.Reason,
                    AgentEmployeeId = dto.AgentEmployeeId
                };

                var relativePath = _leaveService.GetRelativeFileRoute(dto.FileRoute);

                model.RelativeFileRoute = relativePath;

                var approvalEmp = _leaveService.GetApprovalEmp(model.EmployeesId);
                if (approvalEmp == null) throw new Exception("資料庫錯誤");

                model.ApprovalEmployeeId = approvalEmp;


                var entity = new LeaveRequest
                {
                    EmployeesId = model.EmployeesId,
                    StartTime = model.StartTime,
                    EndTime = model.EndTime,
                    VacationDetailsId = model.VacationDetailsId,
                    Reason = model.Reason,
                    FileRoute = model.RelativeFileRoute,
                    AgentEmployeeId = model.AgentEmployeeId,
                    ApprovalEmployeeId = model.ApprovalEmployeeId,
                    LeaveMinutes = model.LeaveMinutes
                };
                _requestStatusService.NewRequestStatus(entity.Id);
                _approvalService.NewApproval(entity.Id);
                _leaveService.CreateLeaveRequest(entity);

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
                var models = new List<LeaveDto>();
                foreach (var entity in entities)
                {
                    models.Add(new LeaveDto
                    {
                        Id = entity.Id,
                        Date = entity.StartTime.ToString("yyyy-MM-dd") + "-" + entity.EndTime.ToString("yyyy-MM-dd"),
                        StartTime = entity.StartTime.ToString("HH:mm"),
                        EndTime = entity.EndTime.ToString("HH:mm"),
                        EmployeesId = entity.EmployeesId,
                        VacationType = entity.VacationDetails.VacationType,
                        ApprovalEmpName = entity.ApprovalEmployee.Name,
                        AgentEmployeeName = entity.AgentEmployee.Name,
                    });
                }
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
                var dto = _leaveService.GetEditDataOrNull(Id);
                return Ok(dto);
            }
            catch
            {
                return Ok(new { status = false });
            }
        }
    }
}
