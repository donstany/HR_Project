namespace IOWebFramework.Shared.Common.MessageQueue
{
    public class MQMessageModel
    {
        public string ClientId { get; set; }

        public string Method { get; set; }

        public string Params { get; set; }
    }
}
