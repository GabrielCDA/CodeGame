using CodeGame.Business;
using CodeGame.Data.Mock;
using System.Linq;
using Xunit;

namespace CodeGame.Test
{
    public class TestGameChars

    { 
        [Fact]
        public void TesteLoadChar()
        {
            bool loadChar = false;
            var listHeroes = new DataHeroes().GetAllHeroes();
            var listChars = new DataChars().CreateMockLoad(listHeroes);

            var LoadCharSimplified = new GameChar(listChars).LoadCharsSimplified();

            if (LoadCharSimplified.Chars.Count > 0)
            {
                if (LoadCharSimplified.returnDTO.statusCode ==0)
                    loadChar = true;
            }
            Assert.True(loadChar);
        }

        [Fact]
        public void DetailCharExist()
        {
            bool loadChar = false;
            var listHeroes = new DataHeroes().GetAllHeroes();
            var listChars = new DataChars().CreateMockLoad(listHeroes);

            var DetailCharSimplified = new GameChar(listChars).DetailChar("Aerif");

                if (DetailCharSimplified.returnDTO.statusCode == 0)
                    loadChar = true;
            
            Assert.True(loadChar);
        }

        [Fact]
        public void DetailCharnotExist()
        {
            bool loadChar = false;
            var listHeroes = new DataHeroes().GetAllHeroes();
            var listChars = new DataChars().CreateMockLoad(listHeroes);

            var DetailCharSimplified = new GameChar(listChars).DetailChar("Not_Exist_Char");

            if (DetailCharSimplified.returnDTO.statusCode == 0)
                loadChar = true;

            Assert.False(loadChar);
        }

        [Fact]
        public void TreyCreateCharAlredyExist()
        {
            bool loadChar = false;
            var listHeroes = new DataHeroes().GetAllHeroes();
            var listChars = new DataChars().CreateMockLoad(listHeroes);             
            var CreateChar = new GameChar(listChars).CreateChar(listHeroes.Where(x => x.nameClass == Business.Abstract.DTO.nameClass.Mage).First() ,"Aerif");

            if (CreateChar.statusCode == 0)
                loadChar = true;

            Assert.False(loadChar);
        }

        [Fact]
        public void CreateNewChar()
        {
            bool loadChar = false;
            var listHeroes = new DataHeroes().GetAllHeroes();
            var CreateChar = new GameChar(new System.Collections.Generic.List<Business.Abstract.DTO.Characters>()).CreateChar(listHeroes.Where(x => x.nameClass == Business.Abstract.DTO.nameClass.Mage).First(), "Aerif");

            if (CreateChar.statusCode == 0)
                loadChar = true;

            Assert.True(loadChar);
        }

        [Fact]
        public void BattleChar()
        {
            bool loadChar = false;
            var listHeroes = new DataHeroes().GetAllHeroes();
            var listChars = new DataChars().CreateMockLoad(listHeroes);
            var battleChar = new GameChar(listChars).BattleChar("Legodas", "Aerif");

            if (battleChar.returnDTO.statusCode == 0)
                loadChar = true; 

            Assert.True(loadChar);
        }

        [Fact]
        public void TryBattleCharWithCharNotExist()
        {
            bool loadChar = false;
            var listHeroes = new DataHeroes().GetAllHeroes();
            var listChars = new DataChars().CreateMockLoad(listHeroes);
            var battleChar = new GameChar(listChars).BattleChar("NoExist", "Aerif");

            if (battleChar.returnDTO.statusCode == 11)
                loadChar = true;

            Assert.True(loadChar);
        }

        [Fact]
        public void TryBattleCharWithCharBothNotExist()
        {
            bool loadChar = false;
            var listHeroes = new DataHeroes().GetAllHeroes();
            var battleChar = new GameChar(new System.Collections.Generic.List<Business.Abstract.DTO.Characters>()).BattleChar("Legodas", "Aerif");

            if (battleChar.returnDTO.statusCode == 10)
                loadChar = true;

            Assert.True(loadChar);
        }

        [Fact]
        public void TryBattleCharWithCharDead()
        {
            bool loadChar = false;
            var listHeroes = new DataHeroes().GetAllHeroes();
            var listChars = new DataChars().CreateMockLoad(listHeroes);
            var battleChar = new GameChar(listChars).BattleChar("Rei_Lagarto", "Aerif");

            if (battleChar.returnDTO.statusCode == 5)
                loadChar = true;

            Assert.True(loadChar);
        }

        [Fact]
        public void TryBattleCharWithBothCharDead()
        {
            bool loadChar = false;
            var listHeroes = new DataHeroes().GetAllHeroes();
            var listChars = new DataChars().CreateMockLoad(listHeroes);
            var battleChar = new GameChar(listChars).BattleChar("Rei_Lagarto", "Ledolas_");

            if (battleChar.returnDTO.statusCode == 4)
                loadChar = true;

            Assert.True(loadChar);
        }
    }
}
