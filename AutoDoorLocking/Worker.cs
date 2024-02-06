using Clay.Application.Services;
using Clay.Domain.Services;

namespace AutoDoorLocking
{
    public class Worker : BackgroundService
    {
        public IServiceProvider Services {  get; set; }

        public Worker(IServiceProvider services)
        {
            Services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using(var scope = Services.CreateAsyncScope())
                {
                    var lockService = scope.ServiceProvider.GetRequiredService<ILockService>();
                    await lockService.AutoLockDoors();
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
