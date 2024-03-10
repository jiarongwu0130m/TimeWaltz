using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models.Entity;
using WebApplication1.Models.Enums;

namespace WebApplication1.Helpers
{
    public class DropDownHelper
    {
        public static List<SelectListItem> GetVacationTypeDropDownList(List<VacationDetail> data)
        {
            var vacationType = data.Select(v => new SelectListItem
            {
                Value = v.Id.ToString(),
                Text = v.VacationType
            }).ToList();
            return vacationType;
        }
        public static List<SelectListItem> GetAgentDropDownList(List<AgentEmployee> data)
        {
            var employee = data.Select(e => new SelectListItem
            {
                Value = e.EmployeesId.ToString(),
                Text = e.AgentEmployeeName,
            }).ToList();
            return employee;
        }
        public static List<SelectListItem> GetAgentDropDownList(List<Employee> data)
        {
            var employee =  data.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.Name,
            }).ToList();
            return employee;
        }
        public static List<SelectListItem> GetEmployeeNameDropDownList(List<Employee> data)
        {
            return data.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name,
            }).ToList();
        }
        public static List<SelectListItem> GetDepartmentNameDropDownList(List<Department> data)
        {
            return data.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.DepartmentName.ToString()
            }).ToList();

        }

        public static List<SelectListItem> GetShiftNameDropDownList(List<ShiftSchedule> data)
        {
            return data.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.ShiftsName.ToString()
            }).ToList();

        }
        public static List<SelectListItem> GetGenderDropDownList()
        {
            return Enum.GetValues(typeof(GenderLimitEnum)).Cast<GenderLimitEnum>().Select(c => new SelectListItem
            {
                Text = c.ToString(),
                Value = ((int)c).ToString()
            }).ToList();

        }
        /// <summary>
        /// 性別限制Enum的下拉式選單
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> GetGenderLimitDropDownList()
        {
            return Enum.GetValues(typeof(GenderLimitEnum)).Cast<GenderLimitEnum>().Select(c => new SelectListItem
            {
                Text = c.ToString(),
                Value = ((int)c).ToString()
            }).ToList();

        }
        public static List<SelectListItem> GetCycleDropDownList()
        {
            return Enum.GetValues(typeof(CycleEnum)).Cast<CycleEnum>().Select(c => new SelectListItem
            {
                Text = c.ToString(),
                Value = ((int)c).ToString()
            }).ToList();

        }
        public static List<SelectListItem> GetHowToGiveDropDownList()
        {
            return Enum.GetValues(typeof(HowToGiveEnum)).Cast<HowToGiveEnum>().Select(c => new SelectListItem
            {
                Text = c.ToString(),
                Value = ((int)c).ToString()
            }).ToList();

        }
        public static List<SelectListItem> GetClockStatusDropDownList()
        {
            return Enum.GetValues(typeof(ClockStatusEnum)).Cast<ClockStatusEnum>().Select(c => new SelectListItem
            {
                Text = c.ToString(),
                Value = ((int)c).ToString()
            }).ToList();

        }

    }
}
