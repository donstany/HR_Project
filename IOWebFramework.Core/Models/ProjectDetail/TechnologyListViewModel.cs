using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace IOWebFramework.Core.Models.ProjectDetail
{
    public class TechnologyListViewModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
