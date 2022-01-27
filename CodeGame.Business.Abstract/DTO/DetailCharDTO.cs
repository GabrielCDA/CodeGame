using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGame.Business.Abstract.DTO
{
    public class Characteristics
    {
        public string nameCharacter { get; set; }
        public string professionCharacter { get; set; }
        public int lifeChar { get; set; }
        public int strengthChar { get; set; }
        public int dexterityChar { get; set; }
        public int intelligenceChar { get; set; }
        public string attackDescription { get; set; }
        public string speedDescription { get; set; }

    }
    public class DetailCharDTO
    {
        public CommonDTO returnDTO { get; set; }
        public Characteristics characteristics { get; set; }
    }

}
