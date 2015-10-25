using Nancy;
using Newtonsoft.Json;
using YouDecide.Domain;

namespace YouDecideAPI.Modules
{
    public class GameStateModule : NancyModule
    {
        public GameStateModule(IStoryNavigator storyNavigator)
        {
            Get["/gamestate/{gameId}"] = parameters =>
                {
                    var gameId = parameters.gameId;

                    var gameState = storyNavigator.GetCurrentGameState(gameId);
           
                    return JsonConvert.SerializeObject(gameState);
                };
        }
    }
}