using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risiko_Rechner
{
    public class Output
    {
        public bool MissingUnitNames(string name1, string name2, System.Windows.Forms.TextBox outputTextbox)
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
        public bool MissingUnitNumbers(int unitNumerAttacker, int unitNumberDefender, System.Windows.Forms.TextBox outputTextbox)
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
        public void StartupText(System.Windows.Forms.TextBox outputTextbox, int playerOneDiceOne, int playerTwoDiceOne, int playerOneDiceTwo, int playerTwoDiceTwo, int Round)
        {
            MakeSpace(outputTextbox, 3);
            outputTextbox.Text += $"Kampfrunde: {Round}";
            //Würfel:
            outputTextbox.Text += $"{Environment.NewLine}Angreifer würfelt: {playerOneDiceOne}, {playerOneDiceOne} und {playerOneDiceTwo}";
            //Würfel
            outputTextbox.Text += $"{Environment.NewLine} Verteidiger würfelt: {playerTwoDiceOne} und {playerTwoDiceTwo}";
        }
        public void HighestDice(System.Windows.Forms.TextBox outputTextbox, int playerOneFirstDice, int playerTwoFirstDice)
        {
            // würfel ausgabe
            MakeSpace(outputTextbox, 2);
            outputTextbox.Text += $"höchster Angreifer Würfel: {playerOneFirstDice}{Environment.NewLine}höchster Verteidiger Würfel: {playerTwoFirstDice}";
        }

        private static void MakeSpace(System.Windows.Forms.TextBox outputTextbox, int blankLineAmount)
        {
            for (int i = 0; i < blankLineAmount; i++)
            {
                outputTextbox.Text += Environment.NewLine;
            }
        }

        public void SecondHighestDice(System.Windows.Forms.TextBox outputTextbox, int playerOneSecondDice, int playerTwoSecondDice)
        {
            // würfel ausgabe
            MakeSpace(outputTextbox, 2);
            outputTextbox.Text += $"zweithöchster Angreifer Würfel: {playerOneSecondDice}.{Environment.NewLine}zweithöchsterhöchster Verteidiger Würfel:{playerTwoSecondDice}";
        }
    }
}
