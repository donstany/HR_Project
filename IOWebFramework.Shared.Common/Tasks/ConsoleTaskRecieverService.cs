using System.Reflection;
using IOWebFramework.Shared.Common.Contracts;
using IOWebFramework.Shared.Common.MessageQueue;
using Microsoft.Extensions.Logging;

namespace IOWebFramework.Shared.Common.Tasks
{
    public class ConsoleTaskRecieverService : IConsoleTaskRecieverService
    {
        private readonly ILogger _logger;
        private readonly IConsoleTaskExecuteMessageService _executeMessageService;

        public ConsoleTaskRecieverService(
             ILogger<ConsoleTaskRecieverService> logger,
             IConsoleTaskExecuteMessageService executeMessageService)
        {
            this._logger = logger;
            this._executeMessageService = executeMessageService;
        }
        public void RecieveMessage(MQMessageModel resultModel)
        {
            MethodInfo[] methodInfos = typeof(ConsoleTaskExecuteMessageService)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance);

            foreach (var method in methodInfos)
            {
                if (resultModel.Method == method.Name)
                {
                    method.Invoke(_executeMessageService, null);
                }
            }
        }
    }
}
