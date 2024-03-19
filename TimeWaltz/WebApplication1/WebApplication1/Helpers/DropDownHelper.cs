using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Enums;
using Repository.Models;

namespace WebApplication1.Helpers
{
    public class DropDownHelper
    {
        /// <summary>
        /// 在請假申請單用來選擇假別的下拉式選單
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<SelectListItem> GetVacationTypeDropDownList(List<VacationDetail> data)
        {
            var vacationType = data.Select(v => new SelectListItem
            {
                Value = v.Id.ToString(),
                Text = v.VacationType
            }).ToList();
            return vacationType;
        }
<<<<<<< HEAD
        //public static List<SelectListItem> GetAgentDropDownList(List<AgentEmployee> data)
        //{
        //    var employee = data.Select(e => new SelectListItem
        //    {
        //        Value = e.EmployeesId.ToString(),
        //        Text = e.AgentEmployeeName,
        //    }).ToList();
        //    return employee;
        //}
=======
        /// <summary>
        /// 在請假申請單中用來選擇代理人的下拉式選單
        /// 傳入一個代理人List的entityModel
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<SelectListItem> GetAgentDropDownList(List<AgentEmployee> data)
        {
            var employee = data.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.AgentEmployeeName,
            }).ToList();
            return employee;
        }
>>>>>>> 6294efb4a5ce87ed96d4a9fa589d8fca49a8ba03
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
            return Enum.GetValues(typeof(GenderEnum)).Cast<GenderEnum>().Select(c => new SelectListItem
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
