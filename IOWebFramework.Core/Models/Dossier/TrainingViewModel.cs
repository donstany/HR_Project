using System;
using System.ComponentModel.DataAnnotations;
using static IOWebFramework.Shared.Common.MessageConstant;

namespace IOWebFramework.Core.Models.Dossier
{
    public class TrainingViewModel
    {
        public int Id { get; set; }
        public int PersonId { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Начална дата")]
        [Required(ErrorMessage = FieldIsMandatory)]
        public DateTime DateStart { get; set; }

        [Display(Name = "Крайна дата")]
        public DateTime? DateEnd { get; set; }

        [Display(Name = "Обучителен център")]
        [Required(ErrorMessage = FieldIsMandatory)]
        public int TrainingCenterId { get; set; }

        [Display(Name = "Име на обучение")]
        [Required(ErrorMessage = FieldIsMandatory)]
        public int TrainingNameId { get; set; }

        [Display(Name = "Прикачи документи")]
        public string FileContentId { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsAddingMode { get; set; }

        public BreadcrumbInfoModel BreadcrumbInfo { get; set; } = new BreadcrumbInfoModel();
    }
}
