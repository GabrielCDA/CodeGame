using CodeGame.Business.Abstract.DTO;
using System.Collections.Generic;

namespace CodeGame.Business.Abstract
{
    public interface IGameChar<T>
    {
        public GetCharsCompletDTO chars();
        public CommonDTO CreateChar(HeroClasses heroClass, string nameChar);
        public BattleCharDTO BattleChar(string nameChallenger, string nameOpponent);
    }
}
