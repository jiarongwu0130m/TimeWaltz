﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication1.Models.Dto
{
    public class LeaveDto
    {

    }

    public class LeaveDropDownDto
    {
        public List<SelectListItem> VacationDropDownList { get; set; }
        public List<SelectListItem> AgentDropDownList { get; set; }
    }
}
