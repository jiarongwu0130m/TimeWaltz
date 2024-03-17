﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.Entity;

namespace WebApplication1.Models.SettingViewModels
{
    public class AccessViewModel
    {
        public int Id { get; set; }
        
        [Display(Name = "角色名稱")]
        public string ManuName { get; set; } = null!;

        [Display(Name = "路徑")]
        public string? Controller { get; set; }

        [Display(Name = "頁面")]
        public string? Action { get; set; }

        [Display(Name = "權限")]
        public virtual ICollection<AccessRoleBind> AccessRoleBinds { get; set; } = new List<AccessRoleBind>();
    }

    public class AccessCreateViewModel
    {


        [Display(Name = "角色名稱")]
        public string ManuName { get; set; } = null!;

        [Display(Name = "路徑")]
        public string? Controller { get; set; }

        [Display(Name = "頁面")]
        public string? Action { get; set; }

        [Display(Name = "權限")]
        public virtual ICollection<AccessRoleBind> AccessRoleBinds { get; set; } = new List<AccessRoleBind>();

    }
    public class AccessEditViewModel
    {

        public int Id { get; set; }

        [Display(Name = "角色名稱")]
        public string ManuName { get; set; } = null!;

        [Display(Name = "路徑")]
        public string? Controller { get; set; }

        [Display(Name = "頁面")]
        public string? Action { get; set; }

        [Display(Name = "權限")]
        public virtual ICollection<AccessRoleBind> AccessRoleBinds { get; set; } = new List<AccessRoleBind>();

    }
}
