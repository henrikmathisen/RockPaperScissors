using RockPaperScissors.Models;

namespace RockPaperScissors.Services
{
    public interface IGameService
    {
        GameModel PlayGame(string gameId);
        GameModel GetGameState(string gameId);
        void SetGameState(string gameId, GameModel resultModel);
    }
}