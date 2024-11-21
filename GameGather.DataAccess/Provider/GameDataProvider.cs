using GameGather.DataAccess.Interfaces;
using GameGather.DataAccess.Model;
using GameGather.Model;

namespace GameGather.DataAccess.Provider
{
    public class GameDataProvider : IGameDataProvider
    {
        string m_connectionString;

        public GameDataProvider(string connectionString)
        {
            m_connectionString = connectionString;
        }

        public async Task<GameData[]> GetGamesAsync(GameQueryModel query)
        {
            string topQuery = query.RowCount > 0 ? $"Top {query.RowCount}" : "";
            var filter = GetSqlFilter(query);
            string sqlText =
                $"SELECT {topQuery} Id, StartDate, SportType, Name, TeamName1, TeamName2 " +
                $"  FROM GameData {filter.GetWhereText()}";

            var result = await DapperExt.GetDataTableAsync<GameData>(m_connectionString,
                sqlText,
                filter.GetParameters());
            return result;
        }

        private SqlFilter GetSqlFilter(GameQueryModel query)
        {
            var filter = new SqlFilter();

            if (query.FromDate.HasValue)
                filter.AddCondition($"unixepoch(StartDate) >= @StartDate", 
                    "@StartDate", 
                    new DateTimeOffset(DateTime.SpecifyKind(query.FromDate.Value, DateTimeKind.Utc)).ToUnixTimeSeconds());

            if (query.ToDate.HasValue)
                filter.AddCondition($"StartDate <= @ToDate", "@ToDate", query.ToDate.Value);

            if (!string.IsNullOrWhiteSpace(query.SportType))
                filter.AddCondition($"SportType = @SportType", "@SportType", query.SportType);

            if (!string.IsNullOrWhiteSpace(query.Name))
                filter.AddCondition($"instr(Name, @Name) > 0", "@Name", query.Name);

            if (!string.IsNullOrWhiteSpace(query.Team))
                filter.AddCondition($"instr(TeamName1 || char(1) || TeamName2, @Team) > 0", "@Team", query.Team);

            return filter;
        }

        public async Task AddGame(GameData game) 
        {
            // save teams in sorted order
            var teams = new[] { game.TeamName1, game.TeamName2 };
            string minTeam = teams.Min();
            string maxTeam = teams.Max();

            string sqlText =
                "BEGIN TRANSACTION; " +
                "Insert Or Ignore Into GameData (StartDate, SportType, Name, TeamName1, TeamName2) "+
                "Select @StartDate, @SportType, @Name, @TeamName1, @TeamName2 " +
                "Where not exists(Select Id From GameData " +
                                 " Where SportType = @SportType " +
                                 "   and Name = @Name " +
                                 "   and TeamName1 = @TeamName1 " +
                                 "   and TeamName2 = @TeamName2 " +
                                 "   and Abs(unixepoch(StartDate) - unixepoch(@StartDate)) < @MinGamesDelta); " +
                "COMMIT;";

            await DapperExt.ExecAsync(m_connectionString, sqlText,
                new Dictionary<string, object>
                {
                    ["@StartDate"] = game.StartDate,
                    ["@Name"] = game.Name,
                    ["@SportType"] = game.SportType,
                    ["@TeamName1"] = minTeam,
                    ["@TeamName2"] = maxTeam,
                    ["@MinGamesDelta"] = (int)GameConst.MinGamesDelta.TotalSeconds
                });
        }
    }
}
