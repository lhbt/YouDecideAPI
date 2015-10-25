using Nancy;
using Newtonsoft.Json;
using YouDecide.Domain;

namespace YouDecideAPI.Modules
{
    public class GameStateModule : NancyModule
    {
        public GameStateModule(IStoryNavigator storyNavigator)
        {
            

            Get["/gamestate"] = parameters =>
                {
                    var gameState = storyNavigator.GetCurrentGameState("0");
                    return JsonConvert.SerializeObject(gameState);
                };

            Get["/mpgamestate/{id}"] = parameters =>
                {
                    var gameState = storyNavigator.GetCurrentGameState(parameters.id);
                    return JsonConvert.SerializeObject(gameState);
                };
        }
    }
}