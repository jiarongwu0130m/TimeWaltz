using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Areas.Employee.Controllers.Api
{
    [Route("mobile/api/Approval/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "EmployeeAuthScheme")]
    public class ApprovalApiController : ControllerBase
    {
        private TimeWaltzContext _TWdb;
        private ApprovalService _ApprovalService;

        public ApprovalApiController(TimeWaltzContext TWdb, ApprovalService ApprovalService)
        {
            _TWdb = TWdb;
            _ApprovalService = ApprovalService;
        }
        public object GetApprovalData(int id)
        {
            var query = _TWdb.Approvals.Where(x => x.Id == id && x.Status == Repository.Enum.RequestStatusEnum.簽核中)
                .Select(x => new
                {
                    x.TableId,
                    x.TableType
                }).ToList(); 
            return query;
        }
        public void ApprovalEdit(ApprovaEditDto model)
        {
            var entity = _TWdb.Approvals.FirstOrDefault(x => x.Id == model.Id);
            entity.Remark = model.Remark;
            entity.Status = (Repository.Enum.RequestStatusEnum?)model.Status;

            _TWdb.SaveChanges();

        }
        public object GetAllApprovalData()
        {
            //var query = _TWdb.Approvals
            //        .Where(x => x.Status == Repository.Enum.RequestStatusEnum.簽核中)
            //        .Join(_TWdb.OvertimeApplications, a => a.TableId, oa => oa.Id, (a, oa) => new { Approval = a, Overtime = oa })
            //        .Join(_TWdb.AdditionalClockIns, aa => aa.Approval.TableId, ac => ac.Id, (aa, ac) => new { aa.Approval, aa.Overtime, Add = ac })
            //        .Join(_TWdb.LeaveRequests, aaa => aaa.Approval.TableId, lr => lr.Id, (aaa, lr) => new
            //        {
            //            LRAgentEmployeeId = lr.AgentEmployeeId,
            //            OTApprovalEmployeeId = aaa.Overtime.ApprovalEmployeeId,
            //            ADDEmployeeId=aaa.Add.ApprovalEmployeeId,
            //            Id = aaa.Approval.Id,
            //            TableType = aaa.Approval.TableType,
            //            TableId = aaa.Approval.TableId,
            //            Remark = aaa.Approval.Remark,
            //            Status = aaa.Approval.Status
            //        })
            //        .ToList();

            //var query = _TWdb.Approvals
            //    .Join(
            //        _TWdb.OvertimeApplications,
            //        approval => new { TableId = approval.TableId, Type = 0 },
            //        ot => new { TableId = ot.Id, Type = 0 },
            //        (approval, ot) => new { Approval = approval, OvertimeApp = ot }
            //    )
            //    .Join(
            //        _TWdb.LeaveRequests,
            //        combined => new { TableId = combined.Approval.TableId, Type = 1 },
            //        lr => new { TableId = lr.Id, Type = 1 },
            //        (combined, lr) => new { combined.Approval, combined.OvertimeApp, LeaveReq = lr }
            //    )
            //    .Join(
            //        _TWdb.AdditionalClockIns,
            //        combined => new { TableId = combined.Approval.TableId, Type = 2 },
            //        clock => new { TableId = clock.Id, Type = 2 },
            //        (combined, clock) => new
            //        {
            //            combined.Approval.Id,
            //            combined.Approval.TableType,
            //            combined.Approval.TableId,
            //            OvertimeEmpId = combined.OvertimeApp.EmployeesId,
            //            LeaveReqEmpId = combined.LeaveReq.EmployeesId,
            //            ClockInEmpId = clock.EmployeesId,
            //        }
            //    )
            //    .ToList();

            //var query = _TWdb.Approvals
            //            .GroupJoin(
            //                _TWdb.OvertimeApplications,
            //                approval => approval.TableId,
            //                ot => ot.Id,
            //                (approval, ots) => new { Approval = approval, OvertimeApps = ots.FirstOrDefault(), Type = 0 }
            //            )
            //            .GroupJoin(
            //                _TWdb.LeaveRequests,
            //                combined => combined.Approval.TableId,
            //                lr => lr.Id,
            //                (combined, lrs) => new { combined.Approval, combined.OvertimeApps, LeaveReqs = lrs.FirstOrDefault(), Type = 1 }
            //            )
            //            .GroupJoin(
            //                _TWdb.AdditionalClockIns,
            //                combined => combined.Approval.TableId,
            //                clock => clock.Id,
            //                (combined, clocks) => new { combined.Approval, combined.OvertimeApps, combined.LeaveReqs, ClockIns = clocks.FirstOrDefault(), Type = 2 }
            //            )
            //            .Select(combined =>
            //                new
            //                {
            //                    combined.Approval.Id,
            //                    combined.Approval.TableType,
            //                    combined.Approval.TableId,
            //                    Eid = combined.Type == 0 ? combined.OvertimeApps.EmployeesId :
            //                          combined.Type == 1 ? combined.LeaveReqs.EmployeesId :
            //                          combined.ClockIns.EmployeesId,
            //                    combined.LeaveReqs.AgentEmployeeId
            //                }
            //            )
            //            .Join(
            //                _TWdb.Employees,
            //                x => x.Eid,
            //                emp => emp.Id,
            //                (x, emp) => new { ApprovalData = x, Employee = emp }
            //            )
            //            .Join(
            //                _TWdb.Employees,
            //                x => x.ApprovalData.AgentEmployeeId,
            //                emp2 => emp2.Id,
            //                (x, emp2) => new
            //                {
            //                    x.ApprovalData.Id,
            //                    x.ApprovalData.TableType,
            //                    x.ApprovalData.TableId,
            //                    EmployeeName = x.Employee.Name,
            //                    AgentEmployeeName = emp2.Name
            //                }
            //            )
            //            .ToList();

            //var query = _TWdb.Approvals
            //              .GroupJoin(
            //                  _TWdb.OvertimeApplications,
            //                  approval => new { Id = approval.TableId, Type = 0 },
            //                  ot => new { Id = ot.Id, Type = 0 },
            //                  (approval, ots) => new { Approval = approval, OvertimeApps = ots.ToList(), Type = 0 }
            //              ).AsEnumerable()
            //              .GroupJoin(
            //                  _TWdb.LeaveRequests,
            //                  combined => new { Id = combined.Approval.TableId, Type = 1 },
            //                  lr => new { Id = lr.Id, Type = 1 },
            //                  (combined, lrs) => new { combined.Approval, combined.OvertimeApps, LeaveReqs = lrs.ToList(), Type = 1 }
            //              ).AsEnumerable()
            //              .GroupJoin(
            //                  _TWdb.AdditionalClockIns,
            //                  combined => new { Id = combined.Approval.TableId, Type = 2 },
            //                  clock => new { Id = clock.Id, Type = 2 },
            //                  (combined, clocks) => new { combined.Approval, combined.OvertimeApps, combined.LeaveReqs, ClockIns = clocks.ToList(), Type = 2 }
            //              )
            //              .Select(combined =>
            //                  new
            //                  {
            //                      combined.Approval.Id,
            //                      combined.Approval.TableType,
            //                      combined.Approval.TableId,
            //                      Eid = combined.Type == 0 ? combined.OvertimeApps.FirstOrDefault()?.EmployeesId :
            //                            combined.Type == 1 ? combined.LeaveReqs.FirstOrDefault()?.EmployeesId :
            //                            combined.ClockIns.FirstOrDefault()?.EmployeesId,
            //                      combined.LeaveReqs.FirstOrDefault()?.AgentEmployeeId
            //                  }
            //              )
            //              .Join(
            //                  _TWdb.Employees,
            //                  x => x.Eid,
            //                  emp => emp.Id,
            //                  (x, emp) => new { ApprovalData = x, Employee = emp }
            //              )
            //              .Join(
            //                  _TWdb.Employees,
            //                  x => x.ApprovalData.AgentEmployeeId,
            //                  emp2 => emp2.Id,
            //                  (x, emp2) => new
            //                  {
            //                      x.ApprovalData.Id,
            //                      x.ApprovalData.TableType,
            //                      x.ApprovalData.TableId,
            //                      EmployeeName = x.Employee.Name,
            //                      AgentEmployeeName = emp2.Name
            //                  }
            //              )
            //              .ToList();


            var queryResult = _TWdb.Approvals
                            .Join(
                                _TWdb.OvertimeApplications,
                                approval => new { Id = approval.TableId, Type = 0 },
                                ot => new { Id = ot.Id, Type = 0 },
                                (approval, ot) => new { Approval = approval, OvertimeApp = ot, Type = 0 }
                            )
                            .Join(
                                _TWdb.LeaveRequests,
                                combined => new { Id = combined.Approval.TableId, Type = 1 },
                                lr => new { Id = lr.Id, Type = 1 },
                                (combined, lr) => new { combined.Approval, combined.OvertimeApp, LeaveReq = lr, Type = 1 }
                            )
                            .Join(
                                _TWdb.AdditionalClockIns,
                                combined => new { Id = combined.Approval.TableId, Type = 2 },
                                clock => new { Id = clock.Id, Type = 2 },
                                (combined, clock) => new { combined.Approval, combined.OvertimeApp, combined.LeaveReq, ClockIns = clock, Type = 2 }
                            )
                            .ToList() // 將結果讀取到記憶體中
                            .GroupBy(
                                combined => new { combined.Approval.Id, combined.Approval.TableType, combined.Approval.TableId },
                                (key, group) => new
                                {
                                    ApprovalData = group.Select(x => x.Approval).FirstOrDefault(),
                                    OvertimeApp = group.Select(x => x.OvertimeApp).FirstOrDefault(),
                                    LeaveReq = group.Select(x => x.LeaveReq).FirstOrDefault(),
                                    ClockIns = group.Select(x => x.ClockIns).FirstOrDefault(),
                                    AgentEmployeeId = group.Select(x => x.LeaveReq.AgentEmployeeId).FirstOrDefault()
                                }
                            )
                            .Select(result =>
                                new
                                {
                                    result.ApprovalData.Id,
                                    result.ApprovalData.TableType,
                                    result.ApprovalData.TableId,
                                    Eid = result.ApprovalData.TableType == 0 ? result.OvertimeApp.EmployeesId :
                                          result.ApprovalData.TableType == 1 ? result.LeaveReq.EmployeesId :
                                          result.ClockIns.EmployeesId,
                                    result.AgentEmployeeId
                                }
                            )
                            .Join(
                                _TWdb.Employees,
                                x => x.Eid,
                                emp => emp.Id,
                                (x, emp) => new { ApprovalData = x, Employee = emp }
                            )
                            .Join(
                                _TWdb.Employees,
                                x => x.ApprovalData.AgentEmployeeId,
                                emp2 => emp2.Id,
                                (x, emp2) => new
                                {
                                    x.ApprovalData.Id,
                                    x.ApprovalData.TableType,
                                    x.ApprovalData.TableId,
                                    EmployeeName = x.Employee.Name,
                                    EmployeeId=x.Employee.Id,
                                }
                            ).Where(x=>x.EmployeeId ==User.GetId())
                            .ToList();


            return queryResult;
        }
    }
}
