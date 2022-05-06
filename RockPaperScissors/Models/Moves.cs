using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockPaperScissors.Models
{
    public static class Moves
    {
        public enum AvailableMoves
        {
            None,
            Rock,
            Paper,
            Scissors
        }

        private static bool RockVictory(this AvailableMoves availableMoves)
        {
            return availableMoves == AvailableMoves.Scissors;
        }

        private static bool PaperVictory(this AvailableMoves availableMoves)
        {
            return availableMoves == AvailableMoves.Rock;
        }

        private static bool ScissorsVictory(this AvailableMoves availableMoves)
        {
            return availableMoves == AvailableMoves.Paper;
        }

        public static bool MoveResult(AvailableMoves resultForMove, AvailableMoves againstMove)
        {
            switch (resultForMove)
            {
                case AvailableMoves.None:
                    return false;
                case AvailableMoves.Rock:
                    return againstMove.RockVictory();
                case AvailableMoves.Paper:
                    return againstMove.PaperVictory();
                case AvailableMoves.Scissors:
                    return againstMove.ScissorsVictory();
                default:
                    throw new NotImplementedException();
            }
        }

    }
}
