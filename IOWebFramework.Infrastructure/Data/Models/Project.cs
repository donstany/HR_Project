using IOWebFramework.Infrastructure.Data.Models.Nomenclatures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IOWebFramework.Infrastructure.Data.Models
{
    [Display(Name = "Проекти")]
    [Table("d_projects")]
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Код")]
        public string Code { get; set; }

        [Display(Name = "Кратко име")]
        public string Name { get; set; }

        [Display(Name = "Име")]
        public string FullName { get; set; }

        [Display(Name = "Дата на стартиране")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Дата на приключване")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Активен")]
        public bool IsActive { get; set; }

        [Display(Name = "Клиент")]
        public int ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        public Client Client { get; set; }

        [Display(Name = "Ръководител")]
        public int ManagerId { get; set; }

        [ForeignKey(nameof(ManagerId))]
        public Person Person { get; set; }
    }
}
