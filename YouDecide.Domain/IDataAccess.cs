using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouDecide.Domain
{
    public interface IDataAccess
    {
        Task<List<StoryPoint>> FetchAllStoryPoints();
/*        Task<List<GameState>> GetGameState(int gameId);
        Task UpdateGameState(int gameId);*/

        Task UpdateGameParents(List<StoryPoint> parents, string gameId);
        Task UpdateGamePoints(List<StoryPoint> parents, string gameId);
        Task<List<StoryPoint>> GetGameStoryPoints(string gameId);
        Task<List<StoryPoint>> GetGameStoryParents(string gameId);
    }
}
