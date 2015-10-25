using System.Collections.Generic;
using System.Threading.Tasks;

namespace YouDecide.Domain
{
    public interface IDataAccess
    {
        List<StoryPoint> FetchAllStoryPoints();

        void UpdateGameState(GameState gameState);
        void CreateGameState(GameState gameState);

        List<StoryPoint> GetGameStoryPoints(string gameId);
        List<StoryPoint> GetGameStoryParents(string gameId);
        GameState GetCurrentGameState(string gameId);

        void DeleteGameState(string gameId);
    }
}
