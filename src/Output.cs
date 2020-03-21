using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Risiko_Rechner
{
    public class Output
    {
        public TextBox outputTextbox;

        public void ReportDices(List<int> list)
        {
            list.ForEach(x => { outputTextbox.Text += x + ", ";});
        }

        public void ReportDices(string player, List<int> list)
        {
            outputTextbox.Text += player + ": ";
            list.ForEach(x => { outputTextbox.Text += x + ", "; });
            outputTextbox.Text += Environment.NewLine;
        }

        public void ReportRound()
        {
            outputTextbox.Text += "Runde absolviert." + Environment.NewLine;
        }

        public void Report(string output)
        {
            outputTextbox.Text += output + Environment.NewLine;
        }

        public bool Missing(Armee armee, string player)
        {
            var somethingMissing = false;
            try
            {
                foreach (Stack stack in armee.Stacks)
                {
                    if (stack.Count != 0) continue;

                    outputTextbox.Text += $"{player}, gib deine Truppenzahl der {stack.StackUnit.Name} an!";
                    somethingMissing = true;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Fehler ist aufgetreten");
            }
            return somethingMissing;
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

        private void ShortendArmee(int playerOneDiceOne, int playerTwoDiceOne, int playerOneDiceTwo, int playerTwoDiceTwo, int Round)
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
                outputTextbox.Text += $"{Environment.NewLine}Verteidiger würfelt: {playerTwoDiceOne}";
            }
            else
            {
                outputTextbox.Text += $"{Environment.NewLine}Verteidiger würfelt: {playerTwoDiceOne} und {playerTwoDiceTwo}";
            }
        }

        private void Normal(int playerOneDiceOne, int playerTwoDiceOne, int playerOneDiceTwo, int playerTwoDiceTwo, int Round)
        {
            outputTextbox.Text += $"Kampfrunde: {Round}";
            //Würfel:
            outputTextbox.Text += $"{Environment.NewLine}Angreifer würfelt: {playerOneDiceOne}, {playerOneDiceOne} und {playerOneDiceTwo}";
            //Würfel
            outputTextbox.Text += $"{Environment.NewLine}Verteidiger würfelt: {playerTwoDiceOne} und {playerTwoDiceTwo}";
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

        public void SecondHighestDice(int playerOneSecondDice, int playerTwoSecondDice)
        {
            // würfel ausgabe
            MakeSpace(2);
            outputTextbox.Text += $"zweithöchster Angreifer Würfel: {playerOneSecondDice}.{Environment.NewLine}zweithöchster Verteidiger Würfel: {playerTwoSecondDice}";
        }

        public bool UnitDeath(int UnitsAlive, string player, Unit unit)
        {
            MakeSpace( 2);
            if (UnitsAlive > 0)
            {
                outputTextbox.Text += $"Der {player} verliert eine Einheit, die nächste rutscht aber schon nach!{Environment.NewLine}{player} hat noch {UnitsAlive} Einheiten übrig.";
                return false;
            }
            else
            {
                outputTextbox.Text += $"Der {player} wurde besiegt! Alle seine {unit.Name} sind Tod.";
                return true;
            }
        }

        public void Victory(string victor)
        {
            var task = string.Empty;
            task = victor == "Angreifer" ? "erobern" : "verteidigen";

            outputTextbox.Text += $"Der {victor} konnte das Feld {task}!";
        }

        public bool Fight(string Attacker, string Defender, Unit attackerUnit, Unit defenderUnit, out Unit defenderUnitResult, out string withdrawer)
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
                outputTextbox.Text += Environment.NewLine + $"{Attacker} kann die Panzerung des {Defender} nicht durchdringen, er zeiht sein/e {attackerUnit.Name} zurück!";
                defenderUnitResult = defenderUnit;
                withdrawer = Attacker;
                return true;
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
            withdrawer = string.Empty;
            return false;
        }
    }
}
