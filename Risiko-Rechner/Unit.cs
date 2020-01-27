using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risiko_Rechner
{

    class Unit : ICloneable
    {
        public int antiArmor;
        public int armor;
        public int hitpoints;
        public int attackDamage;
        public string name;

        public Unit(string name, int hitpoints, int armor, int attackDamage, int antiArmor) 
        {
            this.name = name;
            this.hitpoints = hitpoints;
            this.armor = armor;
            this.attackDamage = attackDamage;
            this.antiArmor = antiArmor;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
