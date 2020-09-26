using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IOWebFramework.Core.Models.Project
{
    public class ProjectMainInfo
    {
        [Display(Name = "Номер")]
        public string Code { get; set; }

        [Display(Name = "Кратко име")]
        public string Name { get; set; }

        [Display(Name = "Име")]
        public string FullName { get; set; }

        [Display(Name = "Клиент")]
        public string Client { get; set; }

        [Display(Name = "Дата на стартиране")]
        [DisplayFormat(DataFormatString ="{0:d}")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Дата на приключване")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Ръководител на проекта")]
        public string Manager { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }
    }
}
