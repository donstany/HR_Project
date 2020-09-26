using Newtonsoft.Json;
using System.Collections.Generic;

namespace IOWebFramework.Core.Models.Nomenclatures
{
    public class HierarchicalNomenclatureDisplayModel
    {
        [JsonProperty("data")]
        public List<HierarchicalNomenclatureDisplayItem> Data { get; set; }

        public HierarchicalNomenclatureDisplayModel()
        {
            Data = new List<HierarchicalNomenclatureDisplayItem>();
        }
    }
}
