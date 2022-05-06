using Microsoft.Extensions.Caching.Memory;
using RockPaperScissors.Extensions;
using RockPaperScissors.Models;
using RockPaperScissors.Services;
using System;
using Xunit;
using static RockPaperScissors.Models.Moves;

namespace XUnitTestProject1
{
    public class GameServiceTest
    {
        private readonly GameService _gameService;
        public GameServiceTest()
        {
            _gameService = new GameService(new MemoryCache(new MemoryCacheOptions()));
        }

        [Fact]
        public void NewGameCreatesGameAndStoresIt()
        {
            var gameModel = new GameModel(new PlayerModel
            {
                Name = "Test1"
            });

            _gameService.SetGameState(gameModel.GameId, gameModel);
            var fetchedGame = _gameService.GetGameState(gameModel.GameId);

            Assert.NotNull(gameModel.GameId);
            Assert.NotNull(gameModel.PlayerOne);
            Assert.Equal(GameState.Created, gameModel.GameState);
            Assert.Equal(AvailableMoves.None, gameModel.PlayerOne.Move);
            Assert.Equal(gameModel, fetchedGame);
        }

       [Fact]
       public void MakeMoveChangesChosenMove()
        {
            var playerModel = new PlayerModel()
            {
                Name = "Test1"
            };
            var gameModel = new GameModel(playerModel);

            gameModel.SetPlayerMove(new PlayerModel
            {
                Name = "Test1",
                Move = AvailableMoves.Scissors
            });

            Assert.NotEqual(AvailableMoves.None, gameModel.PlayerOne.Move);

            Assert.Throws<InvalidOperationException>(() => gameModel.SetPlayerMove(new PlayerModel
            {
                Name = "Test1",
                Move = AvailableMoves.Paper
            }));
        }

        [Fact]
        public void PlayGameReturnsWinningAndLosingPlayer()
        {
            var playerOneModel = new PlayerModel()
            {
                Name = "Test1",
                Move = AvailableMoves.Paper
            };

            var playerTwoModel = new PlayerModel()
            {
                Name = "Test1",
                Move = AvailableMoves.Rock
            };

            var gameModel = new GameModel(playerOneModel);

            gameModel.PlayerTwo = playerTwoModel;

            _gameService.SetGameState(gameModel.GameId, gameModel);

            var result = _gameService.PlayGame(gameModel.GameId);

            Assert.Equal(WinningPlayer.PlayerOneVictory, result.WinningPlayer);
            Assert.Equal(GameState.Finished, result.GameState);

        }
      
    }
}
