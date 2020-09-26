using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace IOWebFramework.Core.Models.ProjectDetail
{
    public class TeamListViewModel
    {
        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }

        [JsonProperty("endDate")]
        public DateTime? EndDate { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("department")]
        public string Department { get; set; }

        [JsonProperty("projectRole")]
        public string ProjectRole { get; set; }

        [JsonProperty("personId")]
        public int PersonId { get; set; }

        [JsonProperty("projectId")]
        public int ProjectId { get; set; }
    }
}
