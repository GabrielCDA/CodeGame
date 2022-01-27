using CodeGame.Business.Abstract.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGame.Business.Abstract
{
    public interface IRequestDataGame
    {
        public List<HeroClasses> LoadHeroClass();
        public List<Characters> LoadChars();
        public ReturnNewChar CreateChar(HeroClasses heroClass, string nameChar);
        public List<Characters> CreateMockLoadChar(List<HeroClasses> heroes);
    }
}
