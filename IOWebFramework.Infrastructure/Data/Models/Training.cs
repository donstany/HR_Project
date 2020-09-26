using IOWebFramework.Infrastructure.Data.Models.Nomenclatures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOWebFramework.Infrastructure.Data.Models
{
    [Display(Name = "Обучения")]
    [Table("d_trainings")]
    public class Training
    {
        [Key]
        public int Id { get; set; }

        public bool IsActive { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Начална дата")]
        public DateTime DateStart { get; set; }

        [Display(Name = "Крайна дата")]
        public DateTime? DateEnd { get; set; }

        [Display(Name = "Прикачи документи")]
        public string FileContentId { get; set; }

        public List<TrainingAttachment> TrainingAttachments { get; set; } = new List<TrainingAttachment>();

        //public int EmployeeId { get; set; }

        //[ForeignKey(nameof(EmployeeId))]
        //public Employee Employee { get; set; }

        public int PersonId { get; set; }

        [ForeignKey(nameof(PersonId))]
        public Person Person { get; set; }

        [Display(Name = "Обучителен център")]
        public int TrainingCenterId { get; set; }

        [ForeignKey(nameof(TrainingCenterId))]
        public TrainingCenter TrainingCenter { get; set; }

        [Display(Name = "Име на обучение")]
        public int TrainingNameId { get; set; }

        [ForeignKey(nameof(TrainingNameId))]
        public TrainingName TrainingName { get; set; }

        public bool IsDeleted { get; set; }
    }
}
