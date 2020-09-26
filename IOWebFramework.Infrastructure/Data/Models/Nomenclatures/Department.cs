using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOWebFramework.Infrastructure.Data.Models.Nomenclatures
{
    [Display(Name="Отдели")]
    [Table("nom_departments")]
    public class Department : BaseCommonNomenclature
    {
        public List<Employee> Employees { get; set; }

        [DisplayName("Клон")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Полето {0} е задължително")]
        public int BranchId { get; set; }

        [ForeignKey(nameof(BranchId))]
        public Branch Branch { get; set; }
    }
}
