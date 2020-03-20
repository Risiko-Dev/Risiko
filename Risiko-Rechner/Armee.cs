using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risiko_Rechner
{ 
    public class Armee 
    {
        public List<Stack> Stacks { get; private set; }

        /// <summary>
        /// Initialisiert ein Skelett der Armee-Klasse.
        /// </summary>
        public Armee()
        {
            Stacks = new List<Stack>();
        }

        public bool Equals(Armee that)
        {
            return this.Stacks.SequenceEqual(that.Stacks);
        }
    }
}
