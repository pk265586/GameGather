using GameGather.ExternalApi.Interfaces;
using GameGather.Model;

namespace GameGather.ExternalApi.Provider
{
    public class RandomGamesProvider : IExternalGameProvider
    {
        static string[] sports = {
            "Chess",
            "Football",
            "Running",
            "Jumping",
            "Swimming",
            "Poker",
            "Coffee Drinking",
            "Shawarma Eating",
            "TheBox Hacking",
            "DJ Battle",
            "RockPaperScissors",
            "Dishwasher Loading"
        };

        static string[] gameNames = {
            "World Championship",
            "Local Skirmish",
            "Yard Showdown",
            "Regional Cap",
            "Interplanet Contest",
            "Galaxy Rush",
            "Universe Meeting",
            "Solar Emulation",
            "Star Game",
            "35th Srteet Tournament",
            "15th Appartment Playoff",
            "Combo Break"
        };

        static string[] adjectives = {
            "Steel",
            "Determined",
            "Clever",
            "Great",
            "Soaring",
            "Rapid",
            "Tenacious",
            "Enduring",
            "Bright",
            "Free",
            "Stoic",
            "Magic"
        };

        static string[] nouns = {
            "Bulls",
            "Students",
            "Engineers",
            "Crushers",
            "Drivers",
            "Robots",
            "Thinkers",
            "Rangers",
            "Defenders",
            "Jokers",
            "Cats",
            "Doctors"
        };

        const int maxGameCount = 10;

        Random m_random = new Random();

        public Task<GameData[]> GatherGames(DateTime fromDate, DateTime toDate)
        {
            int currentCount = maxGameCount; // m_random.Next(maxGameCount);
            var result = Enumerable.Range(0, currentCount).Select(x => GenerateGame(fromDate, toDate)).ToArray();
            return Task.FromResult(result);
        }

        private GameData GenerateGame(DateTime fromDate, DateTime toDate)
        {
            if (fromDate.Year < 2000)
                fromDate = new DateTime(DateTime.UtcNow.Year, 1, 1);

            return new GameData
            {
                Name = GetRandomItem(gameNames),
                SportType = GetRandomItem(sports),
                TeamName1 = GetRandomTeamName(),
                TeamName2 = GetRandomTeamName(),
                StartDate = fromDate.AddMinutes(m_random.Next((int)toDate.Subtract(fromDate).TotalMinutes))
            };
        }

        private string GetRandomTeamName() 
        {
            return $"{GetRandomItem(adjectives)} {GetRandomItem(nouns)}";
        }

        private string GetRandomItem(string[] items) 
        {
            return items[m_random.Next(items.Length)];
        }
    }
}
