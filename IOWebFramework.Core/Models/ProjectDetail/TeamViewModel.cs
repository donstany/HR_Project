using IOWebFramework.Infrastructure.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static IOWebFramework.Shared.Common.MessageConstant;

namespace IOWebFramework.Core.Models.ProjectDetail
{
    public class TeamViewModel
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        [DisplayName("Дата на започване")]
        [Required(ErrorMessage = FieldIsMandatory)]
        public DateTime StartDate { get; set; }

        [DisplayName("Дата на приключване")]
        public DateTime? EndDate { get; set; }

        [DisplayName("Активен")]
        public bool IsActive { get; set; }

        [DisplayName("Участник")]
        [Required(ErrorMessage = FieldIsMandatory)]
        public int PersonId { get; set; }

        [DisplayName("Роли")]
        [EnsureOneElementAttribute(ErrorMessage = FieldIsMandatory)]
        public List<int> ProjectRoles { get; set; }

        public bool IsAddingMode { get; set; } = false;

        public TeamViewModel()
        {
            ProjectRoles = new List<int>();
        }
    }
}
