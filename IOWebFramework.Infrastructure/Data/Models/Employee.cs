using IOWebFramework.Infrastructure.Data.Models.Nomenclatures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOWebFramework.Infrastructure.Data.Models
{
    [Display(Name = "Служители")]
    [Table("d_employees")]
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "ТД")]
        [Required(ErrorMessage = "Полето {0} е задължително")]
        public string Td { get; set; }

        public string FileContentId { get; set; }

        [Display(Name = "Дата на назначаване")]
        [Required(ErrorMessage = "Полето {0} е задължително")]
        public DateTime HireDate { get; set; }

        [Display(Name = "Дата на напускане")]
        public DateTime? FireDate { get; set; }

        [Display(Name = "Предишен опит сумарно")]
        public int? PreviuosExperienceSummed { get; set; }

        [Display(Name = "Предишен опит в ИО")]
        public int? PreviuosExperienceInIs { get; set; }

        [Display(Name = "Предишен опит в други компании")]
        public int? PreviuosExperience { get; set; }

        public bool IsActive { get; set; } = true;

        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
        public string Branch { get; set; }
        public bool IsWorking { get; set; } = true;
        public string Departament { get; set; }
        public DateTime SyncedAt { get; set; }
        public int PersonId { get; set; }

        [ForeignKey(nameof(PersonId))]
        public Person Person { get; set; }


        //public int DepartmentId { get; set; }

        //[ForeignKey(nameof(DepartmentId))]
        //public Department Department { get; set; }

        //public int PositionId { get; set; }

        //[ForeignKey(nameof(PositionId))]
        //public Position Position { get; set; }

    }
}
