using System;
using Newtonsoft.Json;

namespace IOWebFramework.Core.Models.Dossier
{
    public class CertificateListViewModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("certificateNameIssuer")]
        public string CertificateNameIssuer { get; set; }

        [JsonProperty("certificateType")]
        public string CertificateType { get; set; }

        [JsonProperty("dateStart")]
        public DateTime DateStart { get; set; }

        [JsonProperty("dateEnd")]
        public DateTime? DateEnd { get; set; }

        [JsonProperty("fileContentId")]
        public string FileContentId { get; set; }

        [JsonProperty("personId")]
        public int PersonId { get; set; }


    }
}
