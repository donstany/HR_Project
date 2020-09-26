using IOWebFramework.Infrastructure.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOWebFramework.Infrastructure.Data.Models.Nomenclatures
{

    [Display(Name = "Tехнологии")]
    [Table("nom_technologies")]
    [HiddenDatesOnUI]
    public class Technology : BaseCommonNomenclature
    {
    }
}
