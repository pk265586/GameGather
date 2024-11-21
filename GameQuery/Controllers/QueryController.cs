using GameGather.DataAccess.Interfaces;
using GameGather.Model;
using Microsoft.AspNetCore.Mvc;

namespace GameQuery.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class QueryController : ControllerBase
    {
        IGameDataProvider m_gameDataProvider;

        public QueryController(IGameDataProvider gameDataProvider) 
        {
            m_gameDataProvider = gameDataProvider;
        }

        [HttpPost]
        public async Task<GameData[]> QueryGames(GameQueryModel query)
        {
            return await m_gameDataProvider.GetGamesAsync(query);
        }
    }
}
