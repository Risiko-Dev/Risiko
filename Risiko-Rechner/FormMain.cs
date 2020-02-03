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
        Armee DefenderArmee = new Armee();
        Armee AttackerArmee = new Armee();
        Output outputter = new Output();

        public FormMain()
        {
            InitializeComponent();
            outputter.outputTextbox = Textausgabe;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            victory = false;
            var UnitMissing = UnitsMissing();


            if (!UnitMissing)
            {
                Fight();
            }


        }

        private bool UnitsMissing()
        {
            var comboBoxes = this.Controls
            .OfType<ComboBox>()
            .Where(x => x.Name.Contains("UnitBox")); //finde alle Comboboxen die fürs einheiten auswählen sind

            var unitnumbers = this.Controls
            .OfType<NumericUpDown>()
            .Where(x => x.Name.Contains("UnitNumber")); //finde alle NumericUpDowns die fürs einheiten auswählen sind

            var count = 0;
            foreach (var combobox in comboBoxes) //check ob in einem Feld ne unit ausgewählt is und anzahl größer 0
            {
                if (combobox.SelectedItem == null)
                {
                    count++;
                    continue;
                }
                if ((combobox.SelectedItem.ToString() != null || combobox.SelectedItem.ToString() != String.Empty))
                {
                    Unit unit = GetUnit(combobox.SelectedItem.ToString());
                    AddToArmee(combobox, unit, (int)unitnumbers.ToList()[count].Value);
                    count++;
                    continue;
                }
                count++;
            }
            return (outputter.Missing(AttackerArmee, "Angreifer") && outputter.Missing(AttackerArmee, "Verteidiger"));
    
        }
        private Unit GetUnit(Object item)
        {
            foreach (var unit in Units)
            {
                if (unit.Name == item)
                {
                    return unit;
                }
            }
            return null;
        }
        private void AddToArmee(ComboBox UnitName, Unit unit, int UnitNumber = 0)
        {
            if (UnitName.Name.Contains("Defender"))
            {
                DefenderArmee.Units.Add(unit);
                DefenderArmee.NumberOfUnit.Add(UnitNumber);
            }
            else
            {
                AttackerArmee.Units.Add(unit);
                AttackerArmee.NumberOfUnit.Add(UnitNumber);
            }
        }
        private void Fight()
        {

            GetUnits();
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
        private void GetUnits()
        {
            playerOneUnit = AttackerArmee.Units[0];
            playerOneUnitsAlive = AttackerArmee.NumberOfUnit[0];
            playerTwoUnit = DefenderArmee.Units[0];
            playerTwoUnitsAlive = DefenderArmee.NumberOfUnit[0];
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
                if (AttackerArmee.NumberOfUnit.Count > 0)
                {
                    NextUnits(AttackerArmee, 1);
                    return;
                }
                outputter.Victory("Verteidiger");
                victory = true;
            }
            if (playerTwoUnitsAlive <= 0)
            {
                if (AttackerArmee.NumberOfUnit.Count > 0)
                {
                    NextUnits(DefenderArmee, 0);
                    return;
                }
                outputter.Victory("Angreifer");
                victory = true;
            }
        }

        private void NextUnits(Armee armee, int player)
        {
            armee.NumberOfUnit.RemoveAt(0);
            armee.Units.RemoveAt(0);
            if (player == 1)
            {
                playerOneUnit = AttackerArmee.Units[0];
                playerOneUnitsAlive = AttackerArmee.NumberOfUnit[0];
            }
            else
            {
                playerTwoUnit = DefenderArmee.Units[0];
                playerTwoUnitsAlive = DefenderArmee.NumberOfUnit[0];
            }
        }

        private void LoadUnits(object sender, EventArgs e)
        {
            // csv einlesen
            Unitserzeugen(@"..\..\Units.csv");


            foreach (Unit einheit in Units)
            {
                AttackerUnitBox1.Items.Add(einheit.Name);
                DefenderUnitBox6.Items.Add(einheit.Name);
            }
            SetupGUI();
            Textausgabe.Text = "Alle Truppen bereit zu kämpfen, wählen sie ihre Einheiten!";
        }
        private void SetupGUI()
        {
        var comboBoxes = this.Controls
        .OfType<ComboBox>()
        .Where(x => x.Name.Contains("UnitBox")); //finde alle Comboboxen die fürs einheiten auswählen sind

            var Unitnumbers = this.Controls
            .OfType<NumericUpDown>()
            .Where(x => x.Name.Contains("UnitNumber")); //finde alle NumericUpDowns die fürs einheiten auswählen sind

            for (int i = 0; i < comboBoxes.Count(); i++) // nur der erste Angreifer und Verteidiger soll auswählbar sein 
            {
                comboBoxes.ToList()[i].Text = string.Empty;
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
            AttackerUnitBox2.Enabled = true;
            AttackerUnitNumber2.Enabled = true;
            removeEntry(AttackerUnitBox1, AttackerUnitBox2, AttackerUnitBox1.Text);
        }

        private void UnitBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            AttackerUnitBox3.Enabled = true;
            AttackerUnitNumber3.Enabled = true;
            removeEntry(AttackerUnitBox2,AttackerUnitBox3, AttackerUnitBox2.Text);
        }

        private void UnitBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            AttackerUnitBox4.Enabled = true;
            AttackerUnitNumber4.Enabled = true;
            removeEntry(AttackerUnitBox3,AttackerUnitBox4, AttackerUnitBox3.Text);
        }

        private void UnitBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            AttackerUnitBox5.Enabled = true;
            AttackerUnitNumber5.Enabled = true;
            removeEntry(AttackerUnitBox4,AttackerUnitBox5, AttackerUnitBox4.Text);
        }

        private void UnitBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            DefenderUnitBox7.Enabled = true;
            DefenderUnitNumber7.Enabled = true;
            removeEntry(DefenderUnitBox6, DefenderUnitBox7, DefenderUnitBox6.Text);
        }

        private void UnitBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            DefenderUnitBox8.Enabled = true;
            DefenderUnitNumber8.Enabled = true;
            removeEntry(DefenderUnitBox7, DefenderUnitBox8, DefenderUnitBox7.Text);
        }

        private void UnitBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            DefenderUnitBox9.Enabled = true;
            DefenderUnitNumber9.Enabled = true;
            removeEntry(DefenderUnitBox8, DefenderUnitBox9, DefenderUnitBox8.Text);
        }

        private void UnitBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            DefenderUnitBox10.Enabled = true;
            DefenderUnitNumber10.Enabled = true;
            removeEntry(DefenderUnitBox9, DefenderUnitBox10, DefenderUnitBox9.Text);
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
            .Where(x => x.Name.Contains("UnitNumber")); //finde alle NumericUpDowns die fürs einheiten auswählen sind

            foreach (var item in Unitnumbers)
            {
                item.Value = 0;
            }
            AttackerArmee = new Armee();
            DefenderArmee = new Armee();
            SetupGUI();
            Round = 0;

        }
        private void KillCheck(int playerOneUnitHitpoints, int playerTwoUnitHitpoints)
        {
            if (playerOneUnitHitpoints <= 0)
            {
                playerOneUnitsAlive--;
                //Kill anouncment
                victory = outputter.UnitDeath(playerOneUnitsAlive, "Angreifer");
            }

            if (playerTwoUnitHitpoints <= 0)
            {
                playerTwoUnitsAlive--;
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
