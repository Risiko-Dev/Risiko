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
    public class Stack
    {
        public Unit StackUnit { get; private set; }
        public int Count;

        private Unit _templateUnit;

        /// <summary>
        /// Erstellt eine Instanz eines Stacks, mit einer Einheit.
        /// </summary>
        /// <param name="unit"></param>
        public Stack(Unit unit)
        {
            StackUnit = unit;
            _templateUnit = unit;
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
            _templateUnit = unit;
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
            refreshUnit();
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
            refreshUnit();
        }

        private void refreshUnit()
        {
            StackUnit = _templateUnit;
        }

        public bool Equals(Stack that)
        {
            return this.StackUnit.Equals(that.StackUnit);
        }

        public override string ToString()
        {
            return StackUnit.Name + ", " + Count;
        }
    }
}
