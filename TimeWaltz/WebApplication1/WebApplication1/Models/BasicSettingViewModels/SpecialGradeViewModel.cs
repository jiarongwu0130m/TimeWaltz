﻿using System.ComponentModel;

namespace WebApplication1.Models.BasicSettingViewModels
{
    public class SpecialGradeViewModel
    {
        public int Id { get; set; }
        public int ServiceLength { get; set; }
        public int Days { get; set; }
    }
    public class SpecialGradeCreateViewModel
    {
        [DisplayName("服務年資")]
        public int ServiceLength { get; set; }
        [DisplayName("特休天數")]
        public int Days { get; set; }
    }
    public class SpecialGradeEditViewModel
    {
        public int Id { get; set; }
        public int ServiceLength { get; set; }
        public int Days { get; set; }
    }
}
