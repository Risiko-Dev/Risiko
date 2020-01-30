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
    public partial class Form1 : Form
    {
        List<Unit> Units = new List<Unit>();
        int Runde = 0, anz1 = 0, anz2 = 0;
        Unit kaempfer1 = null, kaempfer2 = null;
        Unit bauplan1 = null;
        Unit bauplan2 = null;
        bool victory = false;
        Output outputter = new Output();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            victory = false;

            string name1 = comboBox1.Text;
            string name2 = comboBox2.Text;

            anz1 = (int)numericUpDown1.Value;
            anz2 = (int)numericUpDown2.Value;

            var readyForFight = !(outputter.MissingUnitNames(name1, name2, Textausgabe) || outputter.MissingUnitNumbers(anz1, anz2, Textausgabe));
            if (readyForFight)
            {
                Fight(name1, name2);
            }


        }

        private void Fight(string name1, string name2)
        {
            //hab mal das foreach zu einer Linq abfrage geändert -> einfacher zu lesen

            bauplan1 = Units.Where(a => a.Name == name1).FirstOrDefault();

            bauplan2 = Units.Where(a => a.Name == name2).FirstOrDefault();

            kaempfer1 = (Unit)bauplan1.Clone();
            kaempfer2 = (Unit)bauplan2.Clone();

            //So bitte bitte mal die Variablennamen ausschreiben und vorallem sinvoll wählen
            // -> was macht bauplan wofür brauch ich den ? warum ist er da

            List<int> S1W = new List<int>();
            List<int> S2W = new List<int>();

            Random Wuerfel = new Random();
            do
            {
                S1W.Clear();
                S2W.Clear();
                Runde++;

                int S1W1 = Wuerfel.Next(1, 7);
                int S1W2 = Wuerfel.Next(1, 7);
                int S1W3 = Wuerfel.Next(1, 7);

                int S2W1 = Wuerfel.Next(1, 7);
                int S2W2 = Wuerfel.Next(1, 7);

                S1W.Add(S1W1);
                S1W.Add(S1W2);
                S1W.Add(S1W3);

                S2W.Add(S2W1);
                S2W.Add(S2W2);

                S1W.Sort();
                S2W.Sort();

                S1W1 = S1W[2];
                S1W2 = S1W[1];
                S2W1 = S2W[1];
                S2W2 = S2W[0];
                outputter.StartupText(Textausgabe, S1W1, S2W1, S1W2, S2W2, Runde);
                outputter.HighestDice(Textausgabe, S1W1, S1W2);

                WuerfelErgebnis(S1W1, S2W1); //quick reforctor -> da stand 2 mal das selbe ich hab das mal zu ner methode gemacht

                if (!victory)
                {
                    outputter.SecondHighestDice(Textausgabe,S2W1, S2W2);
                    WuerfelErgebnis(S1W2, S2W2);
                }

            } while ((anz1 > 0 && anz2 > 0) && !victory);
        }


        private bool Angreifer()
        {
            //Würfel ergb.
            Textausgabe.Text += Environment.NewLine + "Angreifer führt Attacke aus:";

            int remainingArmor2 = kaempfer2.Armor - kaempfer1.AntiArmor;
            //Armortest report
            Textausgabe.Text += Environment.NewLine + "Rüstungswert des Verteidigers nach angriff mit AP-Waffen: " + Convert.ToString(remainingArmor2);

            if (remainingArmor2 < 0)
            { remainingArmor2 = 0; }

            if (remainingArmor2 - kaempfer1.AttackDamage >= 0)
            {
                // TEXT: ABBRUCH DES KAMPFES, 1 zieht sich zurück
                Textausgabe.Text += Environment.NewLine + "Angreifer kann die Panzerung des Verteidigers nicht durchdringen, er muss sich zurückziehen";
                return true;
            }

            if (remainingArmor2 == 0)
            {
                kaempfer2.HitPoints = kaempfer2.HitPoints - kaempfer1.AttackDamage;
                //Dmg and HP-loss
                Textausgabe.Text += Environment.NewLine + "Verteidiger erhält " + Convert.ToString(kaempfer1.AttackDamage) + " Schaden,"
                    + Environment.NewLine + kaempfer2.Name + " hat noch " + Convert.ToString(kaempfer2.HitPoints) + " HP";
            }
            else
            {
                kaempfer2.HitPoints = kaempfer2.HitPoints - kaempfer1.AttackDamage + remainingArmor2;
                //Dmg and HP-loss
                Textausgabe.Text += Environment.NewLine + "Verteidiger erhält " + Convert.ToString(kaempfer1.AttackDamage) + " Schaden, kann dabei " + Convert.ToString(remainingArmor2) +
                    " abwehren" + Environment.NewLine + kaempfer2.Name + " hat noch " + Convert.ToString(kaempfer2.HitPoints) + " HP";
            }
            return false;
        }

        private bool Verteidiger()
        {
            //würfel ergebnis
            Textausgabe.Text += Environment.NewLine + "Verteidiger führt Attacke aus:";

            int remainingArmor1 = kaempfer1.Armor - kaempfer2.AntiArmor;
            //Armortest report
            Textausgabe.Text += Environment.NewLine + "Rüstungswert des Angreifers nach angriff mit AP-Waffen: " + Convert.ToString(remainingArmor1);

            if (remainingArmor1 < 0)
            { remainingArmor1 = 0; }

            if (remainingArmor1 - kaempfer2.AttackDamage >= 0)
            {
                // TEXT: ABBRUCH DES KAMPFES, 2 zieht sich zurück
                Textausgabe.Text += Environment.NewLine + "Verteidiger kann die Panzerung des Angreifers nicht durchdringen, er muss sich zurückziehen";
                return true;
            }

            if (remainingArmor1 == 0)
            {
                kaempfer1.HitPoints = kaempfer1.HitPoints - kaempfer2.AttackDamage;
                //Dmg and HP-loss
                Textausgabe.Text += Environment.NewLine + "Angreifer erhält " + Convert.ToString(kaempfer2.AttackDamage) + " Schaden,"
                    + Environment.NewLine + kaempfer1.Name + " hat noch " + Convert.ToString(kaempfer1.HitPoints) + " HP";
            }
            else
            {
                kaempfer1.HitPoints = kaempfer1.HitPoints - kaempfer2.AttackDamage + remainingArmor1;
                //Dmg and HP-loss
                Textausgabe.Text += Environment.NewLine + "Angreifer erhält " + Convert.ToString(kaempfer2.AttackDamage) + " Schaden, kann dabei " + Convert.ToString(remainingArmor1) +
                    " abwehren" + Environment.NewLine + kaempfer1.Name + " hat noch " + Convert.ToString(kaempfer1.HitPoints) + " HP";
            }
            return false;
        }
        private void WuerfelErgebnis(int spieler11, int spieler21)
        {
            var withdrawal = false;
            if (spieler11 > spieler21)
            {
                withdrawal= Angreifer();
            }
            else
            {
                withdrawal = Verteidiger();
            }
            if (!withdrawal)
            {
                Killcheck(kaempfer1.HitPoints, kaempfer2.HitPoints);
                Victorycheck(anz1, anz2);
            }
            else
            {
                victory = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // csv einlesen

            Unitserzeugen(@"..\..\Units.csv");

            foreach (Unit einheit in Units)
            {
                comboBox1.Items.Add(einheit.Name);
                comboBox2.Items.Add(einheit.Name);
            }

            Textausgabe.Text = "Alle Truppen bereit zu kämpfen, wählen sie ihre Einheiten!";


        }
        //umbenennen !
        private void button2_Click(object sender, EventArgs e)
        {
            Textausgabe.Text = "Alle Truppen bereit zu kämpfen, wählen sie ihre Einheiten!";
            comboBox1.Text = "";
            comboBox2.Text = "";
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            Runde = 0;

        }
        /// <summary>
        /// Auch hier bitte die namen ausschreiben, ich denke ich weiß was das heißen soll aber is mega unübersichtlich 
        /// </summary>
        private void Killcheck(int k1hp, int k2hp)
        {
            if (k1hp <= 0)
            {
                anz1--;
                kaempfer1 = (Unit)bauplan1.Clone();
                //Kill anouncment
                Textausgabe.Text += Environment.NewLine + "Angreifer verliert eine Einheit, die nächste rutscht aber schon nach!" +
                    Environment.NewLine + "Angreifer hat noch " + Convert.ToString(anz1) + " Einheiten übrig.";
            }

            if (k2hp <= 0)
            {
                anz2--;
                kaempfer2 = (Unit)bauplan2.Clone();
                //Kill anouncment
                Textausgabe.Text += Environment.NewLine + "Verteidiger verliert eine Einheit, die nächste rutscht aber schon nach!" +
                    Environment.NewLine + "Verteidiger hat noch " + Convert.ToString(anz2) + " Einheiten übrig.";
            }


        }
        /// <summary>
        /// Bitte hier auch was is z1 und z2 das is wenn man nur die methode sich anschaut absolut nicht ersichtlich !
        /// </summary>
        private void Victorycheck(int z1, int z2)
        {
            if (z1 <= 0)
            {
                //Winnner...
                Textausgabe.Text += Environment.NewLine + "Der Angreifer hat alle Einheiten verloren!" +
                    Environment.NewLine + "Der Verteidiger gewinnt und hält sein Feld";

            }

            if (z2 <= 0)
            {
                //Winnner...
                Textausgabe.Text += Environment.NewLine + "Der Verteidiger hat alle Einheiten verloren!" +
                    Environment.NewLine + "Der Angreifer gewinnt und erobert das Feld";
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
