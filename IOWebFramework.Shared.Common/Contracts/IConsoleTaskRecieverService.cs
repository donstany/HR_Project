using IOWebFramework.Shared.Common.MessageQueue;

namespace IOWebFramework.Shared.Common.Contracts
{
    public interface IConsoleTaskRecieverService
    {
       public void RecieveMessage(MQMessageModel resultModel);
    }
}
