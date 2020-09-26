using IOWebFramework.Infrastructure.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IOWebFramework.Infrastructure.Data.Models.Nomenclatures
{
    [Display(Name = "Клиенти")]
    [Table("nom_clients")]
    [HiddenDatesOnUI]
    public class Client : BaseCommonNomenclature
    {
        [Display(Name = "Адрес")]
        public string Adress { get; set; }
    }
}
