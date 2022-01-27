using System;
using System.Collections.Generic;

namespace CodeGame.Business.Abstract
{
    public interface IGameHero<T>
    {
        public List<T> heroes();
    }
}
