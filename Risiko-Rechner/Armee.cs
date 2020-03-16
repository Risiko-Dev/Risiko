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

        /// <summary>
        /// Initialisiert eine Armee aus einem Stack.
        /// </summary>
        public Armee(Stack stack)
        {
            Stacks.Add(stack); 
        }

        /// <summary>
        /// Initialisiert eine Armee aus zwei Stacks.
        /// </summary>
        public Armee(Stack stack, Stack stack2)
        {
            Stacks.Add(stack);
            Stacks.Add(stack2);
        }

        /// <summary>
        /// Initialisiert eine Armee aus drei Stacks.
        /// </summary>
        public Armee(Stack stack, Stack stack2, Stack stack3)
        {
            Stacks.Add(stack);
            Stacks.Add(stack2);
            Stacks.Add(stack3);
        }

        /// <summary>
        /// Initialisiert eine Armee aus vier Stacks.
        /// </summary>
        public Armee(Stack stack, Stack stack2, Stack stack3, Stack stack4)
        {
            Stacks.Add(stack);
            Stacks.Add(stack2);
            Stacks.Add(stack3);
            Stacks.Add(stack4);
        }

        /// <summary>
        /// Initialisiert eine Armee aus fünf Stacks.
        /// </summary>
        public Armee(Stack stack, Stack stack2, Stack stack3, Stack stack4, Stack stack5)
        {
            Stacks.Add(stack);
            Stacks.Add(stack2);
            Stacks.Add(stack3);
            Stacks.Add(stack4);
            Stacks.Add(stack5);
        }

        public bool Equals(Armee that)
        {
            return this.Stacks.SequenceEqual(that.Stacks);
        }
    }
}
