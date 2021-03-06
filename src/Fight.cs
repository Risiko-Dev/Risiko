﻿using System;

namespace Risiko_Rechner
{
    public class Fight
    {
        private enum round { One, Two }
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

        private void ExecFullRound()
        {
            // roll dices
            _playerOne.ThrowDice();
            _playerTwo.ThrowDice();

            _reporter.ReportDices("Player 1", _playerOne.DiceList);
            _reporter.ReportDices("Player 2", _playerTwo.DiceList);
            
            // execute first sub round
            if (!_playerOne.HasValidStack() || !_playerTwo.HasValidStack()) return;
            execSubRound(round.One);
            
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

            execSubRound(round.Two);

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

        private void execSubRound(round round)
        {
            if (!_playerOne.HasValidStack() || !_playerTwo.HasValidStack()) return;
            if (!_playerOne.TopStackIsValid())
            {
                _playerOne.KillTopStack();
                return;
            }

            if (!_playerTwo.TopStackIsValid())
            {
                _playerTwo.KillTopStack();
                return;
            }

            testWithdraw();

            if (!_playerOne.HasValidStack() || !_playerTwo.HasValidStack()) return;
            if (decideAttacker(round) == _playerOne)
            {
                _playerTwo.GetDamage(_playerOne);
            }
            else
            {
                _playerOne.GetDamage(_playerTwo);
            }
        }

        private Player decideAttacker(round round)
        {
            if (round == round.One)
            {
                return _playerTwo.HighestDice() >= _playerOne.HighestDice() ? _playerTwo : _playerOne;
            }

            return _playerTwo.LowestDice() >= _playerOne.LowestDice() ? _playerTwo : _playerOne;
        }

        private void testWithdraw()
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
