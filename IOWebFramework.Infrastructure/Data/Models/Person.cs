using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOWebFramework.Infrastructure.Data.Models
{
    [Display(Name = "Личности")]
    [Table("d_persons")]
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Пълно име")]
        //[Required(ErrorMessage = "Полето {0} е задължително")]
        public string FullName { get; set; }

        //[Display(Name = "Първо име")]
        ////[Required(ErrorMessage = "Полето {0} е задължително")]
        //public string FirstName { get; set; }

        //[Display(Name = "Бащино име")]
        //public string MiddleName { get; set; }

        //[Display(Name = "Фамилно име")]
        ////[Required(ErrorMessage = "Полето {0} е задължително")]
        //public string LastName { get; set; }

        [Display(Name = "ЕГН")]
        [Required(ErrorMessage = "Полето {0} е задължително")]
        public string PID { get; set; }

        public byte[] Photo { get; set; }
        public DateTime SyncedAt { get; set; }
        public List<Employee> Employee { get; set; }
        public List<Diploma> Diplomas { get; set; }
        public List<Training> Trainings { get; set; }
        public List<Certificate> Certificates { get; set; }
        public List<Team> Teams { get; set; }
    }
}
