using IOWebFramework.Core.Contracts;
using IOWebFrameworkApplicationService.Extentions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace IOWebApplicationService
{
    /// <summary>
    /// Имплементация на универсална услуга с Таймер. Long running service - daemon or windows service
    /// (Windows Service / Linux Systemd Service)
    /// </summary>
    public class TimedHostedService : IHostedService
    {
        private readonly IHostApplicationLifetime appLifetime;
        private readonly ILogger<TimedHostedService> logger;
        private readonly IHostEnvironment environment;
        private readonly IConfiguration configuration;
        private System.Timers.Timer _timer = null;
        private readonly double interval;
        private readonly int stopServiceTimeout;
        private readonly string startSyncTime;
        private readonly string endSyncTime;
        private readonly IServiceProvider serviceProvider;
        private bool processCompleted;

        public TimedHostedService(
            IConfiguration configuration,
            IHostEnvironment environment,
            ILogger<TimedHostedService> logger,
            IHostApplicationLifetime appLifetime,
            IServiceProvider serviceProvider)
        {
            this.configuration = configuration;
            this.logger = logger;
            this.appLifetime = appLifetime;
            this.environment = environment;
            this.interval = configuration.GetValue<double>("TimerInterval");
            this.stopServiceTimeout = configuration.GetValue<int>("StopServiceTimeout");
            this.startSyncTime = configuration.GetValue<string>("StartSyncTime");
            this.endSyncTime = configuration.GetValue<string>("EndSyncTime");
            this.serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.logger.LogInformation("StartAsync method called.");

            this.appLifetime.ApplicationStarted.Register(OnStarted);
            this.appLifetime.ApplicationStopping.Register(OnStopping);
            this.appLifetime.ApplicationStopped.Register(OnStopped);

            return Task.CompletedTask;

        }

        private void OnStarted()
        {
            this.logger.LogInformation("OnStarted method called.");

            try
            {
                _timer = new System.Timers.Timer();
                _timer.Interval = interval;

                // Закачаме функцията, която се вика при изтичане на таймера
                _timer.Elapsed += Timer_Elapsed;
                _timer.Start();
                //processCompleted = true;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, ex.Message);
            }
        }

        private void OnStopping()
        {
            this.logger.LogInformation("OnStopping method called.");
            int seconds = 0;

            while (!processCompleted)
            {
                Thread.Sleep(1000);
                seconds++;

                if (stopServiceTimeout > 0 && (seconds * 1000) > stopServiceTimeout)
                {
                    break;
                }
            }

            try
            {
                _timer?.Stop();
                _timer?.Close();
                _timer?.Dispose();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, ex.Message);
            }

        }

        private void OnStopped()
        {
            this.logger.LogInformation("OnStopped method called.");
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.logger.LogInformation("StopAsync method called.");

            return Task.CompletedTask;
        }

        protected void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                // Спираме таймера за да си осигурим една единствена инстанция на work-ъра
                _timer.Stop();
                // рестартиране на всеки ... в милисекунди
                _timer.Interval = interval;

                using (var scope = serviceProvider.CreateScope())
                {
                    var syncService = scope.ServiceProvider.GetRequiredService<IEmployeeService>();
                    var currentTimeSync = DateTime.Now.ToString("HH:mm");
                   
                    //processCompleted = false;
                    if (currentTimeSync.CutLastChar() == this.startSyncTime.CutLastChar() && processCompleted == false)
                    {
                        syncService.SyncJobScheduler();
                        processCompleted = true;
                    }

                    if (currentTimeSync.CutLastChar() == this.endSyncTime)
                    {
                        processCompleted = false;
                    }
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, ex.Message);
            }
            finally
            {
                // Включваме таймера независимо от резултата
                _timer.Start();
            }
        }
    }
}