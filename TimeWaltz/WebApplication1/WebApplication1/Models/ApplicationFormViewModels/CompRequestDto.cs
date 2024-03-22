using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Repository.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication1.Models.ApplicationFormViewModels
{
    /// <summary>
    /// 資料表：AdditionalClockIn
    /// 原始entity資料型別
    /// </summary>
    public class CompRequestDto
    {
        public int Id { get; set; }

        public int EmployeesId { get; set; }

        public DateTime AdditionalTime { get; set; }

        public int Status { get; set; }

        public string Reason { get; set; }

        public int ApprovalEmployeeId { get; set; }
    }
    /// <summary>
    /// 補打卡申請單
    /// Status用ClockStatusEnum
    /// 補打卡原因不可以null
    /// </summary>
    public class CompRequestCreateDto
    {
        public int EmployeesId { get; set; }
        public DateTime AdditionalTime { get; set; }
        public ClockStatusEnum Status { get; set; }
        public List<SelectListItem>? ClockStatusSelectItems { get; set; }
        public string Reason { get; set; }
        public int ApprovalEmployeeId { get; set; }

    }
    /// <summary>
    /// 補打卡申請單_瀏覽
    /// </summary>
    public class CompRequestListDto
    {
        public int Id { get; set; }
        public int EmployeesId { get; set; }
        public DateTime AdditionalTime { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
        public string ApprovalStatus { get; set; }

    }
    /// <summary>
    /// 補打卡申請單_詳細資料
    /// </summary>
    public class CompRequestDetailDto
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public DateTime AdditionalTime { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
        public string ApprovalStatus { get; set; }
        public string ApprovalEmpName { get; set; }
    }
    public class CompRequestGetEmpIdName
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
    }


}
