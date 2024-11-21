using GameGather.Interfaces;
using GameGather.Provider;
using GameGather.Configuration;
using GameGather.DataAccess.Interfaces;
using GameGather.DataAccess.Provider;
using GameGather.ExternalApi.Interfaces;
using GameGather.Logic;

namespace GameGather.GatherLogic
{
    public class GameGatherLogic: IGatherLogic
    {
        IProviderRegistry m_providerRegistry;
        IGameDataProvider m_gameDataProvider;

        public GameGatherLogic(IProviderRegistry providerRegistry, IGameDataProvider gameDataProvider) 
        {
            m_providerRegistry = providerRegistry;
            m_gameDataProvider = gameDataProvider;
        }

        public async Task StartGatherData()
        {
            var lastGatherDate = AppConfig.LastGatherDate;
            var currentDate = DateTime.UtcNow;

            var providers = m_providerRegistry.GetRegisteredProviders();
            var gatherTasks = new List<Task>();
            foreach (var provider in providers)
            {
                gatherTasks.Add(GatherData(provider, lastGatherDate, currentDate));
            }

            await Task.WhenAll(gatherTasks);

            AppConfig.LastGatherDate = currentDate;
        }

        private async Task GatherData(IExternalGameProvider gameProvider, DateTime fromDate, DateTime toDate)
        {
            var engine = new GameDataGatherEngine(gameProvider, m_gameDataProvider);
            await engine.GatherAndSave(fromDate, toDate);
        }
    }
}
