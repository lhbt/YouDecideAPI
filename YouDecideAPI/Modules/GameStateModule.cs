using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using YouDecide.Domain;

namespace YouDecideAPI.Modules
{
    public class GameStateModule : NancyModule
    {
        public GameStateModule(IStoryNavigator storyNavigator)
        {
            Get["/gamestate"] = parameters =>
                {
                    return Response.AsJson(storyNavigator.GetCurrentGameState());
                };
        }
    }
}