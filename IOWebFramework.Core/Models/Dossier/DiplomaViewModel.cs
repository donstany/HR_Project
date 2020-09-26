using System;
using System.ComponentModel.DataAnnotations;
using static IOWebFramework.Shared.Common.MessageConstant;

namespace IOWebFramework.Core.Models.Dossier
{
    public class DiplomaViewModel
    {
        public int Id { get; set; }
        public int PersonId { get; set; }

        [Display(Name = "Регистрационен номер")]
        public string RegisterNumber { get; set; }

        [Display(Name = "Висше учебно заведение")]
        [Required(ErrorMessage = FieldIsMandatory)]
        public int EducationInstitutionId { get; set; }

        [Display(Name = "Степен")]
        [Required(ErrorMessage = FieldIsMandatory)]
        public int DegreeId { get; set; }

        [Display(Name = "Оценка")]
        //[Required(ErrorMessage = "Оценката е задължителна")]
        [StringLength(4)]
        [RegularExpression(GradeRegexPattern, ErrorMessage = GradeMsg)]
        public string Grade { get; set; }

        [Display(Name = "Дата на издаване")]
        [Required(ErrorMessage = FieldIsMandatory)]
        public DateTime IssueDate { get; set; }

        [Display(Name = "Дата на успешно пложен изпит")]
        [Required(ErrorMessage = FieldIsMandatory)]
        public DateTime SuccessfulExam { get; set; }

        [Display(Name = "Специалност")]
        public int? SpecialtyId { get; set; }

        [Display(Name = "Прикачени файлове")]
        public string FileContentId { get; set; }

        [Display(Name = "Профил")]
        public int? SchoolProfileId { get; set; }

        public bool IsAddingMode { get; set; } = false;

        public BreadcrumbInfoModel BreadcrumbInfo { get; set; } = new BreadcrumbInfoModel();
    }
}