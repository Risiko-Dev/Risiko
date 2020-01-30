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
                outputTextbox.Text +=  Environment.NewLine + "Angreifer, wählen Sie eine Einheitenklasse";
                return true;
            }
            if (name2 == "")
            {
                outputTextbox.Text += Environment.NewLine + "Verteidiger, wählen Sie eine Einheitklasse";
                return true;
            }
            return false;
        }
        public bool MissingUnitNumbers(int unitNumerAttacker ,int unitNumberDefender ,System.Windows.Forms.TextBox outputTextbox)
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
            outputTextbox.Text += Environment.NewLine + Environment.NewLine + Environment.NewLine + "Kampfrunde: " + Convert.ToString(Round) + Environment.NewLine;
            //Würfel:
            outputTextbox.Text += Environment.NewLine + " Angreifer würfelt: " + Convert.ToString(playerOneDiceOne) + ", "
                + Convert.ToString(playerOneDiceOne) + " und " + Convert.ToString(playerOneDiceTwo);
            //Würfel
            outputTextbox.Text += Environment.NewLine + " Verteidiger würfelt: " + Convert.ToString(playerTwoDiceOne) + " und "
                + Convert.ToString(playerTwoDiceTwo);
        }
        public void HighestDice(System.Windows.Forms.TextBox outputTextbox, int S1W1, int S1W2)
        {
            // würfel ausgabe
            outputTextbox.Text += Environment.NewLine + Environment.NewLine + "höchster Angreifer Würfel: " + Convert.ToString(S1W1) +
                Environment.NewLine + "höchster Verteidiger Würfel: " + Convert.ToString(S1W2);
        }

        public void SecondHighestDice(System.Windows.Forms.TextBox outputTextbox,int S2W1, int S2W2)
        {
            // würfel ausgabe
            outputTextbox.Text += Environment.NewLine + Environment.NewLine + "zweithöchster Angreifer Würfel: " + Convert.ToString(S2W1) +
                Environment.NewLine + "zweithöchsterhöchster Verteidiger Würfel: " + Convert.ToString(S2W2);
        }
    }
}
