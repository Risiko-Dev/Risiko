using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risiko_Rechner
{
    public class Output
    {
        public System.Windows.Forms.TextBox outputTextbox;
        public bool MissingUnitNames(string name1, string name2)
        {
            if (name1 == "")
            {
                outputTextbox.Text += Environment.NewLine + "Angreifer, wählen Sie eine Einheitenklasse";
                return true;
            }
            if (name2 == "")
            {
                outputTextbox.Text += Environment.NewLine + "Verteidiger, wählen Sie eine Einheitklasse";
                return true;
            }
            return false;
        }
        public bool MissingUnitNumbers(int unitNumerAttacker, int unitNumberDefender)
        {

            if (unitNumerAttacker == 0)
            {
                outputTextbox.Text += Environment.NewLine + "Angreifer, wählen Sie Ihre Einheitenzahl";
                return true;
            }
            if (unitNumberDefender == 0)
            {
                outputTextbox.Text += Environment.NewLine + "Verteidiger, wählen Sie Ihre Einheitenzahl";
                return true;
            }
            return false;
        }
        public void StartupText(int playerOneDiceOne, int playerTwoDiceOne, int playerOneDiceTwo, int playerTwoDiceTwo, int Round)
        {
            MakeSpace(3);
            if (playerOneDiceTwo > 0 && playerTwoDiceTwo > 1)
            {
                Normal(playerOneDiceOne, playerTwoDiceOne, playerOneDiceTwo, playerTwoDiceTwo, Round);
            }
            else
            {
                ShortendArmee(playerOneDiceOne, playerTwoDiceOne, playerOneDiceTwo, playerTwoDiceTwo, Round);
            }
        }

        private  void ShortendArmee( int playerOneDiceOne, int playerTwoDiceOne, int playerOneDiceTwo, int playerTwoDiceTwo, int Round)
        {
            outputTextbox.Text += $"Kampfrunde: {Round}";
            //Würfel:
            if (playerOneDiceTwo < 0)
            {
                outputTextbox.Text += $"{Environment.NewLine}Angreifer würfelt: {playerOneDiceOne}.";
            }
            else
            {
                outputTextbox.Text += $"{Environment.NewLine}Angreifer würfelt: {playerOneDiceOne}, {playerOneDiceOne} und {playerOneDiceTwo}";
            }
            //Würfel
            if (playerTwoDiceTwo < 0)
            {
                outputTextbox.Text += $"{Environment.NewLine} Verteidiger würfelt: {playerTwoDiceOne}";
            }
            else
            {
                outputTextbox.Text += $"{Environment.NewLine} Verteidiger würfelt: {playerTwoDiceOne} und {playerTwoDiceTwo}";
            }
        }

        private void Normal(int playerOneDiceOne, int playerTwoDiceOne, int playerOneDiceTwo, int playerTwoDiceTwo, int Round)
        {
            outputTextbox.Text += $"Kampfrunde: {Round}";
            //Würfel:
            outputTextbox.Text += $"{Environment.NewLine}Angreifer würfelt: {playerOneDiceOne}, {playerOneDiceOne} und {playerOneDiceTwo}";
            //Würfel
            outputTextbox.Text += $"{Environment.NewLine} Verteidiger würfelt: {playerTwoDiceOne} und {playerTwoDiceTwo}";
        }

        public void HighestDice(int playerOneFirstDice, int playerTwoFirstDice)
        {
            // würfel ausgabe
            MakeSpace(2);
            outputTextbox.Text += $"höchster Angreifer Würfel: {playerOneFirstDice}{Environment.NewLine}höchster Verteidiger Würfel: {playerTwoFirstDice}";
        }

        private void MakeSpace( int blankLineAmount)
        {
            for (int i = 0; i < blankLineAmount; i++)
            {
                outputTextbox.Text += Environment.NewLine;
            }
        }

        public void SecondHighestDice( int playerOneSecondDice, int playerTwoSecondDice)
        {
            // würfel ausgabe
            MakeSpace(2);
            outputTextbox.Text += $"zweithöchster Angreifer Würfel: {playerOneSecondDice}.{Environment.NewLine}zweithöchsterhöchster Verteidiger Würfel:{playerTwoSecondDice}";
        }
        public bool UnitDeath(int UnitsAlive, string player)
        {
            MakeSpace( 2);
            if (UnitsAlive > 0)
            {
                outputTextbox.Text += $"Der {player} verliert eine Einheit, die nächste rutscht aber schon nach!{Environment.NewLine}{player} hat noch{UnitsAlive} Einheiten übrig.";
                return false;
            }
            else
            {
                outputTextbox.Text += $"Der {player} wurde besiegt! Alle Einheiten sind Tod.";
                return true;
            }
        }
        public void Victory(string victor)
        {
            var task = string.Empty;
            if (victor == "Angreifer")
            {
                task = "erobrern";
            }
            else
            {
                task = "verteidigen";
            }

            outputTextbox.Text += $"Der {victor} konnte das Feld {task}!";
        }
        public bool Fight(string Attacker, string Defender, Unit attackerUnit, Unit defenderUnit, out Unit defenderUnitResult)
        {
            //würfel ergebnis
            outputTextbox.Text += Environment.NewLine + $"{Attacker} führt Attacke aus:";

            int remainingArmor = defenderUnit.Armor - attackerUnit.AntiArmor;
            //Armortest report
            outputTextbox.Text += Environment.NewLine + $"Rüstungswert des {Defender} nach angriff mit AP-Waffen: {remainingArmor}";

            if (remainingArmor < 0)
            { remainingArmor = 0; }

            if (remainingArmor - attackerUnit.AttackDamage >= 0)
            {
                // TEXT: ABBRUCH DES KAMPFES, 2 zieht sich zurück
                outputTextbox.Text += Environment.NewLine + "Verteidiger kann die Panzerung des Angreifers nicht durchdringen, er muss sich zurückziehen";
                defenderUnitResult = defenderUnit;
                return true ;
            }

            if (remainingArmor == 0)
            {
                defenderUnit.HitPoints -= attackerUnit.AttackDamage;
                //Dmg and HP-loss
                outputTextbox.Text += Environment.NewLine + $"{Defender} erhält {attackerUnit.AttackDamage} Schaden,{Environment.NewLine}{defenderUnit.Name} hat noch {defenderUnit.HitPoints} HP";
            }
            else
            {
                defenderUnit.HitPoints -= attackerUnit.AttackDamage + remainingArmor;
                //Dmg and HP-loss
                outputTextbox.Text += Environment.NewLine + $"{Defender} erhält {attackerUnit.AttackDamage} Schaden, kann dabei {remainingArmor} abwehren{Environment.NewLine}{defenderUnit.Name} hat noch {defenderUnit.HitPoints} HP";
            }
            defenderUnitResult = defenderUnit;
            return false;
        }
    }
}
