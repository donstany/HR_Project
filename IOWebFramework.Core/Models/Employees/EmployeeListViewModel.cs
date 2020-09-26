using Newtonsoft.Json;

namespace IOWebFramework.Core.Models.Employees
{
    public class EmployeeListViewModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("personId")]
        public int PersonId { get; set; }

        [JsonProperty("td")]
        public string Td { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("branch")]
        public string Branch { get; set; }

        [JsonProperty("department")]
        public string Department { get; set; }
    }
}
