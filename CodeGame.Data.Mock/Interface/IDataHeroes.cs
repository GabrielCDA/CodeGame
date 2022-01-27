using CodeGame.Business.Abstract.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGame.Data.Mock.Interface
{
    public interface IDataHeroes
    {
        public List<HeroClasses> GetAllHeroes();
    }
}
