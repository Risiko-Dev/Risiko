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
        int Round = 0, playerOneUnitsAlive = 0, playerTwoUnitsAlive = 0;
        Unit playerOneUnit = null, playerTwoUnit = null;
        Unit bauplan1 = null;
        Unit bauplan2 = null;
        bool victory = false;
        Output outputter = new Output();

        public Form1()
        {
            InitializeComponent();
            outputter.outputTextbox = Textausgabe;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            victory = false;

            string name1 = comboBox1.Text;
            string name2 = comboBox2.Text;

            playerOneUnitsAlive = (int)numericUpDown1.Value;
            playerTwoUnitsAlive = (int)numericUpDown2.Value;

            var readyForFight = !(outputter.MissingUnitNames(name1, name2) || outputter.MissingUnitNumbers(playerOneUnitsAlive, playerTwoUnitsAlive));
            if (readyForFight)
            {
                Setup(name1, name2);
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
                outputter.StartupText( playerOneDiceOne, playerTwoDiceOne, playerOneDiceTwo, playerTwoDiceTwo, Round);
                outputter.HighestDice( playerOneDiceOne, playerOneDiceTwo);

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
            var S1W3 = Wuerfel.Next(1, 7);

            playerTwoDiceOne = Wuerfel.Next(1, 7);
            playerTwoDiceTwo = Wuerfel.Next(1, 7);
            playerOneDices.Add(playerOneDiceOne);
            playerOneDices.Add(playerOneDiceTwo);
            playerOneDices.Add(S1W3);

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

        private bool Angreifer()
        {
            //Würfel ergb.
            Textausgabe.Text += Environment.NewLine + "Angreifer führt Attacke aus:";

            int remainingArmor2 = playerTwoUnit.Armor - playerOneUnit.AntiArmor;
            //Armortest report
            Textausgabe.Text += Environment.NewLine + "Rüstungswert des Verteidigers nach angriff mit AP-Waffen: " + Convert.ToString(remainingArmor2);

            if (remainingArmor2 < 0)
            { remainingArmor2 = 0; }

            if (remainingArmor2 - playerOneUnit.AttackDamage >= 0)
            {
                // TEXT: ABBRUCH DES KAMPFES, 1 zieht sich zurück
                Textausgabe.Text += Environment.NewLine + "Angreifer kann die Panzerung des Verteidigers nicht durchdringen, er muss sich zurückziehen";
                return true;
            }

            if (remainingArmor2 == 0)
            {
                playerTwoUnit.HitPoints = playerTwoUnit.HitPoints - playerOneUnit.AttackDamage;
                //Dmg and HP-loss
                Textausgabe.Text += Environment.NewLine + "Verteidiger erhält " + Convert.ToString(playerOneUnit.AttackDamage) + " Schaden,"
                    + Environment.NewLine + playerTwoUnit.Name + " hat noch " + Convert.ToString(playerTwoUnit.HitPoints) + " HP";
            }
            else
            {
                playerTwoUnit.HitPoints = playerTwoUnit.HitPoints - playerOneUnit.AttackDamage + remainingArmor2;
                //Dmg and HP-loss
                Textausgabe.Text += Environment.NewLine + "Verteidiger erhält " + Convert.ToString(playerOneUnit.AttackDamage) + " Schaden, kann dabei " + Convert.ToString(remainingArmor2) +
                    " abwehren" + Environment.NewLine + playerTwoUnit.Name + " hat noch " + Convert.ToString(playerTwoUnit.HitPoints) + " HP";
            }
            return false;
        }

        private bool Verteidiger()
        {
            //würfel ergebnis
            Textausgabe.Text += Environment.NewLine + "Verteidiger führt Attacke aus:";

            int remainingArmor1 = playerOneUnit.Armor - playerTwoUnit.AntiArmor;
            //Armortest report
            Textausgabe.Text += Environment.NewLine + "Rüstungswert des Angreifers nach angriff mit AP-Waffen: " + Convert.ToString(remainingArmor1);

            if (remainingArmor1 < 0)
            { remainingArmor1 = 0; }

            if (remainingArmor1 - playerTwoUnit.AttackDamage >= 0)
            {
                // TEXT: ABBRUCH DES KAMPFES, 2 zieht sich zurück
                Textausgabe.Text += Environment.NewLine + "Verteidiger kann die Panzerung des Angreifers nicht durchdringen, er muss sich zurückziehen";
                return true;
            }

            if (remainingArmor1 == 0)
            {
                playerOneUnit.HitPoints = playerOneUnit.HitPoints - playerTwoUnit.AttackDamage;
                //Dmg and HP-loss
                Textausgabe.Text += Environment.NewLine + "Angreifer erhält " + Convert.ToString(playerTwoUnit.AttackDamage) + " Schaden,"
                    + Environment.NewLine + playerOneUnit.Name + " hat noch " + Convert.ToString(playerOneUnit.HitPoints) + " HP";
            }
            else
            {
                playerOneUnit.HitPoints = playerOneUnit.HitPoints - playerTwoUnit.AttackDamage + remainingArmor1;
                //Dmg and HP-loss
                Textausgabe.Text += Environment.NewLine + "Angreifer erhält " + Convert.ToString(playerTwoUnit.AttackDamage) + " Schaden, kann dabei " + Convert.ToString(remainingArmor1) +
                    " abwehren" + Environment.NewLine + playerOneUnit.Name + " hat noch " + Convert.ToString(playerOneUnit.HitPoints) + " HP";
            }
            return false;
        }
        private void WuerfelErgebnis(int spieler11, int spieler21)
        {
            var withdrawal = false;
            if (spieler11 > spieler21)
            {
                withdrawal = Angreifer();
            }
            else
            {
                withdrawal = Verteidiger();
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
            if (playerOneUnitsAlive <= 0 )
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
                comboBox1.Items.Add(einheit.Name);
                comboBox2.Items.Add(einheit.Name);
            }

            Textausgabe.Text = "Alle Truppen bereit zu kämpfen, wählen sie ihre Einheiten!";
        }
        private void ResetProgram(object sender, EventArgs e)
        {
            Textausgabe.Text = "Alle Truppen bereit zu kämpfen, wählen sie ihre Einheiten!";
            comboBox1.Text = "";
            comboBox2.Text = "";
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
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
