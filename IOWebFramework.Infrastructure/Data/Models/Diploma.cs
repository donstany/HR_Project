using IOWebFramework.Infrastructure.Data.Models.Nomenclatures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOWebFramework.Infrastructure.Data.Models
{
    [Display(Name = "Дипломи")]
    [Table("d_diplomas")]
    public class Diploma
    {
        [Key]
        public int Id { get; set; }

        [Display(Name= "Дата на издаване")]
        public DateTime IssueDate { get; set; }

        [Display(Name = "Дата на успешно пложен изпит")]
        public DateTime SuccessfulExam { get; set; }

        [Display(Name = "Оценка")]
        [StringLength(4)]
        public string Grade { get; set; }

        [Display(Name = "Регистрационен номер")]
        public string RegisterNumber { get; set; }

        [Display(Name = "Прикачени файлове")]
        public string FileContentId { get; set; }

        public List<DiplomaAttachment> DiplomaAttachments { get; set; } = new List<DiplomaAttachment>();

        public int EducationInstitutionId { get; set; }

        [ForeignKey(nameof(EducationInstitutionId))]
        public EducationInstitution EducationInstitution { get; set; }

        public int DegreeId { get; set; }

        [ForeignKey(nameof(DegreeId))]
        public Degree Degree { get; set; }

        //public int ProfessionalDirectionId { get; set; }
        //[ForeignKey(nameof(ProfessionalDirectionId))]
        //public ProfessionalDirection ProfessionalDirection { get; set; }

        public int? SchoolProfileId { get; set; }

        [ForeignKey(nameof(SchoolProfileId))]
        public SchoolProfile SchoolProfile { get; set; }

        public int? SpecialtyId { get; set; }
    
        [ForeignKey(nameof(SpecialtyId))]
        public Classifier Classifier { get; set; }

        public int PersonId { get; set; }
      
        [ForeignKey(nameof(PersonId))]
        public Person Person { get; set; }
    }
}
