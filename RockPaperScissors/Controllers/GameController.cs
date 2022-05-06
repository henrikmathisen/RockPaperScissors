using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RockPaperScissors.Extensions;
using RockPaperScissors.Models;
using RockPaperScissors.Services;

namespace RockPaperScissors.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost]
        public async Task<IActionResult> NewGame([FromBody] PlayerModel playerModel)
        {
            var game = new GameModel(playerModel);
            _gameService.SetGameState(game.GameId, game);
            return Ok(game.GameId);
        }

        [HttpPost("{gameId}/join")]
        public async Task<IActionResult> JoinGame([FromRoute] string gameId, [FromBody] PlayerModel playerModel)
        {
            var game = _gameService.GetGameState(gameId);
            if (game != default(GameModel))
            {
                game.PlayerTwo = playerModel;
                game.GameState = GameState.Connected;
                _gameService.SetGameState(game.GameId, game);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("{gameId}/move")]
        public async Task<IActionResult> MakeMove([FromRoute] string gameId, [FromBody] PlayerModel playerModel)
        {
            var game = _gameService.GetGameState(gameId);
            if (game != default(GameModel))
            {
                try
                {

                    game.SetPlayerMove(playerModel);

                    _gameService.SetGameState(gameId, game);

                    return Ok();
                }
                catch (InvalidOperationException ioe)
                {
                    return BadRequest(ioe.Message);
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{gameId}")]
        public async Task<IActionResult> GetResult([FromRoute] string gameId)
        {
            var game = _gameService.GetGameState(gameId);

            if (game != default(GameModel))
            {
                // Only run if both players have selected a move
                if (game.PlayerOne.Move != Moves.AvailableMoves.None && game.PlayerTwo.Move != Moves.AvailableMoves.None)
                {
                    var result = _gameService.PlayGame(gameId);

                    // Only happens if game is not found in cache, which it should be since we already checked above
                    if (result == null || result == default(GameModel))
                        return NotFound();

                    return Ok(result);

                }
                else
                {
                    return Ok(new
                    {
                        GameId = gameId,
                        game.GameState,
                        Status = "Waiting for other player."
                    });
                }
            }
            else
            {
                return NotFound();
            }
        }




    }
}