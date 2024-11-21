using GameGather.Configuration;
using GameGather.DataAccess.Interfaces;
using GameGather.DataAccess.Provider;
using GameGather.Interfaces;
using GameGather.Provider;
using GameGather.GatherLogic;

namespace GameGather
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // First, load the configuration from the appsettings.json file
            var configBuilder = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var config = configBuilder.Build();
            AppConfig.Load(config);

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddSingleton<IGatherLogic, GameGatherLogic>();
            builder.Services.AddSingleton<IGameDataProvider, GameDataProvider>(p => new GameDataProvider(AppConfig.GameDataConnection));
            builder.Services.AddSingleton<IProviderRegistry, ProviderRegistry>(p =>
            {
                var registry = new ProviderRegistry();
                registry.RegisterDefaultProviders();
                return registry;
            });

            if (AppConfig.UseSwagger)
            {
                // Add swagger and endpoint API explorer services
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();
            }

            builder.Services.AddHostedService<PeriodicGatherService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            //app.UseHttpsRedirection();
            //app.UseAuthorization();

            app.Map("/", () => "Hello from program!");

            app.MapControllers();

            if (app.Environment.IsDevelopment() && AppConfig.UseSwagger)
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.Run();
        }
    }
}
