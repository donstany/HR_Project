using Newtonsoft.Json;

namespace IOWebFramework.Core.Models.Classifier
{
    public class ClassifierListViewModel
    {
        /// <summary>
        /// Идентификатор на запис
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Име на областта
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Дали записа е активен
        /// </summary>
        [JsonProperty("isActive")]
        public bool IsActive { get; set; }
    }
}
