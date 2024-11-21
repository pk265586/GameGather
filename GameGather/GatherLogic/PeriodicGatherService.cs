using GameGather.Configuration;
using GameGather.Interfaces;

namespace GameGather.GatherLogic
{
    public class PeriodicGatherService : IHostedService, IDisposable
    {
        private System.Timers.Timer m_timer;
        private IGatherLogic m_gatherLogic;

        public PeriodicGatherService(IGatherLogic gatherLogic)
        {
            m_gatherLogic = gatherLogic;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            m_timer = new System.Timers.Timer(TimeSpan.FromMinutes(AppConfig.PeriodicGatherMinutes));
            m_timer.Elapsed += async (s, e) => { await DoPeriodicGather(); };
            m_timer.Start();
            return Task.CompletedTask;
        }

        private async Task DoPeriodicGather()
        {
            await m_gatherLogic.StartGatherData();
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            m_timer?.Stop();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            m_timer?.Dispose();
        }
    }
}
