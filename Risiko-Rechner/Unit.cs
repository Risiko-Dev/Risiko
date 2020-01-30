using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risiko_Rechner
{ 
    /// <summary>
    /// Repräsentiert eine Einheit, mit allen damit verbundenen Stats.
    /// </summary>
    class Unit : ICloneable
    {
        // Generell, wenn mit Klassen und Variablen darin gearbeitet wird, ist es besser, Properties anstatt
        // public Variablen zu verwenden. C# macht dies sehr leicht, durch die Verwendung von Autoproperties,
        // die intern wie eine private Variable (privates Feld), kombiniert mit public get und set Methoden,
        // funktionieren:

        public int AntiArmor { get; set; }
        public int Armor { get; set; }
        public int HitPoints { get; set; }
        public int AttackDamage { get; set; }
        public string Name { get; set; }

        public Unit(string name, int hitPoints, int armor, int attackDamage, int antiArmor) 
        {
            Name = name;
            HitPoints = hitPoints;
            Armor = armor;
            AttackDamage = attackDamage;
            AntiArmor = antiArmor;
        }

        // this ist hier redundant und kann weggelassen werden.
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
