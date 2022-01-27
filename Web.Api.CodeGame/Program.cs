using CodeGame.Business;
using CodeGame.Business.Abstract;
using CodeGame.Business.Abstract.DTO;
using CodeGame.Data.Mock;
using CodeGame.Data.Mock.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Web.Api.CodeGame
{
    public class Program
    {
        private static List<HeroClasses> heroClass;
        private static List<Characters> chars;

        public static void Main(string[] args)
        {
            LoadData();
            CreateHostBuilder(args).Build().Run();
        }
        private static void LoadData()
        {
            IServiceProvider service = (new ServiceCollection())
                .AddScoped<IDataChars, DataChars>()
                .AddScoped<IDataHeroes, DataHeroes>()
                .AddScoped<IRequestDataGame, RequestDataGame>()                
                .BuildServiceProvider();

            var configuration = new ConfigurationBuilder()
                      .AddJsonFile("appsettings.json")
                      .Build();
            bool useMockChar = Convert.ToBoolean(configuration.GetSection("Mock").Get<Dictionary<string, string>>()["UseCharMock"]);
            bool useMockHero = Convert.ToBoolean(configuration.GetSection("Mock").Get<Dictionary<string, string>>()["UseHeroMock"]); 
            var dataGame = service.GetService<IRequestDataGame>();
            if (useMockHero)
                heroClass = dataGame.LoadHeroClass();
            if (useMockChar && useMockHero)            
                chars = dataGame.CreateMockLoadChar(heroClass.ToList());                       
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .ConfigureServices((context, service) =>
            {
                service
                .AddScoped<IDataChars, DataChars>()
                .AddScoped<IDataHeroes, DataHeroes>()
                .AddScoped<IRequestDataGame, RequestDataGame>()
                .AddSingleton(_ => new GameHero(heroClass))
                .AddSingleton(_ => new GameChar(chars));
            });


    }
}
