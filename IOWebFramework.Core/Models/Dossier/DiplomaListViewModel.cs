using System;
using Newtonsoft.Json;

namespace IOWebFramework.Core.Models.Dossier
{
    public class DiplomaListViewModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("degree")]
        public string Degree { get; set; }

        [JsonProperty("specialty")]
        public string Specialty { get; set; }

        [JsonProperty("educationInstitution")]
        public string EducationInstitution { get; set; }

        [JsonProperty("successfulExam")]
        public DateTime SuccessfulExam { get; set; }

        [JsonProperty("issueDate")]
        public DateTime IssueDate { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }
    }
}
