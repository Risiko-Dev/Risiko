using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risiko_Rechner
{
   public  class Fight
    {
        int Round = 0, playerOneUnitsAlive = 0, playerTwoUnitsAlive = 0;
        Unit playerOneUnit = null, playerTwoUnit = null;
        bool victory = false;
        Output outputter = new Output();
        Armee defenderArmee = new Armee();
        Armee attackerArmee = new Armee();
        public void Run(Armee attacker, Armee defender, System.Windows.Forms.TextBox output)
        {
            outputter.outputTextbox = output;
            attackerArmee = attacker;
            defenderArmee = defender;
            Round = 0;
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
            playerOneUnit = attackerArmee.Units[0];
            playerOneUnitsAlive = attackerArmee.NumberOfUnit[0];
            playerTwoUnit = defenderArmee.Units[0];
            playerTwoUnitsAlive = defenderArmee.NumberOfUnit[0];
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
            string withdrawler = string.Empty;
            var withdrawal = false;
            if (spieler11 > spieler21)
            {
                withdrawal = outputter.Fight("Angreifer", "Verteidiger", playerOneUnit, playerTwoUnit, out playerTwoUnit, out withdrawler);
            }
            else
            {
                withdrawal = outputter.Fight("Verteidiger", "Angreifer", playerTwoUnit, playerOneUnit, out playerOneUnit, out withdrawler);
            }
            if (!withdrawal)
            {
                KillCheck(playerOneUnit.HitPoints, playerTwoUnit.HitPoints);
                VictoryCheck();
            }
            else
            {
                if (withdrawler == "Verteidiger")
                {
                    NextUnits(defenderArmee, 2);
                }
                else
                {
                    NextUnits(attackerArmee, 1);
                }
            }
        }
        private void KillCheck(int playerOneUnitHitpoints, int playerTwoUnitHitpoints)
        {
            if (playerOneUnitHitpoints <= 0)
            {
                playerOneUnitsAlive--;
                playerOneUnit = attackerArmee.Units[0];
                //Kill anouncment
                outputter.UnitDeath(playerOneUnitsAlive, "Angreifer");
            }

            if (playerTwoUnitHitpoints <= 0)
            {
                playerTwoUnitsAlive--;
                playerTwoUnit = defenderArmee.Units[0];
                //Kill anouncment
                outputter.UnitDeath(playerTwoUnitsAlive, "Verteidiger");
            }
        }
        private void VictoryCheck()
        {
            if (playerOneUnitsAlive <= 0)
            {
                if (attackerArmee.NumberOfUnit.Count > 0)
                {
                    NextUnits(attackerArmee, 1);
                    return;
                }
                outputter.Victory("Verteidiger");
                victory = true;
            }
            if (playerTwoUnitsAlive <= 0)
            {
                if (attackerArmee.NumberOfUnit.Count > 0)
                {
                    NextUnits(defenderArmee, 0);
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
            if (armee.Units.Count == 0)
            {
                victory = true;
            }
            else
            {
                if (player == 1)
                {
                    playerOneUnit = attackerArmee.Units[0];
                    playerOneUnitsAlive = attackerArmee.NumberOfUnit[0];
                }
                else
                {
                    playerTwoUnit = defenderArmee.Units[0];
                    playerTwoUnitsAlive = defenderArmee.NumberOfUnit[0];
                }
            }
        }
    }
}
