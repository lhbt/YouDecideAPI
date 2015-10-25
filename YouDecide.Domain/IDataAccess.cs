using System.Collections.Generic;
using System.Threading.Tasks;

namespace YouDecide.Domain
{
    public interface IDataAccess
    {
        Task<List<StoryPoint>> FetchAllStoryPoints();

        Task UpdateGameState(GameState gameState);

        Task UpdateGameParents(List<StoryPoint> parents, string gameId);
        Task UpdateGamePoints(List<StoryPoint> parents, string gameId);
        List<StoryPoint> GetGameStoryPoints(string gameId);
        List<StoryPoint> GetGameStoryParents(string gameId);
    }
}
