using IOWebFramework.Infrastructure.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOWebFramework.Infrastructure.Data.Models.Nomenclatures
{

    [Display(Name = "Обучителни центрове")]
    [Table("nom_training_centers")]
    [HiddenDatesOnUI]
    public class TrainingCenter : BaseCommonNomenclature
    {
    }
}
