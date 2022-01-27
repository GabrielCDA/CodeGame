using CodeGame.Business;
using CodeGame.Business.Abstract;
using CodeGame.Business.Abstract.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.RegularExpressions;

namespace Web.Api.CodeGame.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CodeGameController : ControllerBase
    {
        #region Constructor
        private GameChar _chars;
        private GameHero _heroes;

        public CodeGameController(
            IRequestDataGame requestDataGame, GameChar chars, GameHero heroes)
        {
            _chars = chars;
            _heroes = heroes;
        }
        #endregion

        #region ReturnHeroes
        /// <summary>
        /// All heroes class taht exist in game
        /// </summary> 
        /// <remarks>GET to list all heroes</remarks>
        /// <returns>Return all heroes</returns>
        ///        
        [Route("v1/ReturnHeroes/")]
        [HttpGet]
        public ActionResult GetHeroes()
        {
            var getHeroesDTO = _heroes.GetHeroes();
            if (getHeroesDTO.returnDTO.statusCode != 0)
            {
                return Conflict(new
                {
                    getHeroesDTO
                });
            }
            else
            {
                return Ok(new
                {
                    getHeroesDTO
                });
            }
        }
        #endregion

        #region ReturnCharsCompleteInformation
        /// <summary>
        /// All heroes chars that exist in game
        /// </summary> 
        /// <remarks>GET to list all chars with complete information</remarks>
        /// <returns>Return all chars</returns>
        ///        
        [Route("v1/ReturnCharsCompleteInformation/")]
        [HttpGet]
        public ActionResult GetCharComplete()
        {
            var getCharsCompletDTO = _chars.chars();
            if (getCharsCompletDTO.returnDTO.statusCode != 0)
            {
                return Conflict(new
                {
                    getCharsCompletDTO
                });
            }
            else
            {
                return Ok(new
                {
                    getCharsCompletDTO
                });
            }
        }
        #endregion

        #region ReturnCharsSimplified
        /// <summary>
        /// All heroes chars that exist in game
        /// </summary> 
        /// <remarks>GET to list all chars with simplified information</remarks>
        /// <returns>Return all chars</returns>
        ///        
        [Route("v1/ReturnCharsSimplified/")]
        [HttpGet]
        public ActionResult GetCharSimplified()
        {
            var getCharsSimplifiedDTO = _chars.LoadCharsSimplified();
            if (getCharsSimplifiedDTO.returnDTO.statusCode != 0)
            {
                return Conflict(new
                {
                    getCharsSimplifiedDTO
                });
            }
            else
            {
                return Ok(new
                {
                    getCharsSimplifiedDTO
                });
            }
        }
        #endregion

        #region CreateChar
        /// <summary>
        /// Create new char
        /// </summary> 
        /// <returns>Return if was created </returns>
              
        [Route("v1/CreateChar/")]
        [HttpPut]

        public ActionResult PutCreateChar(string nameChar, nameClass nameClass)
        {
            CommonDTO returnCreate = new CommonDTO();
            Regex rgx = new Regex("[^a-zA-Z_]+");
            if (string.IsNullOrWhiteSpace(nameChar))
            {
                returnCreate.statusCode = 1;
                returnCreate.message = "Error - It is not accepeted white space or empty - Plis, choose a name ";
                return Conflict(new
                {
                    returnCreate
                });
            }
            if (nameChar.Length > 15)
            {
                returnCreate.statusCode = 1;
                returnCreate.message = "Error - Maximum size name is 15 characters";
                return Conflict(new
                {
                    returnCreate
                });
            }
            if (rgx.IsMatch(nameChar))
            {
                returnCreate.statusCode = 2;
                returnCreate.message = "Error - Only letters and underscore are valid";
                return Conflict(new
                {
                    returnCreate
                });
            }
            returnCreate = _chars.CreateChar(_heroes.heroes().Where(c => c.nameClass == nameClass).First(), nameChar);
            if (returnCreate.statusCode == 0)
            {
                return Ok(new
                {
                    returnCreate
                });
            }
            else
            {
                return Conflict(new
                {
                    returnCreate
                });
            }
        }
        #endregion

        #region CharFight
        /// <summary>
        /// Fight with two chars
        /// </summary> 
        /// <returns>Return all battle log</returns>
        
        [Route("v1/CharFight/")]
        [HttpPost]

        public ActionResult PostFightChar(string nameChallenger, string nameOpponent)
        {
            var battleCharDTO = _chars.BattleChar(nameChallenger, nameOpponent);
            if (battleCharDTO.returnDTO.statusCode != 0)
            {
                return Conflict(new
                {
                    battleCharDTO
                });
            }
            else
            {
                return Ok(new
                {
                    battleCharDTO
                });
            }
        }
        #endregion

        #region GetDetailChar
        /// <summary>
        /// Detail of char
        /// </summary> 
        /// <returns>Return details of one chars</returns>
           
        [Route("v1/GetDetailChar/")]
        [HttpGet]

        public ActionResult GetDetailChar(string nameChar)
        {
            var detailCharDTO = _chars.DetailChar(nameChar);
            if (detailCharDTO.returnDTO.statusCode != 0)
            {
                return Conflict(new
                {
                    detailCharDTO
                });
            }
            else
            {
                return Ok(new
                {
                    detailCharDTO
                });
            }
        }

        #endregion
     
        
    }
}
