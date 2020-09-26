using IOWebFramework.Infrastructure.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOWebFramework.Infrastructure.Data.Models.Nomenclatures
{

    [Display(Name = "Образователни институции")]
    [Table("nom_education_institutions")]
    [HiddenDatesOnUI]
    public class EducationInstitution : BaseCommonNomenclature
    {
    }
}
