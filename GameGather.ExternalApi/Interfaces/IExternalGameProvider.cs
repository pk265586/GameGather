using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameGather.Model;

namespace GameGather.ExternalApi.Interfaces
{
    public interface IExternalGameProvider
    {
        Task<GameData[]> GatherGames(DateTime fromDate, DateTime toDate);
    }
}
