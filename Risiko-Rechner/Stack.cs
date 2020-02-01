using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risiko_Rechner
{
    /// <summary>
    /// Repräsentiert einen Stapel von Einheiten im Kampf.
    /// </summary>
    class Stack
    {
        public Unit StackUnit { get; set; }
        public int Count;

        /// <summary>
        /// Erstellt eine Instanz eines Stacks, mit einer Einheit.
        /// </summary>
        /// <param name="unit"></param>
        public Stack(Unit unit)
        {
            StackUnit = unit;
            Count = 1;
        }

        /// <summary>
        /// Erstellt eine Instanz eines Stacks, mit wählbar vielen Einheiten.
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="count"></param>
        public Stack(Unit unit, int count)
        {
            StackUnit = unit;
            Count = count;
        }

        /// <summary>
        /// Tötet die oberste Einheit.
        /// </summary>
        public void KillFirst()
        {
            if (Count <= 0)
            {
                throw new Exception("all units already dead");
            }
            Count--;
        }

        /// <summary>
        /// Tötet spezifizierte Anzahl Einheiten. </summary>
        /// <param name="amount">Anzahl der Einheiten</param>
        public void KillCount(int amount)
        {
            if (Count <= 0)
            {
                throw new Exception("all units already dead");
            }
            Count -= amount;
        }
    }
}
