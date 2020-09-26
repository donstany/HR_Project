using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOWebFramework.Infrastructure.Data.Models.Nomenclatures
{
    [Display(Name = "Име на обучение")]
    [Table("nom_training_names")]
    public class TrainingName : BaseCommonNomenclature
    {
    }
}