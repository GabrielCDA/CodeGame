using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGame.Business.Abstract.DTO
{
    public class Characters 
    {
 
        public HeroClasses classHero { get; set; }
        public string nameCharacter { get; set; }
        public string professionCharacter { get; set; }
        public status statusAlive { get; set; }
        public int lifeChar { get; set; }
        public int strengthChar { get; set; }
        public int dexterityChar { get; set; }
        public int intelligenceChar { get; set; }
    }
}
