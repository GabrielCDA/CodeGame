using CodeGame.Business.Abstract.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGame.Data.Mock.Interface
{
    public interface IDataChars
    {
        public List<Characters> GetAllChars();
        List<Characters> CreateMockLoad(List<HeroClasses> heroes);
        public CommonDTO AddNewChar(Characters newChar);
    }
}
