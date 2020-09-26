using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using IOWebFramework.Infrastructure.Attributes;

namespace IOWebFramework.Infrastructure.Data.Models.Nomenclatures
{
    [Display(Name ="Клонове")]
    [Table("nom_branches")]
    [HiddenDatesOnUI]
    public class Branch : BaseCommonNomenclature
    {
        public List<Department> Departments { get; set; }
    }
}
