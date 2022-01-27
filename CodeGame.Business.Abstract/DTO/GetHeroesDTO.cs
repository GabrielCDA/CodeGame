using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGame.Business.Abstract.DTO
{

    public class GetHeroesDTO
    {
        public CommonDTO returnDTO { get; set; }
        public List<HeroClasses> Heroes { get; set; }
    }
}
