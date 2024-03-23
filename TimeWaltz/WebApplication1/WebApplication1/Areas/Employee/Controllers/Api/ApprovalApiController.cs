using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
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

            var query = _TWdb.Approvals.Where(x => x.Id == id && x.Status == Repository.Enum.RequestStatusEnum.簽核中);

            //query = query.Join(_TWdb.Employees, x => x.);



            query.Select(x => new
            {
                x.Id,
                x.TableId,
                x.TableType,
                x.Status
            }).ToList(); ;
            return query;
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

            var query = _TWdb.Approvals
                        .GroupJoin(
                            _TWdb.OvertimeApplications,
                            approval => approval.TableId,
                            ot => ot.Id,
                            (approval, ots) => new { Approval = approval, OvertimeApps = ots.FirstOrDefault(), Type = 0 }
                        )
                        .GroupJoin(
                            _TWdb.LeaveRequests,
                            combined => combined.Approval.TableId,
                            lr => lr.Id,
                            (combined, lrs) => new { combined.Approval, combined.OvertimeApps, LeaveReqs = lrs.FirstOrDefault(), Type = 1 }
                        )
                        .GroupJoin(
                            _TWdb.AdditionalClockIns,
                            combined => combined.Approval.TableId,
                            clock => clock.Id,
                            (combined, clocks) => new { combined.Approval, combined.OvertimeApps, combined.LeaveReqs, ClockIns = clocks.FirstOrDefault(), Type = 2 }
                        )
                        .Select(combined =>
                            new
                            {
                                combined.Approval.Id,
                                combined.Approval.TableType,
                                combined.Approval.TableId,
                                Eid = combined.Type == 0 ? combined.OvertimeApps.ApprovalEmployeeId :
                                      combined.Type == 1 ? combined.LeaveReqs.ApprovalEmployeeId :
                                      combined.ClockIns.ApprovalEmployeeId,
                                combined.LeaveReqs.AgentEmployeeId
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
                                x.ApprovalData.Eid,
                                x.ApprovalData.AgentEmployeeId,
                                EmployeeName = x.Employee.Name,
                                AgentEmployeeName = emp2.Name
                            }
                        )
                        .ToList();

            return query;
        }
    }
}
