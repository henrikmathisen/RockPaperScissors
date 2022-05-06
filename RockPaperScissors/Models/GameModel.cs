using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static RockPaperScissors.Models.Moves;

namespace RockPaperScissors.Models
{
    public class GameModel
    {
        public GameModel(PlayerModel playerOne)
        {
            PlayerOne = playerOne;
            GameId = Guid.NewGuid().ToString();
            WinningPlayer = WinningPlayer.Draw;
            GameState = GameState.Created;
        }
        public PlayerModel PlayerOne { get; set; }
        public PlayerModel PlayerTwo { get; set; }
        public WinningPlayer WinningPlayer { get; set; }
        public GameState GameState { get; set; }
        public string GameId { get; private set; }

    }
    public enum WinningPlayer
    {
        Draw,
        PlayerOneVictory,
        PlayerTwoVictory,
    }
    public enum GameState
    {
        Created,
        Connected,
        Finished
    }
}
