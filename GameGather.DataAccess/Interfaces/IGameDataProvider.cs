using GameGather.Model;

namespace GameGather.DataAccess.Interfaces
{
    public interface IGameDataProvider
    {
        Task<GameData[]> GetGamesAsync(GameQueryModel query);
        Task AddGame(GameData game);
    }
}
