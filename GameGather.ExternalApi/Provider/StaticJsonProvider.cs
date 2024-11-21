using System.Text.Json;
using GameGather.ExternalApi.Interfaces;
using GameGather.Model;

namespace GameGather.ExternalApi.Provider
{
    public class StaticJsonProvider : IExternalGameProvider
    {
        const string FileName = "..\\StaticGames.json";

        public Task<GameData[]> GatherGames(DateTime fromDate, DateTime toDate)
        {
            if (!File.Exists(FileName))
                throw new Exception("File not found!");

            var jsonText = File.ReadAllText(FileName);
            var result = JsonSerializer.Deserialize<GameData[]>(jsonText);
            return Task.FromResult(result);
        }
    }
}
