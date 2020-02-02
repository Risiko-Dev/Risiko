using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Risiko_Rechner
{
    public partial class FormMain : Form
    {
        List<Unit> Units = new List<Unit>();
        int Round = 0, playerOneUnitsAlive = 0, playerTwoUnitsAlive = 0;
        Unit playerOneUnit = null, playerTwoUnit = null;
        Unit bauplan1 = null;
        Unit bauplan2 = null;
        bool victory = false;
        Output outputter = new Output();

        public FormMain()
        {
            InitializeComponent();
            outputter.outputTextbox = Textausgabe;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            victory = false;

            string name1 = UnitBox1.Text;
            string name2 = UnitBox6.Text;

            playerOneUnitsAlive = (int)numericUpDown1.Value;
            playerTwoUnitsAlive = (int)numericUpDown2.Value;

            var UnitMissing = UnitsMissing();


            if (!UnitMissing)
            {
                Setup(name1, name2);
            }


        }

        private bool UnitsMissing()
        {
            var comboBoxes = this.Controls
            .OfType<ComboBox>()
            .Where(x => x.Name.StartsWith("UnitBox")); //finde alle Comboboxen die fürs einheiten auswählen sind

            var Unitnumbers = this.Controls
            .OfType<NumericUpDown>()
            .Where(x => x.Name.StartsWith("numericUpDown")); //finde alle NumericUpDowns die fürs einheiten auswählen sind

            var count = 0;
            var goFight = true;
            foreach (var item in comboBoxes) //check ob in einem Feld ne unit ausgewählt is und anzahl größer 0
            {
                if (item.SelectedItem == null)
                {
                    continue;
                }
                count++;
                if ((item.SelectedItem.ToString() != null || item.SelectedItem.ToString() != String.Empty) && Unitnumbers.ToList()[count].Value != 0)
                {
                    goFight = false;
                }

            }
            if (goFight)
            {
                return false;
            }
            else
            {
                outputter.Missing(comboBoxes.ToList(), Unitnumbers.ToList());
                return true;
            }
        }

        private void Setup(string name1, string name2)
        {
            //hab mal das foreach zu einer Linq abfrage geändert -> einfacher zu lesen

            bauplan1 = Units.Where(a => a.Name == name1).FirstOrDefault();

            bauplan2 = Units.Where(a => a.Name == name2).FirstOrDefault();

            playerOneUnit = (Unit)bauplan1.Clone();
            playerTwoUnit = (Unit)bauplan2.Clone();

            Fight();
        }

        private void Fight()
        {
            do
            {

                Round++;
                int playerOneDiceOne, playerOneDiceTwo, playerTwoDiceOne, playerTwoDiceTwo;
                RollDices(out playerOneDiceOne, out playerOneDiceTwo, out playerTwoDiceOne, out playerTwoDiceTwo);
                outputter.StartupText(playerOneDiceOne, playerTwoDiceOne, playerOneDiceTwo, playerTwoDiceTwo, Round);
                outputter.HighestDice(playerOneDiceOne, playerOneDiceTwo);

                WuerfelErgebnis(playerOneDiceOne, playerTwoDiceOne); //quick reforctor -> da stand 2 mal das selbe ich hab das mal zu ner methode gemacht

                if (!victory && playerOneDiceTwo > 0 && playerTwoDiceTwo > 0)
                {
                    outputter.SecondHighestDice(playerTwoDiceOne, playerTwoDiceTwo);
                    WuerfelErgebnis(playerOneDiceTwo, playerTwoDiceTwo);
                }

            } while ((playerOneUnitsAlive > 0 && playerTwoUnitsAlive > 0) && !victory);
        }

        private void RollDices(out int playerOneDiceOne, out int playerOneDiceTwo, out int playerTwoDiceOne, out int playerTwoDiceTwo)
        {
            List<int> playerOneDices = new List<int>();
            List<int> PlayerTwoDices = new List<int>();

            Random Wuerfel = new Random();
            playerOneDices.Clear();
            PlayerTwoDices.Clear();

            playerOneDiceOne = Wuerfel.Next(1, 7);
            playerOneDiceTwo = Wuerfel.Next(1, 7);
            var palyerOneDiceThree = Wuerfel.Next(1, 7);

            playerTwoDiceOne = Wuerfel.Next(1, 7);
            playerTwoDiceTwo = Wuerfel.Next(1, 7);
            playerOneDices.Add(playerOneDiceOne);
            playerOneDices.Add(playerOneDiceTwo);
            playerOneDices.Add(palyerOneDiceThree);

            PlayerTwoDices.Add(playerTwoDiceOne);
            PlayerTwoDices.Add(playerTwoDiceTwo);

            playerOneDices.Sort();
            PlayerTwoDices.Sort();

            playerOneDiceOne = playerOneDices[2];
            playerTwoDiceOne = PlayerTwoDices[1];
            CheckIfEnoughUnits(out playerOneDiceTwo, out playerTwoDiceTwo, playerOneDices, PlayerTwoDices);
        }

        private void CheckIfEnoughUnits(out int playerOneDiceTwo, out int playerTwoDiceTwo, List<int> playerOneDices, List<int> PlayerTwoDices)
        {
            if (playerOneUnitsAlive > 1)
            {
                playerOneDiceTwo = playerOneDices[1];
            }
            else
            {
                playerOneDiceTwo = -1;
            }
            if (playerTwoUnitsAlive > 1)
            {
                playerTwoDiceTwo = PlayerTwoDices[0];
            }
            else
            {
                playerTwoDiceTwo = -1;
            }
        }
        private void WuerfelErgebnis(int spieler11, int spieler21)
        {
            var withdrawal = false;
            if (spieler11 > spieler21)
            {
                withdrawal = outputter.Fight("Angreifer", "Verteidiger", playerOneUnit, playerTwoUnit, out playerTwoUnit);
            }
            else
            {
                withdrawal = outputter.Fight("Verteidiger", "Angreifer", playerTwoUnit, playerOneUnit, out playerOneUnit);
            }
            if (!withdrawal)
            {
                KillCheck(playerOneUnit.HitPoints, playerTwoUnit.HitPoints);
                VictoryCheck();
            }
            else
            {
                victory = true;
            }
        }
        private void VictoryCheck()
        {
            if (playerOneUnitsAlive <= 0)
            {
                outputter.Victory("Verteidiger");
                victory = true;
            }
            if (playerTwoUnitsAlive <= 0)
            {
                outputter.Victory("Angreifer");
                victory = true;
            }
        }
        private void LoadUnits(object sender, EventArgs e)
        {
            // csv einlesen
            Unitserzeugen(@"..\..\Units.csv");


            foreach (Unit einheit in Units)
            {
                UnitBox1.Items.Add(einheit.Name);
                UnitBox6.Items.Add(einheit.Name);
            }
            SetupGUI();
            Textausgabe.Text = "Alle Truppen bereit zu kämpfen, wählen sie ihre Einheiten!";
        }
        private void SetupGUI()
        {
            var comboBoxes = this.Controls
        .OfType<ComboBox>()
        .Where(x => x.Name.StartsWith("UnitBox")); //finde alle Comboboxen die fürs einheiten auswählen sind

            var Unitnumbers = this.Controls
            .OfType<NumericUpDown>()
            .Where(x => x.Name.StartsWith("numericUpDown")); //finde alle NumericUpDowns die fürs einheiten auswählen sind

            for (int i = 0; i < comboBoxes.Count(); i++)
            {
                if (comboBoxes.ToList()[i].Name.Contains("1") || comboBoxes.ToList()[i].Name.Contains("6"))
                {
                    if (comboBoxes.ToList()[i].Name.Contains("10"))
                    {
                        comboBoxes.ToList()[i].Enabled = false;
                        Unitnumbers.ToList()[i].Enabled = false;
                        continue;
                    }
                    comboBoxes.ToList()[i].Enabled = true;
                    Unitnumbers.ToList()[i].Enabled = true;
                    continue;
                }
                comboBoxes.ToList()[i].Enabled = false;
                Unitnumbers.ToList()[i].Enabled = false;
            }
        }

        private void UnitBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UnitBox2.Enabled = true;
            numericUpDown2.Enabled = true;
            removeEntry(UnitBox1, UnitBox2, UnitBox1.Text);
        }

        private void UnitBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            UnitBox3.Enabled = true;
            numericUpDown3.Enabled = true;
            removeEntry(UnitBox2,UnitBox3, UnitBox2.Text);
        }

        private void UnitBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            UnitBox4.Enabled = true;
            numericUpDown4.Enabled = true;
            removeEntry(UnitBox3,UnitBox4, UnitBox3.Text);
        }

        private void UnitBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            UnitBox5.Enabled = true;
            numericUpDown5.Enabled = true;
            removeEntry(UnitBox4,UnitBox5, UnitBox4.Text);
        }

        private void UnitBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            UnitBox7.Enabled = true;
            numericUpDown7.Enabled = true;
            removeEntry(UnitBox6, UnitBox7, UnitBox6.Text);
        }

        private void UnitBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            UnitBox8.Enabled = true;
            numericUpDown8.Enabled = true;
            removeEntry(UnitBox7, UnitBox8, UnitBox7.Text);
        }

        private void UnitBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            UnitBox9.Enabled = true;
            numericUpDown9.Enabled = true;
            removeEntry(UnitBox8, UnitBox9, UnitBox8.Text);
        }

        private void UnitBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            UnitBox10.Enabled = true;
            numericUpDown10.Enabled = true;
            removeEntry(UnitBox9, UnitBox10, UnitBox9.Text);
        }
        private void removeEntry(ComboBox comboBoxOld, ComboBox comboBoxNew, string name)
        {
            var count = comboBoxNew.Items.Count;
            for (int i = 0; i < count ; i++)
            {
                comboBoxNew.Items.RemoveAt(0);
            }
            foreach (var item in comboBoxOld.Items)
            {
                if (item.ToString() == name)
                {
                    continue;
                }
                comboBoxNew.Items.Add(item);
            }
        }

        private void ResetProgram(object sender, EventArgs e)
        {

            var Unitnumbers = this.Controls
            .OfType<NumericUpDown>()
            .Where(x => x.Name.StartsWith("numericUpDown")); //finde alle NumericUpDowns die fürs einheiten auswählen sind

            foreach (var item in Unitnumbers)
            {
                item.Value = 0;
            }
            UnitBox1.Text = string.Empty;
            SetupGUI();
            Round = 0;

        }
        private void KillCheck(int playerOneUnitHitpoints, int playerTwoUnitHitpoints)
        {
            if (playerOneUnitHitpoints <= 0)
            {
                playerOneUnitsAlive--;
                playerOneUnit = (Unit)bauplan1.Clone();
                //Kill anouncment
                victory = outputter.UnitDeath(playerOneUnitsAlive, "Angreifer");
            }

            if (playerTwoUnitHitpoints <= 0)
            {
                playerTwoUnitsAlive--;
                playerTwoUnit = (Unit)bauplan2.Clone();
                //Kill anouncment
                victory = outputter.UnitDeath(playerTwoUnitsAlive, "Verteidiger");
            }
        }

        private void Unitserzeugen(String pfad)
        {
            Textausgabe.Text += Environment.NewLine + "Units werden eingelesen";

            // Objekt zum Einlesen von Dateien wird angelegt
            var reader = new StreamReader(pfad);
            // Schleife bis zum Ende der Datei
            while (!reader.EndOfStream)
            {
                // einzelne Zeile einlesen 
                String line = reader.ReadLine();
                // Teile der Zeile werden getrennt (Semikoleon ist Trennzeichen)
                String[] _values = line.Split(';');
                // die erste Spalte gibt die ID an
                String loadname = _values[0];



                // Teile des Eigenschaften-Strings werden getrennt (Komma ist Trennzeichen)
                String[] DatenArray = _values[1].Split(',');


                // Das Objekt Einheit wird angelegt mit allen Eigenschaften
                Unit Einheit = new Unit(loadname, Convert.ToInt16(DatenArray[0]), Convert.ToInt16(DatenArray[1]), Convert.ToInt16(DatenArray[2]), Convert.ToInt16(DatenArray[3]));
                if (Einheit != null)
                {

                    // die Eineheiten werden der Liste des Units hinzugefügt
                    Units.Add((Unit)Einheit);

                }
            }
            reader.Close();

        }


    }
}
