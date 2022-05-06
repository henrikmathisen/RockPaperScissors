using Microsoft.Extensions.Caching.Memory;
using RockPaperScissors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockPaperScissors.Services
{
    public class GameService : IGameService
    {
        private readonly IMemoryCache _memoryCache;
        public GameService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public GameModel PlayGame(string gameId)
        {
            if (_memoryCache.TryGetValue<GameModel>(gameId, out var game))
            {
                var playerOneResult = Moves.MoveResult(game.PlayerOne.Move, game.PlayerTwo.Move);
                var playerTwoResult = Moves.MoveResult(game.PlayerTwo.Move, game.PlayerOne.Move);

                // Results are decisive, both cannot return true
                if (playerOneResult)
                {
                    game.WinningPlayer = WinningPlayer.PlayerOneVictory;
                }
                if (playerTwoResult)
                {
                    game.WinningPlayer = WinningPlayer.PlayerTwoVictory;
                }

                game.GameState = GameState.Finished;

                return game;

            } else
            {
                return default(GameModel);
            }
        }

        public void SetGameState(string gameId, GameModel resultModel)
        {
            _memoryCache.Set(gameId, resultModel, DateTimeOffset.Now.AddHours(1));
        }

        public GameModel GetGameState(string gameId)
        {
            if (gameId != null && _memoryCache.TryGetValue<GameModel>(gameId, out var game))
            {
                return game;
            } else
            {
                return default(GameModel);
            }
        }

    }
}
