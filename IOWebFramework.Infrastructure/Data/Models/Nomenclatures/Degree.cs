using IOWebFramework.Infrastructure.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOWebFramework.Infrastructure.Data.Models.Nomenclatures
{
    [Display(Name ="Квалификационна степен")]
    [Table("nom_degrees")]
    [HiddenDatesOnUI]
    public class Degree : BaseCommonNomenclature
    {
    }
}
