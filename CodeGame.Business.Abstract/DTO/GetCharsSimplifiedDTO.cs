using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGame.Business.Abstract.DTO
{
    public class GetChars
    {
        public string nameChar { get; set; }
        public string professionChar { get; set; }
        public status statusChar { get; set; }
    }
    public class GetCharsSimplifiedDTO
    {
        public CommonDTO returnDTO { get; set; }
        public List<GetChars> Chars { get; set; }
    }
}
