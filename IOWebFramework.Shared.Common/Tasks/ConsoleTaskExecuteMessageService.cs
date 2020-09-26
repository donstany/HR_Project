using System;
using System.Collections.Generic;
using System.Text;
using IOWebFramework.Shared.Common.Contracts;
using Microsoft.Extensions.Logging;

namespace IOWebFramework.Shared.Common.Tasks
{
    public class ConsoleTaskExecuteMessageService : IConsoleTaskExecuteMessageService
    {
        private readonly ILogger<ConsoleTaskExecuteMessageService> _logger;

        public ConsoleTaskExecuteMessageService(ILogger<ConsoleTaskExecuteMessageService> logger)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._logger = logger;
        }
        public void DoSomthingA()
        {
            this._logger.LogInformation("DoSomthingA A Called !");
        }

        public void DoSomthingB()
        {
            this._logger.LogInformation("DoSomthingB B Called !");
        }
    }
}
