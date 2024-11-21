namespace GameQuery.Configuration
{
    public class AppConfig
    {
        public static bool UseSwagger { get; private set; }
        public static string GameDataConnection { get; private set; } = "";

        const string GameDataConnectionStringConfig = "GameData";

        public static void Load(IConfiguration config)
        {
            GameDataConnection = config.GetConnectionString(GameDataConnectionStringConfig) ?? "";
            UseSwagger = config.GetValue<bool>("UseSwagger");
        }
    }
}
