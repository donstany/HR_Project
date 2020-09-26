namespace IOWebFramework.Core.Models.Nomenclatures
{
    public class LabelValueVM
    {
        public string Label { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public bool Selected { get; set; }

        public LabelValueVM()
        {

        }

        public LabelValueVM(object value, string label)
        {
            this.Value = value.ToString();
            this.Label = label;
        }
    }
}
