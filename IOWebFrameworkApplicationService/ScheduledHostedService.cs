//using IOWebApplication.Core.Contracts;
//using IOWebApplication.Core.MessageQueue;
using IOWebFramework.Shared.Common.Contracts;
using IOWebFramework.Shared.Common.MessageQueue;
using IOWebFramework.Shared.Common.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
//using Newtonsoft.Json;
//using RabbitMQ.Client;
//using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IOWebApplicationService
{
    public class ScheduledHostedService : IHostedService
    {
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly ILogger<ScheduledHostedService> _logger;
        private readonly IHostEnvironment _environment;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private IConnection _connection;
        private IModel _channel;
        private string _exchangeName;
        private string _queueName;

        private readonly IConsoleTaskRecieverService _consoleTaskReciever;

        public ScheduledHostedService(IConfiguration configuration,
                                      IHostEnvironment environment,
                                      ILogger<ScheduledHostedService> logger,
                                      IHostApplicationLifetime appLifetime,
                                      IServiceProvider serviceProvider,
                                      IConsoleTaskRecieverService consoleTaskReciever)
        {
            this._configuration = configuration;
            this._logger = logger;
            this._appLifetime = appLifetime;
            this._environment = environment;
            this._serviceProvider = serviceProvider;
            this._consoleTaskReciever = consoleTaskReciever;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("StartAsync method called.");

            _appLifetime.ApplicationStarted.Register(OnStarted);
            _appLifetime.ApplicationStopping.Register(OnStopping);
            _appLifetime.ApplicationStopped.Register(OnStopped);

            return Task.CompletedTask;

        }

        private void OnStarted()
        {
            _logger.LogInformation("OnStarted method called.");

            try
            {
                //ToDo: Да се добави кода на RabbitMQ клиента
                string hostName = _configuration.GetValue<string>("MessageQueue:HostName");
                var factory = new ConnectionFactory() { HostName = hostName, UserName = "stamo", Password = "13020333" };

                _exchangeName = _configuration.GetValue<string>("MessageQueue:ExchangeName");

                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare(exchange: _exchangeName, type: "fanout");
                _queueName = _channel.QueueDeclare().QueueName;
                _channel.QueueBind(queue: _queueName,
                                  exchange: _exchangeName,
                                  routingKey: "");

                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += MessageReceived;

                _channel.BasicConsume(queue: _queueName,
                                     autoAck: true,
                                     consumer: consumer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        private void OnStopping()
        {
            _logger.LogInformation("OnStopping method called.");

            try
            {
                // Освобождават се ресурсите свързани с клиента
                _channel.Dispose();
                _connection.Dispose();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

        }

        private void OnStopped()
        {
            _logger.LogInformation("OnStopped method called.");
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("StopAsync method called.");

            return Task.CompletedTask;
        }

        protected void MessageReceived(object sender, BasicDeliverEventArgs ea)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    // проверка дали съобщението е за дадения клиент
                    // обработка на полученото съобщение
                    var body = ea.Body;
                    //var message = Encoding.UTF8.GetString(body);
                    //MQMessageModel resultModel = JsonConvert.DeserializeObject<MQMessageModel>(message);
                    //if (resultModel.ClientId == _configuration.GetValue<string>("MessageQueue:ClientId"))
                    //{
                    //    // TODO call service method
                    //    this._consoleTaskReciever.RecieveMessage(resultModel);
                    //}

                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message);
            }
        }
    }
}
