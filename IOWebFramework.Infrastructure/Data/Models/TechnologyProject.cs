using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IOWebFramework.Infrastructure.Data.Models.Nomenclatures;

namespace IOWebFramework.Infrastructure.Data.Models
{
    [Display(Name = "Технологии в проект")]
    [Table("d_technology_project")]
    public class TechnologyProject
    {
        [Key]
        public int Id { get; set; }

        public int ProjectId { get; set; }
        [ForeignKey(nameof(ProjectId))]
        public Project Project { get; set; }

        public int TechnologyId { get; set; }
        [ForeignKey(nameof(TechnologyId))]
        public Technology Technology { get; set; }
    }
}
