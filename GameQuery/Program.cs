using GameGather.DataAccess.Interfaces;
using GameGather.DataAccess.Provider;
using GameQuery.Configuration;

namespace GameQuery
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

            builder.Services.AddSingleton<IGameDataProvider, GameDataProvider>(p => new GameDataProvider(AppConfig.GameDataConnection));

            if (AppConfig.UseSwagger)
            {
                // Add swagger and endpoint API explorer services
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();
            }

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            //app.UseHttpsRedirection();
            //app.UseAuthorization();

            app.Map("/", () => "Hello from GameQuery program!");

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
