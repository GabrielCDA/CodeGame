using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.Json.Serialization;

namespace CodeGame.Business.Abstract.DTO
{
    public enum nameClass
    {
        Warrior,
        Thief,
        Mage
    }
    public class InitialAttributes
    {
        public int life { get; set; }
        public int strength { get; set; }
        public int dexterity { get; set; }
        public int intelligence { get; set; }

    }
    public class BattleMode
    {
        public string attackDescription { get; set; }
        public double attackValue { get; set; }
        public string speedDescription { get; set; }
        public double speedValue { get; set; }

    }
    public class HeroClasses
    {
        public InitialAttributes attributes { get; set; }
        public BattleMode battleMode { get; set; }
        public nameClass nameClass { get; set; }
    }
}
