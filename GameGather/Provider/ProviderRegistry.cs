using GameGather.ExternalApi.Interfaces;
using GameGather.ExternalApi.Provider;
using GameGather.Interfaces;

namespace GameGather.Provider
{
    public class ProviderRegistry : IProviderRegistry
    {
        private List<IExternalGameProvider> providers = new List<IExternalGameProvider>();

        public void RegisterProvider(IExternalGameProvider provider)
        {
            providers.Add(provider);
        }

        public IEnumerable<IExternalGameProvider> GetRegisteredProviders()
        {
            return providers;
        }

        public void RegisterDefaultProviders() 
        {
            RegisterProvider(new StaticJsonProvider());
            RegisterProvider(new SelfGameProvider());
            RegisterProvider(new RandomGamesProvider());
        }
    }
}
