using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static IOWebFramework.Shared.Common.MessageConstant;

namespace IOWebFramework.Core.Models.CertificateNameIssuer
{
    public class CertificateNameIssuerViewModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("parentId")]
        public int ParentId { get; set; }

        [JsonProperty("name")]
        [DisplayName("Име")]
        [Required(ErrorMessage = FieldIsMandatory)]
        public string Name { get; set; }

        [JsonProperty("isActive")]
        [DisplayName("Активен")]
        public bool IsActive { get; set; }

        public List<BreadcrumbInfoModel> BreadcrumbInfo { get; set; }

        public CertificateNameIssuerViewModel()
        {
            BreadcrumbInfo = new List<BreadcrumbInfoModel>();
        }
    }
}
