using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGame.Business.Abstract.DTO
{
    public class GetCharsCompletDTO
    {
        public CommonDTO returnDTO { get; set; }
        public List<Characters> Chars { get; set; }
    }
}
