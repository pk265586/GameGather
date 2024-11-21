using System;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using GameGather.ExternalApi.Interfaces;
using GameGather.Model;

namespace GameGather.ExternalApi.Provider
{
    public class SelfGameProvider : IExternalGameProvider
    {
        const string url = "http://localhost:5206/api/Query/QueryGames";

        public async Task<GameData[]> GatherGames(DateTime fromDate, DateTime toDate)
        {
            var client = new HttpClient();

            var request = new GameQueryModel
            {
                FromDate = fromDate, 
                ToDate = toDate 
            };

            var payload = JsonSerializer.Serialize(request);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<GameData[]>();
            }

            return null;
        }
    }
}
