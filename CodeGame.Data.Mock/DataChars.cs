
using CodeGame.Business.Abstract.DTO;
using CodeGame.Data.Mock.Interface;
using System.Collections.Generic;
using System.Linq;


namespace CodeGame.Data.Mock
{
    public class DataChars : IDataChars
    {
        public List<Characters> _chars;
        public List<Characters> GetAllChars() => _chars; //buscaria da base todos os Chars ativos
        public List<Characters> CreateMockLoad(List<HeroClasses> heroes)
        {
            HeroClasses Warrior = heroes.Where(c => c.nameClass == nameClass.Warrior).First();
            HeroClasses Mage = heroes.Where(c => c.nameClass == nameClass.Mage).First();
            HeroClasses Thief = heroes.Where(c => c.nameClass == nameClass.Thief).First();

            List<Characters> characters;
            characters = new List<Characters>();
            Characters aerif = new Characters();
            aerif.classHero = Warrior;
            aerif.nameCharacter = "Aerif";
            aerif.professionCharacter = Warrior.nameClass.ToString();
            aerif.statusAlive = status.Alive;
            aerif.lifeChar = Warrior.attributes.life;
            aerif.intelligenceChar = Warrior.attributes.intelligence;
            aerif.dexterityChar = Warrior.attributes.dexterity;
            aerif.strengthChar = Warrior.attributes.strength;
            characters.Add(aerif);

            Characters legodas = new Characters();
            legodas.classHero = Thief;
            legodas.nameCharacter = "Legodas";
            legodas.professionCharacter = Thief.nameClass.ToString();
            legodas.statusAlive = status.Alive;
            legodas.lifeChar = Thief.attributes.life;
            legodas.intelligenceChar = Thief.attributes.intelligence;
            legodas.dexterityChar = Thief.attributes.dexterity;
            legodas.strengthChar = Thief.attributes.strength;
            characters.Add(legodas);

            Characters ermiouni = new Characters();
            ermiouni.classHero = Mage;
            ermiouni.nameCharacter = "Ermiouni";
            ermiouni.professionCharacter = Mage.nameClass.ToString();
            ermiouni.statusAlive = status.Alive;
            ermiouni.lifeChar = Mage.attributes.life;
            ermiouni.intelligenceChar = Mage.attributes.intelligence;
            ermiouni.dexterityChar = Mage.attributes.dexterity;
            ermiouni.strengthChar = Mage.attributes.strength;
            characters.Add(ermiouni);

            Characters ledolas = new Characters();
            ledolas.classHero = Thief;
            ledolas.nameCharacter = "Ledolas_";
            ledolas.professionCharacter = Thief.nameClass.ToString();
            ledolas.statusAlive = status.Dead;
            ledolas.lifeChar = Thief.attributes.life;
            ledolas.intelligenceChar = Thief.attributes.intelligence;
            ledolas.dexterityChar = Thief.attributes.dexterity;
            ledolas.strengthChar = Thief.attributes.strength;
            characters.Add(ledolas);

            Characters reiLagarto = new Characters();
            reiLagarto.classHero = Warrior;
            reiLagarto.nameCharacter = "Rei_Lagarto";
            reiLagarto.professionCharacter = Warrior.nameClass.ToString();
            reiLagarto.statusAlive = status.Dead;
            reiLagarto.lifeChar = Warrior.attributes.life;
            reiLagarto.intelligenceChar = Warrior.attributes.intelligence;
            reiLagarto.dexterityChar = Warrior.attributes.dexterity;
            reiLagarto.strengthChar = Warrior.attributes.strength;
            characters.Add(reiLagarto);

            Characters reiOleiro = new Characters();
            reiOleiro.classHero = Mage;
            reiOleiro.nameCharacter = "Rei_Oleiro";
            reiOleiro.professionCharacter = Mage.nameClass.ToString();
            reiOleiro.statusAlive = status.Dead;
            reiOleiro.lifeChar = Mage.attributes.life;
            reiOleiro.intelligenceChar = Mage.attributes.intelligence;
            reiOleiro.dexterityChar = Mage.attributes.dexterity;
            reiOleiro.strengthChar = Mage.attributes.strength;
            characters.Add(reiOleiro);
            _chars = characters;

            return _chars;
        } //mock para criação de personagens inicial
        public CommonDTO AddNewChar(Characters newChar)
        {                                                    
            return (new CommonDTO
            { 
                statusCode = 0,
                message = "New char successfully created"
            }); 
        }
    }
}
