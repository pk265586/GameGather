using System;
using GameGather.ExternalApi.Interfaces;
using GameGather.Model;
using GameGather.DataAccess.Interfaces;

namespace GameGather.Logic
{
    public class GameDataGatherEngine
    {
        IExternalGameProvider m_gameProvider;
        IGameDataProvider m_gameDataProvider;

        public GameDataGatherEngine(IExternalGameProvider gameProvider, IGameDataProvider gameDataProvider) 
        {
            m_gameProvider = gameProvider;
            m_gameDataProvider = gameDataProvider;
        }

        public async Task GatherAndSave(DateTime fromDate, DateTime toDate) 
        {
            IEnumerable<GameData> games = await m_gameProvider.GatherGames(fromDate, toDate);
            games = SelectDistinctGames(games).ToList();
            await SaveGames(games);
        }

        private async Task SaveGames(IEnumerable<GameData> games)
        {
            foreach (var game in games) 
            {
                await SaveGame(game);
            }
        }

        private async Task SaveGame(GameData game)
        {
            await m_gameDataProvider.AddGame(game);
        }

        private struct GameKey 
        {
            public string SportType;
            public string Name;
            public string TeamName1;
            public string TeamName2;

        }

        private IEnumerable<GameData> SelectDistinctGames(IEnumerable<GameData> games) 
        {
            var groups = games.GroupBy(game => new GameKey
            {
                Name = game.Name,
                SportType = game.SportType,
                TeamName1 = MinString(game.TeamName1, game.TeamName2),
                TeamName2 = MaxString(game.TeamName1, game.TeamName2)
            });

            foreach (var group in groups) 
            {
                DateTime? currentDate = null;
                foreach (var game in group.OrderBy(g => g.StartDate))
                {
                    if (currentDate == null || game.StartDate.Subtract(currentDate.Value) >= GameConst.MinGamesDelta) 
                    {
                        currentDate = game.StartDate;
                        yield return game;
                    }
                }
            }
        }

        private string MinString(params string[] values) 
        {
            return values.Min();
        }

        private string MaxString(params string[] values)
        {
            return values.Max();
        }
    }
}
