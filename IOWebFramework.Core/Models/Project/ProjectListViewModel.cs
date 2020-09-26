using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace IOWebFramework.Core.Models.Project
{
    public class ProjectListViewModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("client")]
        public string Client { get; set; }

        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }

        [JsonProperty("endDate")]
        public DateTime? EndDate { get; set; }

        [JsonProperty("manager")]
        public string Manager { get; set; }
    }
}
