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
    public class Unit : ICloneable
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

        private int tAntiArmor;
        private int tArmor;
        private int tHitPoints;
        private int tAttackDamage;
        private string tName;

        public Unit(string name, int hitPoints, int armor, int attackDamage, int antiArmor) 
        {
            Name = name;
            tName = name;

            HitPoints = hitPoints;
            tHitPoints = hitPoints;
            
            Armor = armor;
            tArmor = armor;
            
            AttackDamage = attackDamage;
            tAttackDamage = attackDamage;
            
            AntiArmor = antiArmor;
            tAntiArmor = antiArmor;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public bool Equals(Unit that)
        {
            return this.AntiArmor == that.AntiArmor && this.Armor == that.Armor &&
                   this.AttackDamage == that.AttackDamage && this.HitPoints == that.HitPoints && this.Name == that.Name;
        }

        public void Refresh()
        {
            Name = tName;
            HitPoints = tHitPoints;
            Armor = tArmor;
            AntiArmor = tAntiArmor;
            AttackDamage = tAttackDamage;
        }
    }
}
