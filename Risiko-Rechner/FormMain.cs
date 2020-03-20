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

        private Player attackerPlayer;
        private Player defenderPlayer;

        Armee attackerArmy = new Armee();
        Armee defenderArmy = new Armee();

        Output reporter = new Output();

        public FormMain()
        {
            InitializeComponent();
            reporter.outputTextbox = Textausgabe;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            attackerPlayer = new Player(PlayerType.Attacker); 
            defenderPlayer = new Player(PlayerType.Defender);

            reporter.outputTextbox.Clear();
            var UnitMissing = UnitsMissing();

            if (!UnitMissing)
            {
                Swap(attackerArmy);
                Swap(defenderArmy);

                Fight calculation = new Fight(attackerPlayer, defenderPlayer, reporter);
            }
        }

        private void Swap(Armee armee)
        {
            armee.Stacks.Reverse();
        }

        private bool UnitsMissing()
        {
            var groupBoxes = this.Controls
            .OfType<GroupBox>();

            List<ComboBox> comboBoxes = new List<ComboBox>();
            List<NumericUpDown> unitnumbers = new List<NumericUpDown>(); ;

            foreach (var item in groupBoxes)
            {
                 comboBoxes.AddRange(item.Controls
                .OfType<ComboBox>()
                .Where(x => x.Name.Contains("UnitBox")).ToList()); //finde alle Comboboxen die fürs einheiten auswählen sind
                
                unitnumbers.AddRange(item.Controls
                .OfType<NumericUpDown>()
                .Where(x => x.Name.Contains("UnitNumber")).ToList()); //finde alle NumericUpDowns die fürs einheiten auswählen sind
            }

            var count = 0;
            foreach (var combobox in comboBoxes) //check ob in einem Feld ne unit ausgewählt is und anzahl größer 0
            {
                if (combobox.SelectedItem == null)
                {
                    count++;
                    continue;
                }

                if ((combobox.SelectedItem.ToString() != null || combobox.SelectedItem.ToString() != string.Empty))
                {
                    Unit unit = GetUnit(combobox.SelectedItem.ToString());
                    AddToArmee(combobox, unit, (int)unitnumbers.ToList()[count].Value);
                    count++;
                    continue;
                }
                count++;
            }
            return (reporter.Missing(attackerArmy, "Angreifer") && reporter.Missing(attackerArmy, "Verteidiger"));

        }

        private Unit GetUnit(Object item)
        {
            foreach (var unit in Units)
            {
                if (unit.Name == (string)item)
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
                defenderPlayer.Armee.Stacks.Add(new Stack(unit, UnitNumber));
            }
            else
            {
                attackerPlayer.Armee.Stacks.Add(new Stack(unit, UnitNumber));
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
            Textausgabe.Text = "Alle Truppen bereit zu kämpfen, wählen sie ihre Einheiten!" + Environment.NewLine;
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
            removeEntry(AttackerUnitBox2, AttackerUnitBox3, AttackerUnitBox2.Text);
        }

        private void UnitBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            AttackerUnitBox4.Enabled = true;
            AttackerUnitNumber4.Enabled = true;
            removeEntry(AttackerUnitBox3, AttackerUnitBox4, AttackerUnitBox3.Text);
        }

        private void UnitBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            AttackerUnitBox5.Enabled = true;
            AttackerUnitNumber5.Enabled = true;
            removeEntry(AttackerUnitBox4, AttackerUnitBox5, AttackerUnitBox4.Text);
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

            for (int i = 0; i < count; i++)
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
            attackerArmy = new Armee();
            defenderArmy = new Armee();
            SetupGUI();
        }

        private void unitserzeugen(String pfad)
        {
            Textausgabe.Text += Environment.NewLine + "Units werden eingelesen";
            // Objekt zum Einlesen von Dateien wird angelegt
            var reader = new StreamReader(pfad);

            // Schleife bis zum Ende der Datei
            while (!reader.EndOfStream)
            {
                // einzelne Zeile einlesen 
                String line = reader.ReadLine();
                // Teile der Zeile werden getrennt (Semikolon ist Trennzeichen)
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
