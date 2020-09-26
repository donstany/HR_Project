using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IOWebFramework.Infrastructure.Data.Models.Nomenclatures
{
    [Display(Name ="Длъжност")]
    [Table("nom_positions")]
    public class Position : BaseCommonNomenclature
    {
    }
}
