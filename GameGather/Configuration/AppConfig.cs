namespace GameGather.Configuration
{
    public class AppConfig
    {
        public static bool UseSwagger { get; private set; }
        public static string GameDataConnection { get; private set; } = "";
        public static int PeriodicGatherMinutes { get; private set; } = 1;

        const string GameDataConnectionStringConfig = "GameData";

        internal static DateTime LastGatherDate { get; set; }

        public static void Load(IConfiguration config)
        {
            GameDataConnection = config.GetConnectionString(GameDataConnectionStringConfig) ?? "";
            UseSwagger = config.GetValue<bool>("UseSwagger");
            PeriodicGatherMinutes = config.GetValue<int>("PeriodicGatherMinutes");
        }
    }
}
