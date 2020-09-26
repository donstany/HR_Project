using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static IOWebFramework.Shared.Common.MessageConstant;

namespace IOWebFramework.Core.Models.Project
{
    public class ProjectViewModel
    {
        public int Id { get; set; }

        [DisplayName("Номер")]
        [Required(ErrorMessage = FieldIsMandatory)]
        public string Code { get; set; }

        [DisplayName("Кратко Име")]
        [Required(ErrorMessage = FieldIsMandatory)]
        public string Name { get; set; }

        [DisplayName("Пълно име")]
        [Required(ErrorMessage = FieldIsMandatory)]
        public string FullName { get; set; }

        [DisplayName("Дата на стартиране")]
        [Required(ErrorMessage = FieldIsMandatory)]
        public DateTime StartDate { get; set; }

        [DisplayName("Дата на приключване")]
        public DateTime? EndDate { get; set; }

        [DisplayName("Описание")]
        public string Description { get; set; }

        [DisplayName("Активен")]
        public bool IsActive { get; set; }

        [DisplayName("Клиент")]
        [Required(ErrorMessage = FieldIsMandatory)]
        public int ClientId { get; set; }

        [DisplayName("Ръководител")]
        [Required(ErrorMessage = FieldIsMandatory)]
        public int ManagerId { get; set; }
    }
}
