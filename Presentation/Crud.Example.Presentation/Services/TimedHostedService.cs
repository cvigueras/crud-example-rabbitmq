using Crud.Example.Main.Interfaces;

namespace Crud.Example.Api.Services
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<TimedHostedService>? _logger;
        private readonly IServiceProvider? _services;
        private Timer? _timer;
        private Task? _executingTask;
        private CancellationTokenSource? _stoppingCts;

        public TimedHostedService(IServiceProvider services, ILogger<TimedHostedService> logger) =>
            (_services, _logger) = (services, logger);

        public Task? StartAsync(CancellationToken cancellationToken)
        {
            _stoppingCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            _timer = new Timer(callback: FireTask, default, TimeSpan.FromSeconds(90), TimeSpan.FromSeconds(90));
            _logger!.LogInformation("Started timer");
            return Task.CompletedTask;
        }

        private void FireTask(object state)
        {
            if (_executingTask == null || _executingTask.IsCompleted)
            {
                _logger!.LogInformation("No task is running, check for new job");
                _executingTask = ExecuteNextJobAsync();
            }
            else
            {
                _logger!.LogInformation("There is a task still running, wait for next cycle");
            }
        }

        private async Task ExecuteNextJobAsync()
        {
            using var scope = _services!.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ICheckShopExpiresServices>();
            context.CheckExpirationShops();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger!.LogInformation("Initiate graceful shutdown");
            _timer?.Change(Timeout.Infinite, 0);
            if (_executingTask == null || _executingTask.IsCompleted)
            {
                return;
            }

            try
            {
                _stoppingCts?.Cancel();
            }
            finally
            {
                await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite, cancellationToken));
            }
        }

        public void Dispose()
        {
            _timer?.Dispose();
            _stoppingCts?.Cancel();
        }
    }
}