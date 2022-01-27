using CodeGame.Business.Abstract;
using CodeGame.Business.Abstract.DTO;
using CodeGame.Data.Mock;
using CodeGame.Data.Mock.Interface;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeGame.Business
{
    public class GameChar : IGameChar<Characters>
    {
        public List<Characters> _chars;
        public IRequestDataGame service;
        public Trace log;

        public GameChar(List<Characters> chars)
        {
            _chars = chars;
            log = new Trace();
            var injectionService = (new ServiceCollection())
              .AddScoped<IDataChars, DataChars>()
              .AddScoped<IDataHeroes, DataHeroes>()
              .AddScoped<IRequestDataGame, RequestDataGame>()
              .BuildServiceProvider();
            service = injectionService.GetService<IRequestDataGame>();
        }

        public GetCharsCompletDTO chars() //buscaria da base de dadps todos os personagens
        {
            GetCharsCompletDTO getCharsCompletDTO = new GetCharsCompletDTO();
            CommonDTO statusReturn = new CommonDTO();

            if (_chars == null)
                _chars = service.LoadChars();
            log.WriteLogs("Find chars");
            if (_chars == null)
            {
                statusReturn.statusCode = 99;
                statusReturn.message = "No exist characters in data base";
                getCharsCompletDTO.returnDTO = statusReturn;
                log.WriteLogs("Return chars: "+ JsonConvert.SerializeObject(getCharsCompletDTO));
                return getCharsCompletDTO;
            }
            if (_chars.Count == 0)
            {
                statusReturn.statusCode = 7;
                statusReturn.message = "No characters were found";
                getCharsCompletDTO.returnDTO = statusReturn;
                log.WriteLogs("Return chars: " + JsonConvert.SerializeObject(getCharsCompletDTO));
                return getCharsCompletDTO;
            }
            statusReturn.message = "Characters were found";
            getCharsCompletDTO.returnDTO = statusReturn;
            getCharsCompletDTO.Chars = _chars;
            log.WriteLogs("Return chars: " + JsonConvert.SerializeObject(getCharsCompletDTO));
            return getCharsCompletDTO;
        }

        public GetCharsSimplifiedDTO LoadCharsSimplified()
        {
            log.WriteLogs("Begin LoadCharsSimplified");
            GetCharsSimplifiedDTO getCharsSimplifiedDTO = new GetCharsSimplifiedDTO();
            CommonDTO statusReturn = new CommonDTO(); 
            if (_chars == null)
                _chars = service.LoadChars();

            if (_chars == null)
            {
                statusReturn.statusCode = 99;
                statusReturn.message = "No exist characters in data base";
                getCharsSimplifiedDTO.returnDTO = statusReturn;
                log.WriteLogs("LoadCharsSimplified Return: " + JsonConvert.SerializeObject(getCharsSimplifiedDTO));
                return getCharsSimplifiedDTO;
            }
            List<GetChars> getChars = new List<GetChars>();
            foreach (var item in _chars)
            {
                getChars.Add(new GetChars { nameChar = item.nameCharacter, professionChar = item.professionCharacter, statusChar = item.statusAlive });
            }
            if (getChars.Count == 0)
            {
                statusReturn.statusCode = 6;
                statusReturn.message = "No characters were found";
                getCharsSimplifiedDTO.returnDTO = statusReturn;
                log.WriteLogs("LoadCharsSimplified Return: " + JsonConvert.SerializeObject(getCharsSimplifiedDTO));
                return getCharsSimplifiedDTO;
            }
            statusReturn.message = "Characters were found";
            getCharsSimplifiedDTO.returnDTO = statusReturn;
            getCharsSimplifiedDTO.Chars = getChars;
            log.WriteLogs("LoadCharsSimplified Return: " + JsonConvert.SerializeObject(getCharsSimplifiedDTO));
            return getCharsSimplifiedDTO;
        }
        public DetailCharDTO DetailChar(string nameChar)
        {
            log.WriteLogs("Begin DetailChar");
            if (_chars == null)
                _chars = service.LoadChars();
            DetailCharDTO detailCharDTO = new DetailCharDTO();
            Characteristics characteristics = new Characteristics();
            CommonDTO statusReturn = new CommonDTO();

            if (_chars.Count == 0)
            {
                statusReturn.statusCode = 8;
                statusReturn.message = "No characters were found";
                detailCharDTO.returnDTO = statusReturn;
                log.WriteLogs("DetailChar Return: " + JsonConvert.SerializeObject(detailCharDTO));
                return detailCharDTO;
            }
            foreach (var item in _chars.Where(x => x.nameCharacter == nameChar))
            {
                characteristics.nameCharacter = item.nameCharacter;
                characteristics.professionCharacter = item.professionCharacter;
                characteristics.lifeChar = item.lifeChar;
                characteristics.strengthChar = item.strengthChar;
                characteristics.dexterityChar = item.dexterityChar;
                characteristics.intelligenceChar = item.intelligenceChar;
                characteristics.attackDescription = item.classHero.battleMode.attackDescription;
                characteristics.speedDescription = item.classHero.battleMode.speedDescription;

            }
            if (_chars.Where(x => x.nameCharacter == nameChar).Count() == 0)
            {
                statusReturn.statusCode = 9;
                statusReturn.message = "Character was not found";
                detailCharDTO.returnDTO = statusReturn;
                log.WriteLogs("DetailChar Return: " + JsonConvert.SerializeObject(detailCharDTO));
                return detailCharDTO;
            }
            
            statusReturn.message = "Character was found";
            detailCharDTO.returnDTO = statusReturn;
            detailCharDTO.characteristics = characteristics;
            log.WriteLogs("DetailChar Return: " + JsonConvert.SerializeObject(detailCharDTO));
            return detailCharDTO;
        }

        public CommonDTO CreateChar(HeroClasses heroClass, string nameChar)
        {
            log.WriteLogs("Begin CreateChar");
            if (_chars != null)
            {
                int existCharName = _chars.Where(x => x.nameCharacter == nameChar).Count();

                if (existCharName > 0)
                {
                    CommonDTO returnCreate = new CommonDTO
                    {
                        statusCode = 3,
                        message = "Char name alredy exist, plis change the name"
                    };
                    log.WriteLogs("CreateChar Return: " + JsonConvert.SerializeObject(returnCreate));
                    return returnCreate;
                }
            }
            ReturnNewChar returnCreateCharDTO;

            returnCreateCharDTO = service.CreateChar(heroClass, nameChar);
            if (returnCreateCharDTO.returnDTO.statusCode == 0)
            {
                if (_chars == null)
                    _chars = new List<Characters>();
                _chars.Add(returnCreateCharDTO.newChar);
            }
            log.WriteLogs("CreateChar Return: " + JsonConvert.SerializeObject(returnCreateCharDTO));
            return returnCreateCharDTO.returnDTO;
        }

        public BattleCharDTO BattleChar(string nameChallenger, string nameOpponent)
        {
            log.WriteLogs("Begin BattleChar");
            BattleCharDTO returnBattleResult = new BattleCharDTO();
            CommonDTO statusDTO = new CommonDTO();
            List<string> logBattle = new List<string>();
            int calculatedSpeedChallenger = 0, calculatedSpeedOpponent = 0;
            bool findFirstAttacker = false;
            var listOpponent = _chars.Where(x => x.nameCharacter == nameOpponent);
            var characterOpponent = listOpponent.Count() == 0 ? null : listOpponent.First(); //consultaria no banco de dados a situação atual do personagem
            var listChallenger = _chars.Where(x => x.nameCharacter == nameChallenger);
            var characterChallenger = listChallenger.Count() == 0 ? null : listChallenger.First();

            statusDTO = ValidateBattleParameters(characterChallenger, characterOpponent, nameChallenger, nameOpponent);
            if (statusDTO.statusCode != 0)
            {
                returnBattleResult.returnDTO = statusDTO;
                log.WriteLogs("BattleChar Return: " + JsonConvert.SerializeObject(returnBattleResult));
                return returnBattleResult;
            }

            BattleMode battleChallenger = characterChallenger.classHero.battleMode;
            BattleMode battleOpponent = characterOpponent.classHero.battleMode;
            

            while (!findFirstAttacker)
            {
                calculatedSpeedChallenger = new Random().Next(0, Convert.ToInt32(battleChallenger.speedValue));
                calculatedSpeedOpponent = new Random().Next(0, Convert.ToInt32(battleOpponent.speedValue));

                if (calculatedSpeedChallenger != calculatedSpeedOpponent)
                    findFirstAttacker = true;
            }

            if (calculatedSpeedChallenger > calculatedSpeedOpponent)
            {
                logBattle.Add(nameChallenger + " " + calculatedSpeedChallenger + " foi mais veloz que o " + nameOpponent + " " + calculatedSpeedOpponent + ", e irá começar!");
                var returnBattle = StartBattle(true, Convert.ToInt32(battleChallenger.attackValue), Convert.ToInt32(battleOpponent.attackValue), characterOpponent, characterChallenger);
                logBattle = logBattle.Concat(returnBattle).ToList();
            }
            else
            {
                logBattle.Add(nameOpponent + " " + calculatedSpeedOpponent + " foi mais veloz que o " + nameChallenger + " " + calculatedSpeedChallenger + ", e irá começar!");
                var returnBattle = StartBattle(false, Convert.ToInt32(battleChallenger.attackValue), Convert.ToInt32(battleOpponent.attackValue), characterOpponent, characterChallenger);
                logBattle = logBattle.Concat(returnBattle).ToList();
            }
            statusDTO.message = "End of battle";
            returnBattleResult.returnDTO = statusDTO;
            returnBattleResult.Steps = logBattle;
            log.WriteLogs("BattleChar Return: " + JsonConvert.SerializeObject(returnBattleResult));
            return returnBattleResult;
        }
        private CommonDTO ValidateBattleParameters(Characters characterChallenger, Characters characterOpponent, string nameChallenger, string nameOpponent)
        {
            CommonDTO statusDTO = new CommonDTO();
            if (characterOpponent == null && characterChallenger == null)
            {
                statusDTO.message = "Both characters didn't exist";
                statusDTO.statusCode = 10;
                return statusDTO;
            }
            if (characterOpponent == null)
            {
                statusDTO.message = nameOpponent + " didn't exist";
                statusDTO.statusCode = 11;
                return statusDTO;
            }
            if (characterChallenger == null)
            {
                statusDTO.message = nameChallenger + " didn't exist";
                statusDTO.statusCode = 11;
                return statusDTO;
            }
            if (characterOpponent.statusAlive == status.Dead && characterChallenger.statusAlive == status.Dead)
            {
                statusDTO.message = "Both characters are dead";
                statusDTO.statusCode = 4;
                return statusDTO;
            }
            if (characterOpponent.statusAlive == status.Dead)
            {
                statusDTO.message = characterOpponent.nameCharacter + " is dead";
                statusDTO.statusCode = 5;
                return statusDTO;
            }
            if (characterChallenger.statusAlive == status.Dead)
            {
                statusDTO.message = characterChallenger.nameCharacter + " is dead";
                statusDTO.statusCode = 5;
                return statusDTO;
            }
            return new CommonDTO();
        }
        private List<string> StartBattle(bool challengerAttack, int attackChallenger, int attackOpponent, Characters characterOpponent, Characters characterChallenger)
        {
            int calculatedAttackChallenger = 0, calculatedAttackOpponent = 0;
            int lifeOpponent = characterOpponent.lifeChar;
            string nameChallenger = characterChallenger.nameCharacter;
            string nameOpponent = characterOpponent.nameCharacter;
            int lifeChallenger = characterChallenger.lifeChar;
            bool firstDefeated = false;
            List<string> logBattle = new List<string>();
            
            List<Characters> listChars = _chars.ToList();
            while (!firstDefeated)
            {
                if (challengerAttack)
                {
                    calculatedAttackChallenger = new Random().Next(0, attackChallenger);

                    lifeOpponent = lifeOpponent - calculatedAttackChallenger;

                    logBattle.Add(nameChallenger + " atacou " + nameOpponent + " com " + calculatedAttackChallenger + " de dano, " + nameOpponent + " com " + (lifeOpponent < 0 ? 0 : lifeOpponent) + " pontos de vida restantes;");

                    for (int i = 0; i < listChars.Count(); i++)
                    {
                        if (listChars[i].nameCharacter == nameOpponent)
                        {
                            if (lifeOpponent <= 0)
                            {
                                listChars[i].lifeChar = 0;
                                listChars[i].statusAlive = status.Dead;
                                firstDefeated = true;
                                logBattle.Add(nameChallenger + " venceu a batalha! " + nameChallenger + " ainda tem " + _chars.Where(x => x.nameCharacter == nameChallenger).First().lifeChar + " pontos de vida restantes!");
                            }
                            else
                            {
                                listChars[i].lifeChar = lifeOpponent;
                            }
                        }
                    }

                    challengerAttack = false;

                }
                else
                 {
                    calculatedAttackOpponent = new Random().Next(0, attackOpponent);

                    lifeChallenger = lifeChallenger - calculatedAttackOpponent;

                    logBattle.Add(nameOpponent + " atacou " + nameChallenger + " com " + calculatedAttackOpponent + " de dano, " + nameChallenger + " com " + (lifeChallenger < 0 ? 0 : lifeChallenger) + " pontos de vida restantes;");


                    for (int i = 0; i < listChars.Count(); i++)
                    {
                        if (listChars[i].nameCharacter == nameChallenger)
                        {
                            if (lifeChallenger <= 0)
                            {
                                listChars[i].lifeChar = 0;
                                listChars[i].statusAlive = status.Dead;
                                firstDefeated = true;
                                logBattle.Add(nameOpponent + " venceu a batalha! " + nameOpponent + " ainda tem " + _chars.Where(x => x.nameCharacter == nameOpponent).First().lifeChar + " pontos de vida restantes!");
                            }
                            else
                            {
                                listChars[i].lifeChar = lifeChallenger;
                            }
                        }
                    }
    
                    challengerAttack = true;
                }
            }
            return logBattle;

        }
    }
}

