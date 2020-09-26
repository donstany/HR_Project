using IOWebFramework.Infrastructure.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IOWebFramework.Infrastructure.Data.Models.Nomenclatures
{
    [Display(Name = "Профили")]
    [Table("nom_school_profiles")]
    [HiddenDatesOnUI]
    public class SchoolProfile : BaseCommonNomenclature
    {

    }
}
