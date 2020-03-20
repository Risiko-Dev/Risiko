using System;

namespace Risiko_Rechner
{
    public class Fight
    {
        private enum Round { One, Two }
        private Player _playerOne; 
        private Player _playerTwo;
        private Output _reporter;

        public Fight(Player playerOne, Player playerTwo, Output output)
        {
            _playerOne = playerOne;
            _playerTwo = playerTwo;
            _reporter = output;

            while (_playerOne.HasValidStack() && _playerTwo.HasValidStack())
            {
                ExecFullRound();
                
            }
            string winner = _playerOne.HasValidStack() ? "Spieler 1" : "Spieler 2";
            _reporter.Report(winner + " hat gewonnen.");
        }

        /* Kampfablauf: -Einheiten prüfen x
                        -Einheiten ausgeben x
                        -Würfel prüfen x
                        -erste Runde
                        -Einheiten prüfen
                        -zweite Runde
                        -Report, Ende
                        */
        private void ExecFullRound()
        {
            // roll dices
            _playerOne.ThrowDice();
            _playerTwo.ThrowDice();
            
            // execute first sub round
            if (!_playerOne.HasValidStack() || !_playerTwo.HasValidStack()) return;
            ExecSubRound(Round.One);
            
            if (_playerOne.HasValidStack()) 
            {
                _reporter.Report("Spieler 1: Top Stack: " + _playerOne.TopStack());
            }
            else
            {
                _reporter.Report("Spieler 1: Top Stack: 0");
                return;
            }

            if (_playerTwo.HasValidStack())
            {
                _reporter.Report("Spieler 2: Top Stack: " + _playerTwo.TopStack());
            }
            else
            {
                _reporter.Report("Spieler 2: Top Stack: 0");
                return;
            }

            ExecSubRound(Round.Two);

            if (_playerOne.HasValidStack())
            {
                _reporter.Report("Spieler 1: Top Stack: " + _playerOne.TopStack());
            }
            else
            {
                _reporter.Report("Spieler 1: Top Stack: 0");
            }

            if (_playerTwo.HasValidStack())
            {
                _reporter.Report("Spieler 2: Top Stack: " + _playerTwo.TopStack());
            }
            else
            {
                _reporter.Report("Spieler 2: Top Stack: 0");
            }
        }

        private void ExecSubRound(Round round)
        {
            if (!_playerOne.HasValidStack() || !_playerTwo.HasValidStack()) return;
         
            TestWithdraw();

            if (!_playerOne.HasValidStack() || !_playerTwo.HasValidStack()) return;
            if (DecideAttacker(round) == _playerOne)
            {
                _playerTwo.GetDamage(_playerOne);
            }
            else
            {
                _playerOne.GetDamage(_playerTwo);
            }
        }

        private Player decideAttacker(Round round)
        {
            if (round == Round.One)
            {
                return _playerTwo.HighestDice() >= _playerOne.HighestDice() ? _playerTwo : _playerOne;
            }

            return _playerTwo.LowestDice() >= _playerOne.LowestDice() ? _playerTwo : _playerOne;
        }

        private void TestWithdraw()
        {
            Unit attackerUnit = _playerOne.TopStack().StackUnit;
            Unit defenderUnit = _playerTwo.TopStack().StackUnit;

            bool attackerWithdraws = attackerUnit.AttackDamage + attackerUnit.AntiArmor <= defenderUnit.Armor;
            bool defenderWithdraws = defenderUnit.AttackDamage + defenderUnit.AntiArmor <= attackerUnit.Armor;

            if (attackerWithdraws)
            {
                _playerOne.WithdrawTopStack();
            }
            if (defenderWithdraws)
            {   
                _playerTwo.WithdrawTopStack();
            }
        }
    }
}
