using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Risiko_Rechner
{
    public enum PlayerType
    {
        Attacker, Defender
    }

    public class Player
    {
        /// <summary>
        /// Die Armee des Spielers.
        /// </summary>
        public Armee Armee { get; set; }

        public Armee Withdrawn { get; set; }
        
        /// <summary>
        /// Der Typ des Spielers, Angreifer oder Verteidiger.
        /// </summary>
        public PlayerType PlayerType { get; private set; }

        /// <summary>
        /// Die Würfelliste des Spielers.
        /// </summary>
        public List<int> DiceList { get; set; }
        
        private readonly Random random = new Random();

        /// <summary>
        /// Initialisiert das Skelett einer Player-Klasse.
        /// </summary>
        public Player(PlayerType playerType)
        {
            PlayerType = playerType;
            Armee = new Armee();
            Withdrawn = new Armee();
            DiceList = new List<int>(3);
            
            ThrowDice();
        }

        /// <summary>
        /// Initialisiert einen Spieler mit dem angegebenen Typ und einer Armee.
        /// </summary>
        public Player(PlayerType playerType, Armee armee)
        {
            PlayerType = playerType;
            Armee = armee;

            ThrowDice();
        }

        /// <summary>
        /// Gibt die Würfelsumme dieses Spielers zurück.
        /// </summary>
        /// <returns></returns>
        public int DiceSum()
        {
            int sum = 0;
            foreach (int dice in DiceList)
            {
                sum += dice;
            }
            return sum;
        }

        /// <summary>
        /// Gibt den höchsten Würfel dieses Spielers zurück.
        /// </summary>
        /// <returns></returns>
        public int HighestDice()
        {
            return DiceList[0];
        }

        /// <summary>
        /// Gibt den niedrigsten Würfel dieses Spielers zurück.
        /// </summary>
        /// <returns></returns>
        public int LowestDice()
        {
            return DiceList[1];
        }

        /// <summary>
        /// Reinitialisiert die Würfelliste. Steht für einen neuen Wurf von Würfeln.
        /// </summary>
        public void ThrowDice()
        {
            DiceList.Clear();
            generateDices();
            sortDices();
        }

        /// <summary>
        /// Gibt zurück, ob dieser Spieler eine Armee mit mindestens einem validen Stack hat.
        /// </summary>
        /// <returns></returns>
        public bool HasValidStack()
        {
            return Armee.Stacks.Count > 0;
        }

        public bool TopStackIsValid()
        {
            return TopStack().Count > 0;
        }

        /// <summary>
        /// Sortiert die Würfelliste, und kürzt sie auf zwei Zahlen.
        /// </summary>
        private void sortDices()
        {
            DiceList.Sort();
            DiceList.Reverse();

            if (DiceList.Count > 2) DiceList.RemoveAt(2);
        }

        /// <summary>
        /// Generiert eine neue Würfelliste.
        /// </summary>
        private void generateDices()
        {
            int diceCount = PlayerType == PlayerType.Attacker ? 3 : 2;

            for (int i = 0; i < diceCount; i++)
            {
                DiceList.Add(random.Next(1, 7));
            }
        }

        public Stack TopStack()
        {
            if (!HasValidStack()) 
            { 
                return null;
            }

            Stack topStack = Armee.Stacks[0];
            return topStack;
        }

        public bool Equals(Player that)
        {
            return this.PlayerType == that.PlayerType && this.Armee == that.Armee &&
                this.DiceList.SequenceEqual(that.DiceList);
        }

        public void GetDamage(Player attacker)
        {
            TopStack().StackUnit.HitPoints -= attacker.TopStack().StackUnit.AttackDamage -
                                                   (TopStack().StackUnit.Armor -
                                                    attacker.TopStack().StackUnit.AntiArmor);
            if (TopStack().StackUnit.HitPoints <= 0)
            {
                TopStack().KillFirst();
            }

            if (!TopStackIsValid())
            {
                KillTopStack();
            }
        }

        public void KillTopStack()
        {
            Armee.Stacks.RemoveAt(0);
        }

        public void WithdrawTopStack()
        {
            Withdrawn.Stacks.Add(TopStack());
            Armee.Stacks.RemoveAt(0);
        }
    }
}
