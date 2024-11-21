using GameGather.ExternalApi.Interfaces;

namespace GameGather.Interfaces
{
    public interface IProviderRegistry
    {
        void RegisterProvider(IExternalGameProvider provider);
        IEnumerable<IExternalGameProvider> GetRegisteredProviders();
    }
}
