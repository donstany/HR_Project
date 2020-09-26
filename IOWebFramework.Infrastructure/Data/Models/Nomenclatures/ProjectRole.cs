using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOWebFramework.Infrastructure.Data.Models.Nomenclatures
{
    [Display(Name = "Роля в проекта")]
    [Table("nom_project_role")]
    public class ProjectRole : BaseCommonNomenclature
    {
    }
}
