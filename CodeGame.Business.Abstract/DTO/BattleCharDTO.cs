using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGame.Business.Abstract.DTO
{
    public class BattleCharDTO
    {
        public CommonDTO returnDTO { get; set; }
        public List<string> Steps { get; set; }
    }
}
