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

        }
    }
}
