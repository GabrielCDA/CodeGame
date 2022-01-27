using CodeGame.Business.Abstract.DTO;
using CodeGame.Data.Mock.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGame.Data.Mock
{
    public class DataHeroes : IDataHeroes
    {
        public List<HeroClasses> GetAllHeroes()
        {
            // VALORES SIMULANDO QUE SERIA PEGO NO BANCO DE DADOS
            string attackWarrior = "80-HeroStrength;20-HeroDexterity";
            string speedWarrior = "60-HeroDexterity;20-HeroIntellingence";

            string attackThief = "25-HeroStrength;100-HeroDexterity;25-HeroIntellingence";
            string speedThief = "80-HeroDexterity";

            string attackMage = "20-HeroStrength;50-HeroDexterity;150-HeroIntellingence";
            string speedMage = "20-HeroStrength;50-HeroDexterity";

            List<HeroClasses> heroes = new List<HeroClasses>();
            HeroClasses warrior = new HeroClasses();
            InitialAttributes warriorAttributes = new InitialAttributes();
            warriorAttributes.life = 20;
            warriorAttributes.strength = 10;
            warriorAttributes.dexterity = 5;
            warriorAttributes.intelligence = 5;
            warrior.attributes = warriorAttributes;
            BattleMode warriorBattleMode = new BattleMode();

            CharactersParameters warriorParametersAttack = GetDescriptionAndValue(attackWarrior, warriorAttributes);
            warriorBattleMode.attackDescription = warriorParametersAttack.description;
            warriorBattleMode.attackValue = warriorParametersAttack.value;

            CharactersParameters warriorParametersSpeed = GetDescriptionAndValue(speedWarrior, warriorAttributes);
            warriorBattleMode.speedDescription = warriorParametersSpeed.description;
            warriorBattleMode.speedValue = warriorParametersSpeed.value;

            warrior.battleMode = warriorBattleMode;
            warrior.nameClass = nameClass.Warrior;
            heroes.Add(warrior);

            HeroClasses thief = new HeroClasses();
            InitialAttributes thiefAttributes = new InitialAttributes();
            thiefAttributes.life = 15;
            thiefAttributes.strength = 4;
            thiefAttributes.dexterity = 10;
            thiefAttributes.intelligence = 4;
            thief.attributes = thiefAttributes;
            BattleMode thiefBattleMode = new BattleMode();

            CharactersParameters thiefParametersAttack = GetDescriptionAndValue(attackThief, thiefAttributes);
            thiefBattleMode.attackDescription = thiefParametersAttack.description;
            thiefBattleMode.attackValue = thiefParametersAttack.value;

            CharactersParameters thiefParametersSpeed = GetDescriptionAndValue(speedThief, thiefAttributes);
            thiefBattleMode.speedDescription = thiefParametersSpeed.description;
            thiefBattleMode.speedValue = thiefParametersSpeed.value;

            thief.battleMode = thiefBattleMode;
            thief.nameClass = nameClass.Thief;
            heroes.Add(thief);

            HeroClasses mage = new HeroClasses();
            InitialAttributes mageAttributes = new InitialAttributes();
            mageAttributes.life = 12;
            mageAttributes.strength = 5;
            mageAttributes.dexterity = 6;
            mageAttributes.intelligence = 10;
            mage.attributes = mageAttributes;
            BattleMode mageBattleMode = new BattleMode();

            CharactersParameters mageParametersAttack = GetDescriptionAndValue(attackMage, mageAttributes);
            mageBattleMode.attackDescription = mageParametersAttack.description;
            mageBattleMode.attackValue = mageParametersAttack.value;

            CharactersParameters mageParametersSpeed = GetDescriptionAndValue(speedMage, mageAttributes);
            mageBattleMode.speedDescription = mageParametersSpeed.description;
            mageBattleMode.speedValue = mageParametersSpeed.value;

            mage.battleMode = mageBattleMode;
            mage.nameClass = nameClass.Mage;
            heroes.Add(mage);

            return heroes;
        }

        private class CharactersParameters
        {
            public string description { get; set; }
            public double value { get; set; }
        }
        private CharactersParameters GetDescriptionAndValue(string description, InitialAttributes initialAttributes)
        {
            double parametersCalculated = 0;
            string returnDescription = "";
            string[] parameters = description.Split(';');
            CharactersParameters returnParameters = new CharactersParameters();
            for (int i = 0; i < parameters.Length; i++)
            {
                string[] valueParameter = parameters[i].Split('-');
                returnDescription = returnDescription + (Convert.ToInt32(valueParameter[0])).ToString() + "% da ";
                switch (valueParameter[1])
                {
                    case "HeroStrength":
                        returnDescription = returnDescription + "Força";
                        parametersCalculated = parametersCalculated + initialAttributes.strength * (Convert.ToDouble(valueParameter[0]) / 100);
                        break;
                    case "HeroDexterity":
                        returnDescription = returnDescription + "Destreza";
                        parametersCalculated = parametersCalculated + initialAttributes.dexterity * (Convert.ToDouble(valueParameter[0]) / 100);
                        break;
                    case "HeroIntellingence":
                        returnDescription = returnDescription + "Inteligência";
                        parametersCalculated = parametersCalculated + initialAttributes.intelligence * (Convert.ToDouble(valueParameter[0]) / 100);
                        break;
                    default:
                        break;
                }
                if (i < parameters.Length - 1)
                {
                    returnDescription = returnDescription + " + ";
                }

            }
            returnParameters.description = returnDescription;
            returnParameters.value = parametersCalculated;
            return returnParameters;
        }
    }
}
