using Nancy;
using YouDecide.Domain;

namespace YouDecideAPI.Modules
{
    public class GameStateModule : NancyModule
    {
        public GameStateModule(IStoryNavigator storyNavigator)
        {
            Get["/gamestate"] = parameters => Response.AsJson(storyNavigator.GetCurrentGameState());
        }
    }
}