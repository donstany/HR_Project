using IOWebFramework.Infrastructure.Contracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static IOWebFramework.Shared.Common.MessageConstant;

namespace IOWebFramework.Infrastructure.Data.Models.Nomenclatures
{
    public class BaseCommonNomenclature : ICommonNomenclature
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Номер по ред")]
        public int OrderNumber { get; set; }

        [Display(Name = "Код")]
        public string Code { get; set; }

        [Display(Name = "Име")]
        [Required(ErrorMessage = FieldIsMandatory)]
        public string Label { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Начална дата")]
        [Required(ErrorMessage = FieldIsMandatory)]
        public DateTime DateStart { get; set; }

        [Display(Name = "Крайна дата")]
        public DateTime? DateEnd { get; set; }

        [Display(Name = "Активен")]
        public bool IsActive { get; set; }
    }
}
