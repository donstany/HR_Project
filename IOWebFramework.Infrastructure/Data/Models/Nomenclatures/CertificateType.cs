using IOWebFramework.Infrastructure.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOWebFramework.Infrastructure.Data.Models.Nomenclatures
{
    [Display(Name = "Тип на сертификат")]
    [Table("nom_certificate_types")]
    [HiddenDatesOnUI]
    public class CertificateType : BaseCommonNomenclature
    {
    }
}
