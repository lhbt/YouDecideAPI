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
                return Response.AsJson(new GameState
                    {
                        GameOptions = new List<GameOption>
                            {
                                new GameOption
                                    {
                                        OptionNumber = 1,
                                        Option = "Run away from lollipop-bearing skeletons."
                                    },
                                new GameOption
                                    {
                                        OptionNumber = 1,
                                        Option = "Eat cake!"
                                    },
                                new GameOption
                                    {
                                        OptionNumber = 1,
                                        Option = "Go home and have a bath."
                                    }
                            }
                    });
            };
        }
    }
}