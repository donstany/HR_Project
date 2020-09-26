using IOWebFramework.Infrastructure.Data.Models.Nomenclatures;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOWebFramework.Infrastructure.Data.Models
{
    [Display(Name = "Екипи")]
    [Table("d_teams")]
    public class Team
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Дата на започване")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Дата на приключване")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Активен")]
        public bool IsActive { get; set; }

        public int ProjectId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public Project Project { get; set; }

        public int PersonId { get; set; }

        [ForeignKey(nameof(PersonId))]
        public Person Person { get; set; }

        public int ProjectRoleId { get; set; }

        [ForeignKey(nameof(ProjectRoleId))]
        public ProjectRole ProjectRole { get; set; }
    }
}
