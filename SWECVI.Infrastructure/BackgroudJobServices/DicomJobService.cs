//using System.Timers;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Hosting;
//using SWECVI.ApplicationCore.Entities;
//using SWECVI.ApplicationCore.Interfaces;
//using SWECVI.ApplicationCore.Constants;
//using SWECVI.ApplicationCore.Interfaces.Services;

//namespace SWECVI.Infrastructure.JobServices
//{
//    public class DicomJobService : IHostedService
//    {
//        private readonly IServiceScopeFactory _serviceScopeFactory;
//        private readonly double IntervalTime = 1000 * 60 * 30;
//        private readonly IConfiguration _configuration;
//        private readonly ILogger<DicomJobService> _logger;
//        private System.Timers.Timer _timer;

//        public DicomJobService(IServiceScopeFactory serviceScopeFactory, ILogger<DicomJobService> logger, IConfiguration config)
//        {
//            _serviceScopeFactory = serviceScopeFactory;
//            _logger = logger;
//            _configuration = config;
//        }

//        public async Task StartAsync(CancellationToken cancellationToken)
//        {
//            _timer = new System.Timers.Timer();
//            _timer.Interval = IntervalTime;
//            _timer.Elapsed += SyncDataFromDcm4cheServerJob;
//            _timer.Enabled = true;
//            await Task.CompletedTask;

//        }
//        //     Indicates that the shutdown process should no longer be graceful.
//        public async Task StopAsync(CancellationToken cancellationToken)
//        {
//            _timer?.Stop();
//            _timer?.Dispose();
//            await Task.CompletedTask;
//        }

//        private async void SyncDataFromDcm4cheServerJob(Object source, ElapsedEventArgs e)
//        {
//            try
//            {
//                _logger.LogInformation("Start Sync Dcm4chee Data");
//                using (var scope = _serviceScopeFactory.CreateScope())
//                {
//                    var service = scope.ServiceProvider.GetService<IDicomService>();
//                    if (service != null)
//                    {
//                        await service.SyncDataFromDcm4cheServer(false);
//                    }
//                }
//                _logger.LogInformation("End Sync Dcm4chee Data");
//            }
//            catch (Exception ex)
//            {
//                using (var scope = _serviceScopeFactory.CreateScope())
//                {
//                    var service = scope.ServiceProvider.GetService<ISystemLogRepository>();
//                    if (service != null)
//                    {
//                        await service.Add(new SystemLog()
//                        {
//                            Message = ex.Message,
//                            Status = SystemLogStatus.SYNC_DCM4CHE_SERVICE,
//                        });
//                    }
//                }
//                _logger.LogError(ex, "Sync Dcm4chee Data job failed");
//            }
//        }
//    }
//}