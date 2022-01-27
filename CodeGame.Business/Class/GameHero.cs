using CodeGame.Business.Abstract;
using CodeGame.Business.Abstract.DTO;
using CodeGame.Data.Mock;
using CodeGame.Data.Mock.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace CodeGame.Business
{
    public class GameHero : IGameHero<HeroClasses>
    {
        public List<HeroClasses> _heroes;
        public IRequestDataGame service;

        public GameHero(List<HeroClasses> heroes)
        {
            _heroes = heroes;

            var injectionService = (new ServiceCollection())
              .AddScoped<IDataChars, DataChars>()
              .AddScoped<IDataHeroes, DataHeroes>()
              .AddScoped<IRequestDataGame, RequestDataGame>()
              .BuildServiceProvider();
            service = injectionService.GetService<IRequestDataGame>();
        }

        public List<HeroClasses> heroes() => service.LoadHeroClass();

        public GetHeroesDTO GetHeroes()
        {
            GetHeroesDTO getHeroesDTO = new GetHeroesDTO();
            CommonDTO statusReturn = new CommonDTO();
            var heroes = service.LoadHeroClass();
            if (heroes.Count == 0)
            {
                statusReturn.statusCode = 8;
                statusReturn.message = "No heroes were found";
                getHeroesDTO.returnDTO = statusReturn;

                return getHeroesDTO;
            }
            statusReturn.message = "Heroes were found";
            getHeroesDTO.returnDTO = statusReturn;
            getHeroesDTO.Heroes = heroes;
            return getHeroesDTO;
        }


    }
}
