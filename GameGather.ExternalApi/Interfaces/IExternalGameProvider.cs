using System;
using GameGather.Model;

namespace GameGather.ExternalApi.Interfaces
{
    public interface IExternalGameProvider
    {
        Task<GameData[]> GatherGames(DateTime fromDate, DateTime toDate);
    }
}
