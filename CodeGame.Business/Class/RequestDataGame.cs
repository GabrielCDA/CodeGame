using CodeGame.Business.Abstract;
using CodeGame.Business.Abstract.DTO;
using CodeGame.Data.Mock.Interface;
using System;
using System.Collections.Generic;


namespace CodeGame.Business
{
    public class RequestDataGame : IRequestDataGame
    {
        readonly IDataHeroes dataHeroes;
        readonly IDataChars dataChars;
        public List<Characters> _chars;
        public List<HeroClasses> _heroes;
        public RequestDataGame(
            IDataHeroes dataHeroes,
            IDataChars dataChars)
        {
            this.dataHeroes = dataHeroes ?? throw new NullReferenceException(nameof(IDataHeroes));
            this.dataChars = dataChars ?? throw new NullReferenceException(nameof(IDataChars));
        }


        public List<HeroClasses> LoadHeroClass()
        {
            return dataHeroes.GetAllHeroes();                      
        }

        public List<Characters> LoadChars()
        {
            return dataChars.GetAllChars();
        }

        public List<Characters> CreateMockLoadChar(List<HeroClasses> heroes)
        {
            return dataChars.CreateMockLoad(heroes);
        }
        public ReturnNewChar CreateChar(HeroClasses heroClass, string nameChar)
        {
            ReturnNewChar returnNewChar = new ReturnNewChar();
            Characters newChar = new Characters();
            newChar.classHero = heroClass;
            newChar.nameCharacter = nameChar;
            newChar.statusAlive = status.Alive;
            newChar.professionCharacter = heroClass.nameClass.ToString();

            newChar.lifeChar = heroClass.attributes.life;
            newChar.intelligenceChar = heroClass.attributes.intelligence;
            newChar.dexterityChar = heroClass.attributes.dexterity;
            newChar.strengthChar = heroClass.attributes.strength;

            returnNewChar.newChar = new Characters();
            returnNewChar.newChar = newChar;
            returnNewChar.returnDTO = dataChars.AddNewChar(newChar);
            return returnNewChar;
        }
    }
}
