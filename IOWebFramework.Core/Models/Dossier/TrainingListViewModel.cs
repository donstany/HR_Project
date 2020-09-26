using System;
using Newtonsoft.Json;

namespace IOWebFramework.Core.Models.Dossier
{
    public class TrainingListViewModel
    {
        public int Id { get; set; }

        [JsonProperty("trainingName")]
        public string TrainingName { get; set; }

        [JsonProperty("trainingCenter")]
        public string TrainingCenter { get; set; }

        [JsonProperty("startDate")]
        public DateTime DateStart { get; set; }

        [JsonProperty("endDate")]
        public DateTime? DateEnd { get; set; }
    }
}
