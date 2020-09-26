using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace IOWebFramework.Core.Models.ProjectDetail
{
    public class ProjectPersonListViewModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }

        [JsonProperty("endDate")]
        public DateTime? EndDate { get; set; }

        [JsonProperty("projectRole")]
        public string ProjectRole { get; set; }
    }
}
