using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models.Entity;
using WebApplication1.Models.Enums;

namespace WebApplication1.Helpers
{
    public class DropDownHelper
    {
        public static List<SelectListItem> GetDepartmentNameDropDownList(List<Department> dropDownData)
        {
            return dropDownData.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.DepartmentName.ToString()
            }).ToList();

        }

        public static List<SelectListItem> GetShiftNameDropDownList(List<ShiftSchedule> dropDownData)
        {
            return dropDownData.Select(c => new SelectListItem
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
    }
}
