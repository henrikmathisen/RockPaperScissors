using RockPaperScissors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockPaperScissors.Extensions
{
    public static class GameExtensions
    {
        public static void SetPlayerMove(this GameModel gameModel, PlayerModel playerModel)
        {
            var player = gameModel.PlayerOne.Name == playerModel.Name ? gameModel.PlayerOne : gameModel.PlayerTwo;
            if (gameModel.GameState == GameState.Finished || player.Move != Moves.AvailableMoves.None) throw new InvalidOperationException("Game is finished or you've already made your move!");
            player.Move = playerModel.Move;
        }
    }
}
